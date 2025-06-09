using Microsoft.AspNetCore.Http;
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
            var principal = new ClientPrincipal();

            if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
            {
                var data = header[0];
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.UTF8.GetString(decoded);
                principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var identity = new ClaimsIdentity(principal.IdentityProvider, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));

            foreach (var claim in principal.Claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value));
            }

            // Add roles from the principal, mapping them to ClaimTypes.Role
            foreach (var role in principal.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var currentUser = new ClaimsPrincipal(identity);

            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return new OkObjectResult(userId);
        }
        catch
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public class ClientPrincipal
    {
        public string IdentityProvider { get; set; }
        public string UserId { get; set; }
        public string UserDetails { get; set; } // Often email or username
        public IEnumerable<string> Roles { get; set; } = new List<string>(); // Ensure it's not null
        public IEnumerable<ClientPrincipalClaim> Claims { get; set; } = new List<ClientPrincipalClaim>(); // Ensure it's not null
    }

    public class ClientPrincipalClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}