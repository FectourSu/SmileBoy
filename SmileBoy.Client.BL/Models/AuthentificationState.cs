using System.Linq;
using System.Security.Claims;

namespace SmileBoyClient.Core
{
    public class AuthentificationState
    {
        public ClaimsPrincipal User { get; private set; }

        public string ErrorMessage { get; set; }

        public bool IsAuthentification => User.Identity.IsAuthenticated;

        public AuthentificationState(ClaimsPrincipal principal)
        {
            User = principal;
        }

        public string GetClaim(string nameClaim) =>
            User.Claims.FirstOrDefault(claim => claim.Type.Equals(nameClaim))?.Value;
    }
}
