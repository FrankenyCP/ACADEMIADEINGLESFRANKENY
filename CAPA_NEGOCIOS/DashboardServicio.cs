using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

// TODO: Interfaces Y Asincrónicos - Implementa la interfaz IDashboardServicio, obliga a definir todos los indicadores del Dashboard en versión asíncrona
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, obtiene los indicadores del Dashboard consultando CAPA_DATOS sin acceder directamente a SQL
// Esta clase obtiene los indicadores del Dashboard consultando la base de datos
// a través de CAPA_DATOS respetando la arquitectura en capas
public class DashboardServicio : IDashboardServicio
{
    // TODO: Clases creadas según su uso, sin código ajeno - Instancias privadas y de solo lectura (readonly) de los DAL necesarios para obtener los indicadores
    private readonly AlumnoCD _alumnoCD;
    private readonly MatriculaCD _matriculaCD;
    private readonly InstructorCD _instructorCD;
    private readonly PagoCD _pagoCD;
    private readonly NivelCD _nivelCD;

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor que inicializa todos los DAL necesarios para que la clase quede lista para usarse
    public DashboardServicio()
    {
        _alumnoCD = new AlumnoCD();
        _matriculaCD = new MatriculaCD();
        _instructorCD = new InstructorCD();
        _pagoCD = new PagoCD();
        _nivelCD = new NivelCD();
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerTotalAlumnosAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para ejecutar en segundo plano el conteo de alumnos sin bloquear el hilo de la interfaz gráfica
    public async Task<int> ObtenerTotalAlumnosAsync()
    {
        return await Task.Run(() =>
        {
            var alumnos = _alumnoCD.ObtenerTodos();
            return alumnos.Count;
        });
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerTotalMatriculasActivasAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para ejecutar en segundo plano el conteo de matrículas sin bloquear el hilo de la interfaz gráfica
    public async Task<int> ObtenerTotalMatriculasActivasAsync()
    {
        return await Task.Run(() =>
        {
            var matriculas = _matriculaCD.ObtenerTodos();
            return matriculas.Count;
        });
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerTotalInstructoresAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para ejecutar en segundo plano el conteo de instructores sin bloquear el hilo de la interfaz gráfica
    public async Task<int> ObtenerTotalInstructoresAsync()
    {
        return await Task.Run(() =>
        {
            var instructores = _instructorCD.ObtenerTodos();
            return instructores.Count;
        });
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerTotalIngresosAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para sumar en segundo plano el monto de todos los pagos sin bloquear el hilo de la interfaz gráfica
    public async Task<decimal> ObtenerTotalIngresosAsync()
    {
        return await Task.Run(() =>
        {
            var pagos = _pagoCD.ObtenerTodos();
            decimal total = 0;
            for (int i = 0; i < pagos.Count; i++)
                total += pagos[i].Monto;
            return total;
        });
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerTotalPendienteAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para calcular en segundo plano la diferencia entre el costo total de los niveles y lo pagado, sin bloquear el hilo de la interfaz gráfica
    // Calcula la diferencia entre el costo total de los niveles y lo pagado
    public async Task<decimal> ObtenerTotalPendienteAsync()
    {
        return await Task.Run(() =>
        {
            var matriculas = _matriculaCD.ObtenerTodos();
            var niveles = _nivelCD.ObtenerTodos();
            decimal totalPendiente = 0;

            for (int i = 0; i < matriculas.Count; i++)
            {
                decimal costoNivel = 0;
                for (int j = 0; j < niveles.Count; j++)
                {
                    if (niveles[j].IdNivel == matriculas[i].IdNivel)
                    {
                        costoNivel = niveles[j].Costo;
                        break;
                    }
                }
                decimal totalPagado = _pagoCD.ObtenerTotalPagado(matriculas[i].IdMatricula);
                decimal pendiente = costoNivel - totalPagado;
                if (pendiente > 0)
                    totalPendiente += pendiente;
            }
            return totalPendiente;
        });
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método ObtenerNivelMasPopularAsync exigido por IDashboardServicio
    // TODO: Llamadas asíncronas - Usa Task.Run para calcular en segundo plano cuál nivel tiene más alumnos matriculados, sin bloquear el hilo de la interfaz gráfica
    public async Task<string> ObtenerNivelMasPopularAsync()
    {
        return await Task.Run(() =>
        {
            var matriculas = _matriculaCD.ObtenerTodos();
            var niveles = _nivelCD.ObtenerTodos();

            int idNivelMasPopular = 0;
            int maxCount = 0;

            for (int i = 0; i < niveles.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < matriculas.Count; j++)
                {
                    if (matriculas[j].IdNivel == niveles[i].IdNivel)
                        count++;
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    idNivelMasPopular = niveles[i].IdNivel;
                }
            }

            for (int i = 0; i < niveles.Count; i++)
            {
                if (niveles[i].IdNivel == idNivelMasPopular)
                    return niveles[i].NombreNivel;
            }

            return "Sin datos";
        });
    }
}