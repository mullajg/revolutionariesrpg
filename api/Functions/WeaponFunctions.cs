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

    [Function("GetWeaponsForTable")]
    public async Task<IActionResult> GetAllWeaponsForTable([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWeaponsForTable")] HttpRequest req)
    {
        _logger.LogInformation("GetAllWeaponsForTable run...");
        var weapons = await _db.Weapons
            .AsNoTracking()
            .Include(w => w.WeaponType)
            .Include(w => w.Die)
            .Select(w => new
            {
                Name = w.Name,
                Type = w.WeaponType.Type,
                Damage = w.DieId.HasValue ? "d" + w.Die.Sides.ToString() + "+" + w.Damage.ToString() : "N/A",
                Cost = w.Cost.HasValue ? w.Cost.ToString() : "N/A",
                Concealable = w.Concealable.HasValue ? w.Concealable.Value ? "Yes" : "No" : "N/A",
                Range = w.Range.HasValue ? w.Range.ToString() : "N/A",
                AmmoCapacity = w.AmmoCapacity.HasValue ? w.AmmoCapacity.ToString() : "N/A", 
                Radius = w.Radius.HasValue ? w.Radius.ToString() : "N/A",
                Description = w.Description
            })
            .ToListAsync();
        return new OkObjectResult(weapons);
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