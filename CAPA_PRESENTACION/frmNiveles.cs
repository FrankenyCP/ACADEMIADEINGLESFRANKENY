using CAPA_DATOS;

namespace CAPA_PRESENTACION
{
    //TODO: Formulario de Gestión de Niveles (Capa Presentación).
    //TODO: Aquí solo va código de interfaz: leer/pintar controles y llamar a la Capa Datos.
    public partial class frmNiveles : Form
    {
        //TODO: Instancia de la clase de acceso a datos de niveles.
        private NivelCD nivelCD = new NivelCD();

        //TODO: Guarda el Id del nivel seleccionado en la grilla (0 = ninguno seleccionado).
        private int idSeleccionado = 0;

        public frmNiveles()
        {
            InitializeComponent();
        }

        //TODO: Evento que se dispara al abrir el formulario.
        //TODO: Ahora es "async void" porque llama a la carga asíncrona (Requisito 7).
        private async void frmNiveles_Load(object sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        //TODO: Método NUEVO. Carga la grilla de niveles y el total de registros
        //TODO: EN PARALELO usando Task.WhenAll(...), tal como pide el Requisito 7,
        //TODO: para que la interfaz no se congele mientras se consulta la base de datos.
        private async Task CargarDatosAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor; //TODO: Cambia el cursor mientras carga (feedback visual).

                Task<List<_Nivel>> tareaLista = nivelCD.ObtenerTodosAsync();
                Task<int> tareaTotal = nivelCD.ContarNivelesAsync();

                //TODO: Aquí ocurre la magia del Requisito 7: ambas tareas corren a la vez.
                await Task.WhenAll(tareaLista, tareaTotal);

                dgvNiveles.DataSource = tareaLista.Result;
                lblTotal.Text = "Total de niveles: " + tareaTotal.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar niveles: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default; //TODO: Vuelve el cursor a la normalidad, pase lo que pase.
            }
        }

        //TODO: Limpia todos los cuadros de texto y el mensaje de validación.
        private void LimpiarCampos()
        {
            txtNombreNivel.Text = string.Empty;
            txtDuracion.Text = string.Empty;
            txtCosto.Text = string.Empty;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
        }

        //TODO: Validaciones (Requisito 3): campos vacíos/espacios en blanco,
        //TODO: duración > 0 y costo > 0. Usa "out" para devolver ya convertidos
        //TODO: los valores numéricos y no tener que volver a parsear después.
        private bool ValidarCampos(out int duracion, out decimal costo)
        {
            duracion = 0;
            costo = 0;

            if (string.IsNullOrWhiteSpace(txtNombreNivel.Text) ||
                string.IsNullOrWhiteSpace(txtDuracion.Text) ||
                string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Todos los campos con * son obligatorios.";
                return false;
            }

            //TODO: TryParse evita que la app truene con FormatException si escriben letras.
            if (!int.TryParse(txtDuracion.Text.Trim(), out duracion) || duracion <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "La duración debe ser un número mayor que cero.";
                return false;
            }

            if (!decimal.TryParse(txtCosto.Text.Trim(), out costo) || costo <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "El costo debe ser un número mayor que cero.";
                return false;
            }

            lblMensaje.Text = string.Empty;
            return true;
        }

        //TODO: Botón GUARDAR (Insertar). Ahora es async y valida duplicados antes de insertar.
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos(out int duracion, out decimal costo)) return;

                string nombreNivel = txtNombreNivel.Text.Trim();

                btnGuardar.Enabled = false; //TODO: Evita doble clic mientras se procesa.
                Cursor = Cursors.WaitCursor;

                //TODO: Requisito 4: antes de insertar, se pregunta a la BD si ya existe
                //TODO: un nivel con el mismo nombre.
                bool existe = await nivelCD.ExisteNivelAsync(nombreNivel);
                if (existe)
                {
                    MessageBox.Show("El registro ya existe.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _Nivel n = new _Nivel();
                n.NombreNivel = nombreNivel;
                n.DuracionMeses = duracion;
                n.Costo = costo;

                //TODO: Inserción real en la base de datos, en su versión async.
                bool resultado = await nivelCD.InsertarAsync(n);
                if (resultado)
                {
                    MessageBox.Show("Nivel guardado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync(); //TODO: Refresca la grilla y el total.
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
            finally
            {
                btnGuardar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //TODO: Botón ACTUALIZAR (Editar). Igual que Guardar, pero excluye el propio
        //TODO: registro al validar duplicados (idExcluir = idSeleccionado).
        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un nivel de la grilla para actualizar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos(out int duracion, out decimal costo)) return;

                string nombreNivel = txtNombreNivel.Text.Trim();

                btnActualizar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //TODO: Requisito 4: chequeo de duplicados también al editar,
                //TODO: excluyendo el registro que se está editando.
                bool existe = await nivelCD.ExisteNivelAsync(nombreNivel, idSeleccionado);
                if (existe)
                {
                    MessageBox.Show("El registro ya existe.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _Nivel n = new _Nivel();
                n.IdNivel = idSeleccionado;
                n.NombreNivel = nombreNivel;
                n.DuracionMeses = duracion;
                n.Costo = costo;

                //TODO: Actualización real en la base de datos, en su versión async.
                bool resultado = await nivelCD.EditarAsync(n);
                if (resultado)
                {
                    MessageBox.Show("Nivel actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync();
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
            finally
            {
                btnActualizar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //TODO: Botón ELIMINAR. Elimina el nivel seleccionado en la grilla.
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un nivel de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnEliminar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //TODO: Eliminación real en la base de datos, en su versión async.
                bool resultado = await nivelCD.EliminarAsync(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Nivel eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync();
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
            finally
            {
                btnEliminar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //TODO: Botón LIMPIAR. Limpia también el cuadro de búsqueda (nuevo).
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtBuscar.Text = string.Empty;
        }

        //TODO: Botón BUSCAR (NUEVO - Requisito 1). Filtra por nombre de nivel.
        //TODO: Si el texto está vacío, simplemente recarga todo.
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                btnBuscar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                string texto = txtBuscar.Text.Trim();
                if (string.IsNullOrEmpty(texto))
                {
                    await CargarDatosAsync();
                    return;
                }

                List<_Nivel> resultado = await nivelCD.BuscarAsync(texto);
                dgvNiveles.DataSource = resultado;
                lblTotal.Text = "Resultados encontrados: " + resultado.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //TODO: Al seleccionar una fila de la grilla, se cargan sus datos en los
        //TODO: cuadros de texto para poder editar o eliminar ese registro.
        private void dgvNiveles_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvNiveles.CurrentRow != null)
                {
                    DataGridViewRow fila = dgvNiveles.CurrentRow;
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

        //TODO: Restringe el campo Duración a solo números mientras se escribe.
        private void txtDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //TODO: Restringe el campo Costo a números y un solo punto decimal.
        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
