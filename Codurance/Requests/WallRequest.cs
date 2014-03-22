namespace Codurance.Requests
{
    using Codurance.Aggregates;

    public class WallRequest : IRequest
    {
        public WallRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }

        public string Process(ISocialNetwork socialNetwork)
        {
            return socialNetwork.GetWall(this.TargetUsername);
        }
    }
}
