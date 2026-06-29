using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    public partial class frmNiveles : Form
    {
        private NivelCD nivelCD = new NivelCD();
        private int idSeleccionado = 0;

        public frmNiveles()
        {
            InitializeComponent();
        }

        private void frmNiveles_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar niveles: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            dgvNiveles.DataSource = nivelCD.ObtenerTodos();
        }

        private void LimpiarCampos()
        {
            txtNombreNivel.Text = string.Empty;
            txtDuracion.Text = string.Empty;
            txtCosto.Text = string.Empty;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
        }

        private bool ValidarCampos()
        {
            if (txtNombreNivel.Text.Trim() == string.Empty ||
                txtDuracion.Text.Trim() == string.Empty ||
                txtCosto.Text.Trim() == string.Empty)
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

                _Nivel n = new _Nivel();
                n.NombreNivel = txtNombreNivel.Text.Trim();
                n.DuracionMeses = Convert.ToInt32(txtDuracion.Text.Trim());
                n.Costo = Convert.ToDecimal(txtCosto.Text.Trim());

                bool resultado = nivelCD.Insertar(n);
                if (resultado)
                {
                    MessageBox.Show("Nivel guardado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al guardar el nivel.", "Error",
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
                    MessageBox.Show("Seleccione un nivel de la grilla para actualizar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos()) return;

                _Nivel n = new _Nivel();
                n.IdNivel = idSeleccionado;
                n.NombreNivel = txtNombreNivel.Text.Trim();
                n.DuracionMeses = Convert.ToInt32(txtDuracion.Text.Trim());
                n.Costo = Convert.ToDecimal(txtCosto.Text.Trim());

                bool resultado = nivelCD.Actualizar(n);
                if (resultado)
                {
                    MessageBox.Show("Nivel actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el nivel.", "Error",
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
                    MessageBox.Show("Seleccione un nivel de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = nivelCD.Eliminar(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Nivel eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el nivel.", "Error",
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

        private void dgvNiveles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvNiveles.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdNivel"].Value);
                    txtNombreNivel.Text = fila.Cells["NombreNivel"].Value.ToString();
                    txtDuracion.Text = fila.Cells["DuracionMeses"].Value.ToString();
                    txtCosto.Text = fila.Cells["Costo"].Value.ToString();
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