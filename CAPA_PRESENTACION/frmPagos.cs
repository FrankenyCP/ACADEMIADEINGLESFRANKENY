using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmPagos : Form
    {
        private PagoCD pagoCD = new PagoCD();
        private MatriculaCD matriculaCD = new MatriculaCD();
        private NivelCD nivelCD = new NivelCD();
        private int idSeleccionado = 0;

        public frmPagos()
        {
            InitializeComponent();
        }

        private void frmPagos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarComboMatriculas();
                CargarComboMetodo();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboMatriculas()
        {
            cmbMatricula.DataSource = matriculaCD.ObtenerTodos();
            cmbMatricula.DisplayMember = "NombreAlumno";
            cmbMatricula.ValueMember = "IdMatricula";
        }

        private void CargarComboMetodo()
        {
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.Items.Add("Transferencia");
            cmbMetodoPago.SelectedIndex = 0;
        }

        private void CargarGrilla()
        {
            if (cmbMatricula.SelectedValue != null && cmbMatricula.SelectedValue is int)
            {
                int idMatricula = Convert.ToInt32(cmbMatricula.SelectedValue);
                dgvPagos.DataSource = pagoCD.ObtenerPorMatricula(idMatricula);
            }
            else
            {
                dgvPagos.DataSource = pagoCD.ObtenerTodos();
            }
        }

        private void LimpiarCampos()
        {
            txtMonto.Text = string.Empty;
            dtpFechaPago.Value = DateTime.Today;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
            if (cmbMatricula.Items.Count > 0) cmbMatricula.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;
        }

        private decimal ObtenerCostoNivel(int idMatricula)
        {
            decimal costoNivel = 0;
            List<_Matricula> matriculas = matriculaCD.ObtenerTodos();
            for (int i = 0; i < matriculas.Count; i++)
            {
                if (matriculas[i].IdMatricula == idMatricula)
                {
                    List<_Nivel> niveles = nivelCD.ObtenerTodos();
                    for (int j = 0; j < niveles.Count; j++)
                    {
                        if (niveles[j].IdNivel == matriculas[i].IdNivel)
                        {
                            costoNivel = niveles[j].Costo;
                            break;
                        }
                    }
                    break;
                }
            }
            return costoNivel;
        }

        private void ActualizarSaldo()
        {
            if (cmbMatricula.SelectedValue == null || !(cmbMatricula.SelectedValue is int)) return;

            int idMatricula = Convert.ToInt32(cmbMatricula.SelectedValue);
            decimal totalPagado = pagoCD.ObtenerTotalPagado(idMatricula);
            decimal costoNivel = ObtenerCostoNivel(idMatricula);
            decimal saldoRestante = costoNivel - totalPagado;

            lblSaldo.Text = "Pagado: RD$" + totalPagado.ToString("N2") +
                            " | Saldo restante: RD$" + saldoRestante.ToString("N2");

            dgvPagos.DataSource = pagoCD.ObtenerPorMatricula(idMatricula);
        }

        private void cmbMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMatricula.SelectedValue != null && cmbMatricula.SelectedValue is int)
                    ActualizarSaldo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMatricula.SelectedValue == null || txtMonto.Text.Trim() == string.Empty)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    return;
                }

                int idMatricula = Convert.ToInt32(cmbMatricula.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text.Trim());
                decimal costoNivel = ObtenerCostoNivel(idMatricula);

                Alumno alumnoNeg = new Alumno();
                _Pago p = new _Pago();
                p.IdMatricula = idMatricula;
                p.FechaPago = dtpFechaPago.Value;
                p.Monto = monto;
                p.MetodoPago = cmbMetodoPago.SelectedItem.ToString();

                bool resultado = alumnoNeg.RegistrarPago(p, costoNivel);
                if (resultado)
                {
                    MessageBox.Show("Pago registrado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Alumno alumnoEval = new Alumno();
                    decimal totalPagadoActual = pagoCD.ObtenerTotalPagado(idMatricula);
                    string evaluacion = alumnoNeg.EvaluarAprobacion(totalPagadoActual, costoNivel);
                    MessageBox.Show(evaluacion, "Estado de Pago",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    ActualizarSaldo();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("El monto excede el saldo restante del curso. Pago no permitido.",
                                    "Error de saldo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Seleccione un pago de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = pagoCD.Eliminar(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Pago eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el pago.", "Error",
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

        private void dgvPagos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPagos.CurrentRow != null)
                {
                    DataGridViewRow fila = dgvPagos.CurrentRow;
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdPago"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMetodoPago.SelectedItem != null &&
                cmbMetodoPago.SelectedItem.ToString() == "Transferencia")
                pnlDatosBancarios.Visible = true;
            else
                pnlDatosBancarios.Visible = false;
        }

        private void btnCerrarPanel_Click(object sender, EventArgs e)
        {
            pnlDatosBancarios.Visible = false;
        }
    }
}