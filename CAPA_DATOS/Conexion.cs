using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

public class Conexion
{
    private static readonly string _cadena =
        @"Server=(localdb)\MSSQLLocalDB;Database=ACADEMIADEINGLESFRANKENY;" +
        "Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection ObtenerConexion()
    {
        var conexion = new SqlConnection(_cadena);
        conexion.Open();
        return conexion;
    }
}