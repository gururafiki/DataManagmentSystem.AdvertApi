using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataManagmentSystem.AdvertApi.Models;

namespace DataManagmentSystem.AdvertApi.Services
{
	public class AdvertProfile : Profile
	{
		public AdvertProfile()
		{
			CreateMap<AdvertModel, AdvertDBModel>();
		}
	}
}
