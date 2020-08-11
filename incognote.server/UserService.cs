using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server
{
    public interface IUserService
    {
        void Add(string connectionId, string name);
        string Name(string connectionId);
    }

    public class UserService : IUserService
    {
        private readonly Dictionary<string, string> _dict = new Dictionary<string, string>();
        public void Add(string connectionId, string name)
        {
            _dict.Add(connectionId, name);
        }

        public string Name(string connectionId)
        {
            return _dict[connectionId];
        }
    }
}
