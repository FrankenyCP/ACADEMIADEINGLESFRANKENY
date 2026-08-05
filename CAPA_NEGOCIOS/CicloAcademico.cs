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
    
      // TODO Destructor: libera los recursos en memoria asociados al ciclo académico.
      // Al destruirse este objeto, se notifica el cierre del ciclo y se liberan
      // las referencias a fechas y nombre del ciclo retenidas durante la sesión.
      // En un entorno de producción, aquí se generarían las constancias de nivel
      // para cada alumno que completó el ciclo académico.
        
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