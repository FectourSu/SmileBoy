using SmileBoyClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmileBoyClient.Core.IContract
{
    public interface ITokenStorage : IReaderTokenStorage, IWriterTokenStorage
    {
        bool TryRemove(string key, out string value);

        void RemoveAll();
    }
    public interface IReaderTokenStorage : IChangeable<TokenEventArgs>, IClearable
    {
        string this[string key] { get; set; }
        string GetValue(string key);
    }

    public interface IWriterTokenStorage : IChangeable<TokenEventArgs>, IClearable
    {
        string AddOrUpdate(string key, string value);
    }
}
