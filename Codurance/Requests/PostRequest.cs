namespace Codurance.Requests
{
    public class PostRequest : IRequest
    {
        public PostRequest(string message, string issuingUsername)
        {
            this.Message = message;
            this.IssuingUsername = issuingUsername;
        }

        public string Message { get; private set; }

        public string IssuingUsername { get; private set; }
    }
}
