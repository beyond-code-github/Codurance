namespace Codurance
{
    using System;

    using Codurance.Aggregates;

    public interface ICommand
    {
        void Process(ISocialNetwork socialNetwork, Func<DateTime> timestampProvider);
    }
}
