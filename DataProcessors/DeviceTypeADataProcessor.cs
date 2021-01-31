using System;
using System.Collections.Generic;
using System.Linq;

using DeviceDataApi.Contracts;
using DeviceDataApi.DataProcessors.Interfaces;

namespace DeviceDataApi.DataProcessors
{
	public class DeviceTypeADataProcessor : IDeviceDataProcessor<DeviceTypeAData>
	{
		public IEnumerable<DeviceData> ProcessDeviceData(DeviceTypeAData data)
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

		public MeasurementType IdentifyMeasurementType(string measurement)
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
