namespace CAPA_NEGOCIOS;

public abstract class Persona
{
    private string nombre;
    private string apellido;
    private string telefono;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public Persona()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.telefono = string.Empty;
    }

    public Persona(string nombre, string apellido, string telefono)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.telefono = telefono;
    }

    // Método abstracto — obliga a cada clase hija a implementarlo
    public abstract string EvaluarNivel();

    // Método virtual — puede ser sobreescrito por las clases hijas
    public virtual string ObtenerInformacion()
    {
        return "Persona: " + this.nombre + " " + this.apellido;
    }

    // Método normal
    public string ObtenerNombreCompleto()
    {
        return this.nombre + " " + this.apellido;
    }
}