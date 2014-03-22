namespace Codurance.Tests.Unit.Requests
{
    using Codurance.Aggregates;
    using Codurance.Requests;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class ReadRequestTests : WithFakes
    {
        protected static ReadRequest subject;

        private static string targetUsername;

        protected static string timeline;

        private Establish context = () =>
            {
                targetUsername = TestHelpers.RandomString();
                timeline = TestHelpers.RandomString();
                
                The<ISocialNetwork>().WhenToldTo(o => o.GetTimeline(targetUsername)).Return(timeline);

                subject = new ReadRequest(targetUsername);
            };
    }


    public class When_processing_a_read_request : ReadRequestTests
    {
        private static string result;

        private Because of = () => result = subject.Process(The<ISocialNetwork>());

        private It should_query_the_aggregate_for_the_relevant_timeline = () => result.ShouldEqual(timeline);
    }
}
