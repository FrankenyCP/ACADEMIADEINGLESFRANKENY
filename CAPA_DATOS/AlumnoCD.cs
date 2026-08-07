using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un registro de la tabla ALUMNOS
// TODO: Arquitectura en Capas - Clase de entidad (modelo) perteneciente a CAPA_DATOS
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

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, permite instanciar el objeto antes de llenarlo desde el formulario
    public _Alumno()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.telefono = string.Empty;
        this.correo = string.Empty;
        this.fechaNacimiento = DateTime.Today;
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, crea un _Alumno con todos sus datos de una sola vez (ej. al leer de la base de datos)
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

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, encargada exclusivamente del acceso a datos de la tabla ALUMNOS
// TODO: Interfaces Y Asincrónicos - Implementa la interfaz genérica ICrudAsync<_Alumno>, obliga a definir las operaciones asíncronas Insertar, Actualizar, Eliminar y ObtenerTodos
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a las operaciones CRUD del alumno, sin lógica de negocio ni de presentación
// Implementa ICrudAsync<_Alumno>: obliga a esta clase a tener las 4 operaciones
// asíncronas básicas (Insertar, Actualizar, Eliminar, ObtenerTodos).
public class AlumnoCD : ICrudAsync<_Alumno>
{
    // ---------- MÉTODOS SÍNCRONOS ORIGINALES ----------
    // Se conservan tal cual porque frmMatriculas.cs y frmReportes.cs (módulos de
    // otros integrantes) ya los usan. Si los elimináramos, su código no compilaría.

    // TODO: Conexión a datos - Abre conexión a SQL Server mediante la clase Conexion para insertar un nuevo alumno
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que inserta un registro en la tabla ALUMNOS
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para leer todos los registros de la tabla ALUMNOS
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que retorna la lista completa de alumnos
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para actualizar los datos de un alumno existente
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el UPDATE sobre la tabla ALUMNOS
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para eliminar un alumno por su Id
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el DELETE sobre la tabla ALUMNOS
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para verificar si el alumno tiene matrículas asociadas
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono de validación, usado antes de permitir eliminar un alumno
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

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de inserción exigido por la interfaz ICrudAsync<_Alumno>
    // TODO: Llamadas asíncronas - Usa await en la apertura de conexión y en la ejecución del comando, sin bloquear el hilo de la interfaz gráfica
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

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de actualización exigido por la interfaz ICrudAsync<_Alumno>
    // TODO: Llamadas asíncronas - Ejecuta el UPDATE de forma asíncrona con ExecuteNonQueryAsync
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

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de eliminación exigido por la interfaz ICrudAsync<_Alumno>
    // TODO: Llamadas asíncronas - Ejecuta el DELETE de forma asíncrona sin bloquear el hilo principal
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

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona de la obtención de todos los registros, exigida por la interfaz ICrudAsync<_Alumno>
    // TODO: Llamadas asíncronas - Usa ExecuteReaderAsync y ReadAsync para leer los resultados sin bloquear el hilo principal
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

    // TODO: Llamadas asíncronas - Método asíncrono adicional que valida si el alumno tiene matrículas antes de eliminarlo
    // TODO: Conexión a datos - Usa ExecuteScalarAsync porque la consulta retorna un único valor (COUNT)
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

    // TODO: Llamadas asíncronas - Método asíncrono de búsqueda usado por la opción Consulta para filtrar alumnos por nombre o apellido
    // TODO: Conexión a datos - Usa el operador LIKE con parámetro para evitar inyección SQL en la búsqueda
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

    // TODO: Llamadas asíncronas - Método asíncrono de validación que evita insertar/editar alumnos con datos duplicados
    // TODO: Conexión a datos - Excluye el propio Id (idExcluir) para que la validación no choque consigo mismo al editar
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

    // TODO: Clases creadas según su uso, sin código ajeno - Método privado auxiliar, evita repetir la misma lógica de lectura en los tres métodos que usan SqlDataReader
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