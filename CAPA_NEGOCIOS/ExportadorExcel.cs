using ClosedXML.Excel;

namespace CAPA_NEGOCIOS;

// TODO: Interfaces Y Asincrónicos - Implementa la interfaz iExportador, obliga a definir el método ExportarAsync de forma asíncrona
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, encapsula la lógica de generación de archivos Excel usada por frmPagos
// TODO Integrante 5: implementación de IExportador para generar archivos Excel con formato profesional
// (título combinado, columnas etiqueta/valor, bordes), usada por frmPagos.
public class ExportadorExcel : iExportador
{
    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ExportarAsync exigido por la interfaz iExportador
    // TODO: Llamadas asíncronas - Usa Task.Run para generar el archivo Excel en segundo plano, sin bloquear el hilo de la interfaz gráfica mientras se arma el documento
    // TODO: Captura de error (try-catch) - Envuelve toda la generación del archivo en try-catch, evitando que un fallo de escritura (ej. archivo abierto, ruta inválida) cierre la aplicación de forma forzada
    public async Task<bool> ExportarAsync(string titulo, List<string> contenido, string rutaDestino)
    {
        try
        {
            await Task.Run(() =>
            {
                using (var workbook = new XLWorkbook())
                {
                    var hoja = workbook.Worksheets.Add(titulo);

                    // TODO Integrante 5: título combinado en dos columnas con fondo de color institucional.
                    hoja.Range("A1:B1").Merge();
                    hoja.Cell(1, 1).Value = titulo;
                    hoja.Cell(1, 1).Style.Font.Bold = true;
                    hoja.Cell(1, 1).Style.Font.FontSize = 16;
                    hoja.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                    hoja.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
                    hoja.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    hoja.Row(1).Height = 28;

                    int fila = 3;
                    foreach (var linea in contenido)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                        {
                            fila++;
                            continue;
                        }

                        int posDosPuntos = linea.IndexOf(':');
                        if (posDosPuntos > 0 && posDosPuntos < 40)
                        {
                            string etiqueta = linea.Substring(0, posDosPuntos + 1);
                            string valor = linea.Substring(posDosPuntos + 1).Trim();

                            hoja.Cell(fila, 1).Value = etiqueta;
                            hoja.Cell(fila, 1).Style.Font.Bold = true;
                            hoja.Cell(fila, 2).Value = valor;
                        }
                        else
                        {
                            hoja.Range(fila, 1, fila, 2).Merge();
                            hoja.Cell(fila, 1).Value = linea;
                            hoja.Cell(fila, 1).Style.Font.Bold = true;
                            hoja.Cell(fila, 1).Style.Font.FontSize = 13;
                        }
                        fila++;
                    }

                    // TODO Integrante 5: fecha de generación al final del documento.
                    fila++;
                    hoja.Cell(fila, 1).Value = "Generado:";
                    hoja.Cell(fila, 1).Style.Font.Italic = true;
                    hoja.Cell(fila, 2).Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    hoja.Cell(fila, 2).Style.Font.Italic = true;

                    var rangoUsado = hoja.RangeUsed();
                    rangoUsado.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    hoja.Columns().AdjustToContents();
                    hoja.Column(2).Width = Math.Max(hoja.Column(2).Width, 30);

                    workbook.SaveAs(rutaDestino);
                }
            });

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al exportar a Excel: " + ex.Message, ex);
        }
    }
}