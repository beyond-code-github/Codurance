namespace Codurance.Tests.Unit
{
    using System;
    using System.Collections.Generic;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Handlers;
    using Codurance.Requests;
    using Codurance.ViewModels;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class InputHandlerTests : WithFakes
    {
        protected static InputHandler subject;

        protected static Func<DateTime> timestampProvider;
        
        protected static DateTime timestamp;

        protected static string result;

        private Establish context = () =>
            {
                timestamp = TestHelpers.RandomDateTime();
                timestampProvider = () => timestamp;

                subject = new InputHandler(
                    The<ISocialNetwork>(),
                    The<IRequestParser>(),
                    The<IRenderingEngine>(),
                    timestampProvider,
                    The<IViewModelProvider>());
            };
    }

    public class When_handling_a_read_request : InputHandlerTests
    {
        protected static ReadRequest request;

        private static IEnumerable<PostViewModel> timelinePosts;

        protected static string targetUsername, input, renderOutput;

        private Establish context = () =>
        {
            input = TestHelpers.RandomString();
            targetUsername = TestHelpers.RandomString();
            renderOutput = TestHelpers.RandomString();
            timelinePosts = TestHelpers.RandomPosts();
            
            request = new ReadRequest(targetUsername);
            The<IRequestParser>().WhenToldTo(o => o.Parse(input)).Return(request);

            The<IViewModelProvider>().WhenToldTo(o => o.GetTimelineForUser(targetUsername)).Return(timelinePosts);
            The<IRenderingEngine>().WhenToldTo(o => o.RenderTimelinePosts(timelinePosts)).Return(renderOutput);
        };

        private Because of = () => result = subject.Handle(input);

        private It should_render_the_viewmodel = () => result.ShouldEqual(renderOutput);
    }

    public class When_handling_a_wall_request : InputHandlerTests
    {
        protected static WallRequest request;

        private static IEnumerable<PostViewModel> wallPosts;

        protected static string targetUsername, input, renderOutput;

        private Establish context = () =>
        {
            input = TestHelpers.RandomString();
            targetUsername = TestHelpers.RandomString();
            renderOutput = TestHelpers.RandomString();
            wallPosts = TestHelpers.RandomPosts();

            request = new WallRequest(targetUsername);
            The<IRequestParser>().WhenToldTo(o => o.Parse(input)).Return(request);

            The<IViewModelProvider>().WhenToldTo(o => o.GetWallForUser(targetUsername)).Return(wallPosts);
            The<IRenderingEngine>().WhenToldTo(o => o.RenderWallPosts(wallPosts)).Return(renderOutput);
        };

        private Because of = () => result = subject.Handle(input);

        private It should_render_the_viewmodel = () => result.ShouldEqual(renderOutput);
    }

    public class When_handling_a_follow_request : InputHandlerTests
    {
        protected static FollowRequest request;
        
        protected static string issuingUsername, targetUsername, input;
        
        private Establish context = () =>
            {
                input = TestHelpers.RandomString();
                issuingUsername = TestHelpers.RandomString();
                targetUsername = TestHelpers.RandomString();

                request = new FollowRequest(issuingUsername, targetUsername);
                The<IRequestParser>().WhenToldTo(o => o.Parse(input)).Return(request);
            };

        private Because of = () => result = subject.Handle(input);

        private It should_dispatch_a_follow_event = () => The<ISocialNetwork>().WasToldTo(o =>
             o.Handle(new FollowEvent(request.IssuingUsername, request.TargetUsername, timestamp)));

        private It should_return_empty_string = () => result.ShouldEqual(string.Empty);
    }

    public class When_handling_a_post_request : InputHandlerTests
    {
        protected static PostRequest request;

        protected static string message, issuingUsername, input;

        private Establish context = () =>
        {
            input = TestHelpers.RandomString();
            message = TestHelpers.RandomString();
            issuingUsername = TestHelpers.RandomString();

            request = new PostRequest(message, issuingUsername);
            The<IRequestParser>().WhenToldTo(o => o.Parse(input)).Return(request);
        };

        private Because of = () => result = subject.Handle(input);

        private It should_dispatch_a_post_event = () => The<ISocialNetwork>().WasToldTo(o =>
             o.Handle(new PostEvent(request.Message, request.IssuingUsername, timestamp)));

        private It should_return_empty_string = () => result.ShouldEqual(string.Empty);
    }
}
