using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.API
{
    [Microsoft.AspNetCore.Mvc.Route("/api/base/[controller]/[action]")]
    public class AddressApi : NikApi
    {
        public IConfiguration Configuration { get; }
        public IITCFService iITCFServ { get; set; }

        private readonly UserManager<DataModel.User> UserManager;

        public AddressApi(IConfiguration configuration, UserManager<DataModel.User> userManager)
        {
            Configuration = configuration;
            UserManager = userManager;
            iITCFServ = new ITCFService(Configuration);
        }

        [HttpPost]
        public async Task<IActionResult> GetCity([FromForm] int countryId, [FromForm] int provinceId)
        {
            if (countryId == 0 && provinceId == 0)
            {
                return Ok(new { 
                    status = 500, 
                    message = "خطا در مقادیر ورودی" 
                });
            }
            var query = iITCFServ.iCityServ.ExpressionMaker();

            if (countryId != 0)
            {
                query.Add(x => x.CountryId == countryId);
            }

            if (provinceId != 0)
            {
                query.Add(x => x.ProvinceId == provinceId);
            }

            var cities = iITCFServ.iCityServ.GetAll(query, y => new {
                y.Id,
                y.Title,
                y.CountryId,
                y.ProvinceId
            }).ToList();

            return Ok(new
            {
                status = 200,
                message = "دریافت موفق",
                count = cities.Count,
                data = cities
            });
        }

        [HttpPost]
        //[Authorize(Policy = "AccessToken")]
        public async Task<IActionResult> GetProvince([FromForm] int countryId)
        {
            if (countryId == 0)
            {
                return Ok(new
                {
                    status = 500,
                    message = "خطا در مقادیر ورودی"
                });
            }

            var provinces = iITCFServ.iProvinceServ.GetAll(x => x.CountryId == countryId, y => new { 
                y.Id, 
                y.Title,
                y.CountryId
            }).ToList();

            return Ok(new
            {
                status = 200,
                message = "دریافت موفق",
                count = provinces.Count,
                data = provinces
            });
        }


        [HttpPost]
        //[Authorize(Policy = "AccessToken")]
        public async Task<IActionResult> GetIndustrialPark([FromForm] int cityId, [FromForm] int provId)
        {
            if (provId == 0 && cityId == 0)
            {
                return Ok(new
                {
                    status = 500,
                    message = "خطا در مقادیر ورودی"
                });
            }

            var query = iITCFServ.IIndustrialParkServ.ExpressionMaker();
            query.Add(x => true);
            if (cityId != 0)
            {
                query.Add(x => x.CityId == cityId);
            }

            if (provId != 0)
            {
                query.Add(x => x.ProvinceId == provId);
            }

            var parks = iITCFServ.IIndustrialParkServ.GetAll(query, y => new {
                y.Id,
                y.Title,
                y.CountryId
            }).ToList();

            return Ok(new
            {
                status = 200,
                message = "دریافت موفق",
                count = parks.Count,
                data = parks
            });
        }


    }
}
