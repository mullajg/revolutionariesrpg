using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class WeaponFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<WeaponFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Weapon> _repository;

    public WeaponFunctions(ILogger<WeaponFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Weapon>();
    }

    [Function("GetAllWeapons")]
    public async Task<IActionResult> GetAllWeapons([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllWeapons")] HttpRequest req)
    {
        _logger.LogInformation("GetAllWeapons run...");
        var weapons = await _repository.GetAllAsync();
        return new OkObjectResult(weapons);
    }

    [Function("GetWeaponById")]
    public async Task<IActionResult> GetWeaponById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWeaponById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetWeaponById run...");
        var weapon = await _repository.GetByIdAsync(id);
        return new OkObjectResult(weapon);
    }

    [Function("GetAllWeaponsWithChildren")]
    public async Task<IActionResult> GetAllWeaponsWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllWeaponsWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllWeapons run...");
        var weapons = await _db.Weapons
            .Include(w => w.WeaponType)
            .Include(w => w.Die)
            .ToListAsync();
        return new OkObjectResult(weapons);
    }

    [Function("GetWeaponByIdWithChildren")]
    public async Task<IActionResult> GetWeaponByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWeaponByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetWeaponById run...");
        var weapon = await _db.Weapons
            .Include(w => w.WeaponType)
            .Include(w => w.Die)
            .FirstOrDefaultAsync(w => w.Id == id);
        return new OkObjectResult(weapon);
    }

    [Function("UpdateWeapon")]
    public async Task<IActionResult> UpdateWeapon([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateWeapon/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateWeapon run...");
        var weapon = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(weapon);
    }

    [Function("CreateWeapon")]
    public async Task<IActionResult> CreateWeapon([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateWeapon")] HttpRequest req)
    {
        _logger.LogInformation("CreateWeapon run...");
        var weapon = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/weapon", weapon);
    }

    [Function("DeleteWeapon")]
    public async Task<IActionResult> DeleteWeapon([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteWeapon/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteWeapon run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}