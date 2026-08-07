using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un registro de la tabla NIVELES
// TODO: Arquitectura en Capas - Clase de entidad (modelo) perteneciente a CAPA_DATOS
public class _Nivel
{
    private int idNivel;
    private string nombreNivel;
    private int duracionMeses;
    private decimal costo;

    public int IdNivel { get => idNivel; set => idNivel = value; }
    public string NombreNivel { get => nombreNivel; set => nombreNivel = value; }
    public int DuracionMeses { get => duracionMeses; set => duracionMeses = value; }
    public decimal Costo { get => costo; set => costo = value; }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, evita valores null al instanciar el objeto antes de llenarlo
    public _Nivel()
    {
        this.nombreNivel = string.Empty;
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, útil al leer una fila completa desde la base de datos
    public _Nivel(int idNivel, string nombreNivel, int duracionMeses, decimal costo)
    {
        this.idNivel = idNivel;
        this.nombreNivel = nombreNivel;
        this.duracionMeses = duracionMeses;
        this.costo = costo;
    }
}

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, encargada exclusivamente del acceso a datos de la tabla NIVELES
// TODO: Interfaces Y Asincrónicos - Implementa la interfaz genérica ICrudAsync<_Nivel>, obliga a definir las operaciones asíncronas Insertar, Actualizar, Eliminar y ObtenerTodos
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a las operaciones CRUD del nivel, sin lógica de negocio ni de presentación
public class NivelCD : ICrudAsync<_Nivel>
{
    // ===================== MÉTODOS SÍNCRONOS ORIGINALES (sin cambios) =====================
    // Métodos originales del proyecto, se dejaron intactos para no romper nada.

    // TODO: Conexión a datos - Abre conexión a SQL Server mediante la clase Conexion para insertar un nuevo nivel
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que inserta un registro en la tabla NIVELES
    public bool Insertar(_Nivel n)
    {
        // "using" abre conexión/comando y los libera automáticamente al salir del bloque.
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO NIVELES (NOMBRENIVEL, DURACIONMESES, COSTO)
              VALUES (@nombre, @duracion, @costo)", con))
        {
            // Parámetros -> evitan inyección SQL.
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            // ExecuteNonQuery -> ejecuta el INSERT y devuelve cuántas filas se afectaron.
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para leer todos los registros de la tabla NIVELES
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que retorna la lista completa de niveles
    public List<_Nivel> ObtenerTodos()
    {
        // Lista vacía donde se acumulan los niveles leídos.
        var lista = new List<_Nivel>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM NIVELES", con))
        // ExecuteReader -> ejecuta el SELECT y da un cursor para leer fila por fila.
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                // Se arma un objeto _Nivel por cada fila leída (mapeo columna -> propiedad).
                _Nivel n = new _Nivel();
                n.IdNivel = reader.GetInt32(0);
                n.NombreNivel = reader.GetString(1);
                n.DuracionMeses = reader.GetInt32(2);
                n.Costo = reader.GetDecimal(3);
                lista.Add(n);
            }
        }
        return lista;
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para actualizar los datos de un nivel existente
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el UPDATE sobre la tabla NIVELES
    public bool Actualizar(_Nivel n)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE NIVELES SET NOMBRENIVEL=@nombre, DURACIONMESES=@duracion,
              COSTO=@costo WHERE IDNIVEL=@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            cmd.Parameters.AddWithValue("@id", n.IdNivel);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para eliminar un nivel por su Id
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el DELETE sobre la tabla NIVELES
    public bool Eliminar(int idNivel)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM NIVELES WHERE IDNIVEL=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idNivel);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para contar cuántos niveles hay en total (versión síncrona)
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono que usa ExecuteScalar porque retorna un único valor
    public int ContarNiveles()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NIVELES", con))
        {
            return (int)cmd.ExecuteScalar();
        }
    }

    // ===================== NUEVO: VERSIONES ASYNC (ICrudAsync<_Nivel>) =====================
    // A partir de aquí, los métodos NUEVOS: versión async de cada operación.

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona de la obtención de todos los registros, exigida por la interfaz ICrudAsync<_Nivel>
    // TODO: Llamadas asíncronas - Usa ExecuteReaderAsync y ReadAsync para leer los resultados sin bloquear el hilo de la interfaz gráfica; se ejecuta al cargar (Load) el formulario
    public async Task<List<_Nivel>> ObtenerTodosAsync()
    {
        var lista = new List<_Nivel>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM NIVELES", con))
        using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                _Nivel n = new _Nivel();
                n.IdNivel = reader.GetInt32(0);
                n.NombreNivel = reader.GetString(1);
                n.DuracionMeses = reader.GetInt32(2);
                n.Costo = reader.GetDecimal(3);
                lista.Add(n);
            }
        }
        return lista;
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de inserción exigido por la interfaz ICrudAsync<_Nivel>
    // TODO: Llamadas asíncronas - await espera la respuesta de la base de datos sin bloquear el hilo de la UI; la usa btnGuardar_Click en frmNiveles
    public async Task<bool> InsertarAsync(_Nivel n)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO NIVELES (NOMBRENIVEL, DURACIONMESES, COSTO)
              VALUES (@nombre, @duracion, @costo)", con))
        {
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de actualización, nombrado EditarAsync porque así lo exige la interfaz ICrudAsync<T>
    // TODO: Llamadas asíncronas - Ejecuta el UPDATE de forma asíncrona; la usa btnActualizar_Click en frmNiveles
    public async Task<bool> EditarAsync(_Nivel n)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE NIVELES SET NOMBRENIVEL=@nombre, DURACIONMESES=@duracion,
              COSTO=@costo WHERE IDNIVEL=@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            cmd.Parameters.AddWithValue("@id", n.IdNivel);
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Interfaces Y Asincrónicos - Implementación asíncrona del método de eliminación exigido por la interfaz ICrudAsync<_Nivel>
    // TODO: Llamadas asíncronas - Ejecuta el DELETE de forma asíncrona; la usa btnEliminar_Click en frmNiveles
    public async Task<bool> EliminarAsync(int id)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM NIVELES WHERE IDNIVEL=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", id);
            int filas = await cmd.ExecuteNonQueryAsync();
            return filas > 0;
        }
    }

    // TODO: Llamadas asíncronas - Método asíncrono de validación que evita insertar/editar niveles con el mismo nombre
    // TODO: Conexión a datos - Excluye el propio Id (idExcluir) para que la validación no choque consigo mismo al editar
    public async Task<bool> ExisteNivelAsync(string nombreNivel, int idExcluir = 0)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT COUNT(*) FROM NIVELES
              WHERE NOMBRENIVEL=@nombre AND IDNIVEL<>@id", con))
        {
            cmd.Parameters.AddWithValue("@nombre", nombreNivel);
            cmd.Parameters.AddWithValue("@id", idExcluir);
            int count = (int)await cmd.ExecuteScalarAsync();
            return count > 0;
        }
    }

    // TODO: Llamadas asíncronas - Método asíncrono de búsqueda usado por la opción Consulta, filtra por nombre de nivel
    // TODO: Conexión a datos - Usa el operador LIKE con parámetro ('%texto%') para evitar inyección SQL en la búsqueda
    public async Task<List<_Nivel>> BuscarAsync(string texto)
    {
        var lista = new List<_Nivel>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT * FROM NIVELES WHERE NOMBRENIVEL LIKE @texto", con))
        {
            cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    _Nivel n = new _Nivel();
                    n.IdNivel = reader.GetInt32(0);
                    n.NombreNivel = reader.GetString(1);
                    n.DuracionMeses = reader.GetInt32(2);
                    n.Costo = reader.GetDecimal(3);
                    lista.Add(n);
                }
            }
        }
        return lista;
    }

    // TODO: Llamadas asíncronas - Cuenta cuántos niveles hay en total; se combina con ObtenerTodosAsync() dentro de un Task.WhenAll(...) al cargar el formulario
    // TODO: Conexión a datos - Usa ExecuteScalarAsync porque la consulta retorna un único valor (COUNT)
    public async Task<int> ContarNivelesAsync()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NIVELES", con))
        {
            return (int)await cmd.ExecuteScalarAsync();
        }
    }

    // TODO: Interfaces Y Asincrónicos - Miembro exigido por ICrudAsync<_Nivel> pero no utilizado en este módulo (se usa EditarAsync en su lugar); se deja implementado con excepción para cumplir el contrato de la interfaz sin alterar el resto del código
    public Task<bool> ActualizarAsync(_Nivel entidad)
    {
        throw new NotImplementedException();
    }
}