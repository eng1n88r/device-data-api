﻿using System;

namespace DeviceDataApi.Contracts
{
	public class DeviceData
	{
		public int CompanyId { get; set; } // Foo1: PartnerId, Foo2: CompanyId
		public string CompanyName { get; set; } // Foo1: PartnerName, Foo2: Company
		public int? TrackerId { get; set; } // Foo1: Id, Foo2: DeviceID
		public string TrackerName { get; set; } // Foo1: Model, Foo2: Name
		public DateTime? StartDate { get; set; } // Foo1: ShipmentStartDtm, Foo2: StartDateTime
		public DateTime? FirstCrumbDtm { get; set; }  // Foo1: Trackers.Sensors.Crumbs, Foo2: Devices.SensorData
		public DateTime? LastCrumbDtm { get; set; }
		public int? TempCount { get; set; }
		public double? AvgTemp { get; set; }
		public int? HumidityCount { get; set; }
		public double? AvgHumidity { get; set; }
	}
}
