using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CAPA_NEGOCIOS;

// TODO: Interfaces Y Asincrónicos - Implementa la interfaz iExportador, obliga a definir el método ExportarAsync de forma asíncrona
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, encapsula la lógica de generación de archivos PDF usada por frmPagos y frmReportes
// TODO Integrante 5: implementación de IExportador para generar archivos PDF con formato profesional
// (encabezado, pie de página, resaltado de etiquetas), usada por frmPagos y frmReportes.
public class ExportadorPdf : iExportador
{
    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ExportarAsync exigido por la interfaz iExportador
    // TODO: Llamadas asíncronas - Usa Task.Run para generar el documento PDF en segundo plano, sin bloquear el hilo de la interfaz gráfica mientras se arma el documento
    // TODO: Captura de error (try-catch) - Envuelve toda la generación del archivo en try-catch, evitando que un fallo al crear el PDF (ej. ruta inválida, archivo en uso) cierre la aplicación de forma forzada
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
                        pagina.Size(PageSizes.A4);
                        pagina.Margin(40);
                        pagina.DefaultTextStyle(estilo => estilo.FontFamily("Segoe UI").FontSize(11));

                        // TODO Integrante 5: encabezado con título y línea divisoria de color institucional.
                        pagina.Header().Column(encabezado =>
                        {
                            encabezado.Item().Text("Academia de Inglés").FontSize(10).FontColor(Colors.Grey.Medium);
                            encabezado.Item().PaddingTop(2).Text(titulo).FontSize(20).Bold().FontColor(Colors.Blue.Darken2);
                            encabezado.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Blue.Darken2);
                        });

                        // TODO Integrante 5: cuerpo del documento, separa "Etiqueta: valor" para resaltar la etiqueta en negrita.
                        pagina.Content().PaddingVertical(15).Column(columna =>
                        {
                            columna.Spacing(6);
                            foreach (var linea in contenido)
                            {
                                if (string.IsNullOrWhiteSpace(linea))
                                {
                                    columna.Item().Height(8); // respeta líneas vacías como espacio real
                                    continue;
                                }

                                int posDosPuntos = linea.IndexOf(':');
                                if (posDosPuntos > 0 && posDosPuntos < 40)
                                {
                                    string etiqueta = linea.Substring(0, posDosPuntos + 1);
                                    string valor = linea.Substring(posDosPuntos + 1);

                                    columna.Item().Row(fila =>
                                    {
                                        fila.ConstantItem(160).Text(etiqueta).Bold();
                                        fila.RelativeItem().Text(valor.Trim());
                                    });
                                }
                                else
                                {
                                    columna.Item().Text(linea).Bold().FontSize(13);
                                }
                            }
                        });

                        // TODO Integrante 5: pie de página con fecha de generación y numeración.
                        pagina.Footer().Row(pie =>
                        {
                            pie.RelativeItem().Text("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                                .FontSize(9).FontColor(Colors.Grey.Medium);

                            pie.RelativeItem().AlignRight().Text(texto =>
                            {
                                texto.Span("Página ").FontSize(9).FontColor(Colors.Grey.Medium);
                                texto.CurrentPageNumber().FontSize(9).FontColor(Colors.Grey.Medium);
                                texto.Span(" de ").FontSize(9).FontColor(Colors.Grey.Medium);
                                texto.TotalPages().FontSize(9).FontColor(Colors.Grey.Medium);
                            });
                        });
                    });
                }).GeneratePdf(rutaDestino);
            });

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al exportar a PDF: " + ex.Message, ex);
        }
    }
}