using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public interface IServicioCorreo
    {
        Task EnviarRegistroExitosoAsync(
            string destinatario,
            string nombreCompleto
        );

        Task EnviarCancelacionMatriculaAsync(
            string destinatario,
            string nombreAlumno,
            string nombreNivel
        );
    }
}