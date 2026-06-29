using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

public class Alumno : Persona
{
    private DateTime fechaNacimiento;
    private bool esIntensivo;

    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public bool EsIntensivo { get => esIntensivo; set => esIntensivo = value; }

    delegate string Evaluar(bool condicion);

    public Alumno()
    {
        this.fechaNacimiento = DateTime.Today;
        this.esIntensivo = false;
    }

    public Alumno(string nombre, string apellido, string telefono)
        : base(nombre, apellido, telefono)
    {
        this.fechaNacimiento = DateTime.Today;
        this.esIntensivo = false;
    }

    public Alumno(string nombre, string apellido, string telefono,
                  DateTime fechaNacimiento, bool esIntensivo)
        : base(nombre, apellido, telefono)
    {
        this.fechaNacimiento = fechaNacimiento;
        this.esIntensivo = esIntensivo;
    }

    ~Alumno()
    {
        // Destructor: libera los recursos del objeto Alumno en memoria.
        // Aquí se limpiarían listas internas vinculadas a este alumno.
    }

    public override string EvaluarNivel()
    {
        if (this.esIntensivo)
            return "Modalidad Intensiva: evaluación cada 2 meses.";
        else
            return "Modalidad Regular: evaluación cada 4 meses.";
    }

    public override string ObtenerInformacion()
    {
        return "Alumno: " + ObtenerNombreCompleto() +
               " | Nacimiento: " + this.fechaNacimiento.ToShortDateString() +
               " | Modalidad: " + (this.esIntensivo ? "Intensiva" : "Regular");
    }

    public string EvaluarAprobacion(decimal totalPagado, decimal costoNivel)
    {
        Evaluar mensaje = condicion => condicion ? "Aprobado: pagos al día."
                                                 : "Pendiente: saldo sin completar.";
        bool aprobado = totalPagado >= costoNivel;
        return mensaje(aprobado);
    }

    public string PromoverAlumno(string nivelActual)
    {
        if (nivelActual == "Básico")
            return "El alumno puede avanzar a Intermedio.";
        else if (nivelActual == "Intermedio")
            return "El alumno puede avanzar a Avanzado.";
        else if (nivelActual == "Avanzado")
            return "El alumno ha completado todos los niveles. ¡Felicitaciones!";
        else
            return "Nivel no reconocido.";
    }
}