using MenuComidaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using MenuComidaMVC.Models;

namespace MenuComidaMVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // --- PANTALLA DE LOGIN ---
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string nombreCliente, string refugio)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                TempData["Error"] = "Ingresa tu nombre para acceder.";
                return View();
            }

          
            HttpContext.Session.SetString("UsuarioNombre", nombreCliente.Trim());
            HttpContext.Session.SetString("UsuarioRefugio", string.IsNullOrWhiteSpace(refugio) ? "Sin Refugio" : refugio.Trim());

            return RedirectToAction(nameof(Crear));
        }

        // --- CREAR PEDIDO ---
        [HttpGet]
        public IActionResult Crear()
        {
            var nombre = HttpContext.Session.GetString("UsuarioNombre");
            var refugio = HttpContext.Session.GetString("UsuarioRefugio");

            if (string.IsNullOrEmpty(nombre))
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.UsuarioNombre = nombre;
            ViewBag.UsuarioRefugio = refugio;
            ViewBag.UsuarioActual = $"{nombre} ({refugio})";

            var productos = Catalogoproductos.ObtenerProductos();
            return View(productos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(string nombreCliente, List<string>? nombres, List<decimal>? precios, List<int>? cantidades)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                TempData["Error"] = "Escribe tu nombre para identificar el pedido.";
                return RedirectToAction(nameof(Crear));
            }

            var items = new List<ItemPedido>();

            if (nombres is not null && precios is not null && cantidades is not null)
            {
                for (int i = 0; i < nombres.Count; i++)
                {
                    if (i < cantidades.Count && cantidades[i] > 0)
                    {
                        items.Add(new ItemPedido
                        {
                            Nombre = nombres[i],
                            Precio = i < precios.Count ? precios[i] : 0,
                            Cantidad = cantidades[i]
                        });
                    }
                }
            }

            if (items.Count == 0)
            {
                TempData["Error"] = "Selecciona al menos un producto para tu pedido.";
                return RedirectToAction(nameof(Crear));
            }

            var pedido = _pedidoService.Crear(nombreCliente.Trim(), items);

            TempData["Confirmacion"] = $"Pedido #{pedido.Id} registrado. ¡Gracias, {pedido.NombreCliente}!";

            return RedirectToAction(nameof(Lista));
        }

        // --- LISTA DE PEDIDOS ---
        [HttpGet]
        public IActionResult Lista()
        {
            var pedidos = _pedidoService.ObtenerTodos();
            return View(pedidos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarEstado(int id, string estado)
        {
            _pedidoService.CambiarEstado(id, estado);
            return RedirectToAction(nameof(Lista));
        }

        // --- RECOMENDACIONES ---
        [HttpGet]
        public IActionResult ObtenerRecomendaciones(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                return Json(new List<object>());
            }

            var busqueda = nombreCliente.Trim().ToLower();
            var pedidos = _pedidoService.ObtenerTodos();

            var recomendaciones = pedidos
                .Where(p => !string.IsNullOrEmpty(p.NombreCliente) &&
                           (p.NombreCliente.Trim().ToLower().Contains(busqueda) || busqueda.Contains(p.NombreCliente.Trim().ToLower())))
                .SelectMany(p => p.Items)
                .GroupBy(i => new { i.Nombre, i.Precio })
                .OrderByDescending(g => g.Sum(i => i.Cantidad))
                .Take(3)
                .Select(g => new
                {
                    nombre = g.Key.Nombre,
                    precio = g.Key.Precio
                })
                .ToList();

            return Json(recomendaciones);
        }
    }
}