using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CAPA_DATOS;

public class Conexion
{
    private static readonly string _cadena =
        @"Server=.;Database=ACADEMIADEINGLESFRANKENY;" +
        "Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection ObtenerConexion()
    {
        var conexion = new SqlConnection(_cadena);
        conexion.Open();
        return conexion;
    }
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
}