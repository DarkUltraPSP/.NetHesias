using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContext, MyDataContext>(
    o => o.UseSqlite("Data Source=C:\\DotNetHesias\\MaDb.db;")
    );

var app = builder.Build();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
