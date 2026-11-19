# Repository Guidelines

## Project Structure

`DemoMVC/` contains the .NET 8 ASP.NET Core MVC application. Keep request handling in `Controllers/`, entities in `Models/`, and EF Core setup in `Data/`. Razor pages are grouped under `Views/<Controller>/`; shared layout and error pages live in `Views/Shared/`. Static files belong in `wwwroot/`. Database schema changes are tracked in `Migrations/`. The repository currently has no separate test project.

## Build, Test, and Development

Run commands from the repository root:

- `dotnet restore DemoMVC/DemoMVC.csproj` downloads project dependencies.
- `dotnet build DemoMVC/DemoMVC.csproj` compiles the application; CI runs this on .NET 8.
- `dotnet run --project DemoMVC/DemoMVC.csproj` starts the local MVC site.
- `dotnet ef migrations add <MigrationName> --project DemoMVC/DemoMVC.csproj` creates an EF Core migration; inspect the generated files before committing.
- `dotnet ef database update --project DemoMVC/DemoMVC.csproj` applies migrations to the configured SQL Server database.

No automated test suite is configured yet. For changes, at minimum build successfully and manually check affected routes and forms in the running app.

## Coding Style and Naming

Follow standard C# conventions: four spaces, PascalCase for types and public members, and camelCase for local variables and parameters. Use descriptive domain names consistent with the existing Vietnamese entities (`Khoa`, `SinhVien`) and controller names. Keep MVC responsibilities in their existing layers, use async EF Core calls for database operations, and retain nullable reference annotations. Name migrations with a concise PascalCase description, such as `AddSinhVien`.

## Database and Configuration

SQL Server connection settings are read from `DemoMVC/appsettings*.json`. Do not commit credentials or machine-specific secrets; use Development user secrets or environment-specific configuration. Keep model changes, migration files, and `ApplicationDbContextModelSnapshot.cs` in sync.

## Commits and Pull Requests

Recent commits use short imperative subjects prefixed by a type, such as `feat: add student and faculty management`. Continue that pattern (`fix: ...`, `feat: ...`). Pull requests should explain the behavior change, list validation performed, link related issues when available, and include screenshots for visible UI changes. Call out migrations or configuration changes explicitly.
