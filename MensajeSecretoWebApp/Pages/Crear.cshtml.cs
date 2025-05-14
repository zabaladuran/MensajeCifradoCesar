using Microsoft.AspNetCore.Mvc; // Importa funcionalidades para controladores y atributos de MVC.
using Microsoft.AspNetCore.Mvc.RazorPages; // Importa funcionalidades para Razor Pages.
using System.Xml.Serialization; // Permite la serialización XML de objetos.
using System.Text; // Proporciona clases para manipulación de texto y cadenas.
using System.IO; // Permite trabajar con archivos y directorios.

public class CrearModel : PageModel // Define el modelo de la página Razor.
{
    [BindProperty] public string Remitente { get; set; } // Propiedad enlazada al formulario para el remitente.
    [BindProperty] public string Mensaje { get; set; } // Propiedad enlazada al formulario para el mensaje.
    [BindProperty] public string Codigo { get; set; } // Propiedad enlazada al formulario para el código.
    [BindProperty] public DateTime Fecha { get; set; } // Propiedad enlazada al formulario para la fecha.

    public string MensajeConfirmacion { get; set; } // Propiedad para mostrar un mensaje de confirmación.

    public void OnGet() { } // Método que se ejecuta en solicitudes GET (cuando se carga la página).

    public void OnPost() // Método que se ejecuta en solicitudes POST (cuando se envía el formulario).
    {
        var mensajeSecreto = new MensajeSecreto // Crea un nuevo objeto MensajeSecreto.
        {
            Remitente = Remitente, // Asigna el remitente.
            Mensaje = Cifrar(Mensaje), // Cifra el mensaje antes de guardarlo.
            Codigo = Codigo, // Asigna el código.
            Fecha = Fecha.ToString("yyyy-MM-dd") // Asigna la fecha en formato de texto.
        };

        var nombreArchivo = $"MensajeSecreto_{Remitente.Replace(" ", "_")}.xml"; // Genera el nombre del archivo XML.
        var rutaCarpeta = Path.Combine("wwwroot", "Mensajes"); // Define la ruta de la carpeta donde se guardará el archivo.

        if (!Directory.Exists(rutaCarpeta)) // Si la carpeta no existe...
            Directory.CreateDirectory(rutaCarpeta); // ...la crea.

        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo); // Une la ruta de la carpeta y el nombre del archivo.

        using (var writer = new StreamWriter(rutaCompleta)) // Abre un StreamWriter para escribir el archivo.
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto)); // Crea un serializador XML para el tipo MensajeSecreto.
            serializer.Serialize(writer, mensajeSecreto); // Serializa el objeto y lo guarda en el archivo.
        }

        MensajeConfirmacion = $"El mensaje fue guardado como '{nombreArchivo}' en la carpeta /wwwroot/Mensajes/"; // Asigna el mensaje de confirmación.
    }

    private string Cifrar(string texto) // Método privado para cifrar el mensaje usando el cifrado César.
    {
        // Usa el código ingresado como desplazamiento
        int desplazamiento = 0;
        int.TryParse(Codigo, out desplazamiento); // Si no es número, desplazamiento será 0

        var resultado = new StringBuilder(); // Crea un objeto StringBuilder para construir el texto cifrado carácter por carácter.
        foreach (char c in texto) // Recorre cada carácter del texto original.
        {
            if (char.IsLetter(c)) // Si el carácter es una letra (mayúscula o minúscula)...
            {
                char d = char.IsUpper(c) ? 'A' : 'a'; // Determina si es mayúscula ('A') o minúscula ('a') para ajustar el rango ASCII.
                resultado.Append((char)(((c + desplazamiento - d) % 26) + d)); // Aplica el cifrado César: desplaza la letra según el código y la agrega al resultado.
            }
            else
            {
                resultado.Append(c); // Si no es una letra, agrega el carácter tal cual al resultado (no lo cifra).
            }
        }
        return resultado.ToString(); // Convierte el StringBuilder a string y lo retorna como texto cifrado.
    }
}

public class MensajeSecreto // Clase que representa el mensaje secreto a guardar.
{
    public string Remitente { get; set; } // Propiedad para el remitente.
    public string Mensaje { get; set; } // Propiedad para el mensaje cifrado.
    public string Codigo { get; set; } // Propiedad para el código.
    public string Fecha { get; set; } // Propiedad para la fecha.
}
