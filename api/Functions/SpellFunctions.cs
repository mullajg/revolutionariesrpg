using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class SpellFunctions 
{
    private readonly AppDbContext _db;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Spell> _repository;

    public SpellFunctions(ILogger<SpellFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Spell>();
    }

    [Function("GetAllSpells")]
    public async Task<IActionResult> GetAllSpells([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllSpells")] HttpRequest req, FunctionContext executionContext)
    {
        ILogger logger = executionContext.InstanceServices.GetService<ILogger<SpellFunctions>>();
        logger.LogInformation("GetAllSpells run...");
        var Spells = await _repository.GetAllAsync();
        return new OkObjectResult(Spells);
    }

    [Function("GetSpellById")]
    public async Task<IActionResult> GetSpellById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSpellById/{id}")] HttpRequest req, Guid id)
    {
        //_logger.LogInformation("GetSpellById run...");
        var Spell = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Spell);
    }

    [Function("GetAllSpellsWithChildren")]
    public async Task<IActionResult> GetAllSpellsWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllSpellsWithChildren")] HttpRequest req)
    {
        //_logger.LogInformation("GetAllSpells run...");
        var Spells = await _db.Spells
            .Include(s => s.SpellEffects)
            .Include(s => s.SpellType)
            .ToListAsync();
        return new OkObjectResult(Spells);
    }

    [Function("GetSpellByIdWithChildren")]
    public async Task<IActionResult> GetSpellByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSpellByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        //_logger.LogInformation("GetSpellById run...");
        var Spell = await _db.Spells
            .Include(s => s.SpellEffects)
            .Include(s => s.SpellType)
            .FirstOrDefaultAsync(s => s.Id == id);
        return new OkObjectResult(Spell);
    }

    [Function("UpdateSpell")]
    public async Task<IActionResult> UpdateSpell([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateSpell/{id}")] HttpRequest req, Guid id)
    {
        //_logger.LogInformation("UpdateSpell run...");
        var Spell = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Spell);
    }

    [Function("CreateSpell")]
    public async Task<IActionResult> CreateSpell([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateSpell")] HttpRequest req)
    {
        //_logger.LogInformation("CreateSpell run...");
        var Spell = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Spell", Spell);
    }

    [Function("DeleteSpell")]
    public async Task<IActionResult> DeleteSpell([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteSpell/{id}")] HttpRequest req, Guid id)
    {
        //_logger.LogInformation("DeleteSpell run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}