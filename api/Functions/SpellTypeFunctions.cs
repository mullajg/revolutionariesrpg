using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class SpellTypeFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<SpellTypeFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<SpellType> _repository;

    public SpellTypeFunctions(ILogger<SpellTypeFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<SpellType>();
    }

    [Function("GetAllSpellTypes")]
    public async Task<IActionResult> GetAllSpellTypes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllSpellTypes")] HttpRequest req)
    {
        _logger.LogInformation("GetAllSpellTypes run...");
        var SpellTypes = await _repository.GetAllAsync();
        return new OkObjectResult(SpellTypes);
    }

    [Function("GetSpellTypeById")]
    public async Task<IActionResult> GetSpellTypeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSpellTypeById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetSpellTypeById run...");
        var SpellType = await _repository.GetByIdAsync(id);
        return new OkObjectResult(SpellType);
    }

    [Function("UpdateSpellType")]
    public async Task<IActionResult> UpdateSpellType([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateSpellType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateSpellType run...");
        var SpellType = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(SpellType);
    }

    [Function("CreateSpellType")]
    public async Task<IActionResult> CreateSpellType([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateSpellType")] HttpRequest req)
    {
        _logger.LogInformation("CreateSpellType run...");
        var SpellType = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/SpellType", SpellType);
    }

    [Function("DeleteSpellType")]
    public async Task<IActionResult> DeleteSpellType([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteSpellType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteSpellType run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}