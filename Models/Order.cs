using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CSharpOrders.Models;

public class Order
{
    public int Id { get; set; }
    public required string Number { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;
    
    public int ProviderId { get; set; }
    public Provider? Provider { get; set; }
}
