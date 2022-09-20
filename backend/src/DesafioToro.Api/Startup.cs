using DesafioToro.Application.Services;
using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.Users;
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
builder.Services.AddTransient<IOrderApplicationService, OrderApplicationService>();

var desafioToroOrigins = "DesafioToroOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: desafioToroOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(desafioToroOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
