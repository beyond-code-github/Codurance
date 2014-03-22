namespace Codurance.Tests.Unit.Requests
{
    using System.Collections.Generic;

    using Codurance.Aggregates;
    using Codurance.Requests;
    using Codurance.ValueObjects;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class WallRequestTests : WithFakes
    {
        protected static WallRequest subject;

        protected static string targetUsername, renderOutput;

        protected static IEnumerable<Post> wallPosts;

        private Establish context = () =>
            {
                targetUsername = TestHelpers.RandomString();
                wallPosts = TestHelpers.RandomPosts();

                renderOutput = TestHelpers.RandomString();

                The<ISocialNetwork>().WhenToldTo(o => o.GetWall(targetUsername)).Return(wallPosts);
                The<IRenderingEngine>().WhenToldTo(o => o.RenderWallPosts(wallPosts)).Return(renderOutput);

                subject = new WallRequest(targetUsername);
            };
    }


    public class When_processing_a_wall_request : WallRequestTests
    {
        private static string result;

        private Because of = () => result = subject.Process(The<ISocialNetwork>(), The<IRenderingEngine>());

        private It should_query_the_aggregate_for_the_relevant_wall = () => result.ShouldEqual(renderOutput);
    }
}
