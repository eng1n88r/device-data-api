using System;
using System.Collections.Generic;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	public class DeviceTypeBDataProcessor : DeviceBProcessor
	{
		public override DeviceData ProcessDeviceData(DeviceTypeB data)
		{
			if (data == null)
			{
				return null;
			}

			var deviceData = new DeviceData
			{
				CompanyId = data.CompanyId,
				CompanyName = data.Company,
				Devices = new List<MeasurementDevice>()
			};

			foreach (var item in data.Devices)
			{
				var device = new MeasurementDevice
				{
					Id = item.DeviceID,
					Name = item.Name,
					StartDate = DateTime.Parse(item.StartDateTime),
				};

				deviceData.Devices.Add(device);

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

				device.Measurements = measurements;
			}

			return deviceData;
		}

		public override MeasurementType IdentifyMeasurementType(string measurement)
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
