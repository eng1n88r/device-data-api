using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceDataApi.Repository.Interfaces
{
	public interface IRepository<T>
	{
		Task SaveDataData(IEnumerable<T> data);

		Task<IEnumerable<T>> ReadData();

		Task ClearData();
	}
}
