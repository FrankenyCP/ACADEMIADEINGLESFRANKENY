using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmInstructores : Form
    {
        private InstructorCD instructorCD = new InstructorCD();
        private int idSeleccionado = 0;

        public frmInstructores()
        {
            InitializeComponent();
        }

        private void frmInstructores_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar instructores: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            dgvInstructores.DataSource = instructorCD.ObtenerTodos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
        }

        private bool ValidarCampos()
        {
            if (txtNombre.Text.Trim() == string.Empty ||
                txtApellido.Text.Trim() == string.Empty ||
                txtEspecialidad.Text.Trim() == string.Empty ||
                txtTelefono.Text.Trim() == string.Empty)
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

                _Instructor i = new _Instructor();
                i.Nombre = txtNombre.Text.Trim();
                i.Apellido = txtApellido.Text.Trim();
                i.Especialidad = txtEspecialidad.Text.Trim();
                i.Telefono = txtTelefono.Text.Trim();

                Instructor instructorNeg = new Instructor(i.Nombre, i.Apellido,
                                                          i.Telefono, i.Especialidad);
                bool resultado = instructorNeg.RegistrarInstructor(i);
                if (resultado)
                {
                    MessageBox.Show(instructorNeg.ObtenerInformacion(), "Instructor Registrado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al guardar el instructor.", "Error",
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
                    MessageBox.Show("Seleccione un instructor de la grilla para actualizar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos()) return;

                _Instructor i = new _Instructor();
                i.IdInstructor = idSeleccionado;
                i.Nombre = txtNombre.Text.Trim();
                i.Apellido = txtApellido.Text.Trim();
                i.Especialidad = txtEspecialidad.Text.Trim();
                i.Telefono = txtTelefono.Text.Trim();

                bool resultado = instructorCD.Actualizar(i);
                if (resultado)
                {
                    MessageBox.Show("Instructor actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el instructor.", "Error",
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
                    MessageBox.Show("Seleccione un instructor de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool tieneMatriculas = instructorCD.TieneMatriculas(idSeleccionado);
                if (tieneMatriculas)
                {
                    MessageBox.Show("No se puede eliminar: el instructor tiene matrículas registradas.",
                                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = instructorCD.Eliminar(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Instructor eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el instructor.", "Error",
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

        private void dgvInstructores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvInstructores.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdInstructor"].Value);
                    txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                    txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                    txtEspecialidad.Text = fila.Cells["Especialidad"].Value.ToString();
                    txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
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