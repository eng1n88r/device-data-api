using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.Repository.Interfaces;
using DeviceDataApi.Services.Interfaces;

namespace DeviceDataApi.Services
{
	public class DeviceDataProcessingService : IDeviceDataProcessingService
	{
		private readonly IRepository<DeviceData> _repository;

		public DeviceDataProcessingService(IRepository<DeviceData> repository)
		{
			_repository = repository;
		}

		public async Task<Result> ProcessDeviceData(object data)
		{
			try
			{
				return Result.Ok();
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail($"Failed to save device data: {ex.Message}");
			}
		}

		public async Task<Result> ProcessDeviceTypeAData(IEnumerable<DeviceTypeA> data)
		{
			try
			{


				return Result.Ok();
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail($"Failed to save device type A data: {ex.Message}");
			}
		}

		public async Task<Result> ProcessDeviceTypeBData(IEnumerable<DeviceTypeB> data)
		{
			try
			{

				return Result.Ok();
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail($"Failed to save device type B data: {ex.Message}");
			}
		}

		public async Task<Result<IEnumerable<DeviceData>>> GetDataForDashboard()
		{
			try
			{
				var data = await _repository.ReadData();

				return Result.Ok(data);
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail<IEnumerable<DeviceData>>($"Failed to read dashboard data: {ex.Message}");
			}
		}


		public async Task<Result> ClearDataStorage()
		{
			try
			{
				await _repository.ClearData();

				return Result.Ok();
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail($"Failed to read clear data storage: {ex.Message}");
			}
		}
	}
}