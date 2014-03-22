namespace Codurance.Requests
{
    public class RequestParser
    {
        public IRequest Parse(string input)
        {
            var parts = input.Split(' ');

            if (parts.Length == 1)
            {
                return new ReadRequest(parts[0]);
            }

            if (parts.Length == 2)
            {
                return new WallRequest(parts[0]);
            }

            if (parts[1] == "->")
            {
                return new PostRequest(parts[2], parts[0]);
            }

            return new FollowRequest(parts[0], parts[2]);
        }
    }
}
