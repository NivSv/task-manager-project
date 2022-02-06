using Newtonsoft.Json;
using TaskManagerBackend.BL;
using TaskManagerBackend.DAL;
using System;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
};

builder.Services.AddSingleton<IPriorityDAL, PriorityDAL>();
builder.Services.AddSingleton<IPriorityBL, PriorityBL>();
builder.Services.AddSingleton<IStatusDAL, StatusDAL>();
builder.Services.AddSingleton<IStatusBL, StatusBL>();
builder.Services.AddSingleton<ITaskDAL, TaskDAL>();
builder.Services.AddSingleton<ITaskBL, TaskBL>();
builder.Services.AddSingleton<IUserDAL, UserDAL>();
builder.Services.AddSingleton<IUserBL, UserBL>();
builder.Services.AddSingleton<IAccessKeyManager,AccessKeyManager>();
builder.Services.AddSingleton<IUserAuthorizer, UserAuthorizer>();

builder.Services.AddRouting(options => options.LowercaseUrls=true);
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

app.Run();
