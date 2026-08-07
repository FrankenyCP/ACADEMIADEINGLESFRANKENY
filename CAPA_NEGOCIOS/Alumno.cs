using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

// TODO: Clases y herencia - Hereda de Persona (clase base de CAPA_NEGOCIOS), reutiliza Nombre, Apellido, Telefono y agrega los datos propios de un alumno
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, contiene la lógica relacionada al alumno (evaluación, pagos, promoción)
public class Alumno : Persona
{
    private DateTime fechaNacimiento;
    private bool esIntensivo;

    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public bool EsIntensivo { get => esIntensivo; set => esIntensivo = value; }

    // TODO: Clases y herencia - Delegado privado utilizado internamente por EvaluarAprobacion para encapsular la lógica del mensaje según una condición booleana
    delegate string Evaluar(bool condicion);

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, inicializa valores por defecto (fecha de hoy, modalidad regular)
    public Alumno()
    {
        this.fechaNacimiento = DateTime.Today;
        this.esIntensivo = false;
    }

    // TODO: Clases y herencia - Constructor que invoca al constructor de la clase base Persona (base(nombre, apellido, telefono)) para reutilizar su inicialización
    public Alumno(string nombre, string apellido, string telefono)
        : base(nombre, apellido, telefono)
    {
        this.fechaNacimiento = DateTime.Today;
        this.esIntensivo = false;
    }

    // TODO: Clases y herencia - Constructor sobrecargado que también llama al constructor base, además recibe los datos propios del alumno
    public Alumno(string nombre, string apellido, string telefono,
                  DateTime fechaNacimiento, bool esIntensivo)
        : base(nombre, apellido, telefono)
    {
        this.fechaNacimiento = fechaNacimiento;
        this.esIntensivo = esIntensivo;
    }

    // TODO estructor: libera los recursos del objeto Alumno en memoria.
    // Aquí se limpiarían listas internas vinculadas a este alumno.
    ~Alumno()
    {
    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Sobrescribe (override) el método abstracto EvaluarNivel definido en la clase abstracta Persona
    public override string EvaluarNivel()
    {
        if (this.esIntensivo)
            return "Modalidad Intensiva: evaluación cada 2 meses.";
        else
            return "Modalidad Regular: evaluación cada 4 meses.";
    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Sobrescribe (override) el método virtual ObtenerInformacion heredado de Persona, personalizando el texto para un Alumno
    public override string ObtenerInformacion()
    {
        return "Alumno: " + ObtenerNombreCompleto() +
               " | Nacimiento: " + this.fechaNacimiento.ToShortDateString() +
               " | Modalidad: " + (this.esIntensivo ? "Intensiva" : "Regular");
    }

    // TODO: Clases y herencia - Método propio de Alumno que usa el delegado Evaluar para construir el mensaje de aprobación según si los pagos están completos
    public string EvaluarAprobacion(decimal totalPagado, decimal costoNivel)
    {
        Evaluar mensaje = condicion => condicion ? "Aprobado: pagos al día."
                                                 : "Pendiente: saldo sin completar.";
        bool aprobado = totalPagado >= costoNivel;
        return mensaje(aprobado);
    }

    // TODO: Arquitectura en Capas - Método de negocio que valida la regla (no exceder el costo del nivel) antes de delegar la inserción a PagoCD (CAPA_DATOS)
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método propio de Alumno que aplica la lógica de negocio del registro de pagos
    public bool RegistrarPago(_Pago p, decimal costoNivel)
    {
        PagoCD pagoCD = new PagoCD();
        decimal totalPagado = pagoCD.ObtenerTotalPagado(p.IdMatricula);
        if (totalPagado + p.Monto > costoNivel)
            return false;
        else
            return pagoCD.Insertar(p);
    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Método propio de Alumno que determina el siguiente nivel académico según el nivel actual
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