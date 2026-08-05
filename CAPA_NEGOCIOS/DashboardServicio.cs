using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

// TODO: Implementación de IDashboardServicio
// Esta clase obtiene los indicadores del Dashboard consultando la base de datos
// a través de CAPA_DATOS respetando la arquitectura en capas
public class DashboardServicio : IDashboardServicio
{
    // TODO: Instancias de los DAL necesarios para obtener los indicadores
    private readonly AlumnoCD _alumnoCD;
    private readonly MatriculaCD _matriculaCD;
    private readonly InstructorCD _instructorCD;
    private readonly PagoCD _pagoCD;
    private readonly NivelCD _nivelCD;

    // TODO: Constructor que inicializa todos los DAL necesarios
    public DashboardServicio()
    {
        _alumnoCD = new AlumnoCD();
        _matriculaCD = new MatriculaCD();
        _instructorCD = new InstructorCD();
        _pagoCD = new PagoCD();
        _nivelCD = new NivelCD();
    }

    // TODO: Obtiene el total de alumnos de forma asíncrona
    public async Task<int> ObtenerTotalAlumnosAsync()
    {
        return await Task.Run(() =>
        {
            var alumnos = _alumnoCD.ObtenerTodos();
            return alumnos.Count;
        });
    }

    // TODO: Obtiene el total de matrículas activas de forma asíncrona
    public async Task<int> ObtenerTotalMatriculasActivasAsync()
    {
        return await Task.Run(() =>
        {
            var matriculas = _matriculaCD.ObtenerTodos();
            return matriculas.Count;
        });
    }

    // TODO: Obtiene el total de instructores de forma asíncrona
    public async Task<int> ObtenerTotalInstructoresAsync()
    {
        return await Task.Run(() =>
        {
            var instructores = _instructorCD.ObtenerTodos();
            return instructores.Count;
        });
    }

    // TODO: Obtiene el total de ingresos recaudados de forma asíncrona
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

    // TODO: Obtiene el total pendiente por cobrar de forma asíncrona
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

    // TODO: Obtiene el nivel con más alumnos matriculados de forma asíncrona
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
