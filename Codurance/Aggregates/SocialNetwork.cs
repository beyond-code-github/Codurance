namespace Codurance.Aggregates
{
    using Codurance.Events;

    public interface ISocialNetwork
    {
        void Handle(PostEvent postEvent);

        void Handle(FollowEvent followEvent);

        string GetTimeline(string targetUsername);

        string GetWall(string targetUsername);
    }

    public class SocialNetwork : ISocialNetwork
    {
        private readonly IEventStore eventStore;

        public SocialNetwork(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public void Handle(PostEvent postEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(FollowEvent followEvent)
        {
            throw new System.NotImplementedException();
        }

        public string GetTimeline(string targetUsername)
        {
            throw new System.NotImplementedException();
        }

        public string GetWall(string targetUsername)
        {
            throw new System.NotImplementedException();
        }
    }
}
