using AuthSystem.Manager;
using AuthSystem.Models;
using AuthSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace AuthSystem.Data.Controller
{

    public class ApiGlobalController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private ApplicationDbContext _context;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private UsersModel user = new UsersModel();
        public ApiGlobalController(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("api/jwt/{username}")]
        public IActionResult Jwt(string username)
        {
            _global.Token = _global.GenerateToken(username, _appSettings.Key.ToString());
            return Ok(_global);
        }

    }
}
