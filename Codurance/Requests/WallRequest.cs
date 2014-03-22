namespace Codurance.Requests
{
    using Codurance.Aggregates;

    public class WallRequest : IRequest, IQuery
    {
        public WallRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }

        public string Process(ISocialNetwork socialNetwork, IRenderingEngine renderingEngine)
        {
            return renderingEngine.RenderWallPosts(socialNetwork.GetWall(this.TargetUsername));
        }
    }
}
