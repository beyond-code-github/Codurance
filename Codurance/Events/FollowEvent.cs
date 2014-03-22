namespace Codurance.Events
{
    using System;

    public class FollowEvent : IEquatable<FollowEvent>
    {
        public FollowEvent(string issuingUsername, string targetUsername, DateTime timestamp)
        {
            this.IssuingUsername = issuingUsername;
            this.TargetUsername = targetUsername;
            this.Timestamp = timestamp;
        }

        public string IssuingUsername { get; private set; }

        public string TargetUsername { get; private set; }

        public DateTime Timestamp { get; private set; }

        public bool Equals(FollowEvent other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(this.IssuingUsername, other.IssuingUsername) && string.Equals(this.TargetUsername, other.TargetUsername) && this.Timestamp.Equals(other.Timestamp);
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
            return Equals((FollowEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.IssuingUsername != null ? this.IssuingUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TargetUsername != null ? this.TargetUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Timestamp.GetHashCode();
                return hashCode;
            }
        }
    }
}
