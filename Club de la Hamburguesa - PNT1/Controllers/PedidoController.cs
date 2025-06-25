using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_de_la_Hamburguesa___PNT1.Context;
using Club_de_la_Hamburguesa___PNT1.Models;

namespace Club_de_la_Hamburguesa___PNT1.Controllers
{
    public class PedidoController : Controller
    {
        private readonly UsuarioDatabaseContext _context;

        public PedidoController(UsuarioDatabaseContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid(); // No sos admin → acceso denegado
            }

            var usuarioDatabaseContext = _context.Pedidos.Include(p => p.Usuario);
            return View(await usuarioDatabaseContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.Include(u => u.Tarjeta).FirstOrDefault(u => u.Id == usuarioId);

            ViewBag.Hamburguesas = _context.Hamburguesas.ToList();
            ViewBag.UsuarioTieneTarjeta = usuario?.Tarjeta != null;
            ViewBag.Bebidas = Enum.GetValues(typeof(Bebida));
            ViewBag.Ingredientes = Enum.GetValues(typeof(Acompañamiento));

            return View(new Pedido());  
        }

        // POST: Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido, List<int> HamburguesasSeleccionadas)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.Include(u => u.Tarjeta).FirstOrDefault(u => u.Id == usuarioId);

            bool usuarioTieneTarjeta = usuario?.Tarjeta != null;

            if (pedido.MetodoEntrega == Entrega.EnvioDomicilio && string.IsNullOrWhiteSpace(pedido.Direccion))
                ModelState.AddModelError("Direccion", "Debe ingresar una dirección si elige Envío a domicilio.");

            if (HamburguesasSeleccionadas == null || !HamburguesasSeleccionadas.Any())
                ModelState.AddModelError("", "Debe seleccionar al menos una hamburguesa.");

            if (pedido.MetodoPago == Pago.Tarjeta && !usuarioTieneTarjeta)
            {
                var tarjeta = new Tarjeta();
                tarjeta.setNumero(Request.Form["Tarjeta_Numero"]);
                tarjeta.setTitular(Request.Form["Tarjeta_Titular"]);
                tarjeta.setAnioVencimiento(int.Parse(Request.Form["Tarjeta_AnioVencimiento"]));
                tarjeta.setCodigoSeguridad(int.Parse(Request.Form["Tarjeta_CodigoSeguridad"]));
                usuario.Tarjeta = tarjeta;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Hamburguesas = _context.Hamburguesas.ToList();
                ViewBag.UsuarioTieneTarjeta = usuarioTieneTarjeta;
                ViewBag.Bebidas = Enum.GetValues(typeof(Bebida));
                ViewBag.Ingredientes = Enum.GetValues(typeof(Acompañamiento));
                return View(pedido);
            }

            pedido.UsuarioId = usuarioId.Value;
            pedido.Estado = Estado.EnPreparacion;
            pedido.Fecha = DateTime.Now;
            pedido.Items = new List<Item>();

            foreach (int id in HamburguesasSeleccionadas)
            {
                var hamburguesa = _context.Hamburguesas.Find(id);
                if (hamburguesa != null)
                {
                    int cantidad = int.Parse(Request.Form["Cantidad_" + id]);
                    if (hamburguesa.Stock < cantidad)
                    {
                        ModelState.AddModelError("", "No hay suficiente stock para " + hamburguesa.Nombre);
                        ViewBag.Hamburguesas = _context.Hamburguesas.ToList();
                        return View(pedido);
                    }

                    hamburguesa.DescontarStock(cantidad);

                    pedido.Items.Add(new Item
                    {
                        HamburguesaId = id,
                        Hamburguesa = hamburguesa,
                        Cantidad = cantidad
                    });
                }
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmacion", new { id = pedido.Id });
        }
        public async Task<IActionResult> Confirmacion(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Items)
                    .ThenInclude(i => i.Hamburguesa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        public IActionResult Historial()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            var pedidos = _context.Pedidos
                .Include(p => p.Items)
                    .ThenInclude(i => i.Hamburguesa)
                .Include(p => p.Usuario) 
                .Where(p => p.UsuarioId == usuarioId)
                .ToList();

            return View(pedidos);
        }

        // GET: Pedido/CambiarEstado/5
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid(); // No sos admin
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            // Mostrar vista con opciones de estado
            ViewBag.Estados = Enum.GetValues(typeof(Estado));
            return View(pedido);
        }

        // POST: Pedido/CambiarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, Estado nuevoEstado)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            // Usás tu clase Administrador para aplicar la lógica:
            var admin = new Administrador(usuario.Nombre);
            admin.CambiarEstadoPedido(pedido, nuevoEstado);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MetodoEntrega,MetodoPago,Direccion,Estado,UsuarioId,Fecha")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
