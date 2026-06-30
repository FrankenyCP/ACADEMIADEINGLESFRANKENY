using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmAlumnos : Form
    {
        private AlumnoCD alumnoCD = new AlumnoCD();
        private MatriculaCD matriculaCD = new MatriculaCD();
        private int idSeleccionado = 0;

        public frmAlumnos()
        {
            InitializeComponent();
        }

        private void frmAlumnos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            dgvAlumnos.DataSource = alumnoCD.ObtenerTodos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            dtpFechaNacimiento.Value = DateTime.Today;
            chkIntensivo.Checked = false;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
        }

        private bool ValidarCampos()
        {
            if (txtNombre.Text.Trim() == string.Empty ||
                txtApellido.Text.Trim() == string.Empty ||
                txtTelefono.Text.Trim() == string.Empty ||
                txtCorreo.Text.Trim() == string.Empty)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Todos los campos con * son obligatorios.";
                return false;
            }
            else
            {
                lblMensaje.Text = string.Empty;
                return true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                _Alumno a = new _Alumno();
                a.Nombre = txtNombre.Text.Trim();
                a.Apellido = txtApellido.Text.Trim();
                a.FechaNacimiento = dtpFechaNacimiento.Value;
                a.Telefono = txtTelefono.Text.Trim();
                a.Correo = txtCorreo.Text.Trim();

                bool resultado = alumnoCD.Insertar(a);
                if (resultado)
                {
                    Alumno alumnoNeg = new Alumno(a.Nombre, a.Apellido, a.Telefono,
                                                  a.FechaNacimiento, chkIntensivo.Checked);
                    MessageBox.Show(alumnoNeg.EvaluarNivel(), "Modalidad del Alumno",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al guardar el alumno.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un alumno de la grilla para actualizar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos()) return;

                _Alumno a = new _Alumno();
                a.IdAlumno = idSeleccionado;
                a.Nombre = txtNombre.Text.Trim();
                a.Apellido = txtApellido.Text.Trim();
                a.FechaNacimiento = dtpFechaNacimiento.Value;
                a.Telefono = txtTelefono.Text.Trim();
                a.Correo = txtCorreo.Text.Trim();

                bool resultado = alumnoCD.Actualizar(a);
                if (resultado)
                {
                    MessageBox.Show("Alumno actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el alumno.", "Error",
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
                    MessageBox.Show("Seleccione un alumno de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool tieneMatriculas = alumnoCD.TieneMatriculas(idSeleccionado);
                if (tieneMatriculas)
                {
                    MessageBox.Show("No se puede eliminar: el alumno tiene matrículas registradas.",
                                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = alumnoCD.Eliminar(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Alumno eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el alumno.", "Error",
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

        private void btnVerNivel_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un alumno de la grilla.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nivel = matriculaCD.ObtenerNivelAlumno(idSeleccionado);
                MessageBox.Show("Nivel actual: " + nivel, "Nivel del Alumno",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPromover_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un alumno de la grilla.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nivelActual = matriculaCD.ObtenerNivelAlumno(idSeleccionado);
                Alumno alumnoNeg = new Alumno();
                string siguienteNivel = alumnoNeg.PromoverAlumno(nivelActual);

                if (siguienteNivel == "Completado")
                {
                    MessageBox.Show("El alumno ha completado todos los niveles. ¡Felicitaciones!",
                                    "Promoción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (siguienteNivel == "Nivel no reconocido.")
                {
                    MessageBox.Show(siguienteNivel, "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NivelCD nivelCD = new NivelCD();
                List<_Nivel> niveles = nivelCD.ObtenerTodos();
                int idNivelNuevo = 0;
                for (int i = 0; i < niveles.Count; i++)
                {
                    if (niveles[i].NombreNivel == siguienteNivel)
                    {
                        idNivelNuevo = niveles[i].IdNivel;
                        break;
                    }
                }

                if (idNivelNuevo == 0)
                {
                    MessageBox.Show("No se encontró el nivel siguiente en la base de datos.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<_Matricula> matriculas = matriculaCD.ObtenerTodos();
                int idInstructorActual = 0;
                for (int i = 0; i < matriculas.Count; i++)
                {
                    if (matriculas[i].IdAlumno == idSeleccionado)
                    {
                        idInstructorActual = matriculas[i].IdInstructor;
                    }
                }

                _Matricula nuevaMatricula = new _Matricula();
                nuevaMatricula.IdAlumno = idSeleccionado;
                nuevaMatricula.IdNivel = idNivelNuevo;
                nuevaMatricula.IdInstructor = idInstructorActual;
                nuevaMatricula.FechaMatricula = DateTime.Today;

                bool resultado = matriculaCD.Insertar(nuevaMatricula);
                if (resultado)
                {
                    MessageBox.Show("Alumno promovido a " + siguienteNivel + " correctamente.",
                                    "Promoción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al promover al alumno.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAlumnos.SelectedRows.Count > 0)
                {
                    DataGridViewRow fila = dgvAlumnos.SelectedRows[0];
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdAlumno"].Value);
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                    txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                    dtpFechaNacimiento.Value = Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value);
                    txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                    txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
