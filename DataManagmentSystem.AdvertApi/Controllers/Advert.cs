using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagmentSystem.AdvertApi.Models;
using DataManagmentSystem.AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagmentSystem.AdvertApi.Controllers
{
	[ApiController]
	[Route("api/v1/adverts")]
	public class Advert : ControllerBase
	{
		private readonly IAdvertStorageService _advertStorageService;

		public Advert(IAdvertStorageService advertStorageService)
		{
			_advertStorageService = advertStorageService;
		}

		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(CreateAdvertResponse))]
		public async Task<IActionResult> Create(AdvertModel model)
		{
			string recordId;
			try
			{
				recordId = await _advertStorageService.Add(model);
			}
			catch(Exception e)
			{
				return StatusCode(500, e.Message);
			}
			return StatusCode(200, new CreateAdvertResponse { Id = recordId });
		}

		[HttpPut]
		[Route("Confirm")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
		{
			try
			{
				await _advertStorageService.Confirm(model);
			}
			catch (KeyNotFoundException)
			{
				return new NotFoundResult();
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
			return new OkResult();
		}
	}
}
