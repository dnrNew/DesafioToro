using DesafioToro.Application.Services;
using DesafioToro.Domain.User;
using DesafioToro.Repository;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(_ => new MySqlConnection(builder.Configuration["ConnectionStrings:MySqlConnect"]));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserApplicationService, UserApplicationService>();

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
