using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public class CrearModel : PageModel
{
    [BindProperty] public string Remitente { get; set; }
    [BindProperty] public string Mensaje { get; set; }
    [BindProperty] public string Codigo { get; set; }
    [BindProperty] public DateTime Fecha { get; set; }

    public string MensajeConfirmacion { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        var mensajeSecreto = new MensajeSecreto
        {
            Remitente = Remitente,
            Mensaje = Cifrar(Mensaje),
            Codigo = Codigo,
            Fecha = Fecha.ToString("yyyy-MM-dd")
        };

        var nombreArchivo = $"MensajeSecreto_{Remitente.Replace(" ", "_")}.xml";
        var rutaCarpeta = Path.Combine("wwwroot", "Mensajes");

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var writer = new StreamWriter(rutaCompleta))
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto));
            serializer.Serialize(writer, mensajeSecreto);
        }

        MensajeConfirmacion = $"El mensaje fue guardado como '{nombreArchivo}' en la carpeta /wwwroot/Mensajes/";
    }

    private string Cifrar(string texto)
    {
        // Cifrado César simple (desplazamiento de 3 caracteres)
        var resultado = new StringBuilder();
        foreach (char c in texto)
        {
            resultado.Append((char)(c + 3));
        }
        return resultado.ToString();
    }
}

public class MensajeSecreto
{
    public string Remitente { get; set; }
    public string Mensaje { get; set; }
    public string Codigo { get; set; }
    public string Fecha { get; set; }
}
