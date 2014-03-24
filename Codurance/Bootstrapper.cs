namespace Codurance
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Requests;
    using Codurance.ViewModels;

    public class Bootstrapper
    {
        public Bootstrapper()
        {
            var eventStore = new InMemoryEventStore();
            
            this.EventStore = () => eventStore;
            this.TimestampProvider = () => DateTime.Now;
            this.RequestParser = () => new RequestParser();
            this.ViewModelProvider = () => new ViewModelProvider(this.EventStore());
            this.SocialNetwork = () => new SocialNetwork(this.EventStore());
            this.RenderingEngine = () => new RenderingEngine(this.TimestampProvider);
        }

        public Func<DateTime> TimestampProvider { get; set; }

        public Func<IRequestParser> RequestParser { get; set; }

        public Func<IEventStore> EventStore { get; set; }

        public Func<IViewModelProvider> ViewModelProvider { get; set; }
        
        public Func<ISocialNetwork> SocialNetwork { get; set; }

        public Func<IRenderingEngine> RenderingEngine { get; set; }
    }
}
