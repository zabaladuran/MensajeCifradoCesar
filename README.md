# MensajeCifradoCesar

Aplicación web desarrollada con .NET Razor Pages para cifrar y descifrar mensajes utilizando el Cifrado César (desplazamiento de +3 letras).

## Pagina inicial
![{0449ACE6-466F-4171-974A-F925E4F717D6}](https://github.com/user-attachments/assets/d7c59dbe-f4b5-4811-84e6-25cdfaa91341)

## formularios
### 1 
![{F03816AC-3507-4C96-BCF6-9EDB376A8497}](https://github.com/user-attachments/assets/be1568cf-7bfb-4dd1-a2ee-e84315c09367)
### 2 
![{0CDD854C-9675-4B3E-A1DF-49A60CC1CFF6}](https://github.com/user-attachments/assets/1efd4b92-0578-4b2a-a027-32c84ab68653)




## Requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Git

## Clonar el repositorio

```sh
git clone https://github.com/zabaladuran/MensajeCifradoCesar.git
cd MensajeCifradoCesar
```

## Estructura del proyecto

```
MensajeCifradoCesar/
│
├── MensajeSecretoWebApp/
│   ├── Pages/           # Páginas Razor (Index, Crear, Leer, Error)
│   ├── Models/          # Modelos de datos usados en la app
│   ├── Helpers/         # Clases auxiliares para cifrado y utilidades
│   ├── wwwroot/         # Archivos estáticos (JS, CSS, imágenes)
│   ├── Program.cs       # Punto de entrada de la aplicación
│   ├── MensajeSecretoWebApp.csproj # Archivo de proyecto
│   └── appsettings.json # Configuración de la aplicación
├── README.md            # Este archivo
```

## Ejecución rápida

Puedes iniciar la aplicación y recargar automáticamente ante cambios usando:

```sh
dotnet watch run --project MensajeSecretoWebApp/MensajeSecretoWebApp.csproj
```

Esto levantará el servidor en [https://localhost:5001](https://localhost:5001) o [http://localhost:5000](http://localhost:5000).

## Uso de la aplicación

### 1. Cifrar un mensaje

- Ve a la página principal.
- Ingresa el mensaje, remitente y código.
- Haz clic en "Cifrar".
- Descarga el archivo XML generado con el mensaje cifrado.

### 2. Descifrar un mensaje

- Ve a la sección "Leer".
- Sube el archivo XML generado previamente.
- Obtén el mensaje original descifrado en pantalla.

## Detalle de archivos y carpetas principales

- **Pages/**  
  Contiene las páginas Razor:
  - `Index.cshtml`: Página de inicio.
  - `Crear.cshtml`: Formulario para crear y cifrar mensajes.
  - `Leer.cshtml`: Formulario para cargar y descifrar mensajes.
  - `Error.cshtml`: Página de error.

- **Models/**  
  Modelos de datos, por ejemplo, la clase `MensajeSecreto` que representa la estructura del mensaje cifrado.

- **Helpers/**  
  Clases auxiliares, como la lógica para cifrar y descifrar usando el algoritmo César.

- **wwwroot/**  
  Archivos estáticos: JavaScript, CSS, imágenes y librerías externas (Bootstrap, jQuery, etc.).

- **Program.cs**  
  Configuración y arranque de la aplicación web.

- **MensajeSecretoWebApp.csproj**  
  Archivo de proyecto de .NET.

- **appsettings.json / appsettings.Development.json**  
  Configuración de la aplicación y del entorno de desarrollo.

## Ejemplo de mensaje cifrado

- **Mensaje original:** `HOLA MUNDO`
- **Mensaje cifrado:** `KROD PXQGR`

## Contribuciones

Las contribuciones son bienvenidas. Puedes abrir issues o enviar pull requests.

## Videos recomendados

- [Cifrado César explicado por Federico Diaz](https://www.youtube.com/watch?v=DT3bsFpuikY&ab_channel=FedericoDiaz)
- [Cifrado César explicado por Isaac López](https://www.youtube.com/watch?v=o6jrfkGqMGQ&ab_channel=IsaacL%C3%B3pez)
---

Desarrollado por [Quinntero].
