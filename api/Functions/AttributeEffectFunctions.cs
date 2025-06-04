using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class AttributeEffectFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<AttributeEffectFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<AttributeEffect> _repository;

    public AttributeEffectFunctions(ILogger<AttributeEffectFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<AttributeEffect>();
    }

    [Function("GetAllAttributeEffects")]
    public async Task<IActionResult> GetAllAttributeEffects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllAttributeEffects")] HttpRequest req)
    {
        _logger.LogInformation("GetAllAttributeEffects run...");
        var AttributeEffects = await _repository.GetAllAsync();
        return new OkObjectResult(AttributeEffects);
    }

    [Function("GetAttributeEffectById")]
    public async Task<IActionResult> GetAttributeEffectById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAttributeEffectById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetAttributeEffectById run...");
        var AttributeEffect = await _repository.GetByIdAsync(id);
        return new OkObjectResult(AttributeEffect);
    }

    [Function("UpdateAttributeEffect")]
    public async Task<IActionResult> UpdateAttributeEffect([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateAttributeEffect/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateAttributeEffect run...");
        var AttributeEffect = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(AttributeEffect);
    }

    [Function("CreateAttributeEffect")]
    public async Task<IActionResult> CreateAttributeEffect([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateAttributeEffect")] HttpRequest req)
    {
        _logger.LogInformation("CreateAttributeEffect run...");
        var AttributeEffect = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/AttributeEffect", AttributeEffect);
    }

    [Function("DeleteAttributeEffect")]
    public async Task<IActionResult> DeleteAttributeEffect([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteAttributeEffect/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteAttributeEffect run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}