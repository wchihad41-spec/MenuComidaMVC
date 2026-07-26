/* Autor: Edgar Eduardo Barreto Hernández
 fecha: 20-07-2026
Se agrego el regitro de los pedidos vinculado con el nomnbre del usuario para que se quede en el historial del usuario*/
using MenuComidaMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. REGISTRAR PEDIDOSERVICE 
builder.Services.AddSingleton<PedidoService>();

// 2. REGISTRAR SERVICIO DE SESIÓN
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// ACTIVAR EL MIDDLEWARE DE SESIÓN
app.UseSession();

app.UseAuthorization();

// Ruta por defecto hacia Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pedido}/{action=Login}/{id?}");

app.Run();