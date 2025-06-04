using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class WeaponTypeFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<WeaponTypeFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<WeaponType> _repository;

    public WeaponTypeFunctions(ILogger<WeaponTypeFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<WeaponType>();
    }

    [Function("GetAllWeaponTypes")]
    public async Task<IActionResult> GetAllWeaponTypes([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllWeaponTypes")] HttpRequest req)
    {
        _logger.LogInformation("GetAllWeaponTypes run...");
        var WeaponTypes = await _repository.GetAllAsync();
        return new OkObjectResult(WeaponTypes);
    }

    [Function("GetWeaponTypeById")]
    public async Task<IActionResult> GetWeaponTypeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetWeaponTypeById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetWeaponTypeById run...");
        var WeaponType = await _repository.GetByIdAsync(id);
        return new OkObjectResult(WeaponType);
    }

    [Function("UpdateWeaponType")]
    public async Task<IActionResult> UpdateWeaponType([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateWeaponType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateWeaponType run...");
        var WeaponType = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(WeaponType);
    }

    [Function("CreateWeaponType")]
    public async Task<IActionResult> CreateWeaponType([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateWeaponType")] HttpRequest req)
    {
        _logger.LogInformation("CreateWeaponType run...");
        var WeaponType = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/WeaponType", WeaponType);
    }

    [Function("DeleteWeaponType")]
    public async Task<IActionResult> DeleteWeaponType([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteWeaponType/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteWeaponType run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}