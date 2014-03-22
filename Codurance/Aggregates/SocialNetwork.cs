namespace Codurance.Aggregates
{
    using Codurance.Events;
    using Codurance.Repositories;

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

        private readonly IUsersRepository usersRepository;

        public SocialNetwork(IEventStore eventStore, IUsersRepository usersRepository)
        {
            this.eventStore = eventStore;
            this.usersRepository = usersRepository;
        }

        public void Handle(PostEvent postEvent)
        {
            this.eventStore.Publish(postEvent);
        }

        public void Handle(FollowEvent followEvent)
        {
            this.eventStore.Publish(followEvent);
        }

        public string GetTimeline(string targetUsername)
        {
            var user = this.usersRepository.GetUser(targetUsername);
            return user.Timeline;
        }

        public string GetWall(string targetUsername)
        {
            var user = this.usersRepository.GetUser(targetUsername);
            return user.Wall;
        }
    }
}
