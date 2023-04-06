using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using filmsApi.Services;
using filmsApi.DataAccess;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FilmsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("filmsdb")));
builder.Services.AddDbContext<JwtContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("jwtdb")));

builder.Services.AddSingleton<IUserInfoService, UserInfoService>();
builder.Services.AddSingleton<JwtContext>();
builder.Services.AddSingleton<FilmsContext>();
builder.Services.AddSingleton<IActorService, ActorService>();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IMovieRatingService, MovieRatingService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers() );
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
