namespace Codurance.Aggregates
{
    using Codurance.Events;

    public interface ISocialNetwork
    {
        void Handle(PostEvent postEvent);

        void Handle(FollowEvent followEvent);
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
            this.eventStore.Publish(postEvent);
        }

        public void Handle(FollowEvent followEvent)
        {
            this.eventStore.Publish(followEvent);
        }
    }
}
