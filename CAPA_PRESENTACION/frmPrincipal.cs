using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                CargarNivelesIniciales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar niveles: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarNivelesIniciales()
        {
            NivelCD nivelCD = new NivelCD();
            int cantidad = nivelCD.ContarNiveles();
            if (cantidad == 0)
            {
                string[] niveles = { "Básico", "Intermedio", "Avanzado" };
                int[] duraciones = { 3, 4, 6 };
                decimal[] costos = { 5000, 7000, 9000 };
                for (int i = 0; i <= 2; i++)
                {
                    _Nivel n = new _Nivel();
                    n.NombreNivel = niveles[i];
                    n.DuracionMeses = duraciones[i];
                    n.Costo = costos[i];
                    nivelCD.Insertar(n);
                }
            }
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            try
            {
                frmAlumnos frm = new frmAlumnos();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNiveles_Click(object sender, EventArgs e)
        {
            try
            {
                frmNiveles frm = new frmNiveles();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInstructores_Click(object sender, EventArgs e)
        {
            try
            {
                frmInstructores frm = new frmInstructores();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMatriculas_Click(object sender, EventArgs e)
        {
            try
            {
                frmMatriculas frm = new frmMatriculas();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            try
            {
                frmPagos frm = new frmPagos();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportes frm = new frmReportes();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro que desea salir?", "Salir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
                Application.Exit();
        }
    }
}
