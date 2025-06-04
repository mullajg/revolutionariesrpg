using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ClassItemFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<ClassItemFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<ClassItem> _repository;

    public ClassItemFunctions(ILogger<ClassItemFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<ClassItem>();
    }

    [Function("GetAllClassItems")]
    public async Task<IActionResult> GetAllClassItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllClassItems")] HttpRequest req)
    {
        _logger.LogInformation("GetAllClassItems run...");
        var ClassItems = await _repository.GetAllAsync();
        return new OkObjectResult(ClassItems);
    }

    [Function("GetClassItemById")]
    public async Task<IActionResult> GetClassItemById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetClassItemById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetClassItemById run...");
        var ClassItem = await _repository.GetByIdAsync(id);
        return new OkObjectResult(ClassItem);
    }

    [Function("UpdateClassItem")]
    public async Task<IActionResult> UpdateClassItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateClassItem/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateClassItem run...");
        var ClassItem = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(ClassItem);
    }

    [Function("CreateClassItem")]
    public async Task<IActionResult> CreateClassItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateClassItem")] HttpRequest req)
    {
        _logger.LogInformation("CreateClassItem run...");
        var ClassItem = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/ClassItem", ClassItem);
    }

    [Function("DeleteClassItem")]
    public async Task<IActionResult> DeleteClassItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteClassItem/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteClassItem run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}