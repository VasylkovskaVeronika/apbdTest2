using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("{CharacterID}/backpacks")]
    public async Task<IActionResult> AddNewItem(int CharacterID, AddItemsDTO newItems)
    {
        if (!await _dbService.DoesCharacterExist(CharacterID))
            return NotFound($"Character with given ID - {CharacterID} doesn't exist");


        Character charr = _dbService.GetCharacterById(CharacterID).Result;
        foreach (var item in newItems.items)
        {
            var it = await _dbService.GetItemById(item.Id);
            if(it is null)
                return NotFound($"item with id - {item.Id} doesn't exist");
    
            charr.Backpacks.Add(new Backpack()
                {
                    Amount = item.Amount,
                    Item = it,
                    Character = charr
                });
        }

        ICollection<Item> itemss=new List<Item>();
        foreach (var item in newItems.items)
        {
            var it = await _dbService.GetItemById(item.Id);
            if(it is null)
                return NotFound($"item with id - {item.Id} doesn't exist");
            itemss.Add(it);
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddNewItem(itemss);
            
            scope.Complete();
        }
    
        return Created();
    }
    [HttpGet ("{CharacterID}")]
    public async Task<IActionResult> GetCharacterData(int CharacterId)
    {
        var orders = await _dbService.GetCharacterData(CharacterId);
        
        return Ok(orders.Select(e => new GetCharacterDTO()
        {
            FirstName = e.FirstName,
            LastName=e.LastName,
            currentWeight = e.currentWeight,
            maxWeight = e.maxWeight,
            CharacterTitles = e.CharacterTitles.Select(t=> new GetTitlesDTO()
                {
                    Title = t.Title.Name,
                    AquiredAt = t.AcquiredAt
                }).ToList(),
            BackpackItems = e.Backpacks.Select(p => new GetItemsDTO()
            {
               ItemName = p.Item.Name,
               ItemWeight = p.Item.Weight,
                Amount = p.Amount
            }).ToList()
        }));
    }
}