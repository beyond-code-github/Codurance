namespace Codurance
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Events;
    using Codurance.Repositories;
    using Codurance.Requests;

    public class Bootstrapper
    {
        public Bootstrapper()
        {
            this.TimestampProvider = () => DateTime.Now;

            var eventStore = new InMemoryEventStore();
            this.EventStore = () => eventStore;

            this.UsersRepository = () => new UsersRepository(this.EventStore());

            this.SocialNetwork = () => new SocialNetwork(this.EventStore(), this.UsersRepository());

            this.RequestParser = () => new RequestParser();

            this.RenderingEngine = () => new RenderingEngine(this.TimestampProvider);
        }

        public Func<IRequestParser> RequestParser { get; set; }

        public Func<DateTime> TimestampProvider { get; set; }
        
        public Func<IEventStore> EventStore { get; set; }

        public Func<IUsersRepository> UsersRepository { get; set; }
        
        public Func<ISocialNetwork> SocialNetwork { get; set; }

        public Func<IRenderingEngine> RenderingEngine { get; set; }
    }
}
