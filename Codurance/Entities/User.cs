namespace Codurance.Entities
{
    using System;
    using System.Collections.Generic;

    using Codurance.ValueObjects;

    public class User : IEquatable<User>
    {
        public string Username { get; set; }

        public IEnumerable<Post> Timeline { get; set; }

        public IEnumerable<Post> Wall { get; set; }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(this.Username, other.Username);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return (this.Username != null ? this.Username.GetHashCode() : 0);
        }
    }
}
