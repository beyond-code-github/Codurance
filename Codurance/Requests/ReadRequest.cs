namespace Codurance.Requests
{
    public class ReadRequest : IRequest
    {
        public ReadRequest(string targetUsername)
        {
            this.TargetUsername = targetUsername;
        }

        public string TargetUsername { get; private set; }
    }
}
