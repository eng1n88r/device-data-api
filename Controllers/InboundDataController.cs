using System.Threading.Tasks;
using DeviceDataApi.Contracts;
using DeviceDataApi.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace DeviceDataApi.Controllers
{
	[ApiController]
	[Route("api/v1")]
	public class InboundDataController : ControllerBase
	{
		private readonly IDataProcessingService _dataProcessingService;

		public InboundDataController(IDataProcessingService dataProcessingService)
		{
			_dataProcessingService = dataProcessingService;
		}

		[HttpGet]
		[Route("device-data")]
		public async Task<IActionResult> GetDevicesData()
		{
			var result = await _dataProcessingService.GetDataForDashboard();

			if (result.IsSuccess)
			{
				return Ok(result.Data);
			}

			return StatusCode(500, result.Message);
		}

		[HttpPost]
		[Route("device-type-a-data")]
		public async Task<IActionResult> SaveDeviceTypeAData(DeviceTypeAData input)
		{
			var result = await _dataProcessingService.ProcessDeviceTypeAData(input);

			if (result.IsSuccess)
			{
				return Ok();
			}

			return StatusCode(500, result.Message);
		}

		[HttpPost]
		[Route("device-type-b-data")]
		public async Task<IActionResult> SaveDeviceTypeBData(DeviceTypeBData input)
		{
			var result = await _dataProcessingService.ProcessDeviceTypeBData(input);

			if (result.IsSuccess)
			{
				return Ok();
			}

			return StatusCode(500, result.Message);
		}

		[HttpDelete]
		[Route("device-data")]
		public async Task<IActionResult> ClearDeviceData()
		{
			var result = await _dataProcessingService.ClearDataStorage();

			if (result.IsSuccess)
			{
				return Ok();
			}

			return StatusCode(500, result.Message);
		}

		[HttpPost]
		[Route("device-data")]
		public async Task<IActionResult> SaveDeviceData([FromBody] object input)
		{
			var result = await _dataProcessingService.ProcessDeviceData(input);

			if (result.IsSuccess)
			{
				return Ok();
			}

			return StatusCode(500, result.Message);
		}
	}
}
