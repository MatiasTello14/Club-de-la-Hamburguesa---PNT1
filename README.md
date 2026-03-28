🍔 El Club de la Hamburguesa - E-commerce System
Un sistema integral de gestión de pedidos para locales gastronómicos, desarrollado bajo la arquitectura ASP.NET MVC. Este proyecto simula el flujo completo de una tienda real, desde la gestión de usuarios hasta la facturación final.

🎥 Video de Demostración

https://github.com/user-attachments/assets/07722260-965a-4fa7-98ef-56eb1c895046


💎 Características Principales
🛒 Experiencia de Compra Completa: Flujo dinámico que incluye selección de productos con adicionales (bebidas, ingredientes extra), carrito de compras y elección de método de envío o retiro.

👤 Gestión de Usuarios (CRUD): Sistema de registro e inicio de sesión con persistencia de perfiles y seguimiento detallado de pedidos anteriores.

💳 Pasarela de Pago y Entrega: Lógica integrada para validación de métodos de pago y actualización de estados del pedido en tiempo real.

📑 Facturación Automática: Generación de tickets de compra tras la confirmación, detallando productos y montos finales.

🗄️ Persistencia Robusta: Modelado de datos profesional utilizando Entity Framework y SQL Server.

🛠️ Stack Tecnológico
Lenguaje: C# con .NET Core (Compatible con .NET 8/9/10).

Arquitectura: ASP.NET MVC con motor de vistas Razor.

ORM: Entity Framework Core (Migrations).

Base de Datos: SQL Server.

Frontend: HTML5, CSS3, JavaScript, Bootstrap.

🚀 Instalación y Ejecución
Para correr este proyecto localmente, seguí estos pasos:

Clonar el repositorio: git clone https://github.com/MatiasTello14/Club-de-la-Hamburguesa---PNT1.git

Configurar la Base de Datos: Editar el archivo appsettings.json y actualizar la cadena de conexión (ConnectionString) con tu instancia local de SQL Server.

Generar las tablas: Abrir la Consola del Administrador de Paquetes NuGet en Visual Studio y ejecutar:
Update-Database

Ejecutar: Presionar F5 o el botón de Play en Visual Studio.

📂 Estructura del Proyecto
Models: Definición de entidades de dominio (Hamburguesas, Bebidas, Usuarios, Pedidos).

Views: Interfaces dinámicas e interactivas desarrolladas con Razor.

Controllers: Lógica de negocio, manejo de sesiones y procesamiento de peticiones.

Data: Contexto de base de datos (DbContext) y control de versiones de esquema mediante migraciones.

Proyecto realizado para la materia Programación en Nuevas Tecnologías 1.
