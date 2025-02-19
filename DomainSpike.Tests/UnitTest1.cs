namespace DomainSpike.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        Address address = null;
        Order order = new Order(orderId, orderItems, address);

        // Act 


        // Asserts

    }
}