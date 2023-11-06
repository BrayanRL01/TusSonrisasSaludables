using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;
using IronPdf;

public class PdfController : Controller
{

    public IActionResult GenerarPDF([FromBody] FormularioData formData)
    {
        var htmlContent = $@"
    <html>
        <body>
            <h1>Cita</h1>
            <p>Nombre: {formData.Nombre}</p>
            <p>Edad: {formData.Edad}</p>
            <p>Fecha: {formData.Fecha}</p>
            <p>Correo: {formData.Correo}</p>
        </body>
    </html>";

        var renderer = new IronPdf.HtmlToPdf();
        var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

        var pdfStream = pdfDocument.BinaryData;

        return File(pdfStream, "application/pdf", "Informe.pdf");
    }





}

