namespace Codurance.Tests.Unit.Aggregates
{
    using System.Collections.Generic;

    using Codurance.Aggregates;
    using Codurance.Entities;
    using Codurance.Events;
    using Codurance.Repositories;
    using Codurance.ValueObject;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class SocialNetworkTests : WithSubject<SocialNetwork> { }

    public class When_handling_a_post_event : SocialNetworkTests
    {
        private static PostEvent postEvent;

        private Establish context = () =>
            {
                var message = TestHelpers.RandomString();
                var issuingUsername = TestHelpers.RandomString();
                var timestamp = TestHelpers.RandomDateTime();

                postEvent = new PostEvent(message, issuingUsername, timestamp);
            };

        private Because of = () => Subject.Handle(postEvent);

        private It should_write_the_event_to_the_event_store =
            () => The<IEventStore>().WasToldTo(o => o.Publish(postEvent));
    }

    public class When_handling_a_follow_event : SocialNetworkTests
    {
        private static FollowEvent followEvent;

        private Establish context = () =>
        {
            var issuingUsername = TestHelpers.RandomString();
            var targetUsername = TestHelpers.RandomString();
            var timestamp = TestHelpers.RandomDateTime();

            followEvent = new FollowEvent(issuingUsername, targetUsername, timestamp);
        };

        private Because of = () => Subject.Handle(followEvent);

        private It should_write_the_event_to_the_event_store =
            () => The<IEventStore>().WasToldTo(o => o.Publish(followEvent));
    }

    public class When_getting_a_timeline : SocialNetworkTests
    {
        private static User user;

        private static IEnumerable<Post> result;

        private Establish context = () =>
            {
                user = new User
                           {
                               Username = TestHelpers.RandomString(),
                               Timeline = TestHelpers.RandomPosts(),
                               Wall = TestHelpers.RandomPosts()
                           };

                The<IUsersRepository>().WhenToldTo(o => o.GetUser(user.Username)).Return(user);
            };

        private Because of = () => result = Subject.GetTimeline(user.Username);

        private It should_query_the_repository_for_the_user_and_return_the_timeline = () => result.ShouldEqual(user.Timeline);
    }

    public class When_getting_a_wall : SocialNetworkTests
    {
        private static User user;

        private static IEnumerable<Post> result;

        private Establish context = () =>
            {
                user = new User
                           {
                               Username = TestHelpers.RandomString(),
                               Timeline = TestHelpers.RandomPosts(),
                               Wall = TestHelpers.RandomPosts()
                           };

                The<IUsersRepository>().WhenToldTo(o => o.GetUser(user.Username)).Return(user);
            };

        private Because of = () => result = Subject.GetWall(user.Username);

        private It should_query_the_repository_for_the_user_and_return_the_wall = () => result.ShouldEqual(user.Wall);
    }
}
