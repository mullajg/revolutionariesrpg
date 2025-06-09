using api.StaticClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class PlayerCharacterFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<PlayerCharacterFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Entities.PlayerCharacter> _repository;

    public PlayerCharacterFunctions(ILogger<PlayerCharacterFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Entities.PlayerCharacter>();
    }

    [Function("GetAllPlayerCharacters")]
    public async Task<IActionResult> GetAllPlayerCharacters([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllPlayerCharacters")] HttpRequest req)
    {
        _logger.LogInformation("GetAllPlayerCharacters run...");
        try
        {
            var PlayerCharacters = await _repository.GetAllAsync();
            return new OkObjectResult(PlayerCharacters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while parsing request data.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [Function("GetPlayerCharacterById")]
    public async Task<IActionResult> GetPlayerCharacterById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPlayerCharacterById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetPlayerCharacterById run...");
        var user = UserAuth.ParseClientPrincipal(req);

        var PlayerCharacter = await _repository.GetByIdAsync(id);
        return new OkObjectResult(PlayerCharacter);
    }

    [Function("GetAllUserPlayerCharacters")]
    public async Task<IActionResult> GetAllUserPlayerCharacters([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllUserPlayerCharacters")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetAllUserPlayerCharacters run...");
        var PlayerCharacter = await _repository.GetByIdAsync(id);
        return new OkObjectResult(PlayerCharacter);
    }

    [Function("GetAllPlayerCharactersWithChildren")]
    public async Task<IActionResult> GetAllPlayerCharactersWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllPlayerCharactersWithChildren")] HttpRequest req)
    {
        _logger.LogInformation("GetAllPlayerCharactersWithChildren run...");
        var PlayerCharacters = await _db.PlayerCharacters
            .ToListAsync();
        return new OkObjectResult(PlayerCharacters);
    }

    [Function("GetPlayerCharacterByIdWithChildren")]
    public async Task<IActionResult> GetPlayerCharacterByIdWithChildren([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPlayerCharacterByIdWithChildren/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetPlayerCharacterById run...");
        var PlayerCharacter = await _db.PlayerCharacters
            .Where(a => a.Id == id)
            .ToListAsync();
        return new OkObjectResult(PlayerCharacter);
    }

    [Function("UpdatePlayerCharacter")]
    public async Task<IActionResult> UpdatePlayerCharacter([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdatePlayerCharacter/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdatePlayerCharacter run...");
        var PlayerCharacter = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(PlayerCharacter);
    }

    [Function("CreatePlayerCharacter")]
    public async Task<IActionResult> CreatePlayerCharacter([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreatePlayerCharacter")] HttpRequest req)
    {
        _logger.LogInformation("CreatePlayerCharacter run...");
        var PlayerCharacter = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/PlayerCharacter", PlayerCharacter);
    }

    [Function("DeletePlayerCharacter")]
    public async Task<IActionResult> DeletePlayerCharacter([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeletePlayerCharacter/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeletePlayerCharacter run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}