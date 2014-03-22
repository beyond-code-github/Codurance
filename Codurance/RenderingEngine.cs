namespace Codurance
{
    using System;
    using System.Collections.Generic;

    using Codurance.ValueObject;

    public interface IRenderingEngine
    {
        string RenderPosts(IEnumerable<Post> posts);
    }

    public class RenderingEngine : IRenderingEngine
    {
        public string RenderPosts(IEnumerable<Post> posts)
        {
            throw new NotImplementedException();
        }
    }
}
