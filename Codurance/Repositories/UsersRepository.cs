namespace Codurance.Repositories
{
    using System.Linq;

    using Codurance.Entities;
    using Codurance.Events;
    using Codurance.ValueObjects;

    public interface IUsersRepository
    {
        User GetUser(string username);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly IEventStore eventStore;

        public UsersRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public User GetUser(string username)
        {
            var timelineEvents = this.eventStore.GetPostEvents(username);
            var followEvents = this.eventStore.GetFollowEvents(username);

            var wallUserNames = followEvents.Select(o => o.TargetUsername).ToList();
            wallUserNames.Add(username);

            var wallPostEvents = this.eventStore.GetPostEvents(wallUserNames);

            return new User
                       {
                           Wall = wallPostEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp)),
                           Timeline = timelineEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp))
                       };
        }
    }
}
