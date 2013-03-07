namespace uSnapUs.Core.Model
{
	public class DeviceRegistration
	{
		[PrimaryKey,AutoIncrement]
		public int Id{get;set;}

		public string Name{get;set;}

		public string Email{get;set;}

		public string Guid{get;set;}

	    public int ServerId { get; set; }

	    public string FacebookId { get; set; }
	}
}

