using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeviceDataApi.Contracts;
using DeviceDataApi.DataProcessors;
using DeviceDataApi.DataProcessors.Interfaces;
using DeviceDataApi.Repositories.Interfaces;
using DeviceDataApi.Services.Interfaces;

namespace DeviceDataApi.Services
{
	public class DataProcessingService : IDataProcessingService
	{
		private readonly IRepository _repository;

		private readonly IDeviceDataProcessor<DeviceTypeAData> _deviceAProcessor;
		private readonly IDeviceDataProcessor<DeviceTypeBData> _deviceBProcessor;

		public DataProcessingService(
			IRepository repository,
			IDeviceDataProcessor<DeviceTypeAData> deviceAProcessor,
			IDeviceDataProcessor<DeviceTypeBData> deviceBProcessor)
		{
			_repository = repository;

			_deviceAProcessor = deviceAProcessor;
			_deviceBProcessor = deviceBProcessor;
		}

		public async Task<Result> ProcessDeviceData(object data)
		{
			try
			{
				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail($"Failed to save device data: {ex.Message}");
			}
		}

		public async Task<Result> ProcessDeviceTypeAData(DeviceTypeAData data)
		{
			try
			{
				var deviceData = _deviceAProcessor.ProcessDeviceData(data);

				await _repository.SaveDataData(deviceData);

				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail($"Failed to save device type A data: {ex.Message}");
			}
		}

		public async Task<Result> ProcessDeviceTypeBData(DeviceTypeBData data)
		{
			try
			{
				var deviceData = _deviceBProcessor.ProcessDeviceData(data);

				await _repository.SaveDataData(deviceData);

				return Result.Ok();
			}
			catch (Exception ex)
			{
				return Result.Fail($"Failed to save device type B data: {ex.Message}");
			}
		}

		public async Task<Result<IEnumerable<DeviceDashboardData>>> GetDataForDashboard()
		{
			try
			{
				var data = await _repository.GetMeasurements();

				if (data == null)
				{
					return Result.Fail<IEnumerable<DeviceDashboardData>>("No data was found.");
				}

				var result = ProcessDataForDashboard(data);
				
				return Result.Ok(result);
			}
			catch (Exception ex)
			{
				return Result.Fail<IEnumerable<DeviceDashboardData>>($"Failed to read dashboard data: {ex.Message}");
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

		private IEnumerable<DeviceDashboardData> ProcessDataForDashboard(IEnumerable<DeviceData> input)
		{
			var result = input
				.Select(device => new DeviceDashboardData
				{
					CompanyId = device.CompanyId,
					CompanyName = device.CompanyName,
					StartDate = device.StartDate,
					TrackerId = device.Id,
					TrackerName = device.Name,
					TempCount = device.Measurements.Count(x => x.Type == MeasurementType.Temperature),
					HumidityCount = device.Measurements.Count(x => x.Type == MeasurementType.Humidity),
					FirstCrumbDtm = device.Measurements.Min(x => x.Date),
					LastCrumbDtm = device.Measurements.Max(x => x.Date),
					AvgHumidity = Math.Round(device.Measurements.Where(x => x.Type == MeasurementType.Humidity).Average(x => x.Value), 2),
					AvgTemp = Math.Round(device.Measurements.Where(x => x.Type == MeasurementType.Temperature).Average(x => x.Value), 2),
				})
				.ToList();

			return result;
		}
	}
}