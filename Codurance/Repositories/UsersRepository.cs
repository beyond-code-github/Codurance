namespace Codurance.Repositories
{
    using System.Linq;

    using Codurance.Entities;
    using Codurance.Events;

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
            var ownPostEvents = this.eventStore.GetPostEvents(new[] { username });
            var followingUserNames = this.eventStore.GetFollowEvents(username).Select(o => o.TargetUsername).ToArray();
            var followingPostEvents = this.eventStore.GetPostEvents(followingUserNames);

            return new User
                       {
                           Wall = string.Join("\n", ownPostEvents),
                           Timeline = string.Join("\n", followingPostEvents)
                       };
        }
    }
}
