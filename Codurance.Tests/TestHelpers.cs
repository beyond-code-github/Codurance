namespace Codurance.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Events;

    public static class TestHelpers
    {
        public static Random gen = new Random();

        public static string RandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static DateTime RandomDateTime()
        {
            var start = new DateTime(1995, 1, 1);
            var range = ((DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        }

        public static IEnumerable<FollowEvent> RandomFollowEvents(string username)
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new FollowEvent(username, RandomString(), RandomDateTime()));
        }

        public static IEnumerable<PostEvent> RandomPostEvents()
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new PostEvent(RandomString(), RandomString(), RandomDateTime()));
        }
    }
}
