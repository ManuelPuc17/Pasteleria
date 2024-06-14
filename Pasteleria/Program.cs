using Pasteleria.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar los servicios
builder.Services.AddScoped<AccesoService>();
builder.Services.AddHttpClient<PastelService>();
builder.Services.AddHttpClient<CalificacionService>();

builder.Services.AddHttpClient<AccesoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7014/");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.Name = "UsuarioLogueado";
        config.LoginPath = "/Acceso/Login";
    });

// Agregar soporte para sesiones
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "notAuthenticated",
    pattern: "Home/NotAuthenticated",
    defaults: new { controller = "Home", action = "NotAuthenticated" });

app.MapControllerRoute(
    name: "operationFailed",
    pattern: "Home/OperationFailed",
    defaults: new { controller = "Home", action = "OperationFailed" });

app.MapControllerRoute(
    name: "invalidOperation",
    pattern: "Home/invalidOperation",
    defaults: new { controller = "Home", action = "invalidOperation" });

app.Run();
