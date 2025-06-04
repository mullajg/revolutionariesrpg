using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ClassActionFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<ClassActionFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<ClassAction> _repository;

    public ClassActionFunctions(ILogger<ClassActionFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<ClassAction>();
    }

    [Function("GetAllClassActions")]
    public async Task<IActionResult> GetAllClassActions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllClassActions")] HttpRequest req)
    {
        _logger.LogInformation("GetAllClassActions run...");
        var ClassActions = await _repository.GetAllAsync();
        return new OkObjectResult(ClassActions);
    }

    [Function("GetClassActionById")]
    public async Task<IActionResult> GetClassActionById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetClassActionById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetClassActionById run...");
        var ClassAction = await _repository.GetByIdAsync(id);
        return new OkObjectResult(ClassAction);
    }

    [Function("UpdateClassAction")]
    public async Task<IActionResult> UpdateClassAction([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateClassAction/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateClassAction run...");
        var ClassAction = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(ClassAction);
    }

    [Function("CreateClassAction")]
    public async Task<IActionResult> CreateClassAction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateClassAction")] HttpRequest req)
    {
        _logger.LogInformation("CreateClassAction run...");
        var ClassAction = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/ClassAction", ClassAction);
    }

    [Function("DeleteClassAction")]
    public async Task<IActionResult> DeleteClassAction([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteClassAction/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteClassAction run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}