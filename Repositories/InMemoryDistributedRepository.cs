using System.Collections.Generic;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.Repositories.Interfaces;

using Phema.Caching;

namespace DeviceDataApi.Repositories
{
	/// <summary>
	/// Class represents concrete implementation on storage.
	/// Could be relational database, NoSQL database, etc.
	/// In this particular case is simplified to distributed cache.
	/// </summary>
	public class InMemoryDistributedRepository : IRepository
	{
		private const string RepositoryKey = "DEVICE:DATA";
		private readonly IDistributedCache<IList<DeviceData>> _distributedCache;

		public InMemoryDistributedRepository(IDistributedCache<IList<DeviceData>> distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task SaveDataData(DeviceData data)
		{
			var cached = await _distributedCache.GetAsync(RepositoryKey);

			if (cached != null)
			{
				cached.Add(data);

				await _distributedCache.SetAsync(RepositoryKey, cached);
			}
			else
			{
				await _distributedCache.SetAsync(RepositoryKey, new List<DeviceData> { data });
			}
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