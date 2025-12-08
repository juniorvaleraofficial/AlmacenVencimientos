# Sistema de Control de Inventario de Productos por Vencimiento

Sistema web para gestionar el inventario de un almacén con enfoque en el control de productos con **fecha de vencimiento**, priorizando los **lotes próximos a caducar** y apoyando la política **FEFO (First Expire, First Out)**.

Este proyecto fue desarrollado como parte de la asignatura **Proyecto Integrador**, utilizando tecnologías del ecosistema **.NET** y buenas prácticas de arquitectura **MVC**.
## Puedes abrir el Proyecto en 
[Sistema de AlmacenVencimientos](https://almacenvencimientos.onrender.com/)

---

## 🎯 Objetivo del proyecto

Reducir pérdidas por caducidad en el almacén mediante un sistema que permita:

- Registrar productos y sus lotes con fecha de vencimiento.
- Controlar entradas y salidas por lote.
- Generar alertas automáticas de productos próximos a vencer.
- Emitir reportes básicos para apoyar la toma de decisiones (rotación, descuentos, mermas).

---

## ✅ Funcionalidades principales (MVP)

- **Gestión de productos**
  - Alta, baja, consulta y modificación de productos.
  - Datos básicos: código, nombre, categoría, unidad, estado.

- **Gestión de lotes / entradas**
  - Registro de nuevas entradas de mercancía por lote.
  - Campos clave: producto, lote, cantidad, fecha de vencimiento, proveedor, ubicación.

- **Salidas de inventario**
  - Registro de salidas por:
    - Venta / despacho.
    - Ajuste.
    - Merma por caducidad.

- **Alertas de productos próximos a vencer**
  - Listado de productos que vencen en:
    - ≤ 7 días
    - 8–15 días
    - 16–30 días (rangos configurables).
  - Vista centralizada en el panel principal.

- **Reportes**
  - Reporte de inventario general por producto y lote.
  - Reporte de productos próximos a vencer.
  - (Opcional) Reporte de mermas por caducidad por período.

- **Usuarios y roles (básico)**
  - Rol **Administrador**: configuración, productos, umbrales de alerta.
  - Rol **Operador**: entradas, salidas y consulta de alertas.

---

## 🧱 Alcance del proyecto

El sistema se enfoca en un **almacén pequeño/mediano** que maneja productos con fecha de vencimiento (alimentos, medicamentos, cosméticos, etc.), con un alcance intencionalmente acotado para cumplir como **Proyecto Integrador**:

- No es un ERP completo.
- No incluye facturación ni módulo de compras (se consideran **futuras fases**).
- Se prioriza un **MVP funcional**: productos, lotes, salidas, alertas y reportes básicos.

---

## 🧰 Tecnologías utilizadas

**Backend / Framework**

- ASP.NET Core MVC (.NET 7/8)
- C#

**Base de datos y acceso a datos**

- SQL Server (Express / LocalDB en desarrollo).
- Entity Framework Core (Code-First + Migrations).

**Frontend**

- Razor Views.
- Bootstrap 5 para diseño responsivo.
- jQuery (mínimo) para interacciones simples.

**Herramientas de desarrollo**

- Visual Studio 2022 Community.
- Git + GitHub para control de versiones.

---

## 🏛️ Arquitectura del sistema

El proyecto sigue el patrón **Modelo–Vista–Controlador (MVC)**:

- **Modelo**
  - Entidades principales: `Producto`, `Lote`, `Entrada`, `Salida`, `Alerta`.
  - Contexto de datos con `DbContext` de Entity Framework Core.

- **Controladores**
  - `ProductosController`
  - `LotesController` / `EntradasController`
  - `SalidasController`
  - `AlertasController`
  - `ReportesController`

- **Vistas (Razor)**
  - Vistas para CRUD de productos.
  - Formularios para entradas/salidas.
  - Pantalla de alertas y reportes.

Opcionalmente se puede agregar una capa de **Servicios** para encapsular la lógica de negocio (por ejemplo, cálculo de alertas por fecha de vencimiento).

---

## 📋 Requisitos previos

Para ejecutar el proyecto localmente necesitas:

- Windows 10/11.
- Visual Studio 2022 Community con la carga de trabajo:
  - **Desarrollo ASP.NET y web**
- .NET SDK 7 u 8.
- SQL Server (Express / Developer / LocalDB).
- Cuenta en GitHub (opcional pero recomendado).

---

## 🚀 Instalación y configuración

1. **Clonar el repositorio**

   ```bash
   https://github.com/juniorvaleraofficial/AlmacenVencimientos.git
   ```
   ---

## 🚀 Abrir el proyecto

1. Abre el archivo de solución .sln con Visual Studio 2022.
2. Configurar la cadena de conexión
3. Edita appsettings.json:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=AlmacenVencimientosDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```
---
Aplicar migraciones de base de datos
En Package Manager Console:
```bash
Update-Database
```
---
Ejecutar el proyecto

Selecciona el proyecto web como Startup Project.

Presiona F5 o haz clic en Start Debugging.
🗂️ Estructura del proyecto
```bash
/ProyectoAlmacenVencimientos
 ├── ProyectoAlmacenVencimientos.Web/       # Proyecto ASP.NET Core MVC
 │   ├── Controllers/
 │   ├── Models/
 │   ├── Views/
 │   ├── wwwroot/
 │   ├── appsettings.json
 │   └── Program.cs
 ├── ProyectoAlmacenVencimientos.Tests/     # (Opcional) Pruebas unitarias
 └── README.md
```
---
🔄 Flujo principal de uso

Registrar productos
El administrador crea el catálogo de productos.

Registrar entradas por lote
El operador registra la entrada indicando: producto, lote, cantidad y fecha de vencimiento.

Revisión de alertas
El sistema calcula qué lotes están próximos a vencer según la configuración de días.
El encargado revisa el panel de alertas cada día.

Registrar salidas
Cuando se vende o se descarta un producto, se registra la salida por lote.
Se sigue la política FEFO (primero salen los productos que vencen antes).

Consultar reportes
Reportes de inventario y de productos próximos a vencer ayudan a tomar decisiones:

Promociones.

Descuentos.

Traslado entre almacenes (si aplica).
🧭 Roadmap / futuras mejoras

Integración con lectores de código de barras/QR.

Integración con módulos de compras/facturación.

Dashboard con gráficas de mermas por caducidad.

Envío de alertas por correo electrónico / WhatsApp.

Control de auditoría (quién registró qué y cuándo).
👨‍💻 Autor

Nombre: Junior Alexis Valera

Carrera: Ingeniería de Software

Asignatura: Proyecto Integrador – Sistema de Control de Inventario de Productos por Vencimiento
📜 Licencia Uso académico MIT GPL-3.0
