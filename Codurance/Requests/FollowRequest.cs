namespace Codurance.Requests
{
    public class FollowRequest : IRequest
    {
        public FollowRequest(string issuingUsername, string targetUsername)
        {
            this.IssuingUsername = issuingUsername;
            this.TargetUsername = targetUsername;
        }

        public string IssuingUsername { get; private set; }

        public string TargetUsername { get; private set; }
    }
}
