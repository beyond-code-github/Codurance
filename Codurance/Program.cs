namespace Codurance
{
    using System;

    using Codurance.Handlers;

    class Program
    {
        static void Main(string[] args)
        {
            var handler = InputHandlerFactory.Create();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                Console.Write(handler.Handle(input));
            }
        }
    }
}
