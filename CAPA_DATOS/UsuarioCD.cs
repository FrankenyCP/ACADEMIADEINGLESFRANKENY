using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

public class _Usuario
{
    private int idUsuario;
    private string usuario;
    private string contrasena;

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Usuario { get => usuario; set => usuario = value; }
    public string Contrasena { get => contrasena; set => contrasena = value; }

    public _Usuario()
    {
        this.usuario = string.Empty;
        this.contrasena = string.Empty;
    }

    public _Usuario(int idUsuario, string usuario, string contrasena)
    {
        this.idUsuario = idUsuario;
        this.usuario = usuario;
        this.contrasena = contrasena;
    }
}

public class UsuarioCD
{
    public bool ValidarUsuario(string usuario, string contrasena)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT COUNT(*) FROM USUARIOS
              WHERE USUARIO=@usuario AND CONTRASENA=@contrasena", con))
        {
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@contrasena", contrasena);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }
}