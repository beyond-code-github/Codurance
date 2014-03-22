namespace Codurance.Events
{
    using System;

    public class PostEvent : IEvent, IEquatable<PostEvent>
    {
        public PostEvent(string message, string issuingUsername, DateTime timestamp)
        {
            this.Message = message;
            this.IssuingUsername = issuingUsername;
            this.Timestamp = timestamp;
        }

        public string Message { get; private set; }

        public string IssuingUsername { get; private set; }

        public DateTime Timestamp { get; private set; }

        public bool Equals(PostEvent other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(this.Message, other.Message) && string.Equals(this.IssuingUsername, other.IssuingUsername) && this.Timestamp.Equals(other.Timestamp);
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
            return Equals((PostEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.Message != null ? this.Message.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IssuingUsername != null ? this.IssuingUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Timestamp.GetHashCode();
                return hashCode;
            }
        }
    }
}
