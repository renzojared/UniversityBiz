# Proyecto University
Aplicación REST de APIs

## Tecnologías
- [ASP .NET Core 6](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [Entity Framwork Core](https://learn.microsoft.com/en-us/ef/core/)
- [Automapper](https://docs.automapper.org/en/stable/)
- [Swagger UI](https://swagger.io/solutions/api-documentation/)
- SQL Server 2022

## String connection
Reemplazar por parámetros de máquina local
```Json
"ConnectionStrings": {
        "DB_CONN": "Server=localhost;Database=University;User Id=sa;Password=D@cker09;TrustServerCertificate=True;Encrypt=False;"
    }
```

## Migration

Desde [.NET Core CLi](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

```Shell
dotnet ef migrations add University -p University -o Database/Migrations -s University
dotnet ef database update University -p University -s University
dotnet ef migrations remove -p University -s University -c Context
```
