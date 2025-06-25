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
    public class HamburguesaController : Controller
    {
        private readonly UsuarioDatabaseContext _context;

        public HamburguesaController(UsuarioDatabaseContext context)
        {
            _context = context;
        }

        // GET: Hamburguesa
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            // Si no está logueado o no es admin -> Prohibir acceso
            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            return View(await _context.Hamburguesas.ToListAsync());
        }

        // GET: Hamburguesa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamburguesa == null)
            {
                return NotFound();
            }

            return View(hamburguesa);
        }

        // GET: Hamburguesa/Create
        public IActionResult Create()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            return View();
        }

        // POST: Hamburguesa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Stock,ImagenUrl")] Hamburguesa hamburguesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hamburguesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hamburguesa);
        }

        // GET: Hamburguesa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas.FindAsync(id);
            if (hamburguesa == null)
            {
                return NotFound();
            }
            return View(hamburguesa);
        }

        // POST: Hamburguesa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Stock,ImagenUrl")] Hamburguesa hamburguesa)
        {
            if (id != hamburguesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var original = await _context.Hamburguesas.FindAsync(id);
                    if (original == null) return NotFound();

                    original.Nombre = hamburguesa.Nombre;
                    original.Descripcion = hamburguesa.Descripcion;
                    original.Precio = hamburguesa.Precio;
                    original.Stock = hamburguesa.Stock;
                    original.ImagenUrl = hamburguesa.ImagenUrl;

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamburguesaExists(hamburguesa.Id)) return NotFound();
                    else throw;
                }
            }
            return View(hamburguesa);
        }

        // GET: Hamburguesa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null || !usuario.EsAdmin)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamburguesa == null)
            {
                return NotFound();
            }

            return View(hamburguesa);
        }

        // POST: Hamburguesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hamburguesa = await _context.Hamburguesas.FindAsync(id);
            if (hamburguesa != null)
            {
                _context.Hamburguesas.Remove(hamburguesa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HamburguesaExists(int id)
        {
            return _context.Hamburguesas.Any(e => e.Id == id);
        }
    }
}
