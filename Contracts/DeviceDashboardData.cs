using System;

namespace DeviceDataApi.Contracts
{
	public class DeviceDashboardData
	{
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public int? TrackerId { get; set; }
		public string TrackerName { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? FirstCrumbDtm { get; set; }
		public DateTime? LastCrumbDtm { get; set; }
		public int? TempCount { get; set; }
		public double? AvgTemp { get; set; }
		public int? HumidityCount { get; set; }
		public double? AvgHumidity { get; set; }
	}
}
