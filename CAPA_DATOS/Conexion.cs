using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CAPA_DATOS;

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, se encarga únicamente de proveer la conexión a la base de datos
// TODO: Conexión a datos - Clase centralizada que gestiona la conexión a SQL Server para todo el sistema
public class Conexion
{
    // TODO: Conexión a datos - Cadena de conexión privada y estática, apunta a la base de datos ACADEMIADEINGLESFRANKENY en el servidor local
    private static readonly string _cadena =
        @"Server=.;Database=ACADEMIADEINGLESFRANKENY;" +
        "Trusted_Connection=True;TrustServerCertificate=True;";

    // TODO: Conexión a datos - Abre y retorna una conexión síncrona lista para usar, empleada por los métodos CRUD síncronos de las demás clases CD
    public static SqlConnection ObtenerConexion()
    {
        var conexion = new SqlConnection(_cadena);
        conexion.Open();
        return conexion;
    }

    // TODO: Llamadas asíncronas - Verifica de forma asíncrona si es posible conectarse a la base de datos, sin bloquear el hilo principal
    // TODO: Captura de error (try-catch) - Atrapa cualquier fallo de conexión y retorna false en vez de dejar que la excepción cierre la aplicación
    public static async Task<bool> ProbarConexionAsync()
    {
        try
        {
            using SqlConnection conexion =
                new SqlConnection(_cadena);

            await conexion.OpenAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    // TODO: Llamadas asíncronas - Abre la conexión de forma asíncrona con OpenAsync(), es la versión que usan todos los métodos *Async de las clases CD
    // TODO: Captura de error (try-catch) - Si falla la apertura, libera los recursos de la conexión antes de relanzar la excepción, evitando fugas de memoria
    internal static async Task<SqlConnection> ObtenerConexionAsync()
    {
        SqlConnection conexion = new SqlConnection(_cadena);

        try
        {
            await conexion.OpenAsync();
            return conexion;
        }
        catch
        {
            await conexion.DisposeAsync();
            throw;
        }
    }
}