using System.Collections.Generic;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors.Interfaces
{
	public interface IDeviceDataProcessor<in T> where T: class
	{
		IEnumerable<DeviceData> ProcessDeviceData(T data);

		MeasurementType IdentifyMeasurementType(string measurement);
	}
}
