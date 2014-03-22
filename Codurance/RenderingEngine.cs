namespace Codurance
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Codurance.ValueObjects;

    public interface IRenderingEngine
    {
        string RenderPosts(IEnumerable<Post> posts);
    }

    public class RenderingEngine : IRenderingEngine
    {
        private readonly Func<DateTime> timestampProvider;

        public RenderingEngine(Func<DateTime> timestampProvider)
        {
            this.timestampProvider = timestampProvider;
        }

        public string RenderPosts(IEnumerable<Post> posts)
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
                return string.Format("{0} second(s) ago", span.TotalSeconds);
            }

            if (span.TotalMinutes < 60)
            {
                return string.Format("{0} minute(s) ago", span.TotalMinutes);
            }

            if (span.TotalHours < 24)
            {
                return string.Format("{0} hour(s) ago", span.TotalHours);
            }

            return string.Format("{0} day(s) ago", span.TotalDays);
        }
    }
}
