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
            dgvPagos.DataSource = pagoCD.ObtenerTodos();
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
            if (cmbMatricula.SelectedValue == null) return;

            int idMatricula = Convert.ToInt32(cmbMatricula.SelectedValue);
            decimal totalPagado = pagoCD.ObtenerTotalPagado(idMatricula);
            decimal costoNivel = ObtenerCostoNivel(idMatricula);
            decimal saldoRestante = costoNivel - totalPagado;

            lblSaldo.Text = "Pagado: " + totalPagado.ToString("C") +
                            " | Saldo restante: " + saldoRestante.ToString("C");
        }

        private void cmbMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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

        private void dgvPagos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvPagos.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["IdPago"].Value);
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