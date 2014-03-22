namespace Codurance.Requests
{
    using System;

    using Codurance.Aggregates;

    public class FollowRequest : IRequest, ICommand
    {
        public FollowRequest(string issuingUsername, string targetUsername)
        {
            this.IssuingUsername = issuingUsername;
            this.TargetUsername = targetUsername;
        }

        public string IssuingUsername { get; private set; }

        public string TargetUsername { get; private set; }

        public void Process(ISocialNetwork socialNetwork, Func<DateTime> timestampProvider)
        {
            throw new NotImplementedException();
        }
    }
}
