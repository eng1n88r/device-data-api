using System;
using System.Collections.Generic;

using DeviceDataApi.Contracts;
using DeviceDataApi.DataProcessors.Interfaces;

namespace DeviceDataApi.DataProcessors
{
	public class DeviceTypeBDataProcessor : IDeviceDataProcessor<DeviceTypeBData>
	{
		public IEnumerable<DeviceData> ProcessDeviceData(DeviceTypeBData data)
		{
			if (data == null)
			{
				return null;
			}

			var result = new List<DeviceData>();

			foreach (var item in data.Devices)
			{
				var deviceData = new DeviceData
				{
					CompanyId = data.CompanyId,
					CompanyName = data.Company,
					Id = item.DeviceID,
					Name = item.Name,
					StartDate = DateTime.Parse(item.StartDateTime),
				};

				var measurements = new List<Measurement>();

				foreach (var stat in item.SensorData)
				{
					var type = IdentifyMeasurementType(stat.SensorType);

					var measurement = new Measurement
					{
						Type = type,
						Value = stat.Value,
						Date = DateTime.Parse(stat.DateTime)
					};

					measurements.Add(measurement);
				}

				deviceData.Measurements = measurements;

				result.Add(deviceData);
			}

			return result;
		}

		public MeasurementType IdentifyMeasurementType(string measurement)
		{
			switch (measurement)
			{
				case "TEMP":
					return MeasurementType.Temperature;
				case "HUM":
					return MeasurementType.Humidity;
				default:
					return MeasurementType.Unknown;
			}
		}
	}
}
