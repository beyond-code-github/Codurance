namespace Codurance.Tests.Unit.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Entities;
    using Codurance.Events;
    using Codurance.Repositories;
    using Codurance.ValueObject;

    using Machine.Fakes;
    using Machine.Specifications;

    public class When_getting_a_user : WithSubject<UsersRepository>
    {
        private static User user;

        private static string username;

        private static IEnumerable<FollowEvent> followEvents;

        private static IEnumerable<PostEvent> ownPostEvents, followerPostEvents;

        private static string[] followingUsers;

        private Establish context = () =>
            {
                username = TestHelpers.RandomString();

                followEvents = TestHelpers.RandomFollowEvents(username).ToList();
                ownPostEvents = TestHelpers.RandomPostEvents().ToList();
                followerPostEvents = TestHelpers.RandomPostEvents().ToList();

                followingUsers = followEvents.Select(o => o.TargetUsername).ToArray();

                The<IEventStore>().WhenToldTo(o => o.GetFollowEvents(username)).Return(followEvents);
                The<IEventStore>().WhenToldTo(o => o.GetPostEvents(new[] { username })).Return(ownPostEvents);
                The<IEventStore>().WhenToldTo(o => o.GetPostEvents(followingUsers)).Return(followerPostEvents);
            };

        private Because of = () => user = Subject.GetUser(username);

        private It should_build_the_users_wall =
            () => user.Wall.ShouldEqual(ownPostEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp)));

        private It should_build_the_users_timeline =
            () => user.Timeline.ShouldEqual(followerPostEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp)));
    }
}
