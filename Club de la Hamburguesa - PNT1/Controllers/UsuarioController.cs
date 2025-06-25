using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_de_la_Hamburguesa___PNT1.Context;
using Club_de_la_Hamburguesa___PNT1.Models;
using Club_de_la_Hamburguesa___PNT1.Migrations;

namespace Club_de_la_Hamburguesa___PNT1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDatabaseContext _context;

        public UsuarioController(UsuarioDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(string email, string contraseña)
        {

            if (ModelState.IsValid)
            {
                var usuarios = _context.Usuarios.ToList();
                Usuario usuarioExistente = null;
                int i = 0;
                bool encontrado = false;

                while (i < usuarios.Count && !encontrado)
                {
                    var u = usuarios[i];
                    if (u.Email.ToLower() == email.ToLower() && u.Contraseña == contraseña)
                    {
                        usuarioExistente = u;
                        encontrado = true;
                    }
                    i++;
                }
                if (usuarioExistente == null)
                {
                    ModelState.AddModelError("", "No existe un usuario con ese correo.");
                    return View();
                }

                if (usuarioExistente.Contraseña != contraseña)
                {
                    ModelState.AddModelError("", "Contraseña incorrecta.");
                    return View();
                }

                HttpContext.Session.SetInt32("UsuarioId", usuarioExistente.Id);
                HttpContext.Session.SetString("NombreUsuario", usuarioExistente.Nombre);
                HttpContext.Session.SetString("EsAdmin", usuarioExistente.EsAdmin.ToString().ToLower());

                return RedirectToAction("Index", "Home", new { usuarioId = usuarioExistente.Id });
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Borra toda la sesión
            return RedirectToAction("Login", "Usuario"); // Vuelve al Home o Login
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            IActionResult resultado = View(_context.Usuarios.ToList());
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                resultado = RedirectToAction("Login");
            }

            return resultado;
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Contraseña")]
                Usuario usuario,
                string? Tarjeta_Numero,
                string? Tarjeta_Titular,
                int? Tarjeta_AnioVencimiento,
                int? Tarjeta_CodigoSeguridad)
        {
            
            var usuarios = _context.Usuarios.ToList();
            int i = 0;
            bool emailExiste = false;

            while (i < usuarios.Count && !emailExiste)
            {
                if (usuarios[i].Email.ToLower() == usuario.Email.ToLower())
                {
                    emailExiste = true;
                }
                i++;
            }

            if (emailExiste)
            {
                ModelState.AddModelError("Email", "El email ya está registrado.");
                return View(usuario);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //  Validando usando los setters de las clases
                    usuario.setNombre(usuario.Nombre);
                    usuario.setEmail(usuario.Email);
                    usuario.setContraseña(usuario.Contraseña);

                    // Validar y crear la Tarjeta solo si tiene datos
                    bool tarjetaCompleta =
                        !string.IsNullOrWhiteSpace(Tarjeta_Numero) ||
                        !string.IsNullOrWhiteSpace(Tarjeta_Titular) ||
                        Tarjeta_AnioVencimiento.HasValue ||
                        Tarjeta_CodigoSeguridad.HasValue;

                    if (tarjetaCompleta)
                    {
                        if (string.IsNullOrWhiteSpace(Tarjeta_Numero) ||
                            string.IsNullOrWhiteSpace(Tarjeta_Titular) ||
                            !Tarjeta_AnioVencimiento.HasValue ||
                            !Tarjeta_CodigoSeguridad.HasValue)
                        {
                            ModelState.AddModelError("Tarjeta", "Completá todos los campos de la tarjeta o dejalos todos vacíos.");
                            return View(usuario);
                        }

                        var tarjeta = new Tarjeta();
                        tarjeta.setNumero(Tarjeta_Numero);
                        tarjeta.setTitular(Tarjeta_Titular);
                        tarjeta.setAnioVencimiento(Tarjeta_AnioVencimiento.Value);
                        tarjeta.setCodigoSeguridad(Tarjeta_CodigoSeguridad.Value);

                        usuario.Tarjeta = tarjeta;
                    }

                    // Guardar en bbdd
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Usuario");
                }
                catch (Exception ex)
                {
                    // Capturar cualquier error y lo muestra
                    ModelState.AddModelError("", ex.Message);
                    return View(usuario);
                }
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Contraseña")] Usuario usuario)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
