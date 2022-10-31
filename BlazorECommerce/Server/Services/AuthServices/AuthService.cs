namespace BlazorECommerce.Server.Services.AuthServices;

public record AuthService(DatabaseContext DatabaseContext, AppSettings AppSettings) : IAuthService
{
    public async Task<ServiceResponse<int>> RegisterAsync(User user, string password)
    {
        if (await UserExistsAsync(user.Email))
        {
            return new ServiceResponse<int> { Success = false, Message = "User already exists.", };
        }

        CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        DatabaseContext.Users.Add(user);
        await DatabaseContext.SaveChangesAsync();

        return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful.", };
    }

    public async Task<ServiceResponse<string>> LogInAsync(string email, string password)
    {
        var user = await DatabaseContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        var response = new ServiceResponse<string>();

        if (user is null)
        {
            response.Success = false;
            response.Message = "User not found.";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong password.";
        }
        else
        {
            response.Data = CreateToken(user);
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword)
    {
        var user = await DatabaseContext.Users.FindAsync(userId);
        if (user is null)
        {
            return new ServiceResponse<bool> { Success = false, Message = "User not found.", Data = false, };
        }

        CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await DatabaseContext.SaveChangesAsync();

        return new ServiceResponse<bool> { Success = true, Message = "Password has been changed.", Data = true, };
    }

    private async Task<bool> UserExistsAsync(string email) =>
        await DatabaseContext.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower());


    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Name, user.Email),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.AuthSettings.AuthToken));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims,
            expires: DateTime.Now.AddDays(AppSettings.AuthSettings.DaysToExpire), signingCredentials: creds);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
