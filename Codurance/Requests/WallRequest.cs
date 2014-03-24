namespace Codurance.Requests
{
    public class WallRequest : IRequest
    {
        public WallRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }
    }
}
