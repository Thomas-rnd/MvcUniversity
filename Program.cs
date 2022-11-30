using System.Text.Json.Serialization;
using MvcUniversity.Data;
using MvcUniversity.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Ignore circular references when serializing objects into JSON
// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/preserve-references?pivots=dotnet-6-0
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Attach an EF Core database context to each query
builder.Services.AddDbContext<UniversityContext>();

var app = builder.Build();

// Seed data into DB
SeedData.Init();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
