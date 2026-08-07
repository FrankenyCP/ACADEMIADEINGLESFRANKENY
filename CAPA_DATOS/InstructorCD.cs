using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un registro de la tabla INSTRUCTORES
// TODO: Arquitectura en Capas - Clase de entidad (modelo) perteneciente a CAPA_DATOS
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

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, evita valores null al instanciar el objeto antes de llenarlo
    public _Instructor()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.especialidad = string.Empty;
        this.telefono = string.Empty;
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, útil al leer una fila completa desde la base de datos
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

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, encargada exclusivamente del acceso a datos de la tabla INSTRUCTORES
// TODO: Interfaces Y Asincrónicos - Implementa la interfaz genérica ICrudAsync<_Instructor>, obliga a definir las operaciones asíncronas Insertar, Actualizar, Eliminar y ObtenerTodos
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a las operaciones CRUD del instructor, sin lógica de negocio ni de presentación
public class InstructorCD : ICrudAsync<_Instructor>
{
    // ===================== MÉTODOS SÍNCRONOS ORIGINALES (sin cambios) =====================
    // Estos métodos ya existían en el proyecto original. Se dejaron intactos
    // para no romper nada que ya estuviera funcionando o siendo usado en otra parte.

    // TODO: Conexión a datos - Abre conexión a SQL Server mediante la clase Conexion para insertar un nuevo instructor
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que inserta un registro en la tabla INSTRUCTORES
    public bool Insertar(_Instructor i)
    {
        // "using" abre la conexión y el comando, y los cierra/libera automáticamente al terminar.
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO INSTRUCTORES (NOMBRE, APELLIDO, ESPECIALIDAD, TELEFONO)
              VALUES (@nombre, @apellido, @especialidad, @telefono)", con))
        {
            // Parámetros -> evitan inyección SQL, en vez de concatenar texto.
            cmd.Parameters.AddWithValue("@nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@apellido", i.Apellido);
            cmd.Parameters.AddWithValue("@especialidad", i.Especialidad);
            cmd.Parameters.AddWithValue("@telefono", i.Telefono);
            // ExecuteNonQuery -> ejecuta el INSERT y devuelve cuántas filas se afectaron.
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para leer todos los registros de la tabla INSTRUCTORES
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que retorna la lista completa de instructores
    public List<_Instructor> ObtenerTodos()
    {
        // Lista vacía donde se van a acumular todos los instructores leídos.
        var lista = new List<_Instructor>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM INSTRUCTORES", con))
        // ExecuteReader -> ejecuta el SELECT y da un "cursor" para leer fila por fila.
        using (var reader = cmd.ExecuteReader())
        {
            // Read() avanza a la siguiente fila; devuelve false cuando ya no hay más.
            while (reader.Read())
            {
                // Se arma un objeto _Instructor por cada fila (mapeo columna -> propiedad).
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para actualizar los datos de un instructor existente
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el UPDATE sobre la tabla INSTRUCTORES
    public bool Actualizar(_Instructor i)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE INSTRUCTORES SET NOMBRE=@nombre, APELLIDO=@apellido,
              ESPECIALIDAD=@especialidad, TELEFONO=@telefono
              WHERE IDINSTRUCTOR=@id", con))
        {
            // Se cargan los nuevos valores + el Id que identifica QUÉ fila actualizar.
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

    // TODO: Conexión a datos - Abre conexión a SQL Server para eliminar un instructor por su Id
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el DELETE sobre la tabla INSTRUCTORES
    public bool Eliminar(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM INSTRUCTORES WHERE IDINSTRUCTOR=@id", con))
        {
            // Se indica el Id del registro a borrar.
            cmd.Parameters.AddWithValue("@id", idInstructor);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para verificar si el instructor tiene matrículas asociadas
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono de validación (regla de negocio: no se puede eliminar un instructor con matrículas registradas)
    public bool TieneMatriculas(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        // ExecuteScalar -> se usa cuando el resultado es UN solo valor (aquí, un conteo).
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

    // ===================== NUEVO: VERSIONES ASYNC (ICrudAsync<_Instructor>) =====================
    // A partir de aquí empiezan los métodos NUEVOS. Son la versión "Async" de cada
    // operación, para no congelar la interfaz mientras se consulta la base de datos.

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona de la obtención de todos los registros, exigida por la interfaz ICrudAsync<_Instructor>
    // TODO: Llamadas asíncronas - Usa ExecuteReaderAsync y ReadAsync para leer los resultados sin bloquear el hilo de la interfaz gráfica; se ejecuta al cargar (Load) el formulario
    public async Task<List<_Instructor>> ObtenerTodosAsync()
    {
        var lista = new List<_Instructor>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM INSTRUCTORES", con))
        using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
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

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de inserción exigido por la interfaz ICrudAsync<_Instructor>
    // TODO: Llamadas asíncronas - await espera la respuesta de la base de datos sin bloquear el hilo de la UI; la usa btnGuardar_Click en frmInstructores
    public async Task<bool> InsertarAsync(_Instructor i)
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
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de actualización, nombrado EditarAsync porque así lo exige la interfaz ICrudAsync<T>
    // TODO: Llamadas asíncronas - Ejecuta el UPDATE de forma asíncrona; la usa btnActualizar_Click en frmInstructores
    public async Task<bool> EditarAsync(_Instructor i)
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
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de eliminación exigido por la interfaz ICrudAsync<_Instructor>
    // TODO: Llamadas asíncronas - Ejecuta el DELETE de forma asíncrona; la usa btnEliminar_Click en frmInstructores
    public async Task<bool> EliminarAsync(int id)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM INSTRUCTORES WHERE IDINSTRUCTOR=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", id);
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Llamadas asíncronas - Método asíncrono de validación que evita insertar/editar instructores duplicados (mismo nombre + apellido + especialidad)
    // TODO: Conexión a datos - Excluye el propio Id (idExcluir) para que la validación no choque consigo mismo al editar
    public async Task<bool> ExisteInstructorAsync(string nombre, string apellido, string especialidad, int idExcluir = 0)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT COUNT(*) FROM INSTRUCTORES
              WHERE NOMBRE=@nombre AND APELLIDO=@apellido AND ESPECIALIDAD=@especialidad
              AND IDINSTRUCTOR<>@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@especialidad", especialidad);
            cmd.Parameters.AddWithValue("@id", idExcluir);
            int count = (int)await cmd.ExecuteScalarAsync();
            return count > 0;
        }
    }

    // TODO: Llamadas asíncronas - Método asíncrono de búsqueda usado por la opción Consulta, filtra por nombre, apellido o especialidad
    // TODO: Conexión a datos - Usa el operador LIKE con parámetro ('%texto%') para evitar inyección SQL en la búsqueda
    public async Task<List<_Instructor>> BuscarAsync(string texto)
    {
        var lista = new List<_Instructor>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT * FROM INSTRUCTORES
              WHERE NOMBRE LIKE @texto OR APELLIDO LIKE @texto OR ESPECIALIDAD LIKE @texto", con))
        {
            cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
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
        }
        return lista;
    }

    // TODO: Llamadas asíncronas - Cuenta cuántos instructores hay en total; se combina con ObtenerTodosAsync() dentro de un Task.WhenAll(...) al cargar el formulario
    // TODO: Conexión a datos - Usa ExecuteScalarAsync porque la consulta retorna un único valor (COUNT)
    public async Task<int> ContarInstructoresAsync()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM INSTRUCTORES", con))
        {
            return (int)await cmd.ExecuteScalarAsync();
        }
    }

    // TODO: Interfaces Y Asincrónicos - Miembro exigido por ICrudAsync<_Instructor> pero no utilizado en este módulo (se usa EditarAsync en su lugar); se deja implementado con excepción para cumplir el contrato de la interfaz sin alterar el resto del código
    public Task<bool> ActualizarAsync(_Instructor entidad)
    {
        throw new NotImplementedException();
    }
}