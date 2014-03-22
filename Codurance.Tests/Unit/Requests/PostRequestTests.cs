namespace Codurance.Tests.Unit.Requests
{
    using System;
    using System.Runtime.Remoting.Messaging;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Requests;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class PostRequestTests : WithFakes
    {
        protected static PostRequest subject;

        protected static Func<DateTime> timestampProvider;
        
        protected static string message, issuingUserName;
        
        protected static DateTime timestamp;

        private Establish context = () =>
            {
                message = Guid.NewGuid().ToString();
                issuingUserName = Guid.NewGuid().ToString();

                timestamp = TestHelpers.RandomDateTime();
                timestampProvider = () => timestamp;

                subject = new PostRequest(message, issuingUserName);
            };
    }

    public class When_processing_a_post_request : PostRequestTests
    {
        private Because of = () => subject.Process(The<ISocialNetwork>(), timestampProvider);

        private It should_dispatch_a_post_event = () => The<ISocialNetwork>().WasToldTo(o => 
             o.Handle(new PostEvent(subject.Message, subject.IssuingUsername, timestampProvider())));
    }
}
