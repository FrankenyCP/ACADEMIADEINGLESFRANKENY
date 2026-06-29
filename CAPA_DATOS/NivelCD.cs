using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

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

    public _Nivel()
    {
        this.nombreNivel = string.Empty;
    }

    public _Nivel(int idNivel, string nombreNivel, int duracionMeses, decimal costo)
    {
        this.idNivel = idNivel;
        this.nombreNivel = nombreNivel;
        this.duracionMeses = duracionMeses;
        this.costo = costo;
    }
}

public class NivelCD
{
    public bool Insertar(_Nivel n)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO NIVELES (NOMBRENIVEL, DURACIONMESES, COSTO)
              VALUES (@nombre, @duracion, @costo)", con))
        {
            cmd.Parameters.AddWithValue("@nombre", n.NombreNivel);
            cmd.Parameters.AddWithValue("@duracion", n.DuracionMeses);
            cmd.Parameters.AddWithValue("@costo", n.Costo);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    public List<_Nivel> ObtenerTodos()
    {
        var lista = new List<_Nivel>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT * FROM NIVELES", con))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
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

    public int ContarNiveles()
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NIVELES", con))
        {
            return (int)cmd.ExecuteScalar();
        }
    }
}