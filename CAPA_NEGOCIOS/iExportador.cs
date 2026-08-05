using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA_NEGOCIOS
{
        // TODO Integrante 5: contrato común para exportar contenido (factura de pago o reporte general)
        // a distintos formatos (Excel, PDF) sin que la capa de negocio dependa de un formato específico.
        public interface iExportador
        {
            Task<bool> ExportarAsync(string titulo, List<string> contenido, string rutaDestino);
        }
}

