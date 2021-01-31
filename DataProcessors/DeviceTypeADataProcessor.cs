using System;
using System.Collections.Generic;
using System.Linq;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	public class DeviceTypeADataProcessor : DeviceAProcessor
	{
		public override IList<DeviceData> ProcessDeviceData(DeviceTypeAOutput data)
		{
			if (data == null)
			{
				return null;
			}

			var result = new List<DeviceData>();

			foreach (var tracker in data.Trackers)
			{
				var deviceData = new DeviceData
				{
					CompanyId = data.PartnerId,
					CompanyName = data.PartnerName,
					Id = tracker.Id,
					Name = tracker.Model,
					StartDate = DateTime.Parse(tracker.ShipmentStartDtm),
				};
				
				var values = new List<Measurement>();

				foreach (var stat in tracker.Sensors)
				{
					var type = IdentifyMeasurementType(stat.Name);

					var measurements = stat.Crumbs.Select(x => new Measurement
					{
						Type = type,
						Date = DateTime.Parse(x.CreatedDtm),
						Value = x.Value
					}).ToList();

					values.AddRange(measurements);
				}

				deviceData.Measurements = values;

				result.Add(deviceData);
			}

			return result;
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
