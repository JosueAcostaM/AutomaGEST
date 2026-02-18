using Servicios_AutomaG.Interfaces;
using Modelos_AutomaG;
using API_AutomaG;
using Servicios_AutomaG;
using API_Consumer;

CRUD<Usuarios>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Usuarios";
CRUD<Aspirantes>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Aspirantes";
CRUD<CamposConocimiento>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/CamposConocimientos";
CRUD<Programas>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Programas";
CRUD<Niveles>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Niveles";
CRUD<Modalidades>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Modalidades";
CRUD<Precios>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Precios";
CRUD<Roles>.EndPoint = "https://automagestapi-a6fsfueugkbrc0ez.canadacentral-01.azurewebsites.net/api/Roles";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication("Cookies") //cokies
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = "/Login/Index"; // Ruta de inicio de sesión


                });
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   // pattern: "{controller=Home}/{action=Index}/{id?}");
  pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
