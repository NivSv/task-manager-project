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

builder.Services.AddScoped<IPriorityDAL, PriorityDAL>();
builder.Services.AddScoped<IPriorityBL, PriorityBL>();
builder.Services.AddScoped<IStatusDAL, StatusDAL>();
builder.Services.AddScoped<IStatusBL, StatusBL>();
builder.Services.AddScoped<ITaskDAL, TaskDAL>();
builder.Services.AddScoped<ITaskBL, TaskBL>();
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddScoped<IUserBL, UserBL>();

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
