using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;
using System.Text;
using System.IO;

// esta es la clase q maneja la pagina donde se crea el mensaje secreto
public class CrearModel : PageModel
{
    // estas son las propiedades que se llenan solas cuando se manda el form
    [BindProperty] public string Remitente { get; set; }
    [BindProperty] public string Mensaje { get; set; }
    [BindProperty] public string Codigo { get; set; }
    [BindProperty] public DateTime Fecha { get; set; }

    // esto es pa mostrarle al user si se guardo bien el mensaje
    public string MensajeConfirmacion { get; set; }

    // este metodo se ejecuta cuando se entra a la pagina (GET)
    public void OnGet()
    {
        // pone la fecha de hoy en el campo por defecto
        Fecha = DateTime.Today;
    }

    // este se ejecuta cuando el usuario manda el form (POST)
    public void OnPost()
    {
        // aca se crea el obj del mensaje con los datos que puso el user
        var mensajeSecreto = new MensajeSecreto
        {
            Remitente = Remitente,
            Mensaje = Cifrar(Mensaje), // se cifra el mensaje pa que no se lea facil
            Codigo = Codigo,
            Fecha = Fecha.ToString("yyyy-MM-dd") // guarda la fecha como texto
        };

        // se arma el nombre del archivo xml usando el nombre del remitente
        var nombreArchivo = $"MensajeSecreto_{Remitente.Replace(" ", "_")}.xml";

        // se define la ruta donde se guardan los mensajes
        var rutaCarpeta = Path.Combine("wwwroot", "Mensajes");

        // si la carpeta no existe la crea
        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        // une la carpeta con el nombre del archivo pa obtener la ruta completa
        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        // aca se guarda el xml en la carpeta
        using (var writer = new StreamWriter(rutaCompleta))
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto));
            serializer.Serialize(writer, mensajeSecreto); // lo convierte en xml y lo escribe
        }

        // mensaje q se le muestra al user cuando ya se guardo todo bien
        MensajeConfirmacion = $"El mensaje fue guardado como '{nombreArchivo}' en la carpeta /wwwroot/Mensajes/";
    }

    // metodo privado pa cifrar el texto con cifrado cesar (mueve las letras 3 posiciones)
    private string Cifrar(string texto)
    {
        var resultado = new StringBuilder();

        foreach (char c in texto)
        {
            // si el caracter es letra, se cifra
            if (char.IsLetter(c))
            {
                char a = char.IsUpper(c) ? 'A' : 'a'; // ve si es mayus o minus
                resultado.Append((char)((((c - a) + 3) % 26) + a)); // hace el corrimiento
            }
            else
            {
                // si no es letra se deja igual
                resultado.Append(c);
            }
        }

        return resultado.ToString(); // devuelve el mensaje cifrado
    }
}

// esta clase es la estructura del mensaje q se guarda en el archivo xml
public class MensajeSecreto
{
    public string Remitente { get; set; }
    public string Mensaje { get; set; } // aca va el mensaje ya cifrado
    public string Codigo { get; set; }
    public string Fecha { get; set; } // la fecha en formato yyyy-MM-dd
}
