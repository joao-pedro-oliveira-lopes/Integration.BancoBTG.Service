using Integration.BancoBTG.Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Integration.BancoBTG.Service.Messaging;
using Integration.BancoBTG.Service.Models;
using System.Text.Json;

static void Main() { }

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BancoBtgDb")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSingleton<RabbitMQConsumer>();
builder.Services.AddSingleton<RabbitMQProducer>();
builder.Services.Configure<RabbitMQConfig>(builder.Configuration.GetSection("RabbitMQConfig"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

EnviarDadosMockParaFila(app.Services.GetService<RabbitMQProducer>());
var rabbitMQConsumer = app.Services.GetService<RabbitMQConsumer>();
rabbitMQConsumer?.StartConsuming();

app.UseAuthentication();
app.UseAuthorization();

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

void EnviarDadosMockParaFila(RabbitMQProducer producer)
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "pedidosMock.json");
    if (File.Exists(filePath))
    {
        var json = File.ReadAllText(filePath);
        var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json);

        if (pedidos != null)
        {
            foreach (var pedido in pedidos)
            {
                var message = JsonSerializer.Serialize(pedido);
                producer.SendMessage(message);
            }
        }
    }
}
