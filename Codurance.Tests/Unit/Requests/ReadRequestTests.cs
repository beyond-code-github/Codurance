namespace Codurance.Tests.Unit.Requests
{
    using System.Collections;
    using System.Collections.Generic;

    using Codurance.Aggregates;
    using Codurance.Requests;
    using Codurance.ValueObject;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class ReadRequestTests : WithFakes
    {
        protected static ReadRequest subject;

        protected static string targetUsername, renderOutput;

        protected static IEnumerable<Post> timelinePosts;

        private Establish context = () =>
            {
                targetUsername = TestHelpers.RandomString();
                timelinePosts = TestHelpers.RandomPosts();

                renderOutput = TestHelpers.RandomString();

                The<ISocialNetwork>().WhenToldTo(o => o.GetTimeline(targetUsername)).Return(timelinePosts);
                The<IRenderingEngine>().WhenToldTo(o => o.RenderPosts(timelinePosts)).Return(renderOutput);

                subject = new ReadRequest(targetUsername);
            };
    }

    public class When_processing_a_read_request : ReadRequestTests
    {
        private static string result;

        private Because of = () => result = subject.Process(The<ISocialNetwork>(), The<IRenderingEngine>());

        private It should_query_the_aggregate_for_the_relevant_timeline = () => result.ShouldEqual(renderOutput);
    }
}
