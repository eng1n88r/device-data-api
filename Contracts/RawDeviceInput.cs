using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeviceDataApi.Contracts
{
	public class RawDeviceInput
	{
		
		public int CompanyId { get; set; }


	}

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	

	public class Root1
	{
	
	}

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	//public class Crumb
	//{
	//	public string CreatedDtm { get; set; }
	//	public double Value { get; set; }
	//}

	//public class Sensor
	//{
	//	public int Id { get; set; }
	//	public string Name { get; set; }
	//	public List<Crumb> Crumbs { get; set; }
	//}

	//public class Tracker
	//{
	//	public int Id { get; set; }
	//	public string Model { get; set; }
	//	public string ShipmentStartDtm { get; set; }
	//	public List<Sensor> Sensors { get; set; }
	//}

	//public class Root2
	//{
	//	public int PartnerId { get; set; }
	//	public string PartnerName { get; set; }
	//	public List<Tracker> Trackers { get; set; }
	//}

}
