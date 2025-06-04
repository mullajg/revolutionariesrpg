using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ActionTypeFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<ActionTypeFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<ActionType> _repository;

    public ActionTypeFunctions(ILogger<ActionTypeFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<ActionType>();
    }

    [Function("GetAllActionTypes")]
    public async Task<IActionResult> GetAllActionTypes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllActionTypes")] HttpRequest req)
    {
        _logger.LogInformation("GetAllActionTypes run...");
        var actionTypes = await _repository.GetAllAsync();
        return new OkObjectResult(actionTypes);
    }

    [Function("GetActionTypeById")]
    public async Task<IActionResult> GetActionTypeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetActionTypeById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetActionTypeById run...");
        var actionType = await _repository.GetByIdAsync(id);
        return new OkObjectResult(actionType);
    }

    [Function("UpdateActionType")]
    public async Task<IActionResult> UpdateActionType([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateActionType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateActionType run...");
        var actionType = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(actionType);
    }

    [Function("CreateActionType")]
    public async Task<IActionResult> CreateActionType([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateActionType")] HttpRequest req)
    {
        _logger.LogInformation("CreateActionType run...");
        var actionType = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/actionType", actionType);
    }

    [Function("DeleteActionType")]
    public async Task<IActionResult> DeleteActionType([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteActionType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteActionType run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}