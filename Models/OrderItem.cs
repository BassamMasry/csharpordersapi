using System.ComponentModel.DataAnnotations;

namespace CSharpOrders.Models;

public class OrderItem
{
    public int Id { get; set;}
    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public required string Name { get; set; }

    public decimal Quantity { get; set; }

    public required string Unit { get; set; }
}
