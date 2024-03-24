# Instalaciones necesarias
- dotnet 8 link de descarga: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Visual Studio 2022 community, link de descarga: https://visualstudio.microsoft.com/vs/
  - Asegurate de instalar la opción de desarrollo web.
- Sql Server versión developer, link de descarga: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

## Instalaciones opcionales
- MSSMS Microsoft SQL Server Management Studio, en caso de que quieras revisar los datos insertados de manera gráfica.
  - Link de descarga: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16

## Reeinstalación de paquetes
Para reinstalar los paquetes necesarios para la ejecución de este programa, en una power shell dirigete a la ruta en donde se encuentre tu
proyecto, por ejemplo: "C:\Users\betaCode\Desktop\Net\GifsAppServer" y ejecuta el siguiente comando.

```
dotnet restore
```

Si recibes algún error, verifica que tengas dotnet 8 instalado o que la ruta que ingresaste sea correcta.

## Crear la base de datos
Una vez hayas descargados los paquetes con el comando anterior. Abre la consola de comandos "Package Manager Console" se ubica
en la parte superior de visual studio en Tools > NuGet Package Manager > Package Manager Console.

Dentro de la consola escribe lo siguiente:
```
PM> Update-Database
```

