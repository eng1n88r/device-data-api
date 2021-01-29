using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.Repository.Interfaces;

using Phema.Caching;

namespace DeviceDataApi.Repository
{
	/// <summary>
	/// Class represents concrete implementation on storage. Could be relational database, NoSQL database, etc. In this particular case is simplified to distributed cache.
	/// </summary>
	public class InMemoryDistributedRepository : IRepository<DeviceData>
	{
		private const string RepositoryKey = "DEVICE:DATA";
		private readonly IDistributedCache<IEnumerable<DeviceData>> _distributedCache;

		public InMemoryDistributedRepository(IDistributedCache<IEnumerable<DeviceData>> distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task SaveDataData(IEnumerable<DeviceData> data)
		{
			var cached = await _distributedCache.GetAsync(RepositoryKey);

			var merged = cached != null ? cached.Concat(data) : data;

			await _distributedCache.SetAsync(RepositoryKey, merged);
		}

		public async Task<IEnumerable<DeviceData>> ReadData()
		{
			return await _distributedCache.GetAsync(RepositoryKey);
		}

		public async Task ClearData()
		{
			await _distributedCache.RemoveAsync(RepositoryKey);
		}
	}
}
