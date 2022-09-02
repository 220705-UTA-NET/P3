using server.Hubs;
using server.Data;

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

string? ConnectionString = Environment.GetEnvironmentVariable("CONN");

builder.Services.AddSingleton<Brass_IRepository>(sp => new Brass_SQLRepository(DB_connectionString, sp.GetRequiredService<ILogger<Brass_SQLRepository>>()));


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

app.UseCors("CorsPolicy");

app.MapControllers();

app.MapHub<ChatHub>("/chatsocket");


app.Run();

