namespace Codurance
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var app = new CoduranceApp();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                Console.Write(app.Process(input));
            }
        }
    }
}
