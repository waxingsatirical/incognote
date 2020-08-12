using incognote.dal.Models;
using incognote.server.Change;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace incognote.server.State
{
    public class StatusCollection : IEnumerable<Status>
    {
        private readonly List<Status> statuses = new List<Status>();
        private readonly IStatusChangeService statusChangeService;

        public StatusCollection(IStatusChangeService statusChangeService)
        {
            this.statusChangeService = statusChangeService;
        }
        public IEnumerator<Status> GetEnumerator()
        {
            lock (statuses)
            {
                return statuses.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (statuses)
            {
                return statuses.ToArray().GetEnumerator();
            }
        }

        public void Add(Status status)
        {
            lock (statuses)
            {
                statuses.Add(status);
                statusChangeService.StatusAdded(statuses.Count, status);
            }
        }
    }
}
