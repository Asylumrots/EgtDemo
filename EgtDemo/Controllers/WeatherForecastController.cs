﻿using BCVP.Sample.IServices;
using EgtDemo.Extensions;
using EgtDemo.IServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EgtDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ISysUserInfoServices _sysUserInfoServices;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDemoServ _demoServ;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        private readonly IRedisCacheManager _redisCacheManager;

        public WeatherForecastController(ISysUserInfoServices sysUserInfoServices, ILogger<WeatherForecastController> logger, IDemoServ demoServ, IRoleModulePermissionServices roleModulePermissionServices,
            IRedisCacheManager redisCacheManager
            )
        {
            _sysUserInfoServices = sysUserInfoServices;
            _logger = logger;
            _demoServ = demoServ;
            _roleModulePermissionServices = roleModulePermissionServices;
            _redisCacheManager = redisCacheManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _redisCacheManager.Set("laozhang", "nihao", TimeSpan.FromMinutes(10));



            var demos = _demoServ.GetDemos();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



    }
}
