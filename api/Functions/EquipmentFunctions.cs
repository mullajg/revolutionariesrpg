using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class EquipmentFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<EquipmentFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Equipment> _repository;

    public EquipmentFunctions(ILogger<EquipmentFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Equipment>();
    }

    [Function("GetAllEquipments")]
    public async Task<IActionResult> GetAllEquipments([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllEquipment")] HttpRequest req)
    {
        _logger.LogInformation("GetAllEquipments run...");
        var Equipments = await _repository.GetAllAsync();
        return new OkObjectResult(Equipments);
    }

    [Function("GetEquipmentById")]
    public async Task<IActionResult> GetEquipmentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetEquipmentById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetEquipmentById run...");
        var Equipment = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Equipment);
    }

    [Function("UpdateEquipment")]
    public async Task<IActionResult> UpdateEquipment([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateEquipment/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateEquipment run...");
        var Equipment = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Equipment);
    }

    [Function("CreateEquipment")]
    public async Task<IActionResult> CreateEquipment([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateEquipment")] HttpRequest req)
    {
        _logger.LogInformation("CreateEquipment run...");
        var Equipment = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Equipment", Equipment);
    }

    [Function("DeleteEquipment")]
    public async Task<IActionResult> DeleteEquipment([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteEquipment/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteEquipment run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}