using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class PdfController : Controller
    {
        private readonly ChromePdfRenderer pdf = new();

        public PdfController(ChromePdfRenderer pdf)
        {
            this.pdf = pdf;
        }

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

            var renderer = pdf;
            var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

            var pdfStream = pdfDocument.BinaryData;

            return File(pdfStream, "application/pdf", "Informe.pdf");
        }

        public IActionResult GenerarCitaPDF([FromBody] VWAdminAppointmentViewModel formData)
        {
            var htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <title>Reporte de Cita Agendada</title>
</head>
<body>
    <div style='width: 600px;
    margin: 0 auto;
    border: 1px solid #000;
            padding: 20px; '>
        <div style='text-align: center;
            font-size: 24px;
            margin-bottom: 20px;'>Reporte de Cita Agendada</div>
        <div style='margin-bottom: 10px;'>
            <p> Doctor: {formData.Doctor}</p>
        </div>
        <div style='margin-bottom: 10px;'>
           <p> Paciente: {formData.PacientName}</p>
        </div>
        <div style='margin-bottom: 10px;'>
            <p> Especialidad: {formData.SpecialtyName} </p>
        </div>
        <div style='margin-bottom: 10px;'>
            <p> Hora de Inicio: {formData.StartTime}</ p>
        </div>
        <div style='margin-bottom: 10px;'>
          <p> Hora de Fin: {formData.EndTime} </p>
        </div>
    </div>
</body>
</html>";

            var renderer = pdf;
            var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

            var pdfStream = pdfDocument.BinaryData;

            return File(pdfStream, "application/pdf", $"Informe Cita - {formData.PacientName}.pdf");
        }
    }
}
