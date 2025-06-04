using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ClassFeatFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<ClassFeatFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<ClassFeat> _repository;

    public ClassFeatFunctions(ILogger<ClassFeatFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<ClassFeat>();
    }

    [Function("GetAllClassFeats")]
    public async Task<IActionResult> GetAllClassFeats([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllClassFeats")] HttpRequest req)
    {
        _logger.LogInformation("GetAllClassFeats run...");
        var ClassFeats = await _repository.GetAllAsync();
        return new OkObjectResult(ClassFeats);
    }

    [Function("GetClassFeatById")]
    public async Task<IActionResult> GetClassFeatById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetClassFeatById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetClassFeatById run...");
        var ClassFeat = await _repository.GetByIdAsync(id);
        return new OkObjectResult(ClassFeat);
    }

    [Function("UpdateClassFeat")]
    public async Task<IActionResult> UpdateClassFeat([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateClassFeat/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateClassFeat run...");
        var ClassFeat = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(ClassFeat);
    }

    [Function("CreateClassFeat")]
    public async Task<IActionResult> CreateClassFeat([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateClassFeat")] HttpRequest req)
    {
        _logger.LogInformation("CreateClassFeat run...");
        var ClassFeat = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/ClassFeat", ClassFeat);
    }

    [Function("DeleteClassFeat")]
    public async Task<IActionResult> DeleteClassFeat([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteClassFeat/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteClassFeat run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}