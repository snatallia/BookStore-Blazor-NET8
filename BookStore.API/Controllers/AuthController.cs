using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
     
        
        

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            _logger.LogInformation($"Register attempt with of name: {userDto.FirstName} {userDto.LastName}");
            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, "User");
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrone in {nameof(Register)}");
                return Problem($"Something went wrone in {nameof(Register)}", statusCode : 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"Login attempt with email: {loginUserDto.Email}");

            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
                if (user == null)
                {
                    return NotFound();
                }

                var passwordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                if (!passwordValid)
                {
                    ModelState.AddModelError("Password", "Incorrect password");
                    return BadRequest(ModelState);
                }

                return Accepted();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrone in {nameof(Login)}");
                return Problem($"Something went wrone in {nameof(Login)}", statusCode: 500);
            }
        }

    }
}
