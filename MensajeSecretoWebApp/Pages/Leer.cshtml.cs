using Microsoft.AspNetCore.Mvc; // Importa funcionalidades para controladores y acciones MVC.
using Microsoft.AspNetCore.Mvc.RazorPages; // Importa soporte para páginas Razor.
using System.Xml.Serialization; // Permite la serialización y deserialización de XML.
using System.Text; // Proporciona clases para manipulación de texto, como StringBuilder.

public class LeerModel : PageModel // Define la clase del modelo de la página Razor llamada LeerModel.
{
    [BindProperty] // Permite el enlace automático de datos del formulario a la propiedad.
    public IFormFile ArchivoXml { get; set; } // Propiedad para recibir el archivo XML subido por el usuario.

    public string MensajeDescifrado { get; set; } // Almacena el mensaje descifrado.
    public string Remitente { get; set; } // Almacena el remitente del mensaje.
    public string Codigo { get; set; } // Almacena el código del mensaje.
    public string Fecha { get; set; } // Almacena la fecha del mensaje.

    public void OnGet() { } // Método que se ejecuta en solicitudes GET, aquí no hace nada.

    public async Task<IActionResult> OnPostAsync() // Método que se ejecuta en solicitudes POST (cuando se envía el formulario).
    {
        if (ArchivoXml == null) // Verifica si no se ha subido ningún archivo.
        {
            ModelState.AddModelError("", "Debes seleccionar un archivo."); // Agrega un error al modelo si no hay archivo.
            return Page(); // Retorna la misma página para mostrar el error.
        }

        MensajeSecreto mensaje; // Declara una variable para almacenar el mensaje deserializado.
        using (var stream = ArchivoXml.OpenReadStream()) // Abre un stream para leer el archivo subido.
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto)); // Crea un serializador XML para la clase MensajeSecreto.
            mensaje = (MensajeSecreto)serializer.Deserialize(stream); // Deserializa el XML a un objeto MensajeSecreto.
        }

        Remitente = mensaje.Remitente; // Asigna el remitente del mensaje deserializado.
        Codigo = mensaje.Codigo; // Asigna el código del mensaje deserializado.
        Fecha = mensaje.Fecha; // Asigna la fecha del mensaje deserializado.
        MensajeDescifrado = Descifrar(mensaje.Mensaje); // Descifra el mensaje y lo asigna.

        return Page(); // Retorna la página con los datos procesados.
    }

    private string Descifrar(string texto) // Método privado para descifrar el mensaje usando el cifrado César.
    {
        // Usa el código del mensaje como desplazamiento
        int desplazamiento = 0;
        int.TryParse(Codigo, out desplazamiento); // Si no es número, desplazamiento será 0

        var resultado = new StringBuilder();
        foreach (char c in texto)
        {
            if (char.IsLetter(c))
            {
                char d = char.IsUpper(c) ? 'A' : 'a';
                // Para descifrar, se resta el desplazamiento y se ajusta el módulo
                resultado.Append((char)(((c - desplazamiento - d + 26) % 26) + d));
            }
            else
            {
                resultado.Append(c);
            }
        }
        return resultado.ToString(); // Devuelve el texto descifrado.
    }

    public class MensajeSecreto // Clase interna que representa la estructura del mensaje secreto.
    {
        public string Remitente { get; set; } // Propiedad para el remitente.
        public string Mensaje { get; set; } // Propiedad para el mensaje cifrado.
        public string Codigo { get; set; } // Propiedad para el código.
        public string Fecha { get; set; } // Propiedad para la fecha.
    }
}