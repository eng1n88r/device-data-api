using System.Collections.Generic;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;

namespace DeviceDataApi.Repositories.Interfaces
{
	public interface IRepository
	{
		Task SaveDataData(IEnumerable<DeviceData> data);

		Task<IEnumerable<DeviceData>> GetMeasurements();

		Task ClearData();
	}
}
