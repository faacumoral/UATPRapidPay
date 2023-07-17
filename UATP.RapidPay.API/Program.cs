using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UATP.RapidPay.API;
using UATP.RapidPay.API.HostedServices;
using UATP.RapidPay.Core.Services;
using UATP.RapidPay.DAL.EfModels;
using UATP.RapidPay.Interfaces.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICardService, CardService>();

builder.AddJwtManager();
builder.InitConfig();

builder.Services.AddHostedService<PaymentFeeHostedService>();

builder.Services.AddDbContext<RapidPayContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("RapidPay"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
