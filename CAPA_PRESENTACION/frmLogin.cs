using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();

                if (usuario == string.Empty || contrasena == string.Empty)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Por favor ingrese usuario y contraseña.";
                    return;
                }

                GestorLogin gestor = new GestorLogin();
                bool acceso = gestor.ValidarAcceso(usuario, contrasena);

                if (acceso)
                {
                    frmPrincipal principal = new frmPrincipal();
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}