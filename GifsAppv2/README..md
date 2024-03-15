# �C�mo ejecutar este proyecto????
Por favor lee cuidadosamente este readme para poder ejecutar este proyecto.
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Instalaciones previas
- Dotnet 8
- Visual Studio 2022
- SQL Server
- MSSMS o cualquier administrador de base de datos para sql sever

## Reeinstalaci�n de paquetes
Dentro de la carpeta del proyecto desde una cmd, digite
'''
dotnet restore
'''
Esto reeinstalar� los paquetes necesarios "se toma en cuenta que ya tiene instalado dotnet".

## Generar un nuevo archivo appsettings.json
- En Visual Studio, haz clic derecho en la carpeta del proyecto y selecciona Agregar > Nuevo elemento.
- En el cuadro de di�logo Agregar nuevo elemento, busca y selecciona Archivo JSON (JSON File en ingl�s).
- Nombra el archivo como appsettings.json y haz clic en Agregar (Add en ingl�s).

Esto crear� un archivo appsettings.json vac�o en la ra�z de tu proyecto. Ahora puedes agregar la configuraci�n deseada en formato JSON.
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

## A�adir una cadena de conexi�n
Este proyecto trabaja con una db local, as� que en tu appsettings.json en la secci�n de "ConnectionStrings"
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

## �C�mo creo mi base de datos?
Para crear tu base de datos es sencillo. En tu visual studio, en las herramientas superiores, hax click
en tools - NuGet Package Manager - Package Manager Console. Esto abrir� una consola de comandos.
En la terminal digite el siguiente comando para recrear la base de datos.

'''
Update-Database
'''

## Secretos
Los secretos permitir�n a la aplicaci�n poder ejecutar peticiones seguras con tu llave secreta, en este caso
para los jwt que validan las peticiones http y la identidad del usuario.

- Haga clic derecho en el proyecto en el Explorador de soluciones y seleccione Administrar secretos de usuario.
- En el cuadro de di�logo Administrar secretos de usuario, haga clic en Agregar y seleccione Archivo.
- Seleccione el archivo secrets.json

A continuaci�n debe agregar la siguiente configuraci�n en los secretos:
'''
"GifsApp": {
    "JwtSecret": "add-your-secret-key"
  }
'''

Una vez tengas todo listo, podr�s ejecutar este proyecto sin problemas.