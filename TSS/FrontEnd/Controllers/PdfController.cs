using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FrontEnd.Controllers
{
    public class PdfController : Controller
    {
        [HttpPost]
        public IActionResult GenerarPDF([FromBody] PdfRequest request)
        {
            var contenidoHtml = request.Contenido;

            // Genera el PDF y lo convierte en bytes
            byte[] pdfBytes = GenerarPDFDesdeHTML(contenidoHtml);

            // Devuelve el PDF como una descarga
            return File(pdfBytes, MediaTypeNames.Application.Pdf, "mi-archivo.pdf");
        }

        private byte[] GenerarPDFDesdeHTML(string html)
        {
            var renderer = new ChromePdfRenderer();
            var pdf = renderer.RenderHtmlAsPdf(html);
            return pdf.BinaryData;
        }
    }

    public class PdfRequest
    {
        public string Contenido { get; set; } = string.Empty;
    }
}