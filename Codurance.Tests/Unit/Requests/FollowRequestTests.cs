namespace Codurance.Tests.Unit.Requests
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Requests;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class FollowRequestTests : WithFakes
    {
        protected static FollowRequest subject;

        protected static Func<DateTime> timestampProvider;

        protected static string issuingUserName, targetUsername;

        protected static DateTime timestamp;

        private Establish context = () =>
            {
                issuingUserName = TestHelpers.RandomString();
                targetUsername = TestHelpers.RandomString();

                timestamp = TestHelpers.RandomDateTime();
                timestampProvider = () => timestamp;

                subject = new FollowRequest(issuingUserName, targetUsername);
            };
    }

    public class When_processing_a_follow_request : FollowRequestTests
    {
        private Because of = () => subject.Process(The<ISocialNetwork>(), timestampProvider);

        private It should_dispatch_a_follow_event = () => The<ISocialNetwork>().WasToldTo(o =>
             o.Handle(new FollowEvent(subject.IssuingUsername, subject.TargetUsername, timestamp)));
    }
}
