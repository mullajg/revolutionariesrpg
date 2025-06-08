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
    public async Task<IActionResult> HealthCheck([HttpTrigger(AuthorizationLevel.Anonymous, "get", "head", Route = "HealthCheck")] HttpRequest req)
    {
        try
        {
            var action = await _db.Actions.FirstOrDefaultAsync();
            if (req.Method.Equals("HEAD", StringComparison.OrdinalIgnoreCase) && action != null)
            {
                return new OkResult();
            }
            else if (req.Method.Equals("HEAD", StringComparison.OrdinalIgnoreCase) && action != null)
            {
                return new OkObjectResult(action);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}