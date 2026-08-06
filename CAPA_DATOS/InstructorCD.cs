using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

//TODO: Clase "entidad" / DTO que representa un registro de la tabla INSTRUCTORES.
//TODO: Solo tiene propiedades (get/set), no lógica de negocio.
public class _Instructor
{
    //TODO: Variables privadas que guardan el valor real de cada propiedad.
    private int idInstructor;
    private string nombre;
    private string apellido;
    private string especialidad;
    private string telefono;

    //TODO: Propiedad Id -> identificador único del instructor en la tabla INSTRUCTORES.
    public int IdInstructor { get => idInstructor; set => idInstructor = value; }
    //TODO: Propiedad Nombre -> nombre de pila del instructor.
    public string Nombre { get => nombre; set => nombre = value; }
    //TODO: Propiedad Apellido -> apellido del instructor.
    public string Apellido { get => apellido; set => apellido = value; }
    //TODO: Propiedad Especialidad -> materia/área en la que enseña (ej: "Conversación").
    public string Especialidad { get => especialidad; set => especialidad = value; }
    //TODO: Propiedad Telefono -> número de contacto del instructor.
    public string Telefono { get => telefono; set => telefono = value; }

    //TODO: Constructor vacío. Inicializa strings para evitar valores null.
    public _Instructor()
    {
        this.nombre = string.Empty;
        this.apellido = string.Empty;
        this.especialidad = string.Empty;
        this.telefono = string.Empty;
    }

    //TODO: Constructor con todos los campos, útil al leer filas desde la base de datos.
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

//TODO: Clase de acceso a datos (Capa Datos) para la tabla INSTRUCTORES.
//TODO: Implementa ICrudAsync<_Instructor> -> obliga a tener las 4 operaciones async.
//TODO: Aquí NO va lógica de negocio, solo consultas SQL.
public class InstructorCD : ICrudAsync<_Instructor>
{
    // ===================== MÉTODOS SÍNCRONOS ORIGINALES (sin cambios) =====================
    //TODO: Estos métodos ya existían en el proyecto original. Se dejaron intactos
    //TODO: para no romper nada que ya estuviera funcionando o siendo usado en otra parte.

    //TODO: Inserta un instructor de forma SÍNCRONA (bloquea el hilo hasta terminar).
    public bool Insertar(_Instructor i)
    {
        //TODO: "using" abre la conexión y el comando, y los cierra/libera automáticamente al terminar.
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO INSTRUCTORES (NOMBRE, APELLIDO, ESPECIALIDAD, TELEFONO)
              VALUES (@nombre, @apellido, @especialidad, @telefono)", con))
        {
            //TODO: Parámetros -> evitan inyección SQL, en vez de concatenar texto.
            cmd.Parameters.AddWithValue("@nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@apellido", i.Apellido);
            cmd.Parameters.AddWithValue("@especialidad", i.Especialidad);
            cmd.Parameters.AddWithValue("@telefono", i.Telefono);
            //TODO: ExecuteNonQuery -> ejecuta el INSERT y devuelve cuántas filas se afectaron.
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    //TODO: Trae TODOS los instructores de forma síncrona (para uso donde no se usa async).
    public List<_Instructor> ObtenerTodos()
    {
        //TODO: Lista vacía donde se van a acumular todos los instructores leídos.
        var lista = new List<_Instructor>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM INSTRUCTORES", con))
        //TODO: ExecuteReader -> ejecuta el SELECT y da un "cursor" para leer fila por fila.
        using (var reader = cmd.ExecuteReader())
        {
            //TODO: Read() avanza a la siguiente fila; devuelve false cuando ya no hay más.
            while (reader.Read())
            {
                //TODO: Se arma un objeto _Instructor por cada fila (mapeo columna -> propiedad).
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

    //TODO: Actualiza (edita) un instructor existente de forma síncrona.
    public bool Actualizar(_Instructor i)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE INSTRUCTORES SET NOMBRE=@nombre, APELLIDO=@apellido,
              ESPECIALIDAD=@especialidad, TELEFONO=@telefono
              WHERE IDINSTRUCTOR=@id", con))
        {
            //TODO: Se cargan los nuevos valores + el Id que identifica QUÉ fila actualizar.
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

    //TODO: Elimina un instructor por id de forma síncrona.
    public bool Eliminar(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM INSTRUCTORES WHERE IDINSTRUCTOR=@id", con))
        {
            //TODO: Se indica el Id del registro a borrar.
            cmd.Parameters.AddWithValue("@id", idInstructor);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    //TODO: Verifica si el instructor tiene matrículas asociadas (regla de negocio:
    //TODO: no se puede eliminar un instructor con matrículas registradas).
    public bool TieneMatriculas(int idInstructor)
    {
        using (var con = Conexion.ObtenerConexion())
        //TODO: ExecuteScalar -> se usa cuando el resultado es UN solo valor (aquí, un conteo).
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
    //TODO: A partir de aquí empiezan los métodos NUEVOS que agregué.
    //TODO: Son la versión "Async" de cada operación, para no congelar la interfaz
    //TODO: mientras se consulta la base de datos (requisito de tu prompt).

    //TODO: Versión async de ObtenerTodos(). La usa el formulario al abrir (Load).
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

    //TODO: Versión async de Insertar(). La usa btnGuardar_Click en frmInstructores.
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
            //TODO: await = "espera sin bloquear" a que la BD responda; el hilo de la UI queda libre.
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    //TODO: Versión async de Actualizar(). Nombre "EditarAsync" porque así lo pide
    //TODO: la interfaz ICrudAsync<T>. La usa btnActualizar_Click en frmInstructores.
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

    //TODO: Versión async de Eliminar(). La usa btnEliminar_Click en frmInstructores.
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

    //TODO: Chequeo de duplicados (Requisito 4 de tu prompt).
    //TODO: Busca si YA existe un instructor con el mismo nombre + apellido + especialidad.
    //TODO: idExcluir sirve para que, al EDITAR, no se compare el registro consigo mismo.
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

    //TODO: Búsqueda (Requisito 1 de tu prompt: "Buscar").
    //TODO: Filtra por nombre, apellido O especialidad usando LIKE '%texto%'.
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

    //TODO: Cuenta cuántos instructores hay en total. Se usa junto con ObtenerTodosAsync()
    //TODO: dentro de un Task.WhenAll(...) al cargar el formulario (Requisito 7).
    public async Task<int> ContarInstructoresAsync()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM INSTRUCTORES", con))
        {
            return (int)await cmd.ExecuteScalarAsync();
        }
    }

    public Task<bool> ActualizarAsync(_Instructor entidad)
    {
        throw new NotImplementedException();
    }
}
