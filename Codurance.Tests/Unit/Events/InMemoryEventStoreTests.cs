namespace Codurance.Tests.Unit.Events
{
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Events;

    using Machine.Fakes;
    using Machine.Specifications;

    public class DummyEvent : IEvent { }

    public abstract class InMemoryEventStoreTests : WithSubject<InMemoryEventStore>
    {
        protected static string username, anotherUsername;

        protected static IEnumerable<FollowEvent> followEvents, anothersFollowEvents;

        protected static IEnumerable<PostEvent> postEvents, anothersPostEvents;

        protected static IEnumerable<DummyEvent> dummyEvents;

        private Establish context = () =>
            {
                username = TestHelpers.RandomString();
                anotherUsername = TestHelpers.RandomString();

                followEvents = TestHelpers.RandomFollowEvents(username);
                anothersFollowEvents = TestHelpers.RandomFollowEvents(anotherUsername);

                postEvents = TestHelpers.RandomPostEvents(username);
                anothersPostEvents = TestHelpers.RandomPostEvents(anotherUsername);

                foreach (var follow in followEvents.Concat(anothersFollowEvents))
                {
                    Subject.Publish(follow);
                }

                foreach (var post in postEvents.Concat(anothersPostEvents))
                {
                    Subject.Publish(post);
                }

                foreach (var dummy in TestHelpers.RandomDummyEvents())
                {
                    Subject.Publish(dummy);
                }
            };
    }

    public class When_reading_follow_events : InMemoryEventStoreTests
    {
        private static IEnumerable<FollowEvent> result;

        private Because of = () => result = Subject.GetFollowEvents(username);

        private It should_only_return_events_matching_the_issuing_username =
            () => result.ShouldEachConformTo(o => o.IssuingUsername == username);

        private It should_return_all_the_records = () => result.Count().ShouldEqual(followEvents.Count());
    }

    public class When_reading_follow_events_for_another_user : InMemoryEventStoreTests
    {
        private static IEnumerable<FollowEvent> result;

        private Because of = () => result = Subject.GetFollowEvents(anotherUsername);

        private It should_only_return_events_matching_the_issuing_username =
            () => result.ShouldEachConformTo(o => o.IssuingUsername == anotherUsername);

        private It should_return_all_the_records = () => result.Count().ShouldEqual(anothersFollowEvents.Count());
    }

    public class When_reading_post_events_for_a_single_user : InMemoryEventStoreTests
    {
        private static IEnumerable<PostEvent> result;

        private Because of = () => result = Subject.GetPostEvents(new[] { username });

        private It should_only_return_events_matching_the_issuing_username =
            () => result.ShouldEachConformTo(o => o.IssuingUsername == username);

        private It should_return_all_the_records = () => result.Count().ShouldEqual(postEvents.Count());
    }

    public class When_reading_post_events_for_a_multiple_users : InMemoryEventStoreTests
    {
        private static IEnumerable<PostEvent> result;

        private Because of = () => result = Subject.GetPostEvents(new[] { username, anotherUsername });

        private It should_only_return_events_matching_the_issuing_usernames =
            () => result.ShouldEachConformTo(o => o.IssuingUsername == username || o.IssuingUsername == anotherUsername);

        private It should_return_all_the_records = () => result.Count().ShouldEqual(postEvents.Count() + anothersPostEvents.Count());
    }
}
