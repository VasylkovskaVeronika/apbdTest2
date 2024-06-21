using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDbService
{
    Task<ICollection<Character>> GetCharacterData(int id);
    Task<bool> DoesCharacterExist(int id);
    public Task<Character?> GetCharacterById(int id);
    Task AddNewItem(ICollection<Item> Item);
    Task<Item?> GetItemById(int id);

}