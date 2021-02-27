using System.Linq;
using System.Security.Claims;

namespace SmileBoyClient.Core
{
    public class AuthenticationState
    {
        public ClaimsPrincipal User { get; private set; }

        public string ErrorMessage { get; set; }

        public bool IsAuthentication => User.Identity.IsAuthenticated;

        public AuthenticationState(ClaimsPrincipal principal)
        {
            User = principal;
        }

        public string GetClaim(string nameClaim) =>
            User.Claims.FirstOrDefault(claim => claim.Type.Equals(nameClaim))?.Value;
    }
}
