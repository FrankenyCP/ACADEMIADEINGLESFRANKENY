using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Encapsulacion - Atributos privados con propiedades públicas (get/set), representa un registro de la tabla USUARIOS
// TODO: Arquitectura en Capas - Clase de entidad (modelo) perteneciente a CAPA_DATOS
public class _Usuario
{
    private int idUsuario;
    private string usuario;
    private string contrasena;

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Usuario { get => usuario; set => usuario = value; }
    public string Contrasena { get => contrasena; set => contrasena = value; }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor vacío, evita valores null al instanciar el objeto antes de llenarlo
    public _Usuario()
    {
        this.usuario = string.Empty;
        this.contrasena = string.Empty;
    }

    // TODO: Clases creadas según su uso, sin código ajeno - Constructor sobrecargado, útil al leer una fila completa desde la base de datos
    public _Usuario(int idUsuario, string usuario, string contrasena)
    {
        this.idUsuario = idUsuario;
        this.usuario = usuario;
        this.contrasena = contrasena;
    }
}

// TODO: Arquitectura en Capas - Clase perteneciente a CAPA_DATOS, encargada exclusivamente del acceso a datos de la tabla USUARIOS
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a la validación de credenciales, sin lógica de negocio ni de presentación
public class UsuarioCD
{
    // TODO: Login - Verifica en la base de datos si el usuario y contraseña ingresados en el formulario de login son correctos
    // TODO: Conexión a datos - Abre conexión a SQL Server para consultar la tabla USUARIOS
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público síncrono que usa ExecuteScalar porque el resultado esperado es un único valor (COUNT)
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