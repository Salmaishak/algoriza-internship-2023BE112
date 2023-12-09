using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Org.BouncyCastle.Cms;
using System.Net.Http.Headers;
using System.Net.Http;

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly VezeetaContext _vezeetaContext;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager, VezeetaContext vezeetaContext, 
        IConfiguration configuration)
    {

        _userManager = userManager;
        _signInManager = signInManager;
        _vezeetaContext = vezeetaContext;
        _configuration = configuration;

    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser()
    {
        // Create a new user
        var newUser = new User
        {
            UserName = "newuser",
            Email = "newuser@example.com",
            // Add other user properties
        };

        var result = await _userManager.CreateAsync(newUser, "Password123!");

        if (result.Succeeded)
        {
            // User created successfully
            // Add user to a role
            await _userManager.AddToRoleAsync(newUser, "Doctor");

            // Other operations after user creation
            return Ok("User created successfully.");
        }
        else
        {
            // Handle errors in user creation
            return BadRequest(result.Errors);
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Username and password are required.");
        }

        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var role = await GetUserRole(username);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var authSignINKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]));

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims:claims,
                signingCredentials: new SigningCredentials(authSignINKey,SecurityAlgorithms.HmacSha256Signature)
            );
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.ToString());
            }
            // User successfully logged in
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken)) ;
        }
        else
        {
            // Handle login failures
            return BadRequest("Login failed.");
        }
    }

    private async Task<string> GetUserRole(string username)
    {
        // Retrieve user role from the database using Entity Framework Core or any other ORM
        var user = await _vezeetaContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user != null)
        {
            // Assuming the role is stored in a property called Role (replace with your actual property name)
            return user.type.ToString(); // Replace 'Role' with your actual property name representing the role
        }

        return null; // Return null or a default role if the user is not found
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Sign out the current user
        await _signInManager.SignOutAsync();

        return Ok("User logged out.");
    }

    // Other actions using UserManager
}
