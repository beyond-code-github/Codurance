namespace Codurance
{
    using System;

    using Codurance.Aggregates;
    using Codurance.Requests;

    class Program
    {
        static void Main(string[] args)
        {
            var app = new CoduranceApp();

            while (true)
            {
                var input = Console.ReadLine();
                Console.Write(app.Process(input));
            }
        }
    }

    public class CoduranceApp
    {
        private readonly ISocialNetwork socialNetwork;

        private readonly IRequestParser requestParser;

        private readonly Func<DateTime> timestampProvider;

        private readonly IRenderingEngine renderingEngine;

        public CoduranceApp() : this(new Bootstrapper()) { }

        public CoduranceApp(Bootstrapper bootstrapper)
        {
            socialNetwork = bootstrapper.SocialNetwork();
            requestParser = bootstrapper.RequestParser();
            timestampProvider = bootstrapper.TimestampProvider;
            renderingEngine = bootstrapper.RenderingEngine();
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
