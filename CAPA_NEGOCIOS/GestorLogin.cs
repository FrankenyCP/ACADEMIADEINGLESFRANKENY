using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

public class GestorLogin
{
    public bool ValidarAcceso(string usuario, string contrasena)
    {
        UsuarioCD usuarioCD = new UsuarioCD();
        return usuarioCD.ValidarUsuario(usuario, contrasena);
    }
}