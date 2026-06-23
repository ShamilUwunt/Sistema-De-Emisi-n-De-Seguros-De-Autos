# Sistema de Emisión de Seguros de Auto — Seguros LAFISE S.A.

Prototipo funcional del módulo de emisión de pólizas de automóviles. Permite capturar los datos del cliente, el vehículo y las coberturas para generar una póliza única, con la `PrimaTotal` calculada en el servidor.

## Stack

- **C# / .NET 9** (ASP.NET Core Web API)
- **Entity Framework Core 9** (Code First, con migraciones)
- **SQL Server** (probado en SQL Server Express)
- **Swagger** para probar la API desde el navegador
- Frontend simple en HTML (`wwwroot/index.html`) para usarlo sin Postman

## Arquitectura (separación de responsabilidades)

```
src/SegurosLafise.Api/
├── Controllers/      → Reciben las peticiones HTTP y devuelven los códigos correctos
│   ├── CatalogosController.cs
│   └── PolizasController.cs
├── Services/         → Lógica de negocio (cálculo de prima, número de póliza, validaciones)
│   ├── CatalogoService.cs
│   ├── PolizaService.cs
│   └── NegocioException.cs
├── Repositories/     → Acceso a datos (consultas con EF Core)
├── Data/             → AppDbContext (configuración EF y datos semilla)
├── Models/           → Entidades (Cliente, Vehiculo, Cobertura, Poliza, PolizaCobertura)
├── Dtos/             → Objetos de entrada/salida de la API
└── wwwroot/          → Frontend HTML
```

Flujo: **Controller → Service → Repository → Base de datos**.

## Modelo de datos

| Tabla | Campos principales |
|---|---|
| **Clientes** | Id, Nombre, Identificación (DNI/RUC), Correo |
| **Vehiculos** | Id, Placa, Marca, Modelo, Año, ValorComercial |
| **Coberturas** | Id, Nombre, MontoCobertura |
| **Polizas** | Id, NumeroPoliza (autogenerado), ClienteId (FK), VehiculoId (FK), FechaEmision, SumaAsegurada, PrimaTotal |
| **PolizaCoberturas** | Tabla intermedia (N:N) entre Polizas y Coberturas |

- `NumeroPoliza` tiene índice **único** y se genera en el servidor con el formato `POL-{año}-{correlativo}` (ej: `POL-2026-00001`).
- La base trae **datos semilla**: 3 clientes y 5 coberturas.

## Requisitos previos

- [.NET SDK 9](https://dotnet.microsoft.com/download) (o superior)
- **SQL Server** (Express, Developer o LocalDB)
- (Opcional) [Postman](https://www.postman.com/) para probar los endpoints

## Configuración de la base de datos

La cadena de conexión está en `src/SegurosLafise.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YAMILDESK\\SQLEXPRESS;Database=SegurosLafiseDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> ⚠️ **Cambiá `Server=YAMILDESK\SQLEXPRESS` por el nombre de tu servidor SQL.**
> Ejemplos comunes: `localhost`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`.

Tenés **dos formas** de crear la base de datos. Usá la que prefieras:

### Opción A — Migraciones de EF Core (recomendada)

Desde la carpeta del proyecto:

```bash
cd src/SegurosLafise.Api
dotnet tool install --global dotnet-ef   # solo si no lo tenés instalado
dotnet ef database update
```

Esto crea la base `SegurosLafiseDb`, todas las tablas y los datos semilla.

### Opción B — Script SQL

Ejecutá el script `database/script.sql` en tu servidor (SSMS, Azure Data Studio o `sqlcmd`). El script crea las tablas y carga los datos semilla.

```bash
sqlcmd -S TU_SERVIDOR -E -d SegurosLafiseDb -i database/script.sql
```

(Creá primero la base vacía `SegurosLafiseDb` si no existe.)

## Cómo ejecutar la API

```bash
cd src/SegurosLafise.Api
dotnet run
```

La API queda escuchando en **http://localhost:5222**.

Una vez levantada, podés usar:

- **Frontend:** http://localhost:5222/
- **Swagger:** http://localhost:5222/swagger
- **API:** http://localhost:5222/api/polizas

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/api/catalogos/clientes` | Lista de clientes |
| `GET` | `/api/catalogos/coberturas` | Lista de coberturas con su monto |
| `POST` | `/api/polizas/emitir` | Emite una póliza (calcula la prima en el servidor) |
| `GET` | `/api/polizas` | Historial de todas las pólizas |
| `GET` | `/api/polizas/{id}` | Detalle de una póliza |

### Ejemplo de emisión

`POST /api/polizas/emitir`

```json
{
  "clienteId": 1,
  "vehiculo": {
    "placa": "M123456",
    "marca": "Toyota",
    "modelo": "Corolla",
    "anio": 2020,
    "valorComercial": 18000
  },
  "coberturasIds": [1, 3, 5]
}
```

Respuesta `201 Created`:

```json
{
  "id": 1,
  "numeroPoliza": "POL-2026-00001",
  "fechaEmision": "2026-06-23T08:47:46",
  "sumaAsegurada": 18000,
  "primaTotal": 21000.00,
  "clienteNombre": "Juan Perez",
  "vehiculoPlaca": "M123456",
  "coberturas": [
    { "id": 1, "nombre": "Robo Total", "montoCobertura": 15000.00 },
    { "id": 3, "nombre": "Responsabilidad Civil", "montoCobertura": 5000.00 },
    { "id": 5, "nombre": "Asistencia en Carretera", "montoCobertura": 1000.00 }
  ]
}
```

> La `PrimaTotal` se calcula en el servidor como la **suma de los montos de las coberturas** seleccionadas (15000 + 5000 + 1000 = 21000). La `SumaAsegurada` es el valor comercial del vehículo.

### Manejo de errores

| Situación | Código HTTP |
|---|---|
| Póliza emitida correctamente | `201 Created` |
| Datos inválidos / sin coberturas / cobertura inexistente | `400 Bad Request` |
| Cliente o póliza no encontrados | `404 Not Found` |

## Probar con Postman

Importá el archivo `postman/SegurosLafise.postman_collection.json`. Incluye todos los endpoints (casos correctos y de error). La variable `{{baseUrl}}` ya apunta a `http://localhost:5222`; cambiala si usás otro puerto.

## Estructura del repositorio

```
PruebaTecnica/
├── src/SegurosLafise.Api/      → Proyecto Web API
├── database/script.sql         → Script de creación de la base de datos
├── postman/                    → Colección de Postman
├── SegurosLafise.sln           → Solución
└── README.md
```
