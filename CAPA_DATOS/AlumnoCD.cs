using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

//TODO Encapsulacion

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

// Implementa ICrudAsync<_Alumno>: obliga a esta clase a tener las 4 operaciones
// asíncronas básicas (Insertar, Actualizar, Eliminar, ObtenerTodos).
public class AlumnoCD : ICrudAsync<_Alumno>
{
    // ---------- MÉTODOS SÍNCRONOS ORIGINALES ----------
    // Se conservan tal cual porque frmMatriculas.cs y frmReportes.cs (módulos de
    // otros integrantes) ya los usan. Si los elimináramos, su código no compilaría.

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
            return filas > 0;
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
                lista.Add(LeerAlumno(reader));
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
            return filas > 0;
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
            return filas > 0;
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
            return count > 0;
        }
    }

    // ---------- MÉTODOS ASÍNCRONOS NUEVOS (parte de la tarea) ----------
    // Métodos async: los usa el frmAlumnos actualizado

    // Inserta un alumno nuevo
    public async Task<bool> InsertarAsync(_Alumno a)
    {
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            @"INSERT INTO ALUMNOS (Nombre, APELLIDO, FECHANACIMIENTO, TELEFONO, CORREO)
              VALUES (@nombre, @apellido, @fecha, @telefono, @correo)", con);
        cmd.Parameters.AddWithValue("@nombre", a.Nombre);
        cmd.Parameters.AddWithValue("@apellido", a.Apellido);
        cmd.Parameters.AddWithValue("@fecha", a.FechaNacimiento);
        cmd.Parameters.AddWithValue("@telefono", a.Telefono);
        cmd.Parameters.AddWithValue("@correo", a.Correo);
        int filas = await cmd.ExecuteNonQueryAsync();
        return filas > 0;
    }

    // Edita un alumno existente
    public async Task<bool> EditarAsync(_Alumno a)
    {
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            @"UPDATE ALUMNOS SET Nombre=@nombre, APELLIDO=@apellido,
              FECHANACIMIENTO=@fecha, TELEFONO=@telefono, CORREO=@correo
              WHERE IDALUMNO=@id", con);
        cmd.Parameters.AddWithValue("@nombre", a.Nombre);
        cmd.Parameters.AddWithValue("@apellido", a.Apellido);
        cmd.Parameters.AddWithValue("@fecha", a.FechaNacimiento);
        cmd.Parameters.AddWithValue("@telefono", a.Telefono);
        cmd.Parameters.AddWithValue("@correo", a.Correo);
        cmd.Parameters.AddWithValue("@id", a.IdAlumno);
        int filas = await cmd.ExecuteNonQueryAsync();
        return filas > 0;
    }

    // Elimina un alumno por su Id
    public async Task<bool> EliminarAsync(int idAlumno)
    {
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            "DELETE FROM ALUMNOS WHERE IDALUMNO=@id", con);
        cmd.Parameters.AddWithValue("@id", idAlumno);
        int filas = await cmd.ExecuteNonQueryAsync();
        return filas > 0;
    }

    // Trae todos los alumnos para llenar la grilla
    public async Task<List<_Alumno>> ObtenerTodosAsync()
    {
        var lista = new List<_Alumno>();
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand("SELECT * FROM ALUMNOS", con);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            lista.Add(LeerAlumno(reader));
        }
        return lista;
    }

    // Revisa si el alumno tiene matrículas antes de dejar eliminarlo
    public async Task<bool> TieneMatriculasAsync(int idAlumno)
    {
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM MATRICULAS WHERE IDALUMNO=@id", con);
        cmd.Parameters.AddWithValue("@id", idAlumno);

        // ExecuteScalarAsync se usa cuando el SELECT devuelve UN solo valor (aquí, un COUNT).
        // Es más liviano que ExecuteReaderAsync porque no arma un cursor de múltiples filas/columnas.
        int count = (int)await cmd.ExecuteScalarAsync();
        return count > 0;
    }

    // Busca por nombre o apellido (no hay columna "matrícula" en esta tabla)
    public async Task<List<_Alumno>> BuscarAsync(string filtro)
    {
        var lista = new List<_Alumno>();
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            @"SELECT * FROM ALUMNOS WHERE Nombre LIKE @filtro OR APELLIDO LIKE @filtro", con);
        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            lista.Add(LeerAlumno(reader));
        }
        return lista;
    }

    // Evita duplicados por correo o nombre+apellido (idExcluir para no chocar consigo mismo al editar)
    public async Task<bool> ExisteDuplicadoAsync(string nombre, string apellido, string correo, int idExcluir)
    {
        using var con = await Conexion.ObtenerConexionAsync();
        using var cmd = new SqlCommand(
            @"SELECT COUNT(*) FROM ALUMNOS
              WHERE (CORREO = @correo OR (Nombre = @nombre AND APELLIDO = @apellido))
                AND IDALUMNO <> @id", con);
        cmd.Parameters.AddWithValue("@nombre", nombre);
        cmd.Parameters.AddWithValue("@apellido", apellido);
        cmd.Parameters.AddWithValue("@correo", correo);
        cmd.Parameters.AddWithValue("@id", idExcluir);
        int count = (int)await cmd.ExecuteScalarAsync();
        return count > 0;
    }

    // Convierte una fila del reader en un objeto _Alumno (evita repetir esto en 3 métodos)
    private static _Alumno LeerAlumno(SqlDataReader reader)
    {
        _Alumno a = new _Alumno();
        a.IdAlumno = reader.GetInt32(0);
        a.Nombre = reader.GetString(1);
        a.Apellido = reader.GetString(2);
        a.FechaNacimiento = reader.GetDateTime(3);
        a.Telefono = reader.GetString(4);
        a.Correo = reader.GetString(5);
        return a;
    }
    //TODO juan
}