﻿namespace Codurance.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.Events;
    using Codurance.Tests.Unit.Events;
    using Codurance.ViewModels;

    public static class TestHelpers
    {
        public static Random gen = new Random();

        public static string RandomString()
        {
            return string.Format("{0}", Guid.NewGuid());
        }

        public static string RandomStringWithSpaces()
        {
            return string.Format("{0} {0}", Guid.NewGuid());
        }

        public static int RandomInt(int min, int max)
        {
            return gen.Next(min, max);
        }

        public static DateTime RandomDateTime()
        {
            var start = new DateTime(1995, 1, 1);
            var range = ((DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        }

        public static IEnumerable<FollowEvent> RandomFollowEvents(string issuingUsername)
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new FollowEvent(issuingUsername, RandomString(), RandomDateTime()))
                    .ToList();
        }

        public static IEnumerable<PostEvent> RandomPostEvents()
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new PostEvent(RandomString(), RandomString(), RandomDateTime()))
                    .ToList();
        }

        public static IEnumerable<PostEvent> RandomPostEvents(string issuingUsername)
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new PostEvent(RandomString(), issuingUsername, RandomDateTime()))
                    .ToList();
        }

        public static IEnumerable<IEvent> RandomDummyEvents()
        {
            return Enumerable.Range(0, gen.Next(1, 5)).Select(o => new DummyEvent()).ToList();
        }

        public static IEnumerable<PostViewModel> RandomPosts()
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new PostViewModel(RandomString(), RandomString(), RandomDateTime()))
                    .ToList();
        }

        public static IEnumerable<PostViewModel> RandomPosts(DateTime timestamp)
        {
            return
                Enumerable.Range(0, gen.Next(1, 5))
                    .Select(o => new PostViewModel(RandomString(), RandomString(), timestamp))
                    .ToList();
        }
    }
}
