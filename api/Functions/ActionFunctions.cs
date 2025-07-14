using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;  

public class ActionFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<ActionFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Entities.Action> _repository;

    public ActionFunctions(ILogger<ActionFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Entities.Action>();
    }

    [Function("GetAllActions")]
    public async Task<IActionResult> GetAllActions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllActions")] HttpRequest req)
    {
        _logger.LogInformation("GetAllActions run...");
        try
        {
            var actions = await _repository.GetAllAsync();
            return new OkObjectResult(actions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while parsing request data.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [Function("GetAllActionsForTable")]
    public async Task<IActionResult> GetAllActionsForTable([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllActionsForTable")] HttpRequest req)
    {
        _logger.LogInformation("GetAllActionsForTable run...");
        var actions = await _db.Actions
            .Include(a => a.ActionType)
            .Select(a => new { 
                name = a.Name,
                actionType = a.ActionType != null ? a.ActionType.Type : "N/A",
                description = a.Description
            })
            .ToListAsync();
        return new OkObjectResult(actions);
    }

    [Function("GetActionById")]
    public async Task<IActionResult> GetActionById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetActionById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetActionById run...");
        var action = await _repository.GetByIdAsync(id);
        return new OkObjectResult(action);
    }

    [Function("GetAllActionsWithChildren")]
    public async Task<IActionResult> GetAllActionsWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllActionsWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllActionsWithChildren run...");
        var actions = await _db.Actions
            .Include(a => a.ActionType)
            .ToListAsync();
        return new OkObjectResult(actions);
    }

    [Function("GetActionByIdWithChildren")]
    public async Task<IActionResult> GetActionByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetActionByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetActionById run...");
        var action = await _db.Actions
            .Include(a => a.ActionType)
            .Where(a => a.Id == id)
            .ToListAsync();
        return new OkObjectResult(action);
    }

    [Function("UpdateAction")]
    public async Task<IActionResult> UpdateAction([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateAction/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateAction run...");
        var action = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(action);
    }

    [Function("CreateAction")]
    public async Task<IActionResult> CreateAction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateAction")] HttpRequest req)
    {
        _logger.LogInformation("CreateAction run...");
        var action = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/action", action);
    }

    [Function("DeleteAction")]
    public async Task<IActionResult> DeleteAction([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteAction/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteAction run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}