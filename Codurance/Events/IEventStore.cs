namespace Codurance.Events
{
    using System.Collections.Generic;

    public interface IEventStore
    {
        void Publish(IEvent postEvent);

        IEnumerable<FollowEvent> GetFollowEvents(string issuingUsername);

        IEnumerable<PostEvent> GetPostEvents(string[] issuingUsernames);
    }
}
