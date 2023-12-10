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
using SendGrid.Helpers.Mail;
using System.Numerics;
using Vezeeta.Core.DTOs;

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
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken)) ;
        }
        else
        {
            return BadRequest("Login failed.");
        }
    }

    private async Task<string> GetUserRole(string username)
    {
        var user = await _vezeetaContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user != null)
        {
            return user.type.ToString(); 
        }

        return null; 
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Sign out the current user
        await _signInManager.SignOutAsync();

        return Ok("User logged out.");
    }


    // Used to Seed Admin 
    //[HttpPost("register")]
    //public async Task<IActionResult> RegisterUser(AddDoctorDTO doctor)
    //{
    //    if (doctor != null)
    //    {
    //        User doc = new User()
    //        {
    //            UserName = doctor.email,
    //            Email = doctor.email,
    //            fname = doctor.fname,
    //            lname = doctor.lname,
    //            email = doctor.email,
    //            dateOfBirth = doctor.dateOfBirth,
    //            image = doctor.image,
    //            phoneNumber = doctor.phone,
    //            gender = doctor.gender,
    //            password = "AdminVezeeta2023!", // Change the length of the password here
    //            type = UserType.admin
    //        };
    //        var result = await _userManager.CreateAsync(doc, doc.password);
    //        if (result.Succeeded)
    //        {
    //            await _userManager.AddToRoleAsync(doc, "Admin");

    //            return Ok("Admin Added");
    //        }
    //        else
    //            return BadRequest("Failed");


    //    }
    //    else
    //        return null;
    //}
}
