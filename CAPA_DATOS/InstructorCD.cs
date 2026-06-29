using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

public class _Instructor
{
    private int idInstructor;
    private string nombre;
    private string apellido;
    private string especialidad;
    private string telefono;

    public int IdInstructor { get => idInstructor; set => idInstructor = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Especialidad { get => especialidad; set => especialidad = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public _Instructor()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.especialidad = string.Empty;
        this.telefono = string.Empty;
    }

    public _Instructor(int idInstructor, string nombre, string apellido,
                       string especialidad, string telefono)
    {
        this.idInstructor = idInstructor;
        this.nombre = nombre;
        this.apellido = apellido;
        this.especialidad = especialidad;
        this.telefono = telefono;
    }
}

public class InstructorCD
{
    public bool Insertar(_Instructor i)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO INSTRUCTORES (NOMBRE, APELLIDO, ESPECIALIDAD, TELEFONO)
              VALUES (@nombre, @apellido, @especialidad, @telefono)", con))
        {
            cmd.Parameters.AddWithValue("@nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@apellido", i.Apellido);
            cmd.Parameters.AddWithValue("@especialidad", i.Especialidad);
            cmd.Parameters.AddWithValue("@telefono", i.Telefono);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public List<_Instructor> ObtenerTodos()
    {
        var lista = new List<_Instructor>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM INSTRUCTORES", con))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                _Instructor i = new _Instructor();
                i.IdInstructor = reader.GetInt32(0);
                i.Nombre = reader.GetString(1);
                i.Apellido = reader.GetString(2);
                i.Especialidad = reader.GetString(3);
                i.Telefono = reader.GetString(4);
                lista.Add(i);
            }
        }
        return lista;
    }

    public bool Actualizar(_Instructor i)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE INSTRUCTORES SET NOMBRE=@nombre, APELLIDO=@apellido,
              ESPECIALIDAD=@especialidad, TELEFONO=@telefono
              WHERE IDINSTRUCTOR=@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@apellido", i.Apellido);
            cmd.Parameters.AddWithValue("@especialidad", i.Especialidad);
            cmd.Parameters.AddWithValue("@telefono", i.Telefono);
            cmd.Parameters.AddWithValue("@id", i.IdInstructor);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public bool Eliminar(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM INSTRUCTORES WHERE IDINSTRUCTOR=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idInstructor);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public bool TieneMatriculas(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM MATRICULAS WHERE IDINSTRUCTOR=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idInstructor);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }
}