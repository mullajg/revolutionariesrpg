using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class HeritageFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<HeritageFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Heritage> _repository;

    public HeritageFunctions(ILogger<HeritageFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Heritage>();
    }

    [Function("GetAllHeritages")]
    public async Task<IActionResult> GetAllHeritages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllHeritages")] HttpRequest req)
    {
        _logger.LogInformation("GetAllHeritages run...");
        var Heritages = await _repository.GetAllAsync();
        return new OkObjectResult(Heritages);
    }

    [Function("GetAllHeritagesForTable")]
    public async Task<IActionResult> GetAllHeritagesForTable([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllHeritagesForTable")] HttpRequest req)
    {
        _logger.LogInformation("GetAllHeritagesForTable run...");
        var Heritages = await _repository.GetAllAsync();
        return new OkObjectResult(Heritages.Select(h => new { name = h.Name, description = h.Description }));
    }

    [Function("GetHeritageById")]
    public async Task<IActionResult> GetHeritageById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetHeritageById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetHeritageById run...");
        var Heritage = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Heritage);
    }

    [Function("UpdateHeritage")]
    public async Task<IActionResult> UpdateHeritage([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateHeritage/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateHeritage run...");
        var Heritage = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Heritage);
    }

    [Function("CreateHeritage")]
    public async Task<IActionResult> CreateHeritage([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateHeritage")] HttpRequest req)
    {
        _logger.LogInformation("CreateHeritage run...");
        var Heritage = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Heritage", Heritage);
    }

    [Function("DeleteHeritage")]
    public async Task<IActionResult> DeleteHeritage([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteHeritage/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteHeritage run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}