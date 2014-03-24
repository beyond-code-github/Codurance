namespace Codurance.Tests.Unit.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Events;
    using Codurance.ViewModels;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class ViewModelProviderTests : WithSubject<ViewModelProvider>
    {
        protected static string username;

        protected static IEnumerable<PostViewModel> posts;

        private static IEnumerable<FollowEvent> followEvents;

        protected static IEnumerable<PostEvent> timelineEvents;

        protected static IEnumerable<PostEvent> wallPostEvents;

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
    }

    public class When_getting_timeline_posts_for_a_user : ViewModelProviderTests
    {
        private Because of = () => posts = Subject.GetTimelineForUser(username);

        private It should_build_a_model_of_their_timeline =
            () => posts.ShouldEqual(timelineEvents.Select(o => new PostViewModel(o.IssuingUsername, o.Message, o.Timestamp)));
    }

    public class When_getting_wall_posts_for_a_user : ViewModelProviderTests
    {
        private Because of = () => posts = Subject.GetWallForUser(username);

        private It should_build_a_model_of_their_wall =
            () => posts.ShouldEqual(wallPostEvents.Select(o => new PostViewModel(o.IssuingUsername, o.Message, o.Timestamp)));
    }
}
