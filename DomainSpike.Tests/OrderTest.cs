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
}