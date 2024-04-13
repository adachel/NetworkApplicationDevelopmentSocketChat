using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocketChat.BLL.Logic;
using SocketChat.Common.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();  // добавили в проект поддержку SignalR

builder.Services.AddScoped<IUserLogic, UserLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("/chat");   // добавили маршрут до вебсокета

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





//var people = new List<User>
// {
//    new User("aaa", "111"),
//    new User("qqq", "111")
//};

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = AuthOptions.ISSUER,
//        ValidateAudience = true,
//        ValidAudience = AuthOptions.AUDIENCE,
//        ValidateLifetime = true,
//        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//        ValidateIssuerSigningKey = true
//    };

//    options.Events = new JwtBearerEvents
//    {
//        OnMessageReceived = context =>
//        {
//            var accessToken = context.Request.Query["access_token"];

//            // если запрос направлен хабу
//            var path = context.HttpContext.Request.Path;
//            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
//            {
//                // получаем токен из строки запроса
//                context.Token = accessToken;
//            }
//            return Task.CompletedTask;
//        }
//    };
//});

//builder.Services.AddSignalR();

//var app = builder.Build();


//app.UseDefaultFiles();
////app.UseStaticFiles();

//app.UseAuthentication();   // добавление middleware аутентификации 
//app.UseAuthorization();   // добавление middleware авторизации 


//// Для непосредственной аутентификации пользователя и выдачи токена определена конечная точка app.MapPost("/login"),
//// которая через post-запрос получает логин и пароль пользователя и и возвращает токен и имя пользователя.

//app.MapPost("/login", (User loginModel) =>
//{
//    // находим пользователя 
//    User? person = people.FirstOrDefault(p => p.Email == loginModel.Email && p.Password == loginModel.Password);

//    // если пользователь не найден, отправляем статусный код 401
//    if (person is null) return Results.Unauthorized();

//    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email!) };

//    // создаем JWT-токен
//    var jwt = new JwtSecurityToken(
//            issuer: AuthOptions.ISSUER,
//            audience: AuthOptions.AUDIENCE,
//            claims: claims,
//            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
//            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
//    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

//    // формируем ответ
//    var response = new
//    {
//        access_token = encodedJwt,
//        username = person.Email
//    };

//    return Results.Json(response);
//});


//app.MapHub<ChatHub>("/chat");
//app.Run();


