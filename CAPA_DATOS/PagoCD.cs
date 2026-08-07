using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un registro de la tabla PAGOS
// TODO: Arquitectura en Capas - Clase de entidad (modelo) perteneciente a CAPA_DATOS
public class _Pago
{
    private int idPago;
    private int idMatricula;
    private DateTime fechaPago;
    private decimal monto;
    private string metodoPago;

    public int IdPago { get => idPago; set => idPago = value; }
    public int IdMatricula { get => idMatricula; set => idMatricula = value; }
    public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
    public decimal Monto { get => monto; set => monto = value; }
    public string MetodoPago { get => metodoPago; set => metodoPago = value; }

    // Campo extra para mostrar en la grilla (no viene directo de la tabla PAGOS, se llena con el JOIN)
    public string InfoMatricula { get; set; } = string.Empty;

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, evita valores null e inicializa la fecha con el día de hoy
    public _Pago()
    {
        this.metodoPago = string.Empty;
        this.fechaPago = DateTime.Today;
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, útil al leer una fila completa desde la base de datos
    public _Pago(int idPago, int idMatricula, DateTime fechaPago,
                 decimal monto, string metodoPago)
    {
        this.idPago = idPago;
        this.idMatricula = idMatricula;
        this.fechaPago = fechaPago;
        this.monto = monto;
        this.metodoPago = metodoPago;
    }
}

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, encargada exclusivamente del acceso a datos de la tabla PAGOS
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a las operaciones sobre pagos, sin lógica de negocio ni de presentación
public class PagoCD
{
    // TODO: Conexión a datos - Abre conexión a SQL Server mediante la clase Conexion para insertar un nuevo pago
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que inserta un registro en la tabla PAGOS
    public bool Insertar(_Pago p)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO PAGOS (IDMATRICULA, FECHAPAGO, MONTO, METODOPAGO)
              VALUES (@matricula, @fecha, @monto, @metodo)", con))
        {
            cmd.Parameters.AddWithValue("@matricula", p.IdMatricula);
            cmd.Parameters.AddWithValue("@fecha", p.FechaPago);
            cmd.Parameters.AddWithValue("@monto", p.Monto);
            cmd.Parameters.AddWithValue("@metodo", p.MetodoPago);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Consulta a SQL Server usando INNER JOIN para traer la información del alumno y nivel asociados a cada pago
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que retorna la lista completa de pagos con su información relacionada
    public List<_Pago> ObtenerTodos()
    {
        var lista = new List<_Pago>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT P.IDPAGO, P.IDMATRICULA, P.FECHAPAGO, P.MONTO, P.METODOPAGO,
                     A.Nombre + ' ' + A.APELLIDO + ' - ' + N.NOMBRENIVEL AS InfoMatricula
              FROM PAGOS P
              INNER JOIN MATRICULAS M ON P.IDMATRICULA = M.IDMATRICULA
              INNER JOIN ALUMNOS A ON M.IDALUMNO = A.IDALUMNO
              INNER JOIN NIVELES N ON M.IDNIVEL = N.IDNIVEL", con))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                _Pago p = new _Pago();
                p.IdPago = reader.GetInt32(0);
                p.IdMatricula = reader.GetInt32(1);
                p.FechaPago = reader.GetDateTime(2);
                p.Monto = reader.GetDecimal(3);
                p.MetodoPago = reader.GetString(4);
                p.InfoMatricula = reader.GetString(5);
                lista.Add(p);
            }
        }
        return lista;
    }

    // TODO: Conexión a datos - Consulta a SQL Server con INNER JOIN filtrada por el Id de matrícula, trae solo los pagos de esa matrícula
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono usado en frmPagos para mostrar el historial de pagos de una matrícula específica
    public List<_Pago> ObtenerPorMatricula(int idMatricula)
    {
        var lista = new List<_Pago>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT P.IDPAGO, P.IDMATRICULA, P.FECHAPAGO, P.MONTO, P.METODOPAGO,
                 A.Nombre + ' ' + A.APELLIDO + ' - ' + N.NOMBRENIVEL AS InfoMatricula
          FROM PAGOS P
          INNER JOIN MATRICULAS M ON P.IDMATRICULA = M.IDMATRICULA
          INNER JOIN ALUMNOS A ON M.IDALUMNO = A.IDALUMNO
          INNER JOIN NIVELES N ON M.IDNIVEL = N.IDNIVEL
          WHERE P.IDMATRICULA = @id", con))
        {
            cmd.Parameters.AddWithValue("@id", idMatricula);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    _Pago p = new _Pago();
                    p.IdPago = reader.GetInt32(0);
                    p.IdMatricula = reader.GetInt32(1);
                    p.FechaPago = reader.GetDateTime(2);
                    p.Monto = reader.GetDecimal(3);
                    p.MetodoPago = reader.GetString(4);
                    p.InfoMatricula = reader.GetString(5);
                    lista.Add(p);
                }
            }
        }
        return lista;
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para eliminar un pago por su Id
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el DELETE sobre la tabla PAGOS
    public bool Eliminar(int idPago)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM PAGOS WHERE IDPAGO=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idPago);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Consulta a SQL Server usando SUM e ISNULL para calcular el total pagado de una matrícula (evita null si no hay pagos)
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono usado en frmMatriculas/frmReportes para mostrar cuánto ha pagado el alumno
    public decimal ObtenerTotalPagado(int idMatricula)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT ISNULL(SUM(MONTO), 0) FROM PAGOS WHERE IDMATRICULA=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idMatricula);
            return (decimal)cmd.ExecuteScalar();
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para verificar si una matrícula tiene pagos registrados
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono de validación, se usa antes de eliminar una matrícula para respetar la integridad referencial
    public bool TienePagos(int idMatricula)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM PAGOS WHERE IDMATRICULA=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idMatricula);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Conexión a datos - Abre conexión a SQL Server para actualizar los datos de un pago existente
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que ejecuta el UPDATE sobre la tabla PAGOS, completa el CRUD (antes solo existían Insertar, Eliminar y las lecturas)
    // TODO Actualizar: se agrega el método Actualizar para completar el CRUD de pagos.
    // Antes solo existían Insertar, Eliminar y las lecturas; faltaba el Update.
    public bool Actualizar(_Pago p)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"UPDATE PAGOS SET FECHAPAGO=@fecha, MONTO=@monto, METODOPAGO=@metodo
          WHERE IDPAGO=@id", con))
        {
            cmd.Parameters.AddWithValue("@fecha", p.FechaPago);
            cmd.Parameters.AddWithValue("@monto", p.Monto);
            cmd.Parameters.AddWithValue("@metodo", p.MetodoPago);
            cmd.Parameters.AddWithValue("@id", p.IdPago);

            int filas = cmd.ExecuteNonQuery();
            return filas > 0;
        }
    }

}