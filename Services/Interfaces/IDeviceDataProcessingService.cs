using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceDataApi.Contracts;

namespace DeviceDataApi.Services.Interfaces
{
	public interface IDeviceDataProcessingService
	{
		// Option 1 for generic processing
		Task<Result> ProcessDeviceData(object data);

		// Option 2 for concrete processing
		Task<Result> ProcessDeviceTypeAData(DeviceTypeA data);
		Task<Result> ProcessDeviceTypeBData(DeviceTypeB data);

		Task<Result<IEnumerable<DeviceData>>> GetDataForDashboard();

		Task<Result> ClearDataStorage();
	}
}
