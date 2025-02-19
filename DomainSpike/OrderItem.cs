namespace DomainSpike;

public class OrderItem
{
    public Guid OrderItemId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
    public Money Subtotal => Price.Multiply(Quantity);

    public OrderItem(Guid orderItemId, Guid productId, int quantity, Money price)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }
        OrderItemId = orderItemId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Quantity to increase must be greater than zero.", nameof(amount));
        }
        Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Quantity to decrease must be greater than zero.", nameof(amount));
        }
        Quantity = Math.Max(0, Quantity - amount);
    }
}