namespace CAPA_NEGOCIOS;

// TODO: Interfaz que define el contrato para obtener los indicadores del Dashboard
// Implementada por DashboardServicio para mostrar estadísticas en frmPrincipal
public interface IDashboardServicio
{
    // TODO: Obtener el total de alumnos registrados en el sistema
    Task<int> ObtenerTotalAlumnosAsync();

    // TODO: Obtener el total de matrículas activas
    Task<int> ObtenerTotalMatriculasActivasAsync();

    // TODO: Obtener el total de instructores registrados
    Task<int> ObtenerTotalInstructoresAsync();

    // TODO: Obtener el total de ingresos recaudados por pagos
    Task<decimal> ObtenerTotalIngresosAsync();

    // TODO: Obtener el saldo pendiente total de todos los alumnos
    Task<decimal> ObtenerTotalPendienteAsync();

    // TODO: Obtener el nivel con más alumnos matriculados
    Task<string> ObtenerNivelMasPopularAsync();
}
