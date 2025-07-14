using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;
public class FeatFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<FeatFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Feat> _repository;

    public FeatFunctions(ILogger<FeatFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Feat>();
    }

    [Function("GetAllFeats")]
    public async Task<IActionResult> GetAllFeats([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllFeats")] HttpRequest req)
    {
        _logger.LogInformation("GetAllFeats run...");
        var Feats = await _repository.GetAllAsync();
        return new OkObjectResult(Feats);
    }

    [Function("GetAllFeatsForTable")]
    public async Task<IActionResult> GetAllFeatsForTable([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllFeatsForTable")] HttpRequest req)
    {
        _logger.LogInformation("GetAllFeatsForTable run...");
        var Feats = await _repository.GetAllAsync();
        return new OkObjectResult(Feats.Select(f => new { name = f.Name, description = f.Description }));
    }

    [Function("GetFeatById")]
    public async Task<IActionResult> GetFeatById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeatById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetFeatById run...");
        var Feat = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Feat);
    }

    [Function("GetAllFeatsWithChildren")]
    public async Task<IActionResult> GetAllFeatsWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllFeatsWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllFeats run...");
        var Feats = await _db.Feats
            .Include(f => f.FeatPrerequisites)
            .ThenInclude(f => f.FeatPrerequisiteType)
            .ToListAsync();

        return new OkObjectResult(Feats);
    }

    [Function("GetFeatByIdWithChildren")]
    public async Task<IActionResult> GetFeatByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFeatByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetFeatById run...");
        var Feat = await _db.Feats
            .Include(f => f.FeatPrerequisites)
            .ThenInclude(f => f.FeatPrerequisiteType)
            .FirstOrDefaultAsync(f => f.Id == id);
        return new OkObjectResult(Feat);
    }

    [Function("UpdateFeat")]
    public async Task<IActionResult> UpdateFeat([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateFeat/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateFeat run...");
        var Feat = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Feat);
    }

    [Function("CreateFeat")]
    public async Task<IActionResult> CreateFeat([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateFeat")] HttpRequest req)
    {
        _logger.LogInformation("CreateFeat run...");
        var Feat = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Feat", Feat);
    }

    [Function("DeleteFeat")]
    public async Task<IActionResult> DeleteFeat([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteFeat/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteFeat run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}