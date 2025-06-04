using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class FeatPrerequisiteFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<FeatPrerequisiteFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<FeatPrerequisite> _repository;

    public FeatPrerequisiteFunctions(ILogger<FeatPrerequisiteFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<FeatPrerequisite>();
    }

    [Function("GetAllFeatPrerequisites")]
    public async Task<IActionResult> GetAllFeatPrerequisites([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllFeatPrerequisites")] HttpRequest req)
    {
        _logger.LogInformation("GetAllFeatPrerequisites run...");
        var FeatPrerequisites = await _repository.GetAllAsync();
        return new OkObjectResult(FeatPrerequisites);
    }

    [Function("GetFeatPrerequisiteById")]
    public async Task<IActionResult> GetFeatPrerequisiteById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeatPrerequisiteById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetFeatPrerequisiteById run...");
        var FeatPrerequisite = await _repository.GetByIdAsync(id);
        return new OkObjectResult(FeatPrerequisite);
    }

    [Function("UpdateFeatPrerequisite")]
    public async Task<IActionResult> UpdateFeatPrerequisite([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateFeatPrerequisite/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateFeatPrerequisite run...");
        var FeatPrerequisite = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(FeatPrerequisite);
    }

    [Function("CreateFeatPrerequisite")]
    public async Task<IActionResult> CreateFeatPrerequisite([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateFeatPrerequisite")] HttpRequest req)
    {
        _logger.LogInformation("CreateFeatPrerequisite run...");
        var FeatPrerequisite = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/FeatPrerequisite", FeatPrerequisite);
    }

    [Function("DeleteFeatPrerequisite")]
    public async Task<IActionResult> DeleteFeatPrerequisite([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteFeatPrerequisite/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteFeatPrerequisite run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}