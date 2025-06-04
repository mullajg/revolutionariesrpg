using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class FeatPrerequisiteTypeFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<FeatPrerequisiteTypeFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FeatPrerequisiteType> _repository;

    public FeatPrerequisiteTypeFunctions(ILogger<FeatPrerequisiteTypeFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<FeatPrerequisiteType>();
    }

    [Function("GetAllFeatPrerequisiteTypes")]
    public async Task<IActionResult> GetAllFeatPrerequisiteTypes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllFeatPrerequisiteTypes")] HttpRequest req)
    {
        _logger.LogInformation("GetAllFeatPrerequisiteTypes run...");
        var FeatPrerequisiteTypes = await _repository.GetAllAsync();
        return new OkObjectResult(FeatPrerequisiteTypes);
    }

    [Function("GetFeatPrerequisiteTypeById")]
    public async Task<IActionResult> GetFeatPrerequisiteTypeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeatPrerequisiteTypeById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetFeatPrerequisiteTypeById run...");
        var FeatPrerequisiteType = await _repository.GetByIdAsync(id);
        return new OkObjectResult(FeatPrerequisiteType);
    }

    [Function("UpdateFeatPrerequisiteType")]
    public async Task<IActionResult> UpdateFeatPrerequisiteType([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateFeatPrerequisiteType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateFeatPrerequisiteType run...");
        var FeatPrerequisiteType = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(FeatPrerequisiteType);
    }

    [Function("CreateFeatPrerequisiteType")]
    public async Task<IActionResult> CreateFeatPrerequisiteType([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateFeatPrerequisiteType")] HttpRequest req)
    {
        _logger.LogInformation("CreateFeatPrerequisiteType run...");
        var FeatPrerequisiteType = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/FeatPrerequisiteType", FeatPrerequisiteType);
    }

    [Function("DeleteFeatPrerequisiteType")]
    public async Task<IActionResult> DeleteFeatPrerequisiteType([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteFeatPrerequisiteType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteFeatPrerequisiteType run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}