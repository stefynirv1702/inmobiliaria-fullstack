# Test Million

Este proyecto consta de un **backend** en .NET 8, un **frontend** en React y una base de datos **SQL Server**. Incluye funcionalidades para gestionar **Owners** y **Properties**, así como su historial de transacciones (**PropertyTrace**).

## BD
se construyo en sql server.
Para la base de datos se tiene un .bak con la estructura de la bd y con algunos datos

---

## Backend

El backend está desarrollado con **.NET 8** y expone un API REST para la gestión de Owners, Properties y PropertyTrace, se desarrollo con una arquitectura DDD en donde encontraremos 4 capas, además de esto se tiene una carpeta de test.
Revisar la cadena de conexión hacia la BD en appsettings.json

### Ejecutar el backend

```bash
# Ir a la carpeta del backend
cd backend

# Restaurar dependencias
dotnet restore

# Construir proyecto
dotnet build

# Ejecutar backend
dotnet run
```

Por defecto se levantará en https://localhost:7087 (Swagger estará disponible en https://localhost:7087/swagger).

### Ejecutar el Frontend
cambiar en .env el puerto para conectarse al backend.
Abrir una terminal en la carpeta del frontend.


Instalar dependencias:
```
npm i
```
Ejecuta el proyecto:
```
npm run dev
```
Revisar el puerto en donde estará disponible el front


