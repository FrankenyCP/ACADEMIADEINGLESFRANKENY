using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA_NEGOCIOS
{
    public interface IServicioCorreo
    {
        Task EnviarRegistroExitosoAsync(
            string correoDestino,
            string nombreAlumno
        );

        Task EnviarConfirmacionPagoAsync(
            string correoDestino,
            string nombreAlumno,
            decimal monto,
            DateTime fechaPago,
            string metodoPago
        );
    }
}