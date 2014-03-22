namespace Codurance.Requests
{
    using Codurance.Aggregates;

    public class ReadRequest : IRequest, IQuery
    {
        public ReadRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }

        public string Process(ISocialNetwork socialNetwork, IRenderingEngine renderingEngine)
        {
            return renderingEngine.RenderPosts(socialNetwork.GetTimeline(this.TargetUsername));
        }
    }
}
