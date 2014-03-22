namespace Codurance
{
    using Codurance.Aggregates;

    public interface IQuery
    {
        string Process(ISocialNetwork socialNetwork);
    }
}
