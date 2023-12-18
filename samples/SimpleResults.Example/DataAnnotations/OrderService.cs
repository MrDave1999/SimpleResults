namespace SimpleResults.Example.DataAnnotations;

public class OrderService
{
    private readonly List<Order> _orders;

    public OrderService(List<Order> orders)
    {
        ArgumentNullException.ThrowIfNull(nameof(orders));
        _orders = orders;
    }

    public Result<CreatedGuid> Create(Order order)
    {
        ArgumentNullException.ThrowIfNull(nameof(order));
        _orders.Add(order);
        var guid = Guid.NewGuid();
        order.Id = guid.ToString();
        return Result.CreatedResource(guid);
    }
}
