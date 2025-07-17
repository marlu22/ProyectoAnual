// src/Services/Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using DataAccess.Entities;
using System.Collections.Generic;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}