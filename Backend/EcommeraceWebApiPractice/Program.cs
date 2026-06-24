using Ecom.Core;
using Ecom.Infrastructure.Data;
using Ecom.infrrastructure.Data;
using Ecom.infrrastructure.Repository;
using EcommeraceWebApiPractice;
using EcommeraceWebApiPractice.Controllers;
using EcommeraceWebApiPractice.Entites;
using EcommeraceWebApiPractice.@interface;
using EcommeraceWebApiPractice.Mapper;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200");
    });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }); 
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

// Identity
builder.Services.AddIdentity<AccountUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<IPhotoService, PhotoServices>();
builder.Services.AddScoped<ICartServices, CartServices>();
// Global auth policy
builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous(); 
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();

// ✅ FIX 4: UseAuthentication was MISSING entirely
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

// Check if products are already seeded to prevent duplication
if (!context.products.Any())
{
    var productsToSeed = EcomDataSeeder.GenerateProducts();

    // AddRange will automatically insert the products and their nested photos
    context.products.AddRange(productsToSeed);
    context.SaveChanges();

    Console.WriteLine("Successfully seeded 150 products and photos!");
}

app.Run();
