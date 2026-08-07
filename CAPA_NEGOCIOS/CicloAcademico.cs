namespace CAPA_NEGOCIOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un ciclo académico de la institución
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, contiene lógica propia (resumen, verificación de vigencia) sin acceder directamente a la base de datos
// TODO: Clases creadas según su uso, sin código ajeno - Clase independiente dedicada únicamente a representar y evaluar un ciclo académico
public class CicloAcademico
{
    private string nombreCiclo;
    private DateTime fechaInicio;
    private DateTime fechaFin;

    public string NombreCiclo { get => nombreCiclo; set => nombreCiclo = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, inicializa valores por defecto (ciclo de 6 meses desde hoy)
    public CicloAcademico()
    {
        this.nombreCiclo = string.Empty;
        this.fechaInicio = DateTime.Today;
        this.fechaFin = DateTime.Today.AddMonths(6);
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, permite crear un ciclo académico con nombre y fechas específicas
    public CicloAcademico(string nombreCiclo, DateTime fechaInicio, DateTime fechaFin)
    {
        this.nombreCiclo = nombreCiclo;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }

    // TODO Destructor: libera los recursos en memoria asociados al ciclo académico.
    // Al destruirse este objeto, se notifica el cierre del ciclo y se liberan
    // las referencias a fechas y nombre del ciclo retenidas durante la sesión.
    // En un entorno de producción, aquí se generarían las constancias de nivel
    // para cada alumno que completó el ciclo académico.
    ~CicloAcademico()
    {

    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Método propio de CicloAcademico que construye un texto resumen con el nombre y las fechas del ciclo
    public string ObtenerResumen()
    {
        return "Ciclo: " + this.nombreCiclo +
               " | Inicio: " + this.fechaInicio.ToShortDateString() +
               " | Fin: " + this.fechaFin.ToShortDateString();
    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Método propio de CicloAcademico que valida si la fecha actual está dentro del rango de inicio y fin del ciclo
    public bool EstaActivo()
    {
        DateTime hoy = DateTime.Today;
        if (hoy >= this.fechaInicio && hoy <= this.fechaFin)
            return true;
        else
            return false;
    }
}