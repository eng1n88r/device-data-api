using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceDataApi.Contracts;

namespace DeviceDataApi.Services.Interfaces
{
	public interface IDataProcessingService
	{
		// Option 1 for generic processing
		Task<Result> ProcessDeviceData(object data);

		// Option 2 for concrete processing
		Task<Result> ProcessDeviceTypeAData(DeviceTypeAOutput data);
		Task<Result> ProcessDeviceTypeBData(DeviceTypeBOutput data);

		Task<Result<IEnumerable<DeviceDashboardData>>> GetDataForDashboard();

		Task<Result> ClearDataStorage();
	}
}
