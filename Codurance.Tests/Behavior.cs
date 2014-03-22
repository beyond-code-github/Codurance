namespace Codurance.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Machine.Specifications;

    public class Behavior
    {
        private static CoduranceApp app;

        private static StringBuilder builder;

        private static dynamic [] inputs;

        protected static DateTime currentTime;

        private Establish context = () =>
            {
                builder = new StringBuilder();
                app = new CoduranceApp(new Bootstrapper { TimestampProvider = () => currentTime });

                var timestamp = TestHelpers.RandomDateTime();

                inputs = new [] {
                        new { Command = "Alice -> I love the weather today", Time = timestamp.AddMinutes(-5.9) },
                        new { Command = "Bob -> Damn! We lost!", Time = timestamp.AddMinutes(-2.9) },
                        new { Command = "Bob -> Good game though.", Time = timestamp.AddMinutes(-1.9) },
                        new { Command = "Alice", Time = timestamp.AddSeconds(-30) },
                        new { Command = "Bob", Time = timestamp.AddSeconds(-32) },
                        new { Command = "Charlie -> I'm in New York today! Anyone wants to have a coffee?", Time = timestamp.AddSeconds(-15) },
                        new { Command = "Charlie follows Alice", Time = timestamp.AddSeconds(-14) },
                        new { Command = "Charlie wall", Time = timestamp.AddSeconds(-13) },
                        new { Command = "Charlie follows Bob", Time = timestamp },
                        new { Command = "Charlie wall", Time = timestamp }
                    };
            };

        private Because of = () =>
            {
                foreach (var input in inputs)
                {
                    builder.AppendLine("> " + input.Command);
                    currentTime = input.Time;
                    builder.Append(app.Process(input.Command));
                }
            };

        private It should_match_the_example = () => builder.ToString().ShouldEqual(
@"> Alice -> I love the weather today
> Bob -> Damn! We lost!
> Bob -> Good game though.
> Alice
I love the weather today (5 minute(s) ago)
> Bob
Good game though. (1 minute(s) ago)
Damn! We lost! (2 minute(s) ago)
> Charlie -> I'm in New York today! Anyone wants to have a coffee?
> Charlie follows Alice
> Charlie wall
Charlie - I'm in New York today! Anyone wants to have a coffee? (2 second(s) ago)
Alice - I love the weather today (5 minute(s) ago)
> Charlie follows Bob
> Charlie wall
Charlie - I'm in New York today! Anyone wants to have a coffee? (15 second(s) ago)
Bob - Good game though. (1 minute(s) ago)
Bob - Damn! We lost! (2 minute(s) ago)
Alice - I love the weather today (5 minute(s) ago)
");
    }
}
