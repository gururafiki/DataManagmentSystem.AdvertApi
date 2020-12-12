using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataManagmentSystem.AdvertApi.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DataManagmentSystem.AdvertApi.HealthChecks
{
	public class StorageHealthCheck : IHealthCheck
	{
		private IAdvertStorageService _storageService;
		public StorageHealthCheck(IAdvertStorageService storageService) {
			_storageService = storageService;
		}


		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
			var isStorageOk = await _storageService.CheckHealthAsync();
			if (!isStorageOk) {
				return HealthCheckResult.Unhealthy();
			}
			return HealthCheckResult.Healthy();
		}
	}
}
