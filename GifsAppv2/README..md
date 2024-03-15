# ¿Cómo ejecutar este proyecto????
Por favor lee cuidadosamente este readme para poder ejecutar este proyecto.
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Instalaciones previas
- Dotnet 8
- Visual Studio 2022
- SQL Server
- MSSMS o cualquier administrador de base de datos para sql sever

## Reeinstalación de paquetes
Dentro de la carpeta del proyecto desde una cmd, digite
'''
dotnet restore
'''
Esto reeinstalará los paquetes necesarios "se toma en cuenta que ya tiene instalado dotnet".

## Generar un nuevo archivo appsettings.json
- En Visual Studio, haz clic derecho en la carpeta del proyecto y selecciona Agregar > Nuevo elemento.
- En el cuadro de diálogo Agregar nuevo elemento, busca y selecciona Archivo JSON (JSON File en inglés).
- Nombra el archivo como appsettings.json y haz clic en Agregar (Add en inglés).

Esto creará un archivo appsettings.json vacío en la raíz de tu proyecto. Ahora puedes agregar la configuración deseada en formato JSON.
Ejemplo de estructura en appsettings.json:

'''
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  }
}

'''

## Añadir una cadena de conexión
Este proyecto trabaja con una db local, así que en tu appsettings.json en la sección de "ConnectionStrings"
haz las siguientes modificaciones:

'''
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GifsDbV2;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
'''

## ¿Cómo creo mi base de datos?
Para crear tu base de datos es sencillo. En tu visual studio, en las herramientas superiores, hax click
en tools - NuGet Package Manager - Package Manager Console. Esto abrirá una consola de comandos.
En la terminal digite el siguiente comando para recrear la base de datos.

'''
Update-Database
'''

## Secretos
Los secretos permitirán a la aplicación poder ejecutar peticiones seguras con tu llave secreta, en este caso
para los jwt que validan las peticiones http y la identidad del usuario.

- Haga clic derecho en el proyecto en el Explorador de soluciones y seleccione Administrar secretos de usuario.
- En el cuadro de diálogo Administrar secretos de usuario, haga clic en Agregar y seleccione Archivo.
- Seleccione el archivo secrets.json

A continuación debe agregar la siguiente configuración en los secretos:
'''
"GifsApp": {
    "JwtSecret": "add-your-secret-key"
  }
'''

Una vez tengas todo listo, podrás ejecutar este proyecto sin problemas.