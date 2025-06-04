using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using revolutionariesrpg.api.Entities;
using revolutionariesrpg.api.Interfaces;

namespace revolutionariesrpg.api.Functions;

public class SkillFunctions
{
    private readonly AppDbContext _db;
    private readonly ILogger<SkillFunctions> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Skill> _repository;

    public SkillFunctions(ILogger<SkillFunctions> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _db = db;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Skill>();
    }

    [Function("GetAllSkills")]
    public async Task<IActionResult> GetAllSkills([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllSkills")] HttpRequest req)
    {
        _logger.LogInformation("GetAllSkills run...");
        var Skills = await _repository.GetAllAsync();
        return new OkObjectResult(Skills);
    }

    [Function("GetSkillById")]
    public async Task<IActionResult> GetSkillById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSkillById/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("GetSkillById run...");
        var Skill = await _repository.GetByIdAsync(id);
        return new OkObjectResult(Skill);
    }

    [Function("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateSkill/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("UpdateSkill run...");
        var Skill = await _repository.Update(req.Body, id);
        await _unitOfWork.CommitAsync();

        return new OkObjectResult(Skill);
    }

    [Function("CreateSkill")]
    public async Task<IActionResult> CreateSkill([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateSkill")] HttpRequest req)
    {
        _logger.LogInformation("CreateSkill run...");
        var Skill = await _repository.AddAsync(req.Body);
        await _unitOfWork.CommitAsync();

        return new CreatedResult("/Skill", Skill);
    }

    [Function("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteSkill/{id}")] HttpRequest req, Guid id)
    {
        _logger.LogInformation("DeleteSkill run...");
        var success = await _repository.Delete(id);
        await _unitOfWork.CommitAsync();

        return success ? new NoContentResult() : new NotFoundResult();
    }
}