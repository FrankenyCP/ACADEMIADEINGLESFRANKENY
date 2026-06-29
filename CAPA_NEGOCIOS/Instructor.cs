using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

public class Instructor : Persona
{
    private string especialidad;

    public string Especialidad { get => especialidad; set => especialidad = value; }

    public Instructor()
    {
        this.especialidad = string.Empty;
    }

    public Instructor(string nombre, string apellido, string telefono)
        : base(nombre, apellido, telefono)
    {
        this.especialidad = string.Empty;
    }

    public Instructor(string nombre, string apellido, string telefono, string especialidad)
        : base(nombre, apellido, telefono)
    {
        this.especialidad = especialidad;
    }

    ~Instructor()
    {
        // Destructor: libera los recursos del objeto Instructor en memoria.
        // Aquí se liberarían listas de grupos o asignaciones vinculadas a este instructor.
    }

    public override string EvaluarNivel()
    {
        return "Instructor especializado en: " + this.especialidad +
               ". Puede impartir todos los niveles de su área.";
    }

    public override string ObtenerInformacion()
    {
        return "Instructor: " + ObtenerNombreCompleto() +
               " | Especialidad: " + this.especialidad;
    }

    public string AsignarNivel(string nivel)
    {
        if (nivel == "Básico" || nivel == "Intermedio" || nivel == "Avanzado")
            return "Instructor " + ObtenerNombreCompleto() + " asignado al nivel " + nivel + ".";
        else
            return "Nivel no válido.";
    }

    public bool RegistrarInstructor(_Instructor i)
    {
        InstructorCD instructorCD = new InstructorCD();
        return instructorCD.Insertar(i);
    }
}