using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Infrastructure.RepositoriesImplementation;
using Vezeeta.Services.Interfaces;
using Vezeeta.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<UserManager<IdentityUser>>();
builder.Services.AddTransient<SignInManager<IdentityUser>>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<VezeetaContext>();

builder.Services.AddControllers();
var Configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Configure authentication and authorization
builder.Services.AddAuthentication( options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options=>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters =
    new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = Configuration["Jwt:ValidAudience"],
        ValidIssuer = Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( Configuration["Jwt:SecretKey"]))    

    };

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser(); 
        policy.RequireRole("Admin"); 
    });

    options.AddPolicy("Doctor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Doctor");
    });

    options.AddPolicy("Patient", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Patient");
    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VezeetaContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseRouting();

app.UseAuthorization(); // This should come after app.UseRouting()

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();