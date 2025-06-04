using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ItemFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<ItemFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Item> _repository;

    public ItemFunctions(ILogger<ItemFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Item>();
    }

    [Function("GetAllItems")]
    public async Task<IActionResult> GetAllItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllItems")] HttpRequest req)
    {
        _logger.LogInformation("GetAllItems run...");
        var Items = await _repository.GetAllAsync();
        return new OkObjectResult(Items);
    }

    [Function("GetItemById")]
    public async Task<IActionResult> GetItemById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetItemById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetItemById run...");
        var Item = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Item);
    }

    [Function("UpdateItem")]
    public async Task<IActionResult> UpdateItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateItem/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateItem run...");
        var Item = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Item);
    }

    [Function("CreateItem")]
    public async Task<IActionResult> CreateItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateItem")] HttpRequest req)
    {
        _logger.LogInformation("CreateItem run...");
        var Item = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Item", Item);
    }

    [Function("DeleteItem")]
    public async Task<IActionResult> DeleteItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteItem/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteItem run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}