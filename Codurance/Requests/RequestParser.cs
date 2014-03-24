namespace Codurance.Requests
{
    using System;

    public interface IRequestParser
    {
        IRequest Parse(string input);
    }

    public class RequestParser : IRequestParser
    {
        public IRequest Parse(string input)
        {
            var parts = input.Split(' ');

            // Peter
            if (IsReadRequest(parts))
            {
                return new ReadRequest(parts[0]);
            }

            // Peter wall
            if (IsWallRequest(parts))
            {
                return new WallRequest(parts[0]);
            }

            // Peter -> Message
            if (IsPostRequest(parts))
            {
                var postParts = input.Split(new [] { "->" }, StringSplitOptions.None);
                return new PostRequest(postParts[1].Trim(), postParts[0].Trim());
            }

            // Kathryn follows Peter
            return new FollowRequest(parts[0], parts[2]);
        }

        private static bool IsPostRequest(string[] parts)
        {
            return parts[1] == "->";
        }

        private static bool IsWallRequest(string[] parts)
        {
            return parts.Length == 2 && parts[1] == "wall";
        }

        private static bool IsReadRequest(string[] parts)
        {
            return parts.Length == 1;
        }
    }
}
