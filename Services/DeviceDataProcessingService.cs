using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.DataProcessors;
using DeviceDataApi.Repositories.Interfaces;
using DeviceDataApi.Services.Interfaces;

namespace DeviceDataApi.Services
{
	public class DeviceDataProcessingService : IDeviceDataProcessingService
	{
		private readonly IRepository _repository;

		private readonly DeviceAProcessor _deviceAProcessor;
		private readonly DeviceBProcessor _deviceBProcessor;

		public DeviceDataProcessingService(DeviceProcessorFactory factory, IRepository repository)
		{
			_repository = repository;

			_deviceAProcessor = factory.CreateDeviceTypeAProcessor();
			_deviceBProcessor = factory.CreateDeviceTypeBProcessor();
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

		public async Task<Result> ProcessDeviceTypeAData(DeviceTypeA data)
		{
			try
			{
				var deviceData = _deviceAProcessor.ProcessDeviceData(data);

				await _repository.SaveDataData(deviceData);

				return Result.Ok();
			}
			catch (Exception ex)
			{
				// TODO: Log ex
				return Result.Fail($"Failed to save device type A data: {ex.Message}");
			}
		}

		public async Task<Result> ProcessDeviceTypeBData(DeviceTypeB data)
		{
			try
			{
				var deviceData = _deviceBProcessor.ProcessDeviceData(data);

				await _repository.SaveDataData(deviceData);

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