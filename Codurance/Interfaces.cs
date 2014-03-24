namespace Codurance
{
    using System.Collections.Generic;

    using Codurance.Events;

    public interface IEvent
    {
    }

    public interface IRequest
    {
    }

    public interface IEventStore
    {
        void Publish(IEvent postEvent);

        IEnumerable<FollowEvent> GetFollowEvents(string issuingUsername);

        IEnumerable<PostEvent> GetPostEvents(string issuingUsername);

        IEnumerable<PostEvent> GetPostEvents(IEnumerable<string> issuingUsernames);
    }
}
