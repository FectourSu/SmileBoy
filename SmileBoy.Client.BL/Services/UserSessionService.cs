using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Services
{
    public class UserSessionService : ISessionService<UserSession>
    {
        public async Task SaveAsync(string sessionPath, UserSession session)
        {
            using (FileStream fs = new FileStream(sessionPath, FileMode.Create)) 
                await JsonSerializer.SerializeAsync(fs, session);
        }

        public bool TryRecover(string sessionPath, out UserSession result)
        {
            result = default;

            if (!File.Exists(sessionPath))
                return false;

            result = JsonSerializer.Deserialize<UserSession>(File.ReadAllText(sessionPath));

            return true;
        }
    }
}
