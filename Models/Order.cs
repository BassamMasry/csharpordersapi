using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpOrders.Models;

public class Order
{
    public int Id { get; set;}
    [Required]
    public string Number { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    public int ProviderId { get; set; }
}
