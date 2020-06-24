using System;
using System.Collections.Generic;

namespace incognote.server
{
    public interface IIncomingServiceCaller
    {
        bool PerformAction(string id, string connectionId, string payload);
    }

    public interface IIncomingServiceListener
    {
        void Subscribe(string id, Action<string, string> callback);
    }

    /// <summary>
    /// This should take the subscriptions as a constructor
    /// </summary>
    public class IncomingService : IIncomingServiceCaller, IIncomingServiceListener
    {
        private readonly Dictionary<string, List<Action<string, string>>> callbacks = new Dictionary<string, List<Action<string, string>>>();
        public void Subscribe(string id, Action<string, string> callback)
        {
            if (!callbacks.ContainsKey(id))
            {
                callbacks[id] = new List<Action<string, string>>();
            }
            callbacks[id].Add(callback);
        }
        public bool PerformAction(string id, string connectionId, string payload)
        {
            if (callbacks.ContainsKey(id))
            {
                callbacks[id].ForEach(c => c(connectionId, payload));
            }
            return false;
        }
    }
}
