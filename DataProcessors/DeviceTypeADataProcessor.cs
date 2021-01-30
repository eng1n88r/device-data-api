using System;
using System.Collections.Generic;
using System.Linq;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	public class DeviceTypeADataProcessor : DeviceAProcessor
	{
		public override DeviceData ProcessDeviceData(DeviceTypeA data)
		{
			if (data == null)
			{
				return null;
			}

			var deviceData = new DeviceData
			{
				CompanyId = data.PartnerId,
				CompanyName = data.PartnerName,
				Devices = new List<MeasurementDevice>()
			};

			foreach (var tracker in data.Trackers)
			{
				var device = new MeasurementDevice
				{
					Id = tracker.Id,
					Name = tracker.Model,
					StartDate = DateTime.Parse(tracker.ShipmentStartDtm),
				};

				deviceData.Devices.Add(device);

				foreach (var stat in tracker.Sensors)
				{
					var type = IdentifyMeasurementType(stat.Name);

					var measurements = stat.Crumbs.Select(x => new Measurement
					{
						Type = type,
						Date = DateTime.Parse(x.CreatedDtm),
						Value = x.Value
					}).ToList();

					device.Measurements = measurements;
				}
			}

			return deviceData;
		}

		public override MeasurementType IdentifyMeasurementType(string measurement)
		{
			switch (measurement)
			{
				case "Temperature":
					return MeasurementType.Temperature;
				case "Humidty":
					return MeasurementType.Humidity;
				default:
					return MeasurementType.Unknown;
			}
		}
	}
}
