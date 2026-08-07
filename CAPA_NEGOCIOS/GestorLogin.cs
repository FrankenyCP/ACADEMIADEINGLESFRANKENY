using CAPA_DATOS;

namespace CAPA_NEGOCIOS;

// TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, hace de intermediaria entre CAPA_PRESENTACION (frmLogin) y CAPA_DATOS (UsuarioCD)
// TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente a gestionar el acceso del usuario, sin mezclar lógica de otras entidades
public class GestorLogin
{
    // TODO: Login - Método que valida las credenciales ingresadas en frmLogin, delegando la verificación real a UsuarioCD (CAPA_DATOS)
    // TODO: Métodos, métodos abstractos y métodos virtuales - Método público que encapsula la lógica de negocio del inicio de sesión
    public bool ValidarAcceso(string usuario, string contrasena)
    {
        UsuarioCD usuarioCD = new UsuarioCD();

        return usuarioCD.ValidarUsuario(usuario, contrasena);


    }
}