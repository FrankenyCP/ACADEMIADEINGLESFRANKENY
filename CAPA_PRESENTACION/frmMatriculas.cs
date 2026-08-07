using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    // TODO: Formulario de gestión de matrículas
    // Permite registrar y eliminar matrículas de alumnos en niveles con instructores

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
                // En modo integrado el encabezado con btnNuevo está oculto.
                // Por eso los campos deben quedar habilitados automáticamente.
                if (modoIntegradoSolicitado)
                {
                    HabilitarControles();
                }
                else
                {
                    DeshabilitarControles();
                }

                lblEstado.Text = "Cargando datos...";
                lblEstado.ForeColor = Color.Orange;

                // TODO: Cargar combos y grilla de forma asíncrona simultáneamente
                await CargarCombosAsync();
                CargarGrilla();

                // Nueva correccion:
                // En modo integrado los controles deben permanecer disponibles.
                if (modoIntegradoSolicitado)
                {
                    HabilitarControles();
                }

                lblEstado.Text = "Listo";
                lblEstado.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar matrículas: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

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
            btnLimpiar.Enabled = true;

            // Nueva correccion:
            // Eliminar solo debe estar disponible cuando se seleccione una fila.
            btnEliminar.Enabled = idSeleccionado > 0;

            // Nueva mejora:
            // Mostrar el cursor de mano para indicar que los botones responden.
            btnGuardar.Cursor = Cursors.Hand;
            btnEliminar.Cursor = Cursors.Hand;
            btnLimpiar.Cursor = Cursors.Hand;

            cmbAlumno.Cursor = Cursors.Hand;
            cmbNivel.Cursor = Cursors.Hand;
            cmbInstructor.Cursor = Cursors.Hand;
        }

        // TODO: Carga los tres ComboBox de forma asíncrona usando Task.WhenAll
        // Esto evita bloquear la interfaz mientras se consulta la base de datos
        private async Task CargarCombosAsync()
        {
            // TODO: Ejecutar las tres consultas simultáneamente
            var tareasAlumnos = Task.Run(() => alumnoCD.ObtenerTodos());
            var tareasNiveles = Task.Run(() => nivelCD.ObtenerTodos());
            var tareasInstructores = Task.Run(() => instructorCD.ObtenerTodos());

            await Task.WhenAll(
                tareasAlumnos,
                tareasNiveles,
                tareasInstructores
            );

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

            // Nueva correccion:
            // Evita conservar una seleccion vieja luego de recargar.
            dgvMatriculas.ClearSelection();

            idSeleccionado = 0;
            btnEliminar.Enabled = false;
        }

        // TODO: Limpia los campos y deshabilita los controles nuevamente
        private void LimpiarCampos()
        {
            lblMensaje.Text = string.Empty;
            dtpFechaMatricula.Value = DateTime.Today;

            idSeleccionado = 0;

            if (cmbAlumno.Items.Count > 0)
            {
                cmbAlumno.SelectedIndex = 0;
            }

            if (cmbNivel.Items.Count > 0)
            {
                cmbNivel.SelectedIndex = 0;
            }

            if (cmbInstructor.Items.Count > 0)
            {
                cmbInstructor.SelectedIndex = 0;
            }

            dgvMatriculas.ClearSelection();
            btnEliminar.Enabled = false;

            // Nueva correccion:
            // El boton Nuevo esta oculto cuando el formulario esta integrado.
            // Por eso no se deben bloquear los controles despues de limpiar.
            if (modoIntegradoSolicitado)
            {
                HabilitarControles();
                btnEliminar.Enabled = false;
            }
            else
            {
                DeshabilitarControles();
            }
        }

        // TODO: Botón Nuevo — habilita los controles para ingresar una nueva matrícula
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            idSeleccionado = 0;

            HabilitarControles();

            // Nueva correccion:
            // No permitir eliminar mientras se registra una matricula nueva.
            btnEliminar.Enabled = false;

            lblMensaje.Text = string.Empty;
            lblEstado.Text = "Ingresando nueva matrícula...";
            lblEstado.ForeColor = Color.Blue;

            cmbAlumno.Focus();
        }

        // TODO: Guarda una nueva matrícula verificando duplicados antes de insertar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Nueva proteccion:
                // Evita ejecutar el guardado varias veces con clics repetidos.
                btnGuardar.Enabled = false;

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
                bool duplicada =
                    matriculaCD.ExisteMatriculaDuplicada(idAlumno, idNivel);

                if (duplicada)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text =
                        "Este alumno ya está matriculado en ese nivel.";

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
                    MessageBox.Show(
                        "Matrícula registrada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CargarGrilla();
                    LimpiarCampos();

                    // Nueva correccion:
                    // Mantiene disponibles los controles en el formulario principal.
                    if (modoIntegradoSolicitado)
                    {
                        HabilitarControles();
                        btnEliminar.Enabled = false;
                        cmbAlumno.Focus();
                    }

                    lblEstado.Text = "Matrícula registrada";
                    lblEstado.ForeColor = Color.Green;
                }
                else
                {
                    MessageBox.Show(
                        "Error al registrar la matrícula.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // Nueva correccion:
                // Reactiva Guardar aunque ocurra una validacion o un error.
                if (modoIntegradoSolicitado)
                {
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnGuardar.Enabled = cmbAlumno.Enabled;
                }
            }
        }

        // TODO: Elimina la matrícula seleccionada si no tiene pagos asociados
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show(
                        "Seleccione una matrícula de la grilla para eliminar.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Seguro que desea eliminar la matrícula seleccionada?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // TODO: Verificar integridad referencial antes de eliminar
                bool tienePagos =
                    matriculaCD.TienePagos(idSeleccionado);

                if (tienePagos)
                {
                    MessageBox.Show(
                        "No se puede eliminar: la matrícula tiene pagos registrados.",
                        "Operación no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                btnEliminar.Enabled = false;

                bool resultado =
                    matriculaCD.Eliminar(idSeleccionado);

                if (resultado)
                {
                    MessageBox.Show(
                        "Matrícula eliminada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CargarGrilla();
                    LimpiarCampos();

                    lblEstado.Text = "Matrícula eliminada";
                    lblEstado.ForeColor = Color.Green;
                }
                else
                {
                    MessageBox.Show(
                        "Error al eliminar la matrícula.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    btnEliminar.Enabled = idSeleccionado > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                btnEliminar.Enabled = idSeleccionado > 0;
            }
        }

        // TODO: Limpia los campos y deshabilita los controles
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            lblEstado.Text = "Formulario limpio";
            lblEstado.ForeColor = Color.Green;

            // Nueva mejora:
            // Lleva el cursor al primer campo disponible.
            if (cmbAlumno.Enabled)
            {
                cmbAlumno.Focus();
            }
        }

        // TODO: Al seleccionar una fila en la grilla se guarda el ID de la matrícula
        private void dgvMatriculas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvMatriculas.SelectedRows.Count == 0)
                {
                    idSeleccionado = 0;
                    btnEliminar.Enabled = false;

                    return;
                }

                DataGridViewRow fila =
                    dgvMatriculas.SelectedRows[0];

                // Nueva validacion:
                // Ignora la fila vacia que puede mostrar el DataGridView.
                if (fila.IsNewRow)
                {
                    idSeleccionado = 0;
                    btnEliminar.Enabled = false;

                    return;
                }

                if (!dgvMatriculas.Columns.Contains("IdMatricula"))
                {
                    idSeleccionado = 0;
                    btnEliminar.Enabled = false;

                    return;
                }

                object valorId =
                    fila.Cells["IdMatricula"].Value;

                if (valorId == null || valorId == DBNull.Value)
                {
                    idSeleccionado = 0;
                    btnEliminar.Enabled = false;

                    return;
                }

                idSeleccionado =
                    Convert.ToInt32(valorId);

                btnEliminar.Enabled = idSeleccionado > 0;
            }
            catch (Exception ex)
            {
                idSeleccionado = 0;
                btnEliminar.Enabled = false;

                MessageBox.Show(
                    "Error al seleccionar la matrícula: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Nuevo metodo:
        // Permite actualizar alumnos, niveles, instructores y matriculas
        // cuando otro modulo agrega o cambia informacion.
        public async Task ActualizarDatosAsync()
        {
            try
            {
                lblEstado.Text = "Actualizando datos...";
                lblEstado.ForeColor = Color.Orange;

                int alumnoAnterior = ObtenerValorSeleccionado(cmbAlumno);
                int nivelAnterior = ObtenerValorSeleccionado(cmbNivel);
                int instructorAnterior = ObtenerValorSeleccionado(cmbInstructor);

                await CargarCombosAsync();
                CargarGrilla();

                RestaurarSeleccionCombo(
                    cmbAlumno,
                    alumnoAnterior
                );

                RestaurarSeleccionCombo(
                    cmbNivel,
                    nivelAnterior
                );

                RestaurarSeleccionCombo(
                    cmbInstructor,
                    instructorAnterior
                );

                if (modoIntegradoSolicitado)
                {
                    HabilitarControles();
                    btnEliminar.Enabled = false;
                }

                lblEstado.Text = "Datos actualizados";
                lblEstado.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error al actualizar";
                lblEstado.ForeColor = Color.Red;

                MessageBox.Show(
                    "No fue posible actualizar las matrículas: " +
                    ex.Message,
                    "Matrículas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Nuevo metodo:
        // Obtiene el identificador seleccionado de un ComboBox.
        private int ObtenerValorSeleccionado(ComboBox combo)
        {
            if (combo.SelectedValue == null)
            {
                return 0;
            }

            int valor;

            bool convertido = int.TryParse(
                combo.SelectedValue.ToString(),
                out valor
            );

            if (!convertido)
            {
                return 0;
            }

            return valor;
        }

        // Nuevo metodo:
        // Intenta mantener la seleccion despues de actualizar los datos.
        private void RestaurarSeleccionCombo(
            ComboBox combo,
            int valorAnterior)
        {
            if (valorAnterior <= 0)
            {
                if (combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                }

                return;
            }

            try
            {
                combo.SelectedValue = valorAnterior;

                // Si el valor ya no existe se usa el primer registro.
                if (combo.SelectedIndex < 0 &&
                    combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                }
            }
            catch
            {
                if (combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                }
            }
        }
    }
}