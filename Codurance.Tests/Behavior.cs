namespace Codurance.Tests
{
    using System.Text;

    using Machine.Specifications;

    public class Behavior
    {
        private static CoduranceApp app;

        private static StringBuilder builder;

        private static string[] inputs;

        private Establish context = () =>
            {
                builder = new StringBuilder();
                app = new CoduranceApp();

                inputs = new []
                    {
                        "Alice -> I love the weather today",
                        "Bob -> Damn! We lost!",
                        "Bob -> Good game though.",
                        "Alice",
                        "Bob",
                        "Charlie -> I'm in New York today! Anyone wants to have a coffee?",
                        "Charlie follows Alice",
                        "Charlie wall",
                        "Charlie follows Bob",
                        "Charlie wall",
                    };
            };

        private Because of = () =>
            {
                foreach (var input in inputs)
                {
                    builder.Append(app.Process(input));
                }
            };

        private It should_match_the_example = () => builder.ToString().Length.ShouldBeGreaterThan(0);
    }
}
