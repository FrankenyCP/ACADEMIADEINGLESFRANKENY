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
        // TODO Destructor: libera los recursos del objeto Alumno en memoria.
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

    public bool RegistrarPago(_Pago p, decimal costoNivel)
    {
        PagoCD pagoCD = new PagoCD();
        decimal totalPagado = pagoCD.ObtenerTotalPagado(p.IdMatricula);
        if (totalPagado + p.Monto > costoNivel)
            return false;
        else
            return pagoCD.Insertar(p);
    }

    public string PromoverAlumno(string nivelActual)
    {
        if (nivelActual == "Básico")
            return "Intermedio";
        else if (nivelActual == "Intermedio")
            return "Avanzado";
        else if (nivelActual == "Avanzado")
            return "Completado";
        else
            return "Nivel no reconocido.";
    }
}
