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

        // Act  
        var order = new Order(orderId, orderItems, address);
        
        // Assert
        Assert.Equal(orderId, order.OrderId);
        Assert.Single(order.Items);
        Assert.Equal(1250m, order.TotalAmount.Amount);
    }

    [Fact]
    public void RecalculatesTotalAmountWhenAddingNewOrderItem()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        var orderItem1 = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 10, new Money(125m, "USD"));
        orderItems.Add(orderItem1);
        Address address = new Address("76 Hideaway Street", "Calgary", "T3C2X4", "Canada");
        
        // Act
        var order = new Order(orderId, orderItems, address);
        var newOrderItem = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 1, new Money(2000m, "USD"));
        order.AddItem(newOrderItem);
        
        // Assert
        Assert.Equal(2, order.Items.Count);
        Assert.Equal(new Money(3150m, "USD"), order.TotalAmount);
        
    }
    
    [Fact]
    public void RecalculatesTotalAmountWhenRemovingNewOrderItem()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        orderItems.Add(new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 10, new Money(100m, "USD")));
        var orderItemToRemove = new OrderItem(Guid.NewGuid(), Guid.NewGuid(), 4, new Money(250m, "USD"));
        orderItems.Add(orderItemToRemove);
        Address address = new Address("76 Hideaway Street", "Calgary", "T3C2X4", "Canada");
        
        // Act
        var order = new Order(orderId, orderItems, address);
        order.RemoveItem(orderItemToRemove);
        
        // Assert
        Assert.Single(order.Items);
        Assert.Equal(new Money(1000m, "USD"), order.TotalAmount);
    }
}