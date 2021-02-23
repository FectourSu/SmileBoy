using SmileBoyClient.Core.Models;
using System.Security;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    public interface IAuthorizationService<TResult>
        where TResult : class, new()
    {
        Task<AuthorizationResult<TResult>> AuthorizeAsync(string email, SecureString password);

        Task<AuthorizationResult<TResult>> RefreshAsync(string refreshToken);
    }
}
