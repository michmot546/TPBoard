using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Repositories;
using TPBoardWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<ITableElementService, TableElementService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<TPBoardDbContext>();

builder.Services.AddDbContext<TPBoardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
