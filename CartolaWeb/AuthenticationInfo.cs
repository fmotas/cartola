using Newtonsoft.Json;

namespace CartolaWeb
{
	public class AuthenticationInfo
	{
		public string id { get; set; }
		public string userMessage { get; set; }
		public string glbId { get; set; }
	}

	public class AuthenticationPayload
	{
		[JsonProperty("payload")]
		public Payload payload { get; set; }
	}

	public class Payload
	{
		[JsonProperty("email")]
		public string email { get; set; }
		[JsonProperty("password")]
		public string password { get; set; }
		[JsonProperty("serviceId")]
		public int serviceId { get; set; }
	}

}
