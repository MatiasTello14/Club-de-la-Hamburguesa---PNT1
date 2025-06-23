using Club_de_la_Hamburguesa___PNT1.Context;
using Club_de_la_Hamburguesa___PNT1.Models;
using Microsoft.EntityFrameworkCore;

namespace Club_de_la_Hamburguesa___PNT1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UsuarioDatabaseContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:UsuarioDBConnection"]));


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60); 
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UsuarioDatabaseContext>();

                if (!context.Hamburguesas.Any())
                {
                    var burgers = new List<Hamburguesa>
        {
            new Hamburguesa
            {
                Nombre = "Explosiva",
                Descripcion = "Hamburguesa con bacon y aros de cebolla",
                Precio = 10000,
                Stock = 20,
                ImagenUrl = "imagenes/burger-bacon-arosCebolla.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Mega",
                Descripcion = "Con bacon crocante",
                Precio = 12000,
                Stock = 15,
                ImagenUrl = "imagenes/burger-mega-4-.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Bacon XL",
                Descripcion = "Bacon y papas",
                Precio = 13000,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-bacon-papas-pay.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Coleslaw",
                Descripcion = "Hamburguesa con Coleslaw",
                Precio = 11500,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-completa-con-colslaw.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Simple JyQ",
                Descripcion = "Hamburguesa simple con Jamon y Queso",
                Precio = 12000,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-jamon-y-queso.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Simple",
                Descripcion = "Hambuguesa simple",
                Precio = 10000,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-simple.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Queso y Pepinillos",
                Descripcion = "Hamburguesa con Queso y Pepinillos",
                Precio = 10500,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-simple-queso-y-pepinillos.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Sin Gluten",
                Descripcion = "Hamburguesa sin Gluten",
                Precio = 12200,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-sin-gluten.jpeg"
            },
            new Hamburguesa
            {
                Nombre = "Vegana",
                Descripcion = "Hamburguesa vegana",
                Precio = 12200,
                Stock = 10,
                ImagenUrl = "imagenes/burguer-vegana.jpg"
            },
            new Hamburguesa
            {
                Nombre = "Super Completa",
                Descripcion = "Hamburguesa super completa",
                Precio = 14000,
                Stock = 10,
                ImagenUrl = "imagenes/doble_completa.jpg"
            },
        };

                    context.Hamburguesas.AddRange(burgers);
                    context.SaveChanges();
                }
            }

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
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Login}/{id?}");

            app.Run();
        }
    }
}