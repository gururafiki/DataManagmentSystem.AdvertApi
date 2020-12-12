using DataManagmentSystem.AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon;

namespace DataManagmentSystem.AdvertApi.Services
{
	public class DynamoDBAdvertStorage : IAdvertStorageService
	{
		private readonly IMapper _mapper;
		private readonly IAmazonDynamoDB _dynamoDBClient;

		public DynamoDBAdvertStorage(IMapper mapper, IAmazonDynamoDB dynamoDBClient)
		{
			_mapper = mapper;
			_dynamoDBClient = dynamoDBClient;
		}

		public async Task<string> Add(AdvertModel model)
		{
			var dbModel = _mapper.Map<AdvertDBModel>(model);
			dbModel.Id = new Guid().ToString();
			dbModel.CreationDateTime = DateTime.UtcNow;
			dbModel.Status = AdvertStatus.Pending;

			using (var context = new DynamoDBContext(_dynamoDBClient))
			{
				await context.SaveAsync(dbModel);
			}
			return dbModel.Id;
		}

		public async Task<bool> CheckHealthAsync() {
			var tableData = await _dynamoDBClient.DescribeTableAsync("Adverts");
			return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
		}

		public async Task Confirm(ConfirmAdvertModel model)
		{
			using (var context = new DynamoDBContext(_dynamoDBClient))
			{
				var record = await context.LoadAsync<AdvertDBModel>(model.Id);
				if (record == null)
				{
					throw new KeyNotFoundException($"A record with Id={model.Id} was not found.");
				}
				if (model.Status == AdvertStatus.Active)
				{
					record.Status = AdvertStatus.Active;
					await context.SaveAsync(record);
				}
				else
				{
					await context.DeleteAsync(record);
				}
			}
		}
	}
}
