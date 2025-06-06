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
    public async Task HealthCheck([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
        await _db.Actions.FirstAsync();
    }
}