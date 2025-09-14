using api.Models;
using api.Interfaces;
using api.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Helpers;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase{

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager){
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.EmailAddress
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password!);
                if (createUser.Succeeded){
                    var roleResult  = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded){
                        var roles = await _userManager.GetRolesAsync(appUser);
                        return ApiResponse.Success(
                            new NewUserDto{
                                UserName = appUser.UserName!,
                                Email = appUser.Email!,
                                Token = _tokenService.CreateToken(appUser, roles)
                            },
                            "User registered successfully"
                        );
                    }
                    else{
                        return ApiResponse.Error("Failed to assign role to user", 500);
                    }
                }
                else{
                    return ApiResponse.Error("Failed to create user", 500);
                }
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
                return ApiResponse.NotFound("Invalid UserName!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return ApiResponse.Unauthorized("Invalid Credentials");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return ApiResponse.Success(new NewUserDto
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user, roles),
            }, "Login successful");
        }
    }
}
