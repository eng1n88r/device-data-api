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
		private readonly IDeviceDataProcessingService _dataProcessingService;

		public InboundDataController(IDeviceDataProcessingService dataProcessingService)
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
				return Ok(result);
			}

			return StatusCode(500, result.Message);
		}

		[HttpPost]
		[Route("device-type-a-data")]
		public async Task<IActionResult> SaveDeviceTypeAData(DeviceTypeA input)
		{
			var result = await _dataProcessingService.ProcessDeviceTypeAData(input);

			if (result.IsSuccess)
			{
				return Ok(result);
			}

			return StatusCode(500, result.Message);
		}

		[HttpPost]
		[Route("device-type-b-data")]
		public async Task<IActionResult> SaveDeviceTypeBData(DeviceTypeB input)
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
	}
}
