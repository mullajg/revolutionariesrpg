using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RevolutionariesApi.Functions.HealthCheckFunctions;

namespace api.StaticClasses
{
    public static class UserAuth
    {
        public static ClaimsPrincipal ParseClientPrincipal(HttpRequest req)
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

            return new ClaimsPrincipal(identity);
        }
    }
    public class ClientPrincipal
    {
        public string IdentityProvider { get; set; }
        public string UserId { get; set; }
        public string UserDetails { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public IEnumerable<ClientPrincipalClaim> Claims { get; set; } = new List<ClientPrincipalClaim>();
    }

    public class ClientPrincipalClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
