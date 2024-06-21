namespace WebApplication1.DTOs;

public class AddItemsDTO {
public ICollection<AddItemDTO> items { get; set; } = null!;
}
public class AddItemDTO
{
    public int Id { get; set; }
    public int Amount { get; set; } 
}