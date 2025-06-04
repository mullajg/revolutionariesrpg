using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class ClassFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<ClassFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Class> _repository;

    public ClassFunctions(ILogger<ClassFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Class>();
    }

    [Function("GetAllClasses")]
    public async Task<IActionResult> GetAllClasses([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllClasses")] HttpRequest req)
    {
        _logger.LogInformation("GetAllClasss run...");
        var Classs = await _repository.GetAllAsync();
        return new OkObjectResult(Classs);
    }

    [Function("GetClassById")]
    public async Task<IActionResult> GetClassById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetClassById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetClassById run...");
        var Class = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Class);
    }

    [Function("GetAllClassesWithChildren")]
    public async Task<IActionResult> GetAllClassesWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllClassesWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllClassesWithChildren run...");
        var Classes = await _db.Classes
            .Include(c => c.ClassItems)
            .ThenInclude(ci => ci.Weapon)
            .Include(c => c.ClassItems)
            .ThenInclude(ci => ci.Equipment)
            .Include(c => c.ClassFeats)
            .ThenInclude(cf => cf.Feat)
            .Include(c => c.ClassActions)
            .ThenInclude(ca => ca.Action)
            .ToListAsync();
        return new OkObjectResult(Classes);
    }

    [Function("GetClassByIdWithChildren")]
    public async Task<IActionResult> GetClassByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetClassByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetClassById run...");
        var Class = await _db.Classes
            .Include(c => c.ClassItems)
            .ThenInclude(ci => ci.Weapon)
            .Include(c => c.ClassItems)
            .ThenInclude(ci => ci.Equipment)
            .Include(c => c.ClassFeats)
            .ThenInclude(cf => cf.Feat)
            .Include(c => c.ClassActions)
            .ThenInclude(ca => ca.Action)
            .FirstOrDefaultAsync(c => c.Id == id);
        return new OkObjectResult(Class);
    }

    [Function("UpdateClass")]
    public async Task<IActionResult> UpdateClass([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateClass/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateClass run...");
        var Class = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Class);
    }

    [Function("CreateClass")]
    public async Task<IActionResult> CreateClass([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateClass")] HttpRequest req)
    {
        _logger.LogInformation("CreateClass run...");
        var Class = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Class", Class);
    }

    [Function("DeleteClass")]
    public async Task<IActionResult> DeleteClass([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteClass/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteClass run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}