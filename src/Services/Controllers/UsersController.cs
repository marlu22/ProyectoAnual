// src/Services/Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService _managementService;

        public UsersController(IUserManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users = _managementService.GetAllUsers();
            return Ok(users);
        }
    }
}