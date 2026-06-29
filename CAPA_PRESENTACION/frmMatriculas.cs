using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    public partial class frmMatriculas : Form
    {
        private MatriculaCD matriculaCD = new MatriculaCD();
        private AlumnoCD alumnoCD = new AlumnoCD();
        private NivelCD nivelCD = new NivelCD();
        private InstructorCD instructorCD = new InstructorCD();
        private int idSeleccionado = 0;

        public frmMatriculas()
        {
            InitializeComponent();
        }

        private void frmMatriculas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar matrículas: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            cmbAlumno.DataSource = alumnoCD.ObtenerTodos();
            cmbAlumno.DisplayMember = "Nombre";
            cmbAlumno.ValueMember = "IdAlumno";

            cmbNivel.DataSource = nivelCD.ObtenerTodos();
            cmbNivel.DisplayMember = "NombreNivel";
            cmbNivel.ValueMember = "IdNivel";

            cmbInstructor.DataSource = instructorCD.ObtenerTodos();
            cmbInstructor.DisplayMember = "Nombre";
            cmbInstructor.ValueMember = "IdInstructor";
        }

        private void CargarGrilla()
        {
            dgvMatriculas.DataSource = matriculaCD.ObtenerTodos();
        }

        private void LimpiarCampos()
        {
            lblMensaje.Text = string.Empty;
            dtpFechaMatricula.Value = DateTime.Today;
            idSeleccionado = 0;
            if (cmbAlumno.Items.Count > 0) cmbAlumno.SelectedIndex = 0;
            if (cmbNivel.Items.Count > 0) cmbNivel.SelectedIndex = 0;
            if (cmbInstructor.Items.Count > 0) cmbInstructor.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAlumno.SelectedValue == null ||
                    cmbNivel.SelectedValue == null ||
                    cmbInstructor.SelectedValue == null)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    return;
                }

                _Matricula m = new _Matricula();
                m.IdAlumno = Convert.ToInt32(cmbAlumno.SelectedValue);
                m.IdNivel = Convert.ToInt32(cmbNivel.SelectedValue);
                m.IdInstructor = Convert.ToInt32(cmbInstructor.SelectedValue);
                m.FechaMatricula = dtpFechaMatricula.Value;

                bool resultado = matriculaCD.Insertar(m);
                if (resultado)
                {
                    MessageBox.Show("Matrícula registrada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al registrar la matrícula.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione una matrícula de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool tienePagos = matriculaCD.TienePagos(idSeleccionado);
                if (tienePagos)
                {
                    MessageBox.Show("No se puede eliminar: la matrícula tiene pagos registrados.",
                                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = matriculaCD.Eliminar(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Matrícula eliminada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la matrícula.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvMatriculas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvMatriculas.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdMatricula"].Value);
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