using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Character>> GetCharacterData(int id)
    {
        return await _context.Characters
            .Include(e => e.CharacterTitles)
            .ThenInclude(e => e.Title)
            .Include(e=>e.Backpacks)
            .ThenInclude(e=>e.Item)
            .Where(e => e.Id==id)
            .ToListAsync();
    }
    

    public async Task<bool> DoesCharacterExist(int CharacterID)
    {
        return await _context.Characters.AnyAsync(e => e.Id == CharacterID);
    }
    

    public async Task AddNewItem(ICollection<Item> Items)
    {
        Items.ToList().ForEach(i=> _context.AddAsync(i));
        await _context.SaveChangesAsync();
    }

    public async Task<Item?> GetItemById(int id)
    {
        return await _context.Items.FirstOrDefaultAsync(e => e.Id==id);
    }
    public async Task<Character?> GetCharacterById(int id)
    {
        return await _context.Characters.FirstOrDefaultAsync(e => e.Id==id);
    }

    
}