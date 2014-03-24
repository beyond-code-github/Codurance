namespace Codurance.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Events;

    public interface IViewModelProvider
    {
        IEnumerable<PostViewModel> GetTimelineForUser(string username);

        IEnumerable<PostViewModel> GetWallForUser(string username);
    }

    public class ViewModelProvider : IViewModelProvider
    {
        private readonly IEventStore eventStore;

        public ViewModelProvider(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public IEnumerable<PostViewModel> GetTimelineForUser(string username)
        {
            return
                this.eventStore.GetPostEvents(username).Select(o => new PostViewModel(o.IssuingUsername, o.Message, o.Timestamp));
        }

        public IEnumerable<PostViewModel> GetWallForUser(string username)
        {
            var wallUserNames = this.eventStore.GetFollowEvents(username).Select(o => o.TargetUsername).ToList();
            wallUserNames.Add(username);
            
            return
                this.eventStore.GetPostEvents(wallUserNames)
                    .Select(o => new PostViewModel(o.IssuingUsername, o.Message, o.Timestamp));
        }
    }
}
