﻿namespace Codurance.Entities
{
    using System;

    public class User : IEquatable<User>
    {
        public string Username { get; set; }

        public string Timeline { get; set; }

        public string Wall { get; set; }

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