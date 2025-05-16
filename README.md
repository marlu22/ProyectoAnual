# Sistema de Gestión de Usuarios

## 1. Descripción  
Sistema de escritorio (.NET 8, WinForms/WPF) con arquitectura N-Capas para ABM de usuarios, autenticación SHA-256 y políticas de seguridad.

## 2. Tecnologías  
- .NET 9, C#  
- Entity Framework Core + SQL Server  
- JWT para sesión  
- NUnit/xUnit para pruebas  
- GitHub Actions para CI

## 3. Estructura del repositorio
- `src/Presentation`: Interfaz de usuario (WinForms/WPF)
- `src/BusinessLogic`: Lógica de negocio
- `src/DataAccess`: Acceso a datos y modelos EF Core
- `src/Services`: Servicios de aplicación y APIs
- `src/Common`: Utilidades y modelos compartidos
- `src/Session`: Gestión de sesión y autenticación

## 4. Cómo ejecutar localmente  
1. Clonar repo  
2. Restaurar paquetes: `dotnet restore UserManagementSystem.sln`  
3. Crear BD en SQL Server y actualizar `appsettings.json`  
4. Ejecutar migraciones: `dotnet ef database update --project src/DataAccess/DataAccess.csproj --startup-project src/Services/Services.csproj`  
5. Abrir solución en VS2022 y ejecutar proyecto `Presentation`

## 5. Branching model  
- **main**: siempre verde, releases en producción.  
- **develop**: integración de features, base de los sprints.  
- **feature/xxx**: ramitas para cada historia de usuario.  
- **release/x.y**: preparación de versión.  
- **hotfix/x.y.z**: corrección urgente desde main.

## 6. Contribuir  
- Abrir PRs contra `develop`.  
- Asignar reviewers.  
- Referenciar Issue: “Closes #12”.  

## 7. CI/CD  
El workflow `.github/workflows/dotnet-ci.yml` compila, testea y publica artefactos.

## 8. Licencia  
MIT (u otra que prefiráis)
