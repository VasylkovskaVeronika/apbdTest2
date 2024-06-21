namespace WebApplication1.DTOs;

public class GetCharacterDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }
    public List<GetItemsDTO> BackpackItems { get; set; } = null!;
    public ICollection<GetTitlesDTO> CharacterTitles { get; set; } = null!;
}

public class GetBackpacksDTO
{
    public ICollection<GetItemsDTO> BackpackItems { get; set; } = null!;
}
    public class GetItemsDTO
    {
        public string ItemName { get; set; }
        public int ItemWeight { get; set; }
        public int Amount { get; set; }
    }
public class GetTitlesDTO
{
    public string Title { get; set; }
    public DateTime AquiredAt { get; set; }
}