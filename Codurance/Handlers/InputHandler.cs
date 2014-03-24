namespace Codurance.Handlers
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Requests;
    using Codurance.ViewModels;

    public class InputHandler
    {
        private readonly ISocialNetwork socialNetwork;

        private readonly IRequestParser requestParser;

        private readonly IRenderingEngine renderingEngine;

        private readonly Func<DateTime> timestampProvider;

        private readonly IViewModelProvider viewModelProvider;

        public InputHandler(ISocialNetwork socialNetwork, IRequestParser requestParser, IRenderingEngine renderingEngine, Func<DateTime> timestampProvider, IViewModelProvider viewModelProvider)
        {
            this.socialNetwork = socialNetwork;
            this.requestParser = requestParser;
            this.renderingEngine = renderingEngine;
            this.timestampProvider = timestampProvider;
            this.viewModelProvider = viewModelProvider;
        }

        public string Handle(string input)
        {
            var request = this.requestParser.Parse(input);

            var readRequest = request as ReadRequest;
            if (readRequest != null)
            {
                return
                    this.renderingEngine.RenderTimelinePosts(
                        this.viewModelProvider.GetTimelineForUser(readRequest.TargetUsername));
            }

            var wallRequest = request as WallRequest;
            if (wallRequest != null)
            {
                return
                    this.renderingEngine.RenderWallPosts(
                        this.viewModelProvider.GetWallForUser(wallRequest.TargetUsername));
            }

            var followRequest = request as FollowRequest;
            if (followRequest != null)
            {
                this.socialNetwork.Handle(
                    new FollowEvent(
                        followRequest.IssuingUsername,
                        followRequest.TargetUsername,
                        this.timestampProvider()));
            }

            var postRequest = request as PostRequest;
            if (postRequest != null)
            {
                this.socialNetwork.Handle(
                    new PostEvent(postRequest.Message, postRequest.IssuingUsername, this.timestampProvider()));
            }
            
            return string.Empty;
        }
    }
}