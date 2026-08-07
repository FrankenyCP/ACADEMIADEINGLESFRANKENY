using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    // TODO: Interfaces Y Asincrónicos - Interfaz que define el contrato asíncrono para el envío de notificaciones por correo electrónico
    // TODO: Arquitectura en Capas - Interfaz perteneciente a CAPA_NEGOCIOS, permite que la capa de negocio no dependa de una implementación específica del servicio de correo
    public interface IServicioCorreo
    {
        // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que envía la notificación de registro exitoso al destinatario
        Task EnviarRegistroExitosoAsync(
            string destinatario,
            string nombreCompleto
        );

        // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que envía la notificación de cancelación de matrícula al destinatario
        Task EnviarCancelacionMatriculaAsync(
            string destinatario,
            string nombreAlumno,
            string nombreNivel
        );
    }
}