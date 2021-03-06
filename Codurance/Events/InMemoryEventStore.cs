﻿namespace Codurance.Events
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentStack<IEvent> events;

        public InMemoryEventStore()
        {
            this.events = new ConcurrentStack<IEvent>();
        }

        public void Publish(IEvent postEvent)
        {
            events.Push(postEvent);
        }

        public IEnumerable<FollowEvent> GetFollowEvents(string issuingUsername)
        {
            return
                events.Where(o => o is FollowEvent).Cast<FollowEvent>().Where(o => o.IssuingUsername == issuingUsername);
        }

        public IEnumerable<PostEvent> GetPostEvents(string issuingUsername)
        {
            return events.Where(o => o is PostEvent).Cast<PostEvent>().Where(o => o.IssuingUsername == issuingUsername);
        }

        public IEnumerable<PostEvent> GetPostEvents(IEnumerable<string> issuingUsernames)
        {
            return
                events.Where(o => o is PostEvent)
                    .Cast<PostEvent>()
                    .Where(o => issuingUsernames.Contains(o.IssuingUsername));
        }
    }
}
