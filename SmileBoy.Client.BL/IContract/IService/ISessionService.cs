using SmileBoyClient.Core.Models;
using System.Threading.Tasks;

namespace SmileBoyClient.Core.IContract.IService
{
    public interface ISessionService<T>
        where T : UserSession
    {
        Task SaveAsync(string sessionPath, T session);

        bool TryRecover(string sessionPath, out T result);
    }
}
