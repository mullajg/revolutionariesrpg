using api.StaticClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using revolutionariesrpg.api;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
            else if (req.Method.Equals("GET", StringComparison.OrdinalIgnoreCase) && action != null)
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

    [Function("GetMyId")]
    public async Task<IActionResult> GetMyId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMyId")] HttpRequest req)
    {
        try
        {
            var user = UserAuth.ParseClientPrincipal(req);
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return new OkObjectResult(userId);
        }
        catch (Exception ex) 
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}