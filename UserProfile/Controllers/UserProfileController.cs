using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UserProfile.Services;
using UserProfile.Entities;
using UserProfile.Models.Users;
using UserProfile.Helpers;
using Microsoft.Extensions.Logging;

namespace UserProfile.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private IUserProfileService _userService;
        private IMapper _mapper;
        private static ILogger Logger { get; } =
      ApplicationLogging.CreateLogger<UserProfileController>();
        private readonly AppSettings _appSettings;

        public UserProfileController(
            IUserProfileService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Country = user.Country,
                Active = user.isActive,
                Salary = user.Salary,
                DateOfBirth = user.DateOfBirth,
                ProfileImagePath = user.ProfileImagePath,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
               User newUser = _userService.Create(user, model.Password);
                Logger.LogInformation(
              "A user has been created successfully");
                return Ok(new
                {
                    Id = newUser.Id,
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                Logger.LogError("Error occured while creating new user");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getProfilePhoto/{id}")]
        public IActionResult Get(int id)
        {

            var user = _userService.GetUserById(id);
            Byte[] b = System.IO.File.ReadAllBytes(user.ProfileImagePath);   // You can use your own method over here.         
            return Ok(File(b, "image/jpeg"));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUserById(id);
            var model = _mapper.Map<UserModel>(user);
            Logger.LogInformation(
             "The user has been retrieved");
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                // update user 
                _userService.Update(user, model.Password, model.ProfileImagePath);
                Logger.LogInformation(
             "Done editing the user");
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
