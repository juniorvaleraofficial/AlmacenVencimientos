using AlmacenVencimientos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// En PRODUCCIÓN (Render) usar el puerto que nos da la variable PORT
if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// CONFIGURAR LA BD SEGÚN EL ENTORNO

if (builder.Environment.IsDevelopment())
{
    // En tu PC: SQL Server normal (lo que ya tenías)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    // En Render: BD EN MEMORIA (no necesita servidor de SQL)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("AlmacenVencimientosDb"));
}

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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