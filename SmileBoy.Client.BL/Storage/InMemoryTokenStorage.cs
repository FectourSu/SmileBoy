using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.Models;
using System;
using System.Collections.Concurrent;

namespace SmileBoyClient.Core
{
    class InMemoryTokenStorage : ITokenStorage
    {
        public event EventHandler<TokenEventArgs> Changed;
        public event EventHandler Cleared;

        private readonly ConcurrentDictionary<string, string> _tokens;

        public string this[string key] 
        { 
            get => this.GetValue(key); 
            set => this.AddOrUpdate(key, value); 
        }

        public InMemoryTokenStorage()
        {
            _tokens = new ConcurrentDictionary<string, string>();
        }

        public string AddOrUpdate(string key, string value)
        {
            var oldToken = _tokens.AddOrUpdate(key, value, (k, oldValue) => oldValue);

            Changed?.Invoke(this, new TokenEventArgs(key, value, oldToken));
            return this[key];
        }

        public string GetValue(string key)
        {
            _tokens.TryGetValue(key, out string token);
            return token;
        }

        public bool TryRemove(string key, out string value)
        {
            return _tokens.TryRemove(key, out value);
        }

        public void RemoveAll()
        {
            _tokens.Clear();
            Cleared?.Invoke(this, null);
        }
    }
}
