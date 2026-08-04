using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CAPA_NEGOCIOS;
// TODO Integrante 5: implementación de IExportador para generar archivos PDF,
// usada tanto por frmPagos (factura individual) como por frmReportes (reporte general).
public class ExportadorPdf : iExportador
{
    public async Task<bool> ExportarAsync(string titulo, List<string> contenido, string rutaDestino)
    {
        try
        {
            await Task.Run(() =>
            {
                Document.Create(documento =>
                {
                    documento.Page(pagina =>
                    {
                        pagina.Margin(30);

                        pagina.Header()
                            .Text(titulo)
                            .FontSize(18)
                            .Bold();

                        pagina.Content().Column(columna =>
                        {
                            columna.Spacing(5);
                            foreach (var linea in contenido)
                            {
                                columna.Item().Text(linea).FontSize(11);
                            }
                        });
                    });
                }).GeneratePdf(rutaDestino);
            });

            return true;
        }
        catch (Exception ex)
        {
            // TODO Integrante 5: se captura cualquier error de generación o escritura del PDF.
            throw new Exception("Error al exportar a PDF: " + ex.Message, ex);
        }
    }
}

