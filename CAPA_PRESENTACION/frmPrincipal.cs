using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Formulario principal del sistema — Dashboard de la Academia de Inglés
    // Muestra indicadores en tiempo real y permite navegar a todos los módulos
    // Integrante 4 - Frankeny Castillo
    public partial class frmPrincipal : Form
    {
        // TODO: Servicio del Dashboard — usa la interfaz IDashboardServicio
        // para obtener los indicadores del sistema de forma asíncrona
        private readonly IDashboardServicio _dashboardServicio;

        // TODO: Constructor — inicializa el servicio del Dashboard
        public frmPrincipal()
        {
            InitializeComponent();
            _dashboardServicio = new DashboardServicio();
        }

        // TODO: Al cargar el formulario se inicializan los niveles y el Dashboard
        private async void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: Insertar niveles iniciales si la tabla está vacía
                CargarNivelesIniciales();

                // TODO: Cargar el Dashboard de forma asíncrona
                await CargarDashboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Inserta los niveles Básico, Intermedio y Avanzado automáticamente
        // si la tabla NIVELES está vacía al iniciar el sistema por primera vez
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

        // TODO: Carga todos los indicadores del Dashboard de forma asíncrona
        // Usa Task.WhenAll para ejecutar todas las consultas simultáneamente
        // y así no bloquear la interfaz mientras se obtienen los datos
        private async Task CargarDashboardAsync()
        {
            try
            {
                lblEstadoDashboard.Text = "Actualizando...";
                lblEstadoDashboard.ForeColor = Color.Orange;

                // TODO: Ejecutar todas las consultas al mismo tiempo con Task.WhenAll
                var tareaAlumnos = _dashboardServicio.ObtenerTotalAlumnosAsync();
                var tareaMatriculas = _dashboardServicio.ObtenerTotalMatriculasActivasAsync();
                var tareaInstructores = _dashboardServicio.ObtenerTotalInstructoresAsync();
                var tareaIngresos = _dashboardServicio.ObtenerTotalIngresosAsync();
                var tareaPendiente = _dashboardServicio.ObtenerTotalPendienteAsync();
                var tareaNivel = _dashboardServicio.ObtenerNivelMasPopularAsync();

                await Task.WhenAll(tareaAlumnos, tareaMatriculas, tareaInstructores,
                                   tareaIngresos, tareaPendiente, tareaNivel);

                // TODO: Actualizar los labels del Dashboard con los resultados
                lblTotalAlumnos.Text = tareaAlumnos.Result.ToString();
                lblTotalMatriculas.Text = tareaMatriculas.Result.ToString();
                lblTotalInstructores.Text = tareaInstructores.Result.ToString();
                lblTotalIngresos.Text = "RD$" + tareaIngresos.Result.ToString("N2");
                lblTotalPendiente.Text = "RD$" + tareaPendiente.Result.ToString("N2");
                lblNivelPopular.Text = tareaNivel.Result;

                lblEstadoDashboard.Text = "✓ Dashboard actualizado — " + DateTime.Now.ToString("hh:mm tt");
                lblEstadoDashboard.ForeColor = Color.LimeGreen;
            }
            catch (Exception ex)
            {
                lblEstadoDashboard.Text = "Error al cargar Dashboard";
                lblEstadoDashboard.ForeColor = Color.Red;
                MessageBox.Show("Error en Dashboard: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Botón para refrescar el Dashboard manualmente
        private async void btnRefrescarDashboard_Click(object sender, EventArgs e)
        {
            await CargarDashboardAsync();
        }

        // ===================== NAVEGACIÓN =====================

        // TODO: Abre el formulario de gestión de alumnos
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

        // TODO: Abre el formulario de gestión de niveles
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

        // TODO: Abre el formulario de gestión de instructores
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

        // TODO: Abre el formulario de gestión de matrículas
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

        // TODO: Abre el formulario de gestión de pagos
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

        // TODO: Abre el formulario de reportes
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

        // TODO: Cierra la aplicación con confirmación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro que desea salir?", "Salir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
                Application.Exit();
        }

        // TODO: Abre el formulario de consulta de matrículas y estado de pagos
        private void btnConsultaMatriculas_Click(object sender, EventArgs e)
        {
            try
            {
                frmConsultaMatriculas frm = new frmConsultaMatriculas();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
