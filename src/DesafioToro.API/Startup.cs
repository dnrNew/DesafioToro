using DesafioToro.Application.Services;
using DesafioToro.Domain.Stock;
using DesafioToro.Domain.User;
using DesafioToro.Repository;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(_ => new MySqlConnection(builder.Configuration["ConnectionStrings:MySqlConnect"]));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();

builder.Services.AddTransient<IUserApplicationService, UserApplicationService>();
builder.Services.AddTransient<IStockApplicationService, StockApplicationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
