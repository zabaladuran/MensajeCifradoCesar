using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;
using System.Text;

// esta es la pagina pa leer un mensaje secreto desde un archivo xml
public class LeerModel : PageModel
{
    // propiedad pa recibir el archivo xml subido desde el form
    [BindProperty] public IFormFile ArchivoXml { get; set; }

    // propiedades pa mostrar los datos que se leen del xml
    public string MensajeDescifrado { get; set; }
    public string Remitente { get; set; }
    public string Codigo { get; set; }
    public string Fecha { get; set; }

    // este metodo corre cuando se entra a la pagina (GET), aca no hace nada
    public void OnGet() { }

    // este metodo se ejecuta cuando el user sube un archivo y manda el form (POST)
    public async Task<IActionResult> OnPostAsync()
    {
        // si el usuario no selecciono ningun archivo muestra un error
        if (ArchivoXml == null)
        {
            ModelState.AddModelError("", "Debes seleccionar un archivo.");
            return Page(); // vuelve a cargar la pagina con el error
        }

        // aca se va a guardar el contenido del xml despues de deserializarlo
        MensajeSecreto mensaje;

        // abre el archivo pa leerlo
        using (var stream = ArchivoXml.OpenReadStream())
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto));
            mensaje = (MensajeSecreto)serializer.Deserialize(stream); // convierte el xml a obj
        }

        // saca los datos del mensaje y los asigna a las propiedades de la pagina
        Remitente = mensaje.Remitente;
        Codigo = mensaje.Codigo;
        Fecha = mensaje.Fecha;
        MensajeDescifrado = Descifrar(mensaje.Mensaje); // descifra el mensaje

        return Page(); // recarga la pagina con los datos cargados
    }

    // metodo pa descifrar el texto que viene cifrado (cifrado cesar -3)
    private string Descifrar(string texto)
    {
        var resultado = new StringBuilder();
        foreach (char c in texto)
        {
            // si es letra la descifra, si no la deja como esta
            if (char.IsLetter(c))
            {
                char a = char.IsUpper(c) ? 'A' : 'a'; // ve si es mayus o minus
                resultado.Append((char)((((c - a + 26 - 3) % 26) + a))); // le resta 3
            }
            else
            {
                resultado.Append(c); // no cambia los signos, espacios, numeros, etc
            }
        }
        return resultado.ToString(); // devuelve el mensaje original
    }

    // clase interna que representa el mensaje secreto, igual a la usada pa guardar
    public class MensajeSecreto
    {
        public string Remitente { get; set; }
        public string Mensaje { get; set; } // mensaje cifrado en el xml
        public string Codigo { get; set; }
        public string Fecha { get; set; }
    }
}
