using System;
using server.Hubs;
using server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Model;
using Microsoft.Extensions.DependencyInjection;



// builder.Services.AddSingleton<IRepository>(sp => new SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<SQLRepository>>()));
var builder = WebApplication.CreateBuilder(args);
// Backend_Bronze
//string? DB_connectionString = Environment.GetEnvironmentVariable("CONN", EnvironmentVariableTarget.User);
string? DB_connectionString = Environment.GetEnvironmentVariable("CONN");


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader()
            .WithOrigins("https://misty-api-dev2.azurewebsites.net")
            .AllowCredentials();
    }));

// string? ConnectionString = Environment.GetEnvironmentVariable("CONN");

builder.Services.AddSingleton<Brass_IRepository>(sp => new Brass_SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<Brass_SQLRepository>>()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<Bronze_IRepository>(sp => new Bronze_SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<Bronze_SQLRepository>>()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT gernerator
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    var secretBytes = Encoding.UTF8.GetBytes(JWTConstants.Secret);
    var key = new SymmetricSecurityKey(secretBytes);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JWTConstants.Issuer,
        ValidAudience = JWTConstants.Audience,
        IssuerSigningKey = key
    };
});

builder.Services.AddSingleton<IBudgetRepository>(sp => new SQLBudgetRepository(DB_connectionString));

builder.Services.AddSingleton<TRANSACTION_IRepository>(sp => new TRANSACTION_SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<TRANSACTION_SQLRepository>>()));
//string MyAllowAllOrgins = "_myAllowAllOrigins";

builder.Services.AddSingleton<TeamCopper_IRepo>(sp => new TeamCopper_SQLRepo(DB_connectionString, sp.GetRequiredService<ILogger<TeamCopper_SQLRepo>>()));

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrgins, builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseCors(MyAllowAllOrgins);

app.MapHub<ChatHub>("/chatsocket");


app.Run();

