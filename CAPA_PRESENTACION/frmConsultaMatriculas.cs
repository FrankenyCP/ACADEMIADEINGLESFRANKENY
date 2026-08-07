using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Arquitectura en Capas - Formulario perteneciente a CAPA_PRESENTACION, es la opción de "consulta" del sistema
    // TODO: Opción consulta - Permite dar un vistazo a los datos ya guardados (matrículas y su estado de pago), sin permitir modificarlos
    // Permite buscar alumnos y ver su estado de pago con semáforo visual
    // Integrante 4 - Frankeny Castillo
    public partial class frmConsultaMatriculas : Form
    {
        // TODO: Arquitectura en Capas - Instancias de las clases de CAPA_DATOS usadas por este formulario para acceder a la información
        private readonly MatriculaCD _matriculaCD = new MatriculaCD();
        private readonly PagoCD _pagoCD = new PagoCD();
        private readonly NivelCD _nivelCD = new NivelCD();

        // TODO: Clases creadas según su uso, sin código ajeno - Lista completa de matrículas guardada en memoria para filtrar sin volver a consultar la base de datos
        private List<_Matricula> _todasLasMatriculas = new List<_Matricula>();

        public frmConsultaMatriculas()
        {
            InitializeComponent();
        }

        // TODO: Llamadas asíncronas - Evento Load declarado async void, permite usar await al cargar las matrículas sin bloquear la interfaz gráfica
        // TODO: Captura de error (try-catch) - Envuelve la carga inicial en try-catch para evitar que un fallo de conexión cierre la aplicación de forma forzada
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

        // TODO: Llamadas asíncronas - Método asíncrono que usa Task.Run para traer todas las matrículas sin congelar la interfaz gráfica
        // TODO: Opción consulta - Alimenta la grilla que permite dar un vistazo a los datos ya guardados
        private async Task CargarMatriculasAsync()
        {
            _todasLasMatriculas = await Task.Run(() => _matriculaCD.ObtenerTodos());
            dgvConsulta.DataSource = _todasLasMatriculas;
            LimpiarDetalle();
        }

        // TODO: Opción consulta - Filtra las matrículas ya guardadas por nombre de alumno, sin permitir editarlas
        // TODO: Captura de error (try-catch) - Envuelve la búsqueda en try-catch para evitar el cierre forzado de la aplicación
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

                // Filtrar la lista sin hacer otra consulta a la BD
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

        // TODO: Opción consulta - Restaura la vista completa de matrículas ya guardadas, sin necesidad de una nueva consulta a la base de datos
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            dgvConsulta.DataSource = _todasLasMatriculas;
            LimpiarDetalle();
        }

        // TODO: Opción consulta - Al seleccionar una fila, muestra el detalle del alumno (solo lectura), incluyendo el estado de pago mediante semáforo visual
        // TODO: Captura de error (try-catch) - Envuelve la lectura de la fila seleccionada y el cálculo del saldo en try-catch
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

                // Obtener costo del nivel
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

                // Obtener total pagado
                decimal totalPagado = _pagoCD.ObtenerTotalPagado(idMatricula);
                decimal saldoPendiente = costoNivel - totalPagado;

                // Mostrar datos en el panel de detalle
                lblValorNombreAlumno.Text = nombreAlumno;
                lblValorNivel.Text = nombreNivel;
                lblValorCosto.Text = "RD$" + costoNivel.ToString("N2");
                lblValorPagado.Text = "RD$" + totalPagado.ToString("N2");
                lblValorPendiente.Text = "RD$" + saldoPendiente.ToString("N2");

                // Semáforo de estado según el saldo pendiente
                // Verde = pagado completo, Amarillo = pagado parcial, Rojo = sin pagos
                ActualizarSemaforo(totalPagado, costoNivel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que calcula y aplica el color del semáforo (verde/amarillo/rojo) según el estado de pago del alumno
        // Verde = al día, Amarillo = pago parcial, Rojo = sin pagos
        private void ActualizarSemaforo(decimal totalPagado, decimal costoNivel)
        {
            if (totalPagado >= costoNivel)
            {
                // Verde — pagado completo
                lblValorEstado.Text = "✅ Al día — Pago completo";
                lblValorEstado.ForeColor = Color.FromArgb(46, 213, 115);
                picEstado.BackColor = Color.FromArgb(46, 213, 115);
            }
            else if (totalPagado > 0)
            {
                // Amarillo — pago parcial
                lblValorEstado.Text = "⚠️ Pago parcial — Tiene saldo pendiente";
                lblValorEstado.ForeColor = Color.FromArgb(255, 165, 0);
                picEstado.BackColor = Color.FromArgb(255, 165, 0);
            }
            else
            {
                // Rojo — sin pagos
                lblValorEstado.Text = "🔴 Sin pagos — Debe el total";
                lblValorEstado.ForeColor = Color.FromArgb(192, 57, 43);
                picEstado.BackColor = Color.FromArgb(192, 57, 43);
            }
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que reinicia el panel de detalle a sus valores por defecto
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