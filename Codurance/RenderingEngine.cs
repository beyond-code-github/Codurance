namespace Codurance
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Codurance.ViewModels;

    public interface IRenderingEngine
    {
        string RenderWallPosts(IEnumerable<PostViewModel> posts);

        string RenderTimelinePosts(IEnumerable<PostViewModel> posts);
    }

    public class RenderingEngine : IRenderingEngine
    {
        private readonly Func<DateTime> timestampProvider;

        public RenderingEngine(Func<DateTime> timestampProvider)
        {
            this.timestampProvider = timestampProvider;
        }

        public string RenderTimelinePosts(IEnumerable<PostViewModel> posts)
        {
            var now = timestampProvider();
            var builder = new StringBuilder();

            foreach (var post in posts)
            {
                builder.AppendLine(
                    string.Format("{0} ({1})", post.Message, this.PostAge(now, post.Timestamp)));
            }

            return builder.ToString();
        }

        public string RenderWallPosts(IEnumerable<PostViewModel> posts)
        {
            var now = timestampProvider();
            var builder = new StringBuilder();
            
            foreach (var post in posts)
            {
                builder.AppendLine(
                    string.Format("{0} - {1} ({2})", post.Username, post.Message, this.PostAge(now, post.Timestamp)));
            }

            return builder.ToString();
        }

        private string PostAge(DateTime now, DateTime posted)
        {
            var span = now.Subtract(posted);

            if (span.TotalSeconds < 60)
            {
                return string.Format("{0} second(s) ago", Math.Floor(span.TotalSeconds));
            }

            if (span.TotalMinutes < 60)
            {
                return string.Format("{0} minute(s) ago", Math.Floor(span.TotalMinutes));
            }

            if (span.TotalHours < 24)
            {
                return string.Format("{0} hour(s) ago", Math.Floor(span.TotalHours));
            }

            return string.Format("{0} day(s) ago", Math.Floor(span.TotalDays));
        }
    }
}
