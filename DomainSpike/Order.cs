namespace DomainSpike;

public class Order
{
    public Guid OrderId { get; private set; }
    private List<OrderItem> items;
    public IReadOnlyList<OrderItem> Items => items.AsReadOnly();
    public Address ShippingAddress { get; private set; }
    public Money TotalAmount { get; private set; }

    public Order(Guid orderId, List<OrderItem> items, Address shippingAddress)
    {
        OrderId = orderId;
        this.items = new List<OrderItem>(items);
        ShippingAddress = shippingAddress;
        ValidateOrder();
        RecalculateTotalAmount();
    }

    private void ValidateOrder()
    {
        if (!items.Any())
        {
            throw new ArgumentException("Order must contain at least one item");
        }
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = items
            .Select(item => item.Subtotal)
            .Aggregate(new Money(0, "USD"), (sum, next) => sum.Add(next));
    }

    public void AddItem(OrderItem item)
    {
        items.Add(item);
        RecalculateTotalAmount();
    }

    public void RemoveItem(OrderItem item)
    {
        items.Remove(item);
        RecalculateTotalAmount();
    }

    public void UpdateShippingAddress(Address newAddress)
    {
        ShippingAddress = newAddress;
    }
}