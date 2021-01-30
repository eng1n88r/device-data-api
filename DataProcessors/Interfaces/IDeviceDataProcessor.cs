using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors.Interfaces
{
	interface IDeviceDataProcessor<T>
	{
		DeviceData ProcessDeviceData(T data);

		MeasurementType IdentifyMeasurementType(string measurement);
	}
}
