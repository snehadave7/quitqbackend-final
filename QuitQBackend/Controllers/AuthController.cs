using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuitQBackend.Authentication;
using QuitQBackend.Data;
using QuitQBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly QuitQContext _context;


        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, QuitQContext context) {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = config;
            _context = context;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model) {
            var userExist=await _userManager.FindByNameAsync(model.FullName);
            var emailExist=await _userManager.FindByEmailAsync(model.Email);
            if (userExist!=null || emailExist!=null){
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.FullName,
            };

            var result=await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Creation Failed! Please check the user details and try again"
                });
            }
            if (model.Role == "customer") {
                if(!await _roleManager.RoleExistsAsync(UserRoles.Customer)) {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                }
                if(await _roleManager.RoleExistsAsync(UserRoles.Customer)) {
                    await _userManager.AddToRoleAsync(user, UserRoles.Customer);
                }

            }
            if (model.Role == "admin") {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin)) {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);

                }

            }
            if(model.Role == "seller") {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Seller)) {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Seller));

                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Seller)) {
                    await _userManager.AddToRoleAsync(user, UserRoles.Seller);

                }

            }

            var customUser = new User
            {
                FullName = model.FullName,
                ContactNumber = model.ContactNumber,
                Password = model.Password,
                Email = model.Email,
                Role = model.Role,
            };
            _context.Users.Add(customUser);
            await _context.SaveChangesAsync();


            return Ok(new Response { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel login) {
            var user=await _userManager.FindByNameAsync(login.UserName);
            if(user!=null && await _userManager.CheckPasswordAsync(user,login.Password)) {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach( var userRole in userRoles) {
                    authclaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else return Unauthorized();
        }

    }
}
