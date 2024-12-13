using CaseStudyQuitQ.Authentication;
using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager; //  Manages user operations , perform user-related operations
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration; // IConfiguration: Accesses app settings (JWT configuration).
        private readonly QuitQEcomContext _context;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, QuitQEcomContext context) {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = config;
            _context = context;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model) {
            var userNameExist = await _userManager.FindByNameAsync(model.UserName);
            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if(userNameExist != null && emailExist != null) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }
            else if (userNameExist != null) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Username already exists!"
                });
            }
            else if(emailExist != null) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Email already exists!"
                });

            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Creation Failed! Please check the user details and try again"
                });
            }
            if (model.Role == "customer") {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Customer)) {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Customer)) {
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
            if (model.Role == "seller") {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Seller)) {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Seller));

                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Seller)) {
                    await _userManager.AddToRoleAsync(user, UserRoles.Seller);

                }

            }

            var customUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
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
            var user = await _userManager.FindByNameAsync(login.UserName);
            var role = await _roleManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password)) {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authclaims = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // Add UserId as a claim
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles) {
                    authclaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));



                // Serialize the token into a string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                Response.Cookies.Append("authToken", tokenString, new CookieOptions
                {
                    HttpOnly = true,  // Prevent access from JavaScript
                    Secure = true,    // Ensure it's only sent over HTTPS (use only in production)
                    SameSite = SameSiteMode.None,  // Necessary for cross-site cookies (useful in modern browsers)
                    Path = "/",       // Path to send the cookie (use "/" to make it accessible globally)
                    Expires = DateTime.UtcNow.AddHours(24),  // Set expiration time for the cookie
                    Domain = "localhost"
                });


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
