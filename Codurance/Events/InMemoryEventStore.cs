namespace Codurance.Events
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentQueue<IEvent> events;

        public InMemoryEventStore()
        {
            this.events = new ConcurrentQueue<IEvent>();
        }

        public void Publish(IEvent postEvent)
        {
            events.Enqueue(postEvent);
        }

        public IEnumerable<FollowEvent> GetFollowEvents(string issuingUsername)
        {
            return events.Where(o => o is FollowEvent).Cast<FollowEvent>().Where(o => o.IssuingUsername == issuingUsername);
        }

        public IEnumerable<PostEvent> GetPostEvents(string[] issuingUsernames)
        {
            return events.Where(o => o is PostEvent).Cast<PostEvent>().Where(o => issuingUsernames.Contains(o.IssuingUsername));
        }
    }
}
