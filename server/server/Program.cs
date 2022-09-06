using System;
using server.Hubs;
using server.Data;
using server_Database;
using server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
string? DB_connectionString = Environment.GetEnvironmentVariable("CONN");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader()
            .WithOrigins("http://localhost:4200")
            .AllowCredentials();
    }));

// string? ConnectionString = Environment.GetEnvironmentVariable("CONN");

builder.Services.AddSingleton<Brass_IRepository>(sp => new Brass_SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<Brass_SQLRepository>>()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer("Bearer", options =>
//{
//    var secretBytes = Encoding.UTF8.GetBytes(JWTConstants.Secret);
//    var key = new SymmetricSecurityKey(secretBytes);

//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = JWTConstants.Issuer,
//        ValidAudience = JWTConstants.Audience,
//        IssuerSigningKey = key
//    };
//});

builder.Services.AddSingleton<IBudgetRepository>(sp => new SQLBudgetRepository(DB_connectionString));

builder.Services.AddSingleton<IRepository>(sp => new SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<SQLRepository>>()));
string MyAllowAllOrgins = "_myAllowAllOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrgins, builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();
app.UseCors(MyAllowAllOrgins);

app.MapHub<ChatHub>("/chatsocket");


app.Run();

