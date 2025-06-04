using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class SpellEffectFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<SpellEffectFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<SpellEffect> _repository;

    public SpellEffectFunctions(ILogger<SpellEffectFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<SpellEffect>();
    }

    [Function("GetAllSpellEffects")]
    public async Task<IActionResult> GetAllSpellEffects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllSpellEffects")] HttpRequest req)
    {
        _logger.LogInformation("GetAllSpellEffects run...");
        var SpellEffects = await _repository.GetAllAsync();
        return new OkObjectResult(SpellEffects);
    }

    [Function("GetSpellEffectById")]
    public async Task<IActionResult> GetSpellEffectById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSpellEffectById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetSpellEffectById run...");
        var SpellEffect = await _repository.GetByIdAsync(id);
        return new OkObjectResult(SpellEffect);
    }

    [Function("UpdateSpellEffect")]
    public async Task<IActionResult> UpdateSpellEffect([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateSpellEffect/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateSpellEffect run...");
        var SpellEffect = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(SpellEffect);
    }

    [Function("CreateSpellEffect")]
    public async Task<IActionResult> CreateSpellEffect([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateSpellEffect")] HttpRequest req)
    {
        _logger.LogInformation("CreateSpellEffect run...");
        var SpellEffect = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/SpellEffect", SpellEffect);
    }

    [Function("DeleteSpellEffect")]
    public async Task<IActionResult> DeleteSpellEffect([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteSpellEffect/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteSpellEffect run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}