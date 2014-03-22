namespace Codurance.ValueObject
{
    using System;

    public class Post : IEquatable<Post>
    {
        public Post(string username, string message, DateTime timestamp)
        {
            this.Username = username;
            this.Message = message;
            this.Timestamp = timestamp;
        }

        public string Username { get; private set; }

        public string Message { get; private set; }

        public DateTime Timestamp { get; private set; }

        public bool Equals(Post other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(this.Username, other.Username) && string.Equals(this.Message, other.Message) && this.Timestamp.Equals(other.Timestamp);
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
            return Equals((Post)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Message != null ? this.Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Timestamp.GetHashCode();
                return hashCode;
            }
        }
    }
}
