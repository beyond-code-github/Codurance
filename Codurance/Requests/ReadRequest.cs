namespace Codurance.Requests
{
    using Codurance.Aggregates;

    public class ReadRequest : IRequest
    {
        public ReadRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }

        public string Process(ISocialNetwork socialNetwork)
        {
            throw new System.NotImplementedException();
        }
    }
}
