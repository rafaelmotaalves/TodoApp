using Api.Controllers.Dtos;
using Core.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthenticationController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper;

    public AuthenticationController(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMapper mapper
    )
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _mapper = mapper;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login(AuthenticationDto authenticationDto)
    {
      var user = await _userManager.FindByNameAsync(authenticationDto.Name);
      if (user is null || !await _userManager.CheckPasswordAsync(user, authenticationDto.Password))
        return Unauthorized();

      var userRoles = await _userManager.GetRolesAsync(user);
      var authClaims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

      foreach (var userRole in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
      }

      var token = GetToken(authClaims);

      return Ok(new
      {
        token = new JwtSecurityTokenHandler().WriteToken(token),
        expiration = token.ValidTo
      });
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(AuthenticationDto authenticationDto)
    {
      var userExists = await _userManager.FindByNameAsync(authenticationDto.Name);
      if (userExists != null)
        return BadRequest(new
        {
          message = "User name already exists"
        });

      User user = new()
      {
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = authenticationDto.Name
      };

      var result = await _userManager.CreateAsync(user, authenticationDto.Password);
      if (!result.Succeeded)
        return StatusCode(StatusCodes.Status500InternalServerError);

      return NoContent();
    }
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
      var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

      var token = new JwtSecurityToken(
          issuer: _configuration["JWT:ValidIssuer"],
          audience: _configuration["JWT:ValidAudience"],
          expires: DateTime.Now.AddHours(3),
          claims: authClaims,
          signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
          );

      return token;
    }

    [Authorize]
    [HttpGet]
    [Route("me")]
    public async Task<UserDto> GetMe()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);

      return _mapper.Map<User, UserDto>(user);
    }
  }

}