namespace Codurance.Requests
{
    public interface IRequestParser
    {
        IRequest Parse(string input);
    }

    public class RequestParser : IRequestParser
    {
        public IRequest Parse(string input)
        {
            var parts = input.Split(' ');

            if (IsReadRequest(parts))
            {
                return new ReadRequest(parts[0]);
            }

            if (IsWallRequest(parts))
            {
                return new WallRequest(parts[0]);
            }

            if (IsPostRequest(parts))
            {
                return new PostRequest(parts[2], parts[0]);
            }

            return new FollowRequest(parts[0], parts[2]);
        }

        private static bool IsPostRequest(string[] parts)
        {
            return parts[1] == "->";
        }

        private static bool IsWallRequest(string[] parts)
        {
            return parts.Length == 2;
        }

        private static bool IsReadRequest(string[] parts)
        {
            return parts.Length == 1;
        }
    }
}
