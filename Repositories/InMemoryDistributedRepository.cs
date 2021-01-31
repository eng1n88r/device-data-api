using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.Repositories.Interfaces;

using Phema.Caching;

namespace DeviceDataApi.Repositories
{
	/// <summary>
	/// Class represents concrete implementation for data storage.
	/// Could be relational database, NoSQL database, etc.
	/// In this particular case is simplified to distributed cache.
	/// </summary>
	public class InMemoryDistributedRepository : IRepository
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

			if (cached != null)
			{
				var merged = cached.Concat(data).ToList();

				await _distributedCache.SetAsync(RepositoryKey, merged);
			}
			else
			{
				await _distributedCache.SetAsync(RepositoryKey, data);
			}
		}

		public async Task<IEnumerable<DeviceData>> GetMeasurements()
		{
			var data = await _distributedCache.GetAsync(RepositoryKey);

			var collection = new Dictionary<string, DeviceData>();

			foreach (var item in data)
			{
				if (!collection.ContainsKey(item.Name))
				{
					collection.Add(item.Name, item);
				}
				else
				{
					var originalData = collection[item.Name].Measurements;

					collection[item.Name].Measurements = originalData.Concat(item.Measurements).ToList();
				}
			}

			return collection.Values.ToList();

		}

		public async Task ClearData()
		{
			await _distributedCache.RemoveAsync(RepositoryKey);
		}
	}
}