using System;
using server.Data;

// TEMP connection string for internal testing
string? DB_connectionString;

DB_connectionString = Environment.GetEnvironmentVariable("CONN");
//DB_connectionString = await File.ReadAllTextAsync(@"./../../../connectionString_P3.txt");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TRANSACTION_IRepository>(sp => new SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<SQLRepository>>()));

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

app.MapControllers();

app.UseCors(MyAllowAllOrgins);

app.Run();
