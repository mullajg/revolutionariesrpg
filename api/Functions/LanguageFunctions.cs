using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;
public class LanguageFunctions 
{
    private readonly AppDbContext _db;
    private readonly ILogger<LanguageFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Language> _repository;

    public LanguageFunctions(ILogger<LanguageFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Language>();
    }

    [Function("GetAllLanguages")]
    public async Task<IActionResult> GetAllLanguages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllLanguages")] HttpRequest req)
    {
        _logger.LogInformation("GetAllLanguages run...");
        var Languages = await _repository.GetAllAsync();
        return new OkObjectResult(Languages);
    }

    [Function("GetAllLanguagesForTable")]
    public async Task<IActionResult> GetAllLanguagesForTable([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllLanguagesForTable")] HttpRequest req)
    {
        _logger.LogInformation("GetAllLanguagesForTable run...");
        var Languages = await _repository.GetAllAsync();
        return new OkObjectResult(Languages.Select(s => new { name = s.Name, description = s.Description }));
    }

    [Function("GetLanguageById")]
    public async Task<IActionResult> GetLanguageById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetLanguageById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetLanguageById run...");
        var Language = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Language);
    }

    [Function("UpdateLanguage")]
    public async Task<IActionResult> UpdateLanguage([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateLanguage/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateLanguage run...");
        var Language = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Language);
    }

    [Function("CreateLanguage")]
    public async Task<IActionResult> CreateLanguage([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateLanguage")] HttpRequest req)
    {
        _logger.LogInformation("CreateLanguage run...");
        var Language = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Language", Language);
    }

    [Function("DeleteLanguage")]
    public async Task<IActionResult> DeleteLanguage([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteLanguage/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteLanguage run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}