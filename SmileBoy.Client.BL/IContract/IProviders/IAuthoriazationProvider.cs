using System.Security;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IProviders
{
    public interface IAuthoriazationProvider
    {
        AuthenticationState AuthenticationState { get; }

        Task<AuthenticationState> Login(string email, SecureString password);

        Task<AuthenticationState> ExtendSession();

        Task Logout();
    }
}
