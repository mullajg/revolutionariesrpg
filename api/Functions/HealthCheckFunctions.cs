using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using revolutionariesrpg.api;

namespace RevolutionariesApi.Functions;

public class HealthCheckFunctions
{
    private readonly AppDbContext _db;

    public HealthCheckFunctions(AppDbContext db)
    {
        _db = db;   
    }

    [Function("HealthCheck")]
    public async Task<IActionResult> HealthCheck([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "HealthCheck")] HttpRequest req)
    {
        try
        {
            await _db.Actions.FirstOrDefaultAsync();
            return new OkObjectResult();
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}