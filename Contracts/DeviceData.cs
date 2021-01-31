using System;
using System.Collections.Generic;

namespace DeviceDataApi.Contracts
{
	public class DeviceData
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public int CompanyId { get; set; }

		public string CompanyName { get; set; }

		public DateTime StartDate { get; set; }

		public IList<Measurement> Measurements { get; set; }
	}

	public class Measurement
	{
		public MeasurementType Type { get; set; }

		public DateTime Date { get; set; }

		public double Value { get; set; }
	}

	public enum MeasurementType
	{
		Unknown,
		Temperature,
		Humidity
	}
}
