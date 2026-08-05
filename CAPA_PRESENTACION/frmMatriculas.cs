using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    // TODO: Formulario de gestión de matrículas
    // Permite registrar y eliminar matrículas de alumnos en niveles con instructores
    // Integrante 4 - Frankeny Castillo
    public partial class frmMatriculas : Form
    {
        // TODO: DAL de matrículas y entidades relacionadas
        private readonly MatriculaCD matriculaCD = new MatriculaCD();
        private readonly AlumnoCD alumnoCD = new AlumnoCD();
        private readonly NivelCD nivelCD = new NivelCD();
        private readonly InstructorCD instructorCD = new InstructorCD();
        private int idSeleccionado = 0;

        public frmMatriculas()
        {
            InitializeComponent();
        }

        // TODO: Al cargar el formulario se deshabilitan los controles
        // y se cargan los datos de forma asíncrona con Task.WhenAll
        private async void frmMatriculas_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: Deshabilitar controles al iniciar — se habilitan con btnNuevo
                DeshabilitarControles();
                lblEstado.Text = "Cargando datos...";
                lblEstado.ForeColor = Color.Orange;

                // TODO: Cargar combos y grilla de forma asíncrona simultáneamente
                await CargarCombosAsync();
                CargarGrilla();

                lblEstado.Text = "Listo";
                lblEstado.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar matrículas: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblEstado.Text = "Error al cargar";
                lblEstado.ForeColor = Color.Red;
            }
        }

        // TODO: Deshabilita todos los controles de entrada del formulario
        private void DeshabilitarControles()
        {
            cmbAlumno.Enabled = false;
            cmbNivel.Enabled = false;
            cmbInstructor.Enabled = false;
            dtpFechaMatricula.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        // TODO: Habilita todos los controles de entrada del formulario
        private void HabilitarControles()
        {
            cmbAlumno.Enabled = true;
            cmbNivel.Enabled = true;
            cmbInstructor.Enabled = true;
            dtpFechaMatricula.Enabled = true;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            btnLimpiar.Enabled = true;
        }

        // TODO: Carga los tres ComboBox de forma asíncrona usando Task.WhenAll
        // Esto evita bloquear la interfaz mientras se consulta la base de datos
        private async Task CargarCombosAsync()
        {
            // TODO: Ejecutar las tres consultas simultáneamente
            var tareasAlumnos = Task.Run(() => alumnoCD.ObtenerTodos());
            var tareasNiveles = Task.Run(() => nivelCD.ObtenerTodos());
            var tareasInstructores = Task.Run(() => instructorCD.ObtenerTodos());

            await Task.WhenAll(tareasAlumnos, tareasNiveles, tareasInstructores);

            // TODO: Asignar los resultados a los ComboBox en el hilo de la UI
            cmbAlumno.DataSource = tareasAlumnos.Result;
            cmbAlumno.DisplayMember = "Nombre";
            cmbAlumno.ValueMember = "IdAlumno";

            cmbNivel.DataSource = tareasNiveles.Result;
            cmbNivel.DisplayMember = "NombreNivel";
            cmbNivel.ValueMember = "IdNivel";

            cmbInstructor.DataSource = tareasInstructores.Result;
            cmbInstructor.DisplayMember = "Nombre";
            cmbInstructor.ValueMember = "IdInstructor";
        }

        // TODO: Carga la grilla con todas las matrículas registradas
        private void CargarGrilla()
        {
            dgvMatriculas.DataSource = matriculaCD.ObtenerTodos();
        }

        // TODO: Limpia los campos y deshabilita los controles nuevamente
        private void LimpiarCampos()
        {
            lblMensaje.Text = string.Empty;
            dtpFechaMatricula.Value = DateTime.Today;
            idSeleccionado = 0;
            if (cmbAlumno.Items.Count > 0) cmbAlumno.SelectedIndex = 0;
            if (cmbNivel.Items.Count > 0) cmbNivel.SelectedIndex = 0;
            if (cmbInstructor.Items.Count > 0) cmbInstructor.SelectedIndex = 0;
            DeshabilitarControles();
        }

        // TODO: Botón Nuevo — habilita los controles para ingresar una nueva matrícula
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            lblMensaje.Text = string.Empty;
            lblEstado.Text = "Ingresando nueva matrícula...";
            lblEstado.ForeColor = Color.Blue;
        }

        // TODO: Guarda una nueva matrícula verificando duplicados antes de insertar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Validar que todos los campos estén seleccionados
                if (cmbAlumno.SelectedValue == null ||
                    cmbNivel.SelectedValue == null ||
                    cmbInstructor.SelectedValue == null)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    return;
                }

                int idAlumno = Convert.ToInt32(cmbAlumno.SelectedValue);
                int idNivel = Convert.ToInt32(cmbNivel.SelectedValue);
                int idInstructor = Convert.ToInt32(cmbInstructor.SelectedValue);

                // TODO: Verificar que no exista una matrícula duplicada
                // Un alumno no puede estar matriculado dos veces en el mismo nivel
                bool duplicada = matriculaCD.ExisteMatriculaDuplicada(idAlumno, idNivel);
                if (duplicada)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Este alumno ya está matriculado en ese nivel.";
                    return;
                }

                _Matricula m = new _Matricula();
                m.IdAlumno = idAlumno;
                m.IdNivel = idNivel;
                m.IdInstructor = idInstructor;
                m.FechaMatricula = dtpFechaMatricula.Value;

                bool resultado = matriculaCD.Insertar(m);
                if (resultado)
                {
                    MessageBox.Show("Matrícula registrada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                    lblEstado.Text = "Listo";
                    lblEstado.ForeColor = Color.Green;
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

        // TODO: Elimina la matrícula seleccionada si no tiene pagos asociados
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

                // TODO: Verificar integridad referencial antes de eliminar
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

        // TODO: Limpia los campos y deshabilita los controles
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            lblEstado.Text = "Listo";
            lblEstado.ForeColor = Color.Green;
        }

        // TODO: Al seleccionar una fila en la grilla se guarda el ID de la matrícula
        private void dgvMatriculas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvMatriculas.SelectedRows.Count > 0)
                {
                    DataGridViewRow fila = dgvMatriculas.SelectedRows[0];
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