namespace CAPA_NEGOCIOS;

public class CicloAcademico
{
    private string nombreCiclo;
    private DateTime fechaInicio;
    private DateTime fechaFin;

    public string NombreCiclo { get => nombreCiclo; set => nombreCiclo = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }

    public CicloAcademico()
    {
        this.nombreCiclo = string.Empty;
        this.fechaInicio = DateTime.Today;
        this.fechaFin = DateTime.Today.AddMonths(6);
    }

    public CicloAcademico(string nombreCiclo, DateTime fechaInicio, DateTime fechaFin)
    {
        this.nombreCiclo = nombreCiclo;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }

    ~CicloAcademico()
    {
        // Destructor: al liberar este ciclo académico se generarían
        // las constancias de participación para todos los alumnos
        // inscritos en este ciclo.
    }

    public string ObtenerResumen()
    {
        return "Ciclo: " + this.nombreCiclo +
               " | Inicio: " + this.fechaInicio.ToShortDateString() +
               " | Fin: " + this.fechaFin.ToShortDateString();
    }

    public bool EstaActivo()
    {
        DateTime hoy = DateTime.Today;
        if (hoy >= this.fechaInicio && hoy <= this.fechaFin)
            return true;
        else
            return false;
    }
}