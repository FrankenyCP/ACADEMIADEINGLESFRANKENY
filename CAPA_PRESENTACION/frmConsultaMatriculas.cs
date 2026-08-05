using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Formulario de consulta de matrículas y estado de pagos
    // Permite buscar alumnos y ver su estado de pago con semáforo visual
    // Integrante 4 - Frankeny Castillo
    public partial class frmConsultaMatriculas : Form
    {
        // TODO: DAL necesarios para la consulta
        private readonly MatriculaCD _matriculaCD = new MatriculaCD();
        private readonly PagoCD _pagoCD = new PagoCD();
        private readonly NivelCD _nivelCD = new NivelCD();

        // TODO: Lista completa de matrículas para filtrar sin volver a consultar BD
        private List<_Matricula> _todasLasMatriculas = new List<_Matricula>();

        public frmConsultaMatriculas()
        {
            InitializeComponent();
        }

        // TODO: Al cargar el formulario se cargan todas las matrículas de forma asíncrona
        private async void frmConsultaMatriculas_Load(object sender, EventArgs e)
        {
            try
            {
                await CargarMatriculasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar consulta: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Carga todas las matrículas de forma asíncrona
        private async Task CargarMatriculasAsync()
        {
            _todasLasMatriculas = await Task.Run(() => _matriculaCD.ObtenerTodos());
            dgvConsulta.DataSource = _todasLasMatriculas;
            LimpiarDetalle();
        }

        // TODO: Busca matrículas por nombre de alumno en tiempo real
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string busqueda = txtBuscar.Text.Trim().ToLower();

                if (busqueda == string.Empty)
                {
                    dgvConsulta.DataSource = _todasLasMatriculas;
                    return;
                }

                // TODO: Filtrar la lista sin hacer otra consulta a la BD
                List<_Matricula> filtradas = new List<_Matricula>();
                for (int i = 0; i < _todasLasMatriculas.Count; i++)
                {
                    if (_todasLasMatriculas[i].NombreAlumno.ToLower().Contains(busqueda))
                        filtradas.Add(_todasLasMatriculas[i]);
                }

                dgvConsulta.DataSource = filtradas;
                LimpiarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Limpia la búsqueda y muestra todas las matrículas
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            dgvConsulta.DataSource = _todasLasMatriculas;
            LimpiarDetalle();
        }

        // TODO: Al seleccionar una fila muestra el detalle del alumno con semáforo
        private void dgvConsulta_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsulta.SelectedRows.Count == 0) return;

                DataGridViewRow fila = dgvConsulta.SelectedRows[0];
                int idMatricula = Convert.ToInt32(fila.Cells["IdMatricula"].Value);
                int idNivel = Convert.ToInt32(fila.Cells["IdNivel"].Value);
                string nombreAlumno = fila.Cells["NombreAlumno"].Value.ToString();
                string nombreNivel = fila.Cells["NombreNivel"].Value.ToString();

                // TODO: Obtener costo del nivel
                decimal costoNivel = 0;
                List<_Nivel> niveles = _nivelCD.ObtenerTodos();
                for (int i = 0; i < niveles.Count; i++)
                {
                    if (niveles[i].IdNivel == idNivel)
                    {
                        costoNivel = niveles[i].Costo;
                        break;
                    }
                }

                // TODO: Obtener total pagado
                decimal totalPagado = _pagoCD.ObtenerTotalPagado(idMatricula);
                decimal saldoPendiente = costoNivel - totalPagado;

                // TODO: Mostrar datos en el panel de detalle
                lblValorNombreAlumno.Text = nombreAlumno;
                lblValorNivel.Text = nombreNivel;
                lblValorCosto.Text = "RD$" + costoNivel.ToString("N2");
                lblValorPagado.Text = "RD$" + totalPagado.ToString("N2");
                lblValorPendiente.Text = "RD$" + saldoPendiente.ToString("N2");

                // TODO: Semáforo de estado según el saldo pendiente
                // Verde = pagado completo, Amarillo = pagado parcial, Rojo = sin pagos
                ActualizarSemaforo(totalPagado, costoNivel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Actualiza el semáforo visual según el estado de pago
        // Verde = al día, Amarillo = pago parcial, Rojo = sin pagos
        private void ActualizarSemaforo(decimal totalPagado, decimal costoNivel)
        {
            if (totalPagado >= costoNivel)
            {
                // TODO: Verde — pagado completo
                lblValorEstado.Text = "✅ Al día — Pago completo";
                lblValorEstado.ForeColor = Color.FromArgb(46, 213, 115);
                picEstado.BackColor = Color.FromArgb(46, 213, 115);
            }
            else if (totalPagado > 0)
            {
                // TODO: Amarillo — pago parcial
                lblValorEstado.Text = "⚠️ Pago parcial — Tiene saldo pendiente";
                lblValorEstado.ForeColor = Color.FromArgb(255, 165, 0);
                picEstado.BackColor = Color.FromArgb(255, 165, 0);
            }
            else
            {
                // TODO: Rojo — sin pagos
                lblValorEstado.Text = "🔴 Sin pagos — Debe el total";
                lblValorEstado.ForeColor = Color.FromArgb(192, 57, 43);
                picEstado.BackColor = Color.FromArgb(192, 57, 43);
            }
        }

        // TODO: Limpia el panel de detalle al cambiar la búsqueda
        private void LimpiarDetalle()
        {
            lblValorNombreAlumno.Text = "-";
            lblValorNivel.Text = "-";
            lblValorCosto.Text = "RD$0.00";
            lblValorPagado.Text = "RD$0.00";
            lblValorPendiente.Text = "RD$0.00";
            lblValorEstado.Text = "-";
            lblValorEstado.ForeColor = Color.White;
            picEstado.BackColor = Color.FromArgb(13, 27, 42);
        }
    }
}