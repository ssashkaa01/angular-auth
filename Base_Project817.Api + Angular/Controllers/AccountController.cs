using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base_Project817.Api___Angular.Helper;
using Base_Project817.DataAccess;
using Base_Project817.DataAccess.Entity;
using Base_Project817.Domain.Interfaces;
using Base_Project817.DTO.Models;
using Base_Project817.DTO.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Base_Project817.Api___Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTTokenService _jwtTokenService;

        public AccountController(
                EFContext context,
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                IJWTTokenService jwtTokenService
            )
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
        }

        //localhost:12312/api/Account/register
        [HttpPost("register")]
        public async Task<ResultDTO> Register([FromBody]UserRegisterDTO model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return new ResultErrorDTO()
                    {
                        Status = 401,
                        Message = "ERROR",
                        Errors = CustomValidator.getErrorsByModel(ModelState)
                    };
                }

                var user = new User()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone
                };

                var userProfile = new UserAdditionalInfo()
                {
                    Id = user.Id,
                    FullName = model.FullName,
                    Address = model.Address,
                    Age = model.Age
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    result = _userManager.AddToRoleAsync(user, "User").Result;
                    _context.UserAdditionalInfos.Add(userProfile);
                    _context.SaveChanges();

                    return new ResultDTO()
                    {
                        Message = "OK",
                        Status = 200
                    };
                }
                else
                {
                    return new ResultErrorDTO() {
                        Message = "ERROR",
                        Status = 403,
                        Errors = CustomValidator.getErrorsByIdentityResult(result)
                    };
                }
            }
            catch (Exception e)
            {
                return new ResultErrorDTO
                {
                    Status = 500,
                    Message = e.Message,
                    Errors = new List<string>()
                    {
                        e.Message
                    }
                };
            }

        }
        
        //localhost:12312/api/Account/login
        [HttpPost("login")]
        public async Task<ResultDTO> Login([FromBody]UserLoginDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResultErrorDTO
                    {
                        Message ="ERROR",
                        Status = 401,
                        Errors = CustomValidator.getErrorsByModel(ModelState)
                    };
                }

                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).Result;

                if(!result.Succeeded)
                {
                    return new ResultErrorDTO
                    {
                        Status = 403,
                        Message = "ERROR",
                        Errors = new List<string> { "Incorrect email or password" }
                    };
                } else
                {
                    var user =  await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(user, false);

                    return new ResultLoginDTO
                    {
                        Status = 200,
                        Message = "OK",
                        Token = _jwtTokenService.CreateToken(user)
                    };
                }
            }
            catch (Exception e)
            {
                return new ResultErrorDTO
                {
                    Status = 500,
                    Message = "ERROR",
                    Errors = new List<string> { e.Message }
                };
            }



        }

    }
}