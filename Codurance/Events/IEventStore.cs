namespace Codurance.Events
{
    public interface IEventStore
    {
        void Publish(IEvent postEvent);
    }
}
