﻿namespace Codurance.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Codurance.ViewModels;

    using Machine.Specifications;

    public abstract class RenderingEngineTests
    {
        protected static RenderingEngine subject;

        protected static string result, username, message;

        protected static DateTime snapshot;

        protected static PostViewModel PostViewModel;

        private Establish context = () =>
            {
                snapshot = TestHelpers.RandomDateTime();
                username = TestHelpers.RandomString();
                message = TestHelpers.RandomString();

                subject = new RenderingEngine(() => snapshot);
            };
    }

    public class When_rendering_a_wall_post_made_seconds_ago : RenderingEngineTests
    {
        private static int seconds;

        private Establish context = () =>
            {
                seconds = TestHelpers.RandomInt(1, 59);
                PostViewModel = new PostViewModel(username, message, snapshot.AddSeconds(-seconds - 0.01));
            };

        private Because of = () => result = subject.RenderWallPosts(new[] { PostViewModel });

        private It should_render_showing_age_in_terms_of_whole_seconds =
            () => result.ShouldEqual(string.Format("{0} - {1} ({2} second(s) ago)\r\n", PostViewModel.Username, PostViewModel.Message, seconds));
    }

    public class When_rendering_a_wall_post_made_minutes_ago : RenderingEngineTests
    {
        private static int minutes;

        private Establish context = () =>
        {
            minutes = TestHelpers.RandomInt(1, 59);
            PostViewModel = new PostViewModel(username, message, snapshot.AddMinutes(-minutes - 0.01));
        };

        private Because of = () => result = subject.RenderWallPosts(new[] { PostViewModel });

        private It should_render_showing_age_in_terms_of_whole_minutes =
            () => result.ShouldEqual(string.Format("{0} - {1} ({2} minute(s) ago)\r\n", PostViewModel.Username, PostViewModel.Message, minutes));
    }

    public class When_rendering_a_wall_post_made_hours_ago : RenderingEngineTests
    {
        private static int hours;

        private Establish context = () =>
        {
            hours = TestHelpers.RandomInt(1, 23);
            PostViewModel = new PostViewModel(username, message, snapshot.AddHours(-hours - 0.01));
        };

        private Because of = () => result = subject.RenderWallPosts(new[] { PostViewModel });

        private It should_render_showing_age_in_terms_of_whole_hours =
            () => result.ShouldEqual(string.Format("{0} - {1} ({2} hour(s) ago)\r\n", PostViewModel.Username, PostViewModel.Message, hours));
    }

    public class When_rendering_a_wall_post_made_days_ago : RenderingEngineTests
    {
        private static int days;

        private Establish context = () =>
        {
            days = TestHelpers.RandomInt(1, 59);
            PostViewModel = new PostViewModel(username, message, snapshot.AddDays(-days - 0.01));
        };

        private Because of = () => result = subject.RenderWallPosts(new[] { PostViewModel });

        private It should_render_showing_age_in_terms_of_whole_days =
            () => result.ShouldEqual(string.Format("{0} - {1} ({2} day(s) ago)\r\n", PostViewModel.Username, PostViewModel.Message, days));
    }

    public class When_rendering_a_timeline_post_made_seconds_ago : RenderingEngineTests
    {
        private static int seconds;

        private Establish context = () =>
        {
            seconds = TestHelpers.RandomInt(1, 59);
            PostViewModel = new PostViewModel(username, message, snapshot.AddSeconds(-seconds - 0.01));
        };

        private Because of = () => result = subject.RenderTimelinePosts(new[] { PostViewModel });

        private It should_render_showing_age_in_terms_of_whole_seconds =
            () => result.ShouldEqual(string.Format("{0} ({1} second(s) ago)\r\n", PostViewModel.Message, seconds));
    }

    public class When_rendering_multiple_wall_posts : RenderingEngineTests
    {
        private static int days;

        private static IEnumerable<PostViewModel> posts;

        private Establish context = () =>
        {
            days = TestHelpers.RandomInt(1, 59);
            posts = TestHelpers.RandomPosts(snapshot.AddDays(-days));
        };

        private Because of = () => result = subject.RenderWallPosts(posts);

        private It should_render_the_correct_number_of_line_breaks =
            () => result.Split('\n').Count().ShouldEqual(posts.Count() + 1);
    }
}
