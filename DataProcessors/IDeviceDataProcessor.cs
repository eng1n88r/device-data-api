using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	interface IDeviceDataProcessor<T>
	{
		DeviceData ProcessDeviceData(T data);

		MeasurementType IdentifyMeasurementType(string measurement);
	}
}
