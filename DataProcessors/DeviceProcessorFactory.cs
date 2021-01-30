using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	public abstract class DeviceAProcessor
	{
		public abstract DeviceData ProcessDeviceData(DeviceTypeA data);

		public abstract MeasurementType IdentifyMeasurementType(string measurement);
	}

	public abstract class DeviceBProcessor
	{
		public abstract DeviceData ProcessDeviceData(DeviceTypeB data);

		public abstract MeasurementType IdentifyMeasurementType(string measurement);
	}

	public abstract class DeviceProcessorFactory
	{
		public abstract DeviceAProcessor CreateDeviceTypeAProcessor();
		public abstract DeviceBProcessor CreateDeviceTypeBProcessor();
	}

	public class DeviceDataProcessorFactory : DeviceProcessorFactory
	{
		public override DeviceAProcessor CreateDeviceTypeAProcessor()
		{
			return new DeviceTypeADataProcessor();
		}

		public override DeviceBProcessor CreateDeviceTypeBProcessor()
		{
			return new DeviceTypeBDataProcessor();
		}
	}

}
