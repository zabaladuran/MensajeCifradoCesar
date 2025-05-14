using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Serialization;
using System.Text;

public class LeerModel : PageModel
{
    [BindProperty]
    public IFormFile ArchivoXml { get; set; }

    public string MensajeDescifrado { get; set; }
    public string Remitente { get; set; }
    public string Codigo { get; set; }
    public string Fecha { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ArchivoXml == null)
        {
            ModelState.AddModelError("", "Debes seleccionar un archivo.");
            return Page();
        }

        MensajeSecreto mensaje;
        using (var stream = ArchivoXml.OpenReadStream())
        {
            var serializer = new XmlSerializer(typeof(MensajeSecreto));
            mensaje = (MensajeSecreto)serializer.Deserialize(stream);
        }

        Remitente = mensaje.Remitente;
        Codigo = mensaje.Codigo;
        Fecha = mensaje.Fecha;
        MensajeDescifrado = Descifrar(mensaje.Mensaje);

        return Page();
    }

    private string Descifrar(string texto)
    {
        // Descifrado César simple (desplazamiento de 3 caracteres hacia atrás)
        var resultado = new StringBuilder();
        foreach (char c in texto)
        {
            resultado.Append((char)(c - 3));
        }
        return resultado.ToString();
    }

    public class MensajeSecreto
    {
        public string Remitente { get; set; }
        public string Mensaje { get; set; }
        public string Codigo { get; set; }
        public string Fecha { get; set; }
    }
}