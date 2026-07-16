using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

public class _Alumno
{
    private int idAlumno;
    private string nombre;
    private string apellido;
    private DateTime fechaNacimiento;
    private string telefono;
    private string correo;

    public int IdAlumno { get => idAlumno; set => idAlumno = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string Correo { get => correo; set => correo = value; }

    public _Alumno()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.telefono = string.Empty;
        this.correo = string.Empty;
        this.fechaNacimiento = DateTime.Today;
    }

    public _Alumno(int idAlumno, string nombre, string apellido, DateTime fechaNacimiento, string telefono, string correo)
                  
    {
        this.idAlumno = idAlumno;
        this.nombre = nombre;
        this.apellido = apellido;
        this.fechaNacimiento = fechaNacimiento;
        this.telefono = telefono;
        this.correo = correo;
    }
}

public class AlumnoCD
{
    public bool Insertar(_Alumno a)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO ALUMNOS (Nombre, APELLIDO, FECHANACIMIENTO, TELEFONO, CORREO)
              VALUES (@nombre, @apellido, @fecha, @telefono, @correo)", con))
        {
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@fecha", a.FechaNacimiento);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@correo", a.Correo);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public List<_Alumno> ObtenerTodos()
    {
        var lista = new List<_Alumno>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM ALUMNOS", con))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                _Alumno a = new _Alumno();
                a.IdAlumno = reader.GetInt32(0);
                a.Nombre = reader.GetString(1);
                a.Apellido = reader.GetString(2);
                a.FechaNacimiento = reader.GetDateTime(3);
                a.Telefono = reader.GetString(4);
                a.Correo = reader.GetString(5);
                lista.Add(a);
            }
        }
        return lista;
    }

    public bool Actualizar(_Alumno a)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE ALUMNOS SET Nombre=@nombre, APELLIDO=@apellido,
              FECHANACIMIENTO=@fecha, TELEFONO=@telefono, CORREO=@correo
              WHERE IDALUMNO=@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@fecha", a.FechaNacimiento);
            cmd.Parameters.AddWithValue("@telefono", a.Telefono);
            cmd.Parameters.AddWithValue("@correo", a.Correo);
            cmd.Parameters.AddWithValue("@id", a.IdAlumno);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public bool Eliminar(int idAlumno)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM ALUMNOS WHERE IDALUMNO=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idAlumno);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public bool TieneMatriculas(int idAlumno)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM MATRICULAS WHERE IDALUMNO=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idAlumno);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }
    //TODO juan
}