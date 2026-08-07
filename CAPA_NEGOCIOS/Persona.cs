namespace CAPA_NEGOCIOS;

// TODO: Clase abstracta - Clase base abstracta que no se puede instanciar directamente, define el molde común para Alumno e Instructor
// TODO: Clases y herencia - Clase padre de la que heredan Alumno e Instructor, centraliza los atributos y comportamientos comunes a toda Persona
// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS
public abstract class Persona
{
    private string nombre;
    private string apellido;
    private string telefono;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, inicializa los strings para evitar valores null
    public Persona()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.telefono = string.Empty;
    }

    // TODO: Clases y herencia - Constructor parametrizado que es invocado por las clases hijas (Alumno, Instructor) mediante base(...)
    public Persona(string nombre, string apellido, string telefono)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.telefono = telefono;
    }

    // TODO: Métodos, métodos abstractos y métodos virtuales - Método abstracto, no tiene cuerpo aquí y obliga a cada clase hija (Alumno, Instructor) a implementarlo con su propia lógica
    // Método abstracto — obliga a cada clase hija a implementarlo
    public abstract string EvaluarNivel();

    // TODO: Métodos, métodos abstractos y métodos virtuales - Método virtual con implementación por defecto, puede ser sobrescrito (override) por las clases hijas
    // Método virtual — puede ser sobreescrito por las clases hijas
    public virtual string ObtenerInformacion()
    {
        return "Persona: " + this.nombre + " " + this.apellido;
    }

    // TODO: Clases y herencia - Método normal (no abstracto ni virtual), heredado tal cual por Alumno e Instructor sin necesidad de sobrescribirlo
    // Método normal
    public string ObtenerNombreCompleto()
    {
        return this.nombre + " " + this.apellido;
    }
}