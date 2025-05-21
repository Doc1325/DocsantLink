using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shortifier.Services;
using WebApplication2.Data;
using WebApplication2.Services;
var builder = WebApplication.CreateBuilder(args);   
builder.Services.AddDbContext<ShortDBContext>(options =>
    options.UseSqlServer(builder.Configuration["Database"] ?? throw new InvalidOperationException("Connection string 'DBContext' not found."))); // Utilizo un secreto para el connection string
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUrlsService, UrlsService>();

    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();

    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShortUrls}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
