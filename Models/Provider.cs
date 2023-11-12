using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CSharpOrders.Models;

public class Provider
{
    public int Id { get; set;}
    public required string Name { get; set; }
}
