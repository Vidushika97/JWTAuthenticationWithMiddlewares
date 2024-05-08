
using JWTAuthenticationWithMiddlewares.Services.AuthService;
using JWTAuthenticationWithMiddlewares.Services.StoryService;
using JWTAuthenticationWithMiddlewares.Services.UserService;
using JWTAuthenticationWithMiddlewares;
using Microsoft.EntityFrameworkCore;
using JWTAuthenticationWithMiddlewares.Helpers.Utils.GlobalAttributes;


var builder = WebApplication.CreateBuilder(args);

//add global attributes
GlobalAttributes.mysqlConfiguration.connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

// Add services to the container.
builder.Services.AddControllers();

// Add Application Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    var connectionString = builder.Configuration.GetConnectionString("Defaultconnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//register custom services
//builder.Services.AddScoped<ILoginDetailService, LoginDetailService>();
//builder.Services.AddScoped<IStoryService, StoryService>();
//builder.Services.AddScoped<IUserService, UserService>();




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

app.UseAuthorization();

app.MapControllers();

app.Run();
