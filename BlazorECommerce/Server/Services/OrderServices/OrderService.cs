namespace BlazorECommerce.Server.Services.OrderServices;

public record OrderService
    (DatabaseContext DatabaseContext, IAuthService AuthService, ICartService CartService) : IOrderService
{
    public async Task<ServiceResponse<bool>> PlaceOrderAsync(int userId)
    {
        var products = (await CartService.GetDbCartProductsAsync(userId)).Data;
        if (products is null)
        {
            return new ServiceResponse<bool>(false);
        }

        var totalPrice = 0M;
        var orderItems = new List<OrderItem>();

        products.ForEach(product =>
        {
            var productTotalPrice = product.Price * product.Quantity;
            totalPrice += productTotalPrice;
            orderItems.Add(new OrderItem
            {
                ProductId = product.ProductId,
                ProductTypeId = product.ProductTypeId,
                Quantity = product.Quantity,
                TotalPrice = productTotalPrice
            });
        });

        var order = new Order
        {
            UserId = userId, OrderDate = DateTime.Now, TotalPrice = totalPrice, OrderItems = orderItems
        };

        DatabaseContext.Orders.Add(order);
        DatabaseContext.CartItems.RemoveRange(DatabaseContext.CartItems.Where(cartItem => cartItem.UserId == userId));

        await DatabaseContext.SaveChangesAsync();
        return new ServiceResponse<bool>(true);
    }

    public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrdersAsync()
    {
        var userId = AuthService.GetUserId();

        var orders = await DatabaseContext.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.Product)
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.OrderDate)
            .ToListAsync();

        var orderResponse = new List<OrderOverviewResponse>();
        orders.ForEach(order => orderResponse.Add(new OrderOverviewResponse
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            ProductName = order.OrderItems.Count > 1
                ? $"{order.OrderItems.First().Product.Title} and {order.OrderItems.Count - 1} more..."
                : order.OrderItems.First().Product.Title,
            ProductImageUrl = order.OrderItems.First().Product.ImageUrl
        }));

        return new ServiceResponse<List<OrderOverviewResponse>>(orderResponse);
    }

    public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsAsync(int orderId)
    {
        var userId = AuthService.GetUserId();
        var order = await DatabaseContext.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.Product)
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.ProductType)
            .Where(order => order.UserId == userId && order.Id == orderId)
            .OrderByDescending(order => order.OrderDate)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            return new ServiceResponse<OrderDetailsResponse> { Success = false, Message = "Order not found." };
        }

        var orderDetailsResponse = new OrderDetailsResponse
        {
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Products = new List<OrderDetailsProductResponse>()
        };

        order.OrderItems.ForEach(orderItem => orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
        {
            ProductId = orderItem.ProductId,
            ImageUrl = orderItem.Product.ImageUrl,
            ProductTypeId = orderItem.ProductTypeId,
            ProductType = orderItem.ProductType.Name,
            Quantity = orderItem.Quantity,
            Title = orderItem.Product.Title,
            TotalPrice = orderItem.TotalPrice
        }));

        return new ServiceResponse<OrderDetailsResponse>(orderDetailsResponse);
    }
}
