namespace uSnapUs.Core.Helpers
{
	public struct Coordinate
	{
		public double Latitude{get;set;}
		public double Longitude{get;set;}
		public double Accuracy{get;set;}
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
		    if (obj.GetType () != typeof(Coordinate))
				return false;
			var other = (Coordinate)obj;
			return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
		}
		public override int GetHashCode ()
		{
			return Latitude.GetHashCode() ^ Longitude.GetHashCode();
		}

		public override string ToString ()
		{
			return string.Format ("[Coordinate: Latitude={0}, Longitude={1}]", Latitude, Longitude);
		}
		
		
	}
}

