namespace BlazorECommerce.Shared.Services;

public class ServiceResponse<T>
{
    public ServiceResponse() { }

    public ServiceResponse(T data) => Data = data;

    public T? Data { get; set; }

    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;
}
