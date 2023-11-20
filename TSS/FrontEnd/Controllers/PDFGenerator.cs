using DinkToPdf;
using DinkToPdf.Contracts;

namespace FrontEnd.Controllers
{
    public class PDFGenerator
    {
        private readonly IConverter _converter;

        public PDFGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            },
                Objects = {
                new ObjectSettings()
                {
                    PagesCount = true,
                    HtmlContent = htmlContent
                }
            }
            };
            return _converter.Convert(document);
        }

        public byte[] GenerateAppointmentPDF(string htmlContent)
        {
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            },
                Objects = {
                new ObjectSettings()
                {
                    PagesCount = true,
                    HtmlContent = htmlContent
                }
            }
            };
            return _converter.Convert(document);
        }
    }
}
