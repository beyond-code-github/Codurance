namespace Codurance.Tests.Unit.Requests
{
    using Codurance.Requests;

    using Machine.Fakes;
    using Machine.Specifications;

    public abstract class RequestParserTests : WithSubject<RequestParser> { }

    public class When_parsing_a_post_request : RequestParserTests
    {
        private static PostRequest request;

        private static string issuingUsername, message;

        private Establish context = () =>
            {
                issuingUsername = TestHelpers.RandomString();
                message = TestHelpers.RandomString();
            };

        private Because of = () => request = (PostRequest)Subject.Parse(string.Format("{0} -> {1}", issuingUsername, message));
        
        private It should_set_the_issuing_username_property = () => request.IssuingUsername.ShouldEqual(issuingUsername);

        private It should_set_the_message_property = () => request.Message.ShouldEqual(message);
    }

    public class When_parsing_a_read_request : RequestParserTests
    {
        private static ReadRequest request;

        private static string issuingUsername;

        private Establish context = () =>
        {
            issuingUsername = TestHelpers.RandomString();
        };

        private Because of = () => request = (ReadRequest)Subject.Parse(string.Format("{0}", issuingUsername));
        
        private It should_set_the_target_username_property = () => request.TargetUsername.ShouldEqual(issuingUsername);
    }

    public class When_parsing_a_follow_request : RequestParserTests
    {
        private static FollowRequest request;

        private static string issuingUsername, targetUsername;

        private Establish context = () =>
        {
            issuingUsername = TestHelpers.RandomString();
            targetUsername = TestHelpers.RandomString();
        };

        private Because of = () => request = (FollowRequest)Subject.Parse(string.Format("{0} follows {1}", issuingUsername, targetUsername));
        
        private It should_set_the_issuing_username_property = () => request.IssuingUsername.ShouldEqual(issuingUsername);

        private It should_set_the_target_username_property = () => request.TargetUsername.ShouldEqual(targetUsername);
    }

    public class When_parsing_a_wall_request : RequestParserTests
    {
        private static WallRequest request;

        private static string targetUsername;

        private Establish context = () =>
        {
            targetUsername = TestHelpers.RandomString();
        };

        private Because of = () => request = (WallRequest)Subject.Parse(string.Format("{0} wall", targetUsername));
        
        private It should_set_the_target_username_property = () => request.TargetUsername.ShouldEqual(targetUsername);
    }
}
