using System;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GGDev_Launcher.Epic
{
	public class Exchange
	{
		[JsonPropertyName("expiresInSeconds")]
		public int ExpiresInSeconds { get; set; }

		[JsonPropertyName("code")]
		public string Code { get; set; }

		[JsonPropertyName("creatingClientId")]
		public string CreatingClientId { get; set; }
	}
}
