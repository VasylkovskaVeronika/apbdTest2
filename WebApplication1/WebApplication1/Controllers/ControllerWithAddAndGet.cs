namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IDbService _dbService;
    public ClientsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("{clientID}/orders")]
    public async Task<IActionResult> AddNewOrder(int clientID, NewOrderDTO newOrder)
    {
        if (!await _dbService.DoesClientExist(clientID))
            return NotFound($"Client with given ID - {clientID} doesn't exist");
        if (!await _dbService.DoesEmployeeExist(newOrder.EmployeeID))
            return NotFound($"Employee with given ID - {newOrder.EmployeeID} doesn't exist");
    
        var order = new Order()
        {
            ClientId = clientID,
            EmployeeId = newOrder.EmployeeID,
            AcceptedAt = newOrder.AcceptedAt,
            Comments = newOrder.Comments,
        };
    
        var pastries = new List<OrderPastry>();
        foreach (var newPastry in newOrder.Pastries)
        {
            var pastry = await _dbService.GetPastryByName(newPastry.Name);
            if(pastry is null)
                return NotFound($"Pastry with name - {newPastry.Name} doesn't exist");
    
            pastries.Add(new OrderPastry
            {
                PastryId = pastry.Id,
                Amount = newPastry.Amount,
                Comment = newPastry.Comments,
                Order = order
            });
        }
    
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddNewOrder(order);
            await _dbService.AddOrderPastries(pastries);
    
            scope.Complete();
        }
    
        return Created("api/orders", new
        {
            Id = order.Id,
            order.AcceptedAt,
            order.FulfilledAt,
            order.Comments,
        });
    }
    [HttpGet]
    public async Task<IActionResult> GetOrdersData(string? clientLastName = null)
    {
        var orders = await _dbService.GetOrdersData(clientLastName);
        
        return Ok(orders.Select(e => new GetOrdersDTO()
        {
            Id = e.Id,
            AcceptedAt = e.AcceptedAt,
            FulfilledAt = e.FulfilledAt,
            Comments = e.Comments,
            Pastries = e.OrderPastries.Select(p => new GetOrdersPastryDTO
            {
                Name = p.Pastry.Name,
                Price = p.Pastry.Price,
                Amount = p.Amount
            }).ToList()
        }));
    }
}