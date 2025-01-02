using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //post : /api/Auth/Register
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {


            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

         var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);


            if (identityResult.Succeeded) {

                //Add roles to this user

                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) { 
                    

                    identityResult = await userManager.AddToRolesAsync(identityUser,registerRequestDto.Roles);

                    if (identityResult.Succeeded) {

                        return Ok("User was registered! please login ..");
                    }


                }
            }

            return BadRequest("Something went Wrong");


        }



        //post: login

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequestDto) 
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {

                    //get Roles for this user

                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {


                        //Create Token

                        var JwtToken = this.tokenRepository.CreateJWTToken(user, roles.ToList());


                        var response = new LoginResponseDto
                        {
                            JwtToken = JwtToken,
             

                        };

                        return Ok(response);
                    };



                    return Ok();

                };

            };

            return BadRequest("Username or password incorrect");

        


        }

    }
}
