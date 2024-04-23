using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocketChat.BLL.Logic;
using SocketChat.Common.Entities;
using SocketChat.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IMessageLogic, MessageLogic>();
//builder.Services.AddScoped<IMessageRepository, MessageRepository>();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatContext>(options =>
options.UseNpgsql(connection).UseLazyLoadingProxies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("/chat");   

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

