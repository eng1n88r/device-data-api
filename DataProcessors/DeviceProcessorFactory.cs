using System.Collections.Generic;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.DataProcessors
{
	public abstract class DeviceAProcessor
	{
		public abstract IList<DeviceData> ProcessDeviceData(DeviceTypeAOutput data);

		public abstract MeasurementType IdentifyMeasurementType(string measurement);
	}

	public abstract class DeviceBProcessor
	{
		public abstract IList<DeviceData> ProcessDeviceData(DeviceTypeBOutput data);

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
