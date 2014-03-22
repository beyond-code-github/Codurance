namespace Codurance.Tests.Unit.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Entities;
    using Codurance.Events;
    using Codurance.Repositories;
    using Codurance.ValueObjects;

    using Machine.Fakes;
    using Machine.Specifications;

    public class When_getting_a_user : WithSubject<UsersRepository>
    {
        private static User user;

        private static string username;

        private static IEnumerable<FollowEvent> followEvents;

        private static IEnumerable<PostEvent> timelineEvents, wallPostEvents;

        private static List<string> wallUserNames;

        private Establish context = () =>
            {
                username = TestHelpers.RandomString();

                followEvents = TestHelpers.RandomFollowEvents(username).ToList();
                timelineEvents = TestHelpers.RandomPostEvents().ToList();
                wallPostEvents = TestHelpers.RandomPostEvents().ToList();

                wallUserNames = followEvents.Select(o => o.TargetUsername).ToList();
                wallUserNames.Add(username);
                
                The<IEventStore>().WhenToldTo(o => o.GetFollowEvents(username)).Return(followEvents);
                The<IEventStore>().WhenToldTo(o => o.GetPostEvents(username)).Return(timelineEvents);
                The<IEventStore>().WhenToldTo(o => o.GetPostEvents(wallUserNames)).Return(wallPostEvents);
            };

        private Because of = () => user = Subject.GetUser(username);

        private It should_build_the_users_wall =
            () => user.Wall.ShouldEqual(wallPostEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp)));

        private It should_build_the_users_timeline =
            () => user.Timeline.ShouldEqual(timelineEvents.Select(o => new Post(o.IssuingUsername, o.Message, o.Timestamp)));
    }
}
