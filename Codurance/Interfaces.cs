namespace Codurance
{
    using System;

    using Codurance.Aggregates;

    public interface IEvent
    {
    }

    public interface IRequest
    {
    }

    public interface IQuery
    {
        string Process(ISocialNetwork socialNetwork, IRenderingEngine renderingEngine);
    }

    public interface ICommand
    {
        void Process(ISocialNetwork socialNetwork, Func<DateTime> timestampProvider);
    }
}
