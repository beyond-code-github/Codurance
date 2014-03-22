namespace Codurance.Aggregates
{
    using System.Collections.Generic;

    using Codurance.Events;
    using Codurance.Repositories;
    using Codurance.ValueObjects;

    public interface ISocialNetwork
    {
        void Handle(PostEvent postEvent);

        void Handle(FollowEvent followEvent);

        IEnumerable<Post> GetTimeline(string targetUsername);

        IEnumerable<Post> GetWall(string targetUsername);
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

        public IEnumerable<Post> GetTimeline(string targetUsername)
        {
            var user = this.usersRepository.GetUser(targetUsername);
            return user.Timeline;
        }

        public IEnumerable<Post> GetWall(string targetUsername)
        {
            var user = this.usersRepository.GetUser(targetUsername);
            return user.Wall;
        }
    }
}
