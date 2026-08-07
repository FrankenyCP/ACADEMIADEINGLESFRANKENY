using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Arquitectura en Capas - Formulario perteneciente a CAPA_PRESENTACION, la opción de "entrada" para gestionar pagos
    // TODO: Opción de entrada - Formulario donde se permiten agregar datos a la base de datos (registro y eliminación de pagos)
    public partial class frmPagos : Form
    {
        // TODO: Arquitectura en Capas - Instancias de las clases de CAPA_DATOS usadas por este formulario para acceder a la información
        private PagoCD pagoCD = new PagoCD();
        private MatriculaCD matriculaCD = new MatriculaCD();
        private NivelCD nivelCD = new NivelCD();
        private int idSeleccionado = 0;

        public frmPagos()
        {
            InitializeComponent();
        }

        // TODO: Captura de error (try-catch) - Envuelve la carga inicial de combos y grilla en try-catch para evitar el cierre forzado de la aplicación
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

        // TODO: Conexión a datos - Consulta a MatriculaCD (CAPA_DATOS) para llenar el combo de matrículas disponibles
        // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono (MatriculaCD todavía no tiene versión asíncrona)
        private void CargarComboMatriculas()
        {
            cmbMatricula.DataSource = matriculaCD.ObtenerTodos();
            cmbMatricula.DisplayMember = "NombreAlumno";
            cmbMatricula.ValueMember = "IdMatricula";
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que llena el combo de métodos de pago con valores fijos
        private void CargarComboMetodo()
        {
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta");
            cmbMetodoPago.Items.Add("Transferencia");
            cmbMetodoPago.SelectedIndex = 0;
        }

        // TODO: Opción consulta - Alimenta la grilla que permite dar un vistazo a los pagos ya guardados, filtrados por la matrícula seleccionada o mostrando todos
        // TODO: Conexión a datos - Consulta a PagoCD (CAPA_DATOS), método síncrono
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

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que limpia todos los campos del formulario y reinicia el Id seleccionado
        private void LimpiarCampos()
        {
            txtMonto.Text = string.Empty;
            dtpFechaPago.Value = DateTime.Today;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
            if (cmbMatricula.Items.Count > 0) cmbMatricula.SelectedIndex = 0;
            cmbMetodoPago.SelectedIndex = 0;
        }

        // TODO: Arquitectura en Capas - Combina MatriculaCD y NivelCD (CAPA_DATOS) para determinar el costo del nivel asociado a una matrícula
        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado de cálculo, usado antes de registrar o evaluar un pago
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

        // TODO: Opción consulta - Muestra el total pagado y el saldo restante de la matrícula seleccionada, sin permitir modificarlos directamente
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

        // TODO: Captura de error (try-catch) - Envuelve la actualización del saldo en try-catch para evitar el cierre forzado de la aplicación
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

        // TODO: Clases y herencia - Instancia un objeto Alumno (CAPA_NEGOCIOS) para invocar RegistrarPago() y EvaluarAprobacion(), métodos propios de la subclase
        // TODO: Arquitectura en Capas - Delega la validación de saldo y el registro del pago a la capa de negocio (Alumno.RegistrarPago) en vez de insertar directamente desde el formulario
        // TODO: Opción de entrada - Inserta un nuevo registro de pago, validando que no exceda el saldo restante del curso
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de guardado en try-catch para evitar el cierre forzado de la aplicación
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

        // TODO: Opción de entrada - Elimina un registro de pago de la base de datos
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de eliminación en try-catch para evitar el cierre forzado de la aplicación
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

        // TODO: Botón habilitar campos - Al seleccionar una fila de la grilla, guarda el Id del pago seleccionado para poder eliminarlo o exportarlo
        // TODO: Captura de error (try-catch) - Envuelve la lectura de la fila seleccionada en try-catch para evitar errores si algún dato viene inválido
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

        // TODO: Métodos, métodos abstractos y métodos virtuales - Validación de entrada, solo permite dígitos y un punto decimal en el campo monto
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Muestra u oculta el panel de datos bancarios según el método de pago seleccionado
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

        // TODO: Interfaces Y Asincrónicos - Usa la interfaz iExportador (ExportadorPdf) para exportar el pago seleccionado a PDF, sin depender de la implementación concreta
        // TODO: Llamadas asíncronas - Evento async void que espera con await el resultado de ExportarAsync sin bloquear la interfaz gráfica
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de exportación en try-catch para evitar el cierre forzado de la aplicación
        private async void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                _Pago pagoSeleccionado = dgvPagos.CurrentRow?.DataBoundItem as _Pago;
                if (pagoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un pago de la grilla para exportar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog dialogo = new SaveFileDialog())
                {
                    dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    dialogo.FileName = "Pago_" + pagoSeleccionado.IdPago + ".pdf";

                    if (dialogo.ShowDialog() != DialogResult.OK)
                        return;

                    string titulo = "Factura de Pago";
                    List<string> contenido = new List<string>
            {
                "IdPago: " + pagoSeleccionado.IdPago,
                "Matrícula: " + pagoSeleccionado.InfoMatricula,
                "Fecha de Pago: " + pagoSeleccionado.FechaPago.ToString("dd/MM/yyyy"),
                "Monto: RD$" + pagoSeleccionado.Monto.ToString("N2"),
                "Método de Pago: " + pagoSeleccionado.MetodoPago
            };

                    iExportador exportador = new ExportadorPdf();
                    bool exito = await exportador.ExportarAsync(titulo, contenido, dialogo.FileName);

                    if (exito)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dialogo.FileName)
                        {
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Interfaces Y Asincrónicos - Usa la interfaz iExportador (ExportadorExcel) para exportar el pago seleccionado a Excel, mismo contrato que ExportadorPdf pero con implementación distinta (polimorfismo)
        // TODO: Llamadas asíncronas - Evento async void que espera con await el resultado de ExportarAsync sin bloquear la interfaz gráfica
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de exportación en try-catch para evitar el cierre forzado de la aplicación
        private async void btn_ExportarExcelfrmPagos_Click(object sender, EventArgs e)
        {
            try
            {
                _Pago pagoSeleccionado = dgvPagos.CurrentRow?.DataBoundItem as _Pago;
                if (pagoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un pago de la grilla para exportar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog dialogo = new SaveFileDialog())
                {
                    dialogo.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                    dialogo.FileName = "Pago_" + pagoSeleccionado.IdPago + ".xlsx";

                    if (dialogo.ShowDialog() != DialogResult.OK)
                        return; // el usuario canceló

                    string titulo = "Factura de Pago";
                    List<string> contenido = new List<string>
            {
                "IdPago: " + pagoSeleccionado.IdPago,
                "Matrícula: " + pagoSeleccionado.InfoMatricula,
                "Fecha de Pago: " + pagoSeleccionado.FechaPago.ToString("dd/MM/yyyy"),
                "Monto: RD$" + pagoSeleccionado.Monto.ToString("N2"),
                "Método de Pago: " + pagoSeleccionado.MetodoPago
            };

                    iExportador exportador = new ExportadorExcel();
                    bool exito = await exportador.ExportarAsync(titulo, contenido, dialogo.FileName);

                    if (exito)
                    {
                        // TODO Integrante 5: abre el archivo generado con el programa predeterminado del sistema.
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dialogo.FileName)
                        {
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}