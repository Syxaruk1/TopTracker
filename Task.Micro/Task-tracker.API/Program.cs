using Microsoft.EntityFrameworkCore;
using Task_tracker.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();