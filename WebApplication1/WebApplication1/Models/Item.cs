using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Weight { get; set; } 
}