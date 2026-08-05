using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

//TODO: Entidad/DTO que representa un registro de la tabla NIVELES.
public class _Nivel
{
    //TODO: Variables privadas que guardan el valor real de cada propiedad.
    private int idNivel;
    private string nombreNivel;
    private int duracionMeses;
    private decimal costo;

    //TODO: Propiedad Id -> identificador único del nivel en la tabla NIVELES.
    public int IdNivel { get => idNivel; set => idNivel = value; }
    //TODO: Propiedad NombreNivel -> nombre del nivel (ej: "Básico 1", "Intermedio").
    public string NombreNivel { get => nombreNivel; set => nombreNivel = value; }
    //TODO: Propiedad DuracionMeses -> cuántos meses dura el nivel. Debe ser > 0.
    public int DuracionMeses { get => duracionMeses; set => duracionMeses = value; }
    //TODO: Propiedad Costo -> precio del nivel en la moneda del sistema. Debe ser > 0.
    public decimal Costo { get => costo; set => costo = value; }

    //TODO: Constructor vacío. Inicializa el string para evitar valores null.
    public _Nivel()
    {
        this.nombreNivel = string.Empty;
    }

    //TODO: Constructor con todos los campos, útil al leer filas desde la base de datos.
    public _Nivel(int idNivel, string nombreNivel, int duracionMeses, decimal costo)
    {
        this.idNivel = idNivel;
        this.nombreNivel = nombreNivel;
        this.duracionMeses = duracionMeses;
        this.costo = costo;
    }
}

//TODO: Clase de acceso a datos (Capa Datos) para la tabla NIVELES.
//TODO: Implementa ICrudAsync<_Nivel> -> obliga a tener las 4 operaciones async.
public class NivelCD : ICrudAsync<_Nivel>
{
    // ===================== MÉTODOS SÍNCRONOS ORIGINALES (sin cambios) =====================
    //TODO: Métodos originales del proyecto, se dejaron intactos para no romper nada.

    //TODO: Inserta un nivel de forma síncrona.
    public bool Insertar(_Nivel n)
    {
        //TODO: "using" abre conexión/comando y los libera automáticamente al salir del bloque.
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO NIVELES (NOMBRENIVEL, DURACIONMESES, COSTO)
              VALUES (@nombre, @duracion, @costo)", con))
        {
            //TODO: Parámetros -> evitan inyección SQL.
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            //TODO: ExecuteNonQuery -> ejecuta el INSERT y devuelve cuántas filas se afectaron.
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    //TODO: Trae todos los niveles de forma síncrona.
    public List<_Nivel> ObtenerTodos()
    {
        //TODO: Lista vacía donde se acumulan los niveles leídos.
        var lista = new List<_Nivel>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM NIVELES", con))
        //TODO: ExecuteReader -> ejecuta el SELECT y da un cursor para leer fila por fila.
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                //TODO: Se arma un objeto _Nivel por cada fila leída (mapeo columna -> propiedad).
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

    //TODO: Actualiza (edita) un nivel existente de forma síncrona.
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

    //TODO: Elimina un nivel por id de forma síncrona.
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

    //TODO: Cuenta cuántos niveles hay en total (versión síncrona original).
    public int ContarNiveles()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NIVELES", con))
        {
            return (int)cmd.ExecuteScalar();
        }
    }

    // ===================== NUEVO: VERSIONES ASYNC (ICrudAsync<_Nivel>) =====================
    //TODO: A partir de aquí, los métodos NUEVOS: versión async de cada operación.

    //TODO: Versión async de ObtenerTodos(). La usa el formulario al abrir (Load).
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

    //TODO: Versión async de Insertar(). La usa btnGuardar_Click en frmNiveles.
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

    //TODO: Versión async de Actualizar(). Nombre "EditarAsync" porque así lo pide
    //TODO: la interfaz ICrudAsync<T>. La usa btnActualizar_Click en frmNiveles.
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

    //TODO: Versión async de Eliminar(). La usa btnEliminar_Click en frmNiveles.
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

    //TODO: Chequeo de duplicados (Requisito 4 de tu prompt).
    //TODO: Busca si YA existe un nivel con el mismo nombre.
    //TODO: idExcluir sirve para que, al EDITAR, no se compare el registro consigo mismo.
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

    //TODO: Búsqueda (Requisito 1 de tu prompt: "Buscar").
    //TODO: Filtra por nombre de nivel usando LIKE '%texto%'.
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

    //TODO: Versión async de ContarNiveles(). Se usa junto con ObtenerTodosAsync()
    //TODO: dentro de un Task.WhenAll(...) al cargar el formulario (Requisito 7).
    public async Task<int> ContarNivelesAsync()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NIVELES", con))
        {
            return (int)await cmd.ExecuteScalarAsync();
        }
    }
}
