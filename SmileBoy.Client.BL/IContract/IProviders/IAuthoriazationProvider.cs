using System.Security;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IProviders
{
    public interface IAuthoriazationProvider
    {
        AuthentificationState AuthentificationState { get; }

        Task<AuthentificationState> Login(string email, SecureString password);

        Task<AuthentificationState> ExtendSession();

        Task Logout();
    }
}
