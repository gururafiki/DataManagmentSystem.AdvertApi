using DataManagmentSystem.AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagmentSystem.AdvertApi.Services
{
	public interface IAdvertStorageService
	{
		Task<string> Add(AdvertModel model);
		Task Confirm(ConfirmAdvertModel model);
		Task<bool> CheckHealthAsync();
	}
}
