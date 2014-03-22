namespace Codurance.Tests
{
    using System;

    public static class TestHelpers
    {
        public static string RandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static DateTime RandomDateTime()
        {
            var start = new DateTime(1995, 1, 1);
            var gen = new Random();
            var range = ((DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
