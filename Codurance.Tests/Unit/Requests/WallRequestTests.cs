namespace Codurance.Tests.Unit.Requests
{
    using Codurance.Aggregates;
    using Codurance.Requests;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class WallRequestTests : WithFakes
    {
        protected static WallRequest subject;

        private static string targetUsername;

        protected static string wall;

        private Establish context = () =>
            {
                targetUsername = TestHelpers.RandomString();
                wall = TestHelpers.RandomString();

                The<ISocialNetwork>().WhenToldTo(o => o.GetWall(targetUsername)).Return(wall);

                subject = new WallRequest(targetUsername);
            };
    }


    public class When_processing_a_wall_request : WallRequestTests
    {
        private static string result;

        private Because of = () => result = subject.Process(The<ISocialNetwork>());

        private It should_query_the_aggregate_for_the_relevant_wall = () => result.ShouldEqual(wall);
    }
}
