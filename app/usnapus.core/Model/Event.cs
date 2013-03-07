using System;
using uSnapUs.Core.Helpers;

namespace uSnapUs.Core.Model
{
    public class Event
    {
        [Indexed]
        public string Code {
            get;
            set;
        }
        
        public double Latitude{
            get;set;
        }
        public double Longitude{
            get;set;
        }
        public string Location {
            get;
            set;
        }

        public string Name{
            get;set;
        }
        public DateTime Starts{
            get;set;
        }

        public DateTime Ends{
            get;set;
        }

        [Indexed,PrimaryKey,AutoIncrement]
        public int EventId { get; set; }
        public int Id{
            get;set;
        }

        public Coordinate Coordinate { get { return new Coordinate {Latitude = Latitude, Longitude = Longitude}; }}



        public override string ToString()
        {
            return string.Format("Code: {0}, Latitude: {1}, Longitude: {2}, Location: {3}, Name: {4}, Starts: {5}, Ends: {6}, Id: {7}", Code, Latitude, Longitude, Location, Name, Starts, Ends, Id);
        }

        bool Equals(Event other)
        {
            return string.Equals(Code, other.Code) && Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude) && string.Equals(Location, other.Location) && string.Equals(Name, other.Name) && Starts.Equals(other.Starts) && Ends.Equals(other.Ends) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Event) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode*397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Starts.GetHashCode();
                hashCode = (hashCode*397) ^ Ends.GetHashCode();
                hashCode = (hashCode*397) ^ Id;
                return hashCode;
            }
        }
    }
}

