namespace Codurance.Events
{
    using System.Collections.Generic;

    public class InMemoryEventStore : IEventStore
    {
        public void Publish(IEvent postEvent)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FollowEvent> GetFollowEvents(string issuingUsername)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PostEvent> GetPostEvents(string[] issuingUsernames)
        {
            throw new System.NotImplementedException();
        }
    }
}
