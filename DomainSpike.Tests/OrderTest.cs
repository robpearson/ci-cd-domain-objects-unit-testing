using Xunit.Sdk;

namespace DomainSpike.Tests;

public class OrderTest
{
    [Fact]
    public void CannotCreateOrderWithNoOrderItems()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        Address address = new Address("76 Hideaway Street", "Calgary", "T3C2X4", "Canada");
        
        // Act & Assert 
        Assert.Throws<ArgumentException>(() => new Order(orderId, orderItems, address));
    }

    [Fact]

    public void OrderWithIDItemsAndAddressIsValid()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        orderItems.Add(new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 10, new Money(125m, "USD")));
        Address address = new Address("76 Hideaway Street", "Calgary", "T3C2X4", "Canada");

        // Act & Assert 
        var order = new Order(orderId, orderItems, address);
        Assert.Equal(orderId, order.OrderId);
        Assert.Single(order.Items);
        Assert.Equal(1250m, order.TotalAmount.Amount);

    }
}