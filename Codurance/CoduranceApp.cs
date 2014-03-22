namespace Codurance
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Requests;

    public class CoduranceApp
    {
        private readonly ISocialNetwork socialNetwork;

        private readonly IRequestParser requestParser;

        private readonly Func<DateTime> timestampProvider;

        private readonly IRenderingEngine renderingEngine;

        public CoduranceApp() : this(new Bootstrapper()) { }

        public CoduranceApp(Bootstrapper bootstrapper)
        {
            this.socialNetwork = bootstrapper.SocialNetwork();
            this.requestParser = bootstrapper.RequestParser();
            this.timestampProvider = bootstrapper.TimestampProvider;
            this.renderingEngine = bootstrapper.RenderingEngine();
        }
        
        public string Process(string input)
        {
            var request = this.requestParser.Parse(input);

            var query = request as IQuery;
            if (query != null)
            {
                return query.Process(this.socialNetwork, this.renderingEngine);
            }

            var command = request as ICommand;
            if (command != null)
            {
                command.Process(this.socialNetwork, this.timestampProvider);
            }

            return string.Empty;
        }
    }
}