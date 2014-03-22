namespace Codurance.Requests
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Events;

    public class PostRequest : IRequest, ICommand
    {
        public PostRequest(string message, string issuingUsername)
        {
            this.Message = message;
            this.IssuingUsername = issuingUsername;
        }

        public string Message { get; private set; }

        public string IssuingUsername { get; private set; }

        public void Process(ISocialNetwork socialNetwork, Func<DateTime> timestampProvider)
        {
            socialNetwork.Handle(new PostEvent(this.Message, this.IssuingUsername, timestampProvider()));
        }
    }
}
