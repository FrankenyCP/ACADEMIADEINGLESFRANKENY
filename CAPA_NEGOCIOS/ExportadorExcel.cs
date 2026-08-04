using ClosedXML.Excel;

namespace CAPA_NEGOCIOS;

// TODO Integrante 5: implementación de IExportador para generar archivos Excel (.xlsx)
// a partir de un título y una lista de líneas de contenido (usado por frmPagos).
public class ExportadorExcel : iExportador
{
    public async Task<bool> ExportarAsync(string titulo, List<string> contenido, string rutaDestino)
    {
        try
        {
            // TODO Integrante 5: ClosedXML no tiene API asíncrona nativa,
            // se envuelve en Task.Run para no bloquear el hilo de la UI (WinForms).
            await Task.Run(() =>
            {
                using (var workbook = new XLWorkbook())
                {
                    var hoja = workbook.Worksheets.Add(titulo);

                    hoja.Cell(1, 1).Value = titulo;
                    hoja.Cell(1, 1).Style.Font.Bold = true;
                    hoja.Cell(1, 1).Style.Font.FontSize = 14;

                    int fila = 3;
                    foreach (var linea in contenido)
                    {
                        hoja.Cell(fila, 1).Value = linea;
                        fila++;
                    }

                    hoja.Columns().AdjustToContents();
                    workbook.SaveAs(rutaDestino);
                }
            });

            return true;
        }
        catch (Exception ex)
        {
            // TODO Integrante 5: se captura cualquier error de escritura
            // (permisos, ruta inválida, archivo abierto en otro programa).
            throw new Exception("Error al exportar a Excel: " + ex.Message, ex);
        }
    }
}
