using Microsoft.EntityFrameworkCore;
using Task_tracker.Application.Services;
using Task_tracker.Infrastructure.Database;
using Task_tracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});

builder.Services.AddScoped<SpaceRepository>();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<AccountRepository>();

builder.Services.AddScoped<SpaceService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<AccountService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();