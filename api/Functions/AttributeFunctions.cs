using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class AttributeFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<AttributeFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Entities.Attribute> _repository;

    public AttributeFunctions(ILogger<AttributeFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Entities.Attribute>();
    }

    [Function("GetAllAttributes")]
    public async Task<IActionResult> GetAllAttributes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllAttributes")] HttpRequest req)
    {
        _logger.LogInformation("GetAllAttributes run...");
        var Attributes = await _repository.GetAllAsync();
        return new OkObjectResult(Attributes);
    }

    [Function("GetAttributeById")]
    public async Task<IActionResult> GetAttributeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAttributeById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetAttributeById run...");
        var Attribute = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Attribute);
    }

    [Function("GetAllAttributesWithChildren")]
    public async Task<IActionResult> GetAllAttributesWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllAttributesWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllAttributes run...");
        var attributes = await _db.Attributes
            .Include(a => a.AttributeEffects)
            .ToListAsync();
        return new OkObjectResult(attributes);
    }

    [Function("GetAttributeByIdWithChildren")]
    public async Task<IActionResult> GetAttributeByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAttributeByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetAttributeById run...");
        var Attribute = await _db.Attributes
            .Include(a => a.AttributeEffects)
            .FirstOrDefaultAsync(a => a.Id == id);
        return new OkObjectResult(Attribute);
    }

    [Function("UpdateAttribute")]
    public async Task<IActionResult> UpdateAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateAttribute/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateAttribute run...");
        var Attribute = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Attribute);
    }

    [Function("CreateAttribute")]
    public async Task<IActionResult> CreateAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateAttribute")] HttpRequest req)
    {
        _logger.LogInformation("CreateAttribute run...");
        var Attribute = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Attribute", Attribute);
    }

    [Function("DeleteAttribute")]
    public async Task<IActionResult> DeleteAttribute([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteAttribute/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteAttribute run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}