using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    //TODO: Formulario de Gestión de Instructores (Capa Presentación).
    //TODO: Aquí solo va código de interfaz: leer/pintar controles y llamar a la Capa Datos.
    public partial class frmInstructores : Form
    {
        //TODO: Instancia de la clase de acceso a datos de instructores.
        private InstructorCD instructorCD = new InstructorCD();

        //TODO: Guarda el Id del instructor seleccionado en la grilla (0 = ninguno seleccionado).
        private int idSeleccionado = 0;

        public frmInstructores()
        {
            InitializeComponent();
        }

        //TODO: Evento que se dispara al abrir el formulario.
        //TODO: Ahora es "async void" porque llama a la carga asíncrona (Requisito 7).
        private async void frmInstructores_Load(object sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        //TODO: Método NUEVO. Carga la grilla de instructores y el total de registros
        //TODO: EN PARALELO usando Task.WhenAll(...), tal como pide el Requisito 7,
        //TODO: para que la interfaz no se congele mientras se consulta la base de datos.
        private async Task CargarDatosAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor; //TODO: Cambia el cursor mientras carga (feedback visual).

                Task<List<_Instructor>> tareaLista = instructorCD.ObtenerTodosAsync();
                Task<int> tareaTotal = instructorCD.ContarInstructoresAsync();

                //TODO: Aquí ocurre la magia del Requisito 7: ambas tareas corren a la vez.
                await Task.WhenAll(tareaLista, tareaTotal);

                dgvInstructores.DataSource = tareaLista.Result;
                lblTotal.Text = "Total de instructores: " + tareaTotal.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar instructores: " + ex.Message, "Error",
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
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            lblMensaje.Text = string.Empty;
            idSeleccionado = 0;
        }

        //TODO: Validaciones (Requisito 3): campos vacíos/espacios en blanco y teléfono válido.
        //TODO: Devuelve false y muestra el mensaje en lblMensaje si algo está mal.
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEspecialidad.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Todos los campos con * son obligatorios.";
                return false;
            }

            //TODO: Validación de teléfono: solo dígitos y longitud razonable (7 a 10).
            string telefono = txtTelefono.Text.Trim();
            if (telefono.Length < 7 || telefono.Length > 10 || !telefono.All(char.IsDigit))
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "El teléfono debe contener entre 7 y 10 dígitos numéricos.";
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
                if (!ValidarCampos()) return;

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string especialidad = txtEspecialidad.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                btnGuardar.Enabled = false; //TODO: Evita doble clic mientras se procesa.
                Cursor = Cursors.WaitCursor;

                //TODO: Requisito 4: antes de insertar, se pregunta a la BD si ya existe.
                bool existe = await instructorCD.ExisteInstructorAsync(nombre, apellido, especialidad);
                if (existe)
                {
                    MessageBox.Show("El registro ya existe.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //TODO: Se arma el objeto de datos (_Instructor) para guardar en la BD.
                _Instructor i = new _Instructor();
                i.Nombre = nombre;
                i.Apellido = apellido;
                i.Especialidad = especialidad;
                i.Telefono = telefono;

                //TODO: Se arma el objeto de negocio (Instructor) solo para mostrar el mensaje bonito.
                Instructor instructorNeg = new Instructor(nombre, apellido, telefono, especialidad);

                //TODO: Inserción real en la base de datos, en su versión async.
                bool resultado = await instructorCD.InsertarAsync(i);
                if (resultado)
                {
                    MessageBox.Show(instructorNeg.ObtenerInformacion(), "Instructor Registrado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync(); //TODO: Refresca la grilla y el total.
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
                    MessageBox.Show("Seleccione un instructor de la grilla para actualizar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos()) return;

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string especialidad = txtEspecialidad.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                btnActualizar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //TODO: Requisito 4: chequeo de duplicados también al editar,
                //TODO: excluyendo el registro que se está editando.
                bool existe = await instructorCD.ExisteInstructorAsync(nombre, apellido, especialidad, idSeleccionado);
                if (existe)
                {
                    MessageBox.Show("El registro ya existe.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _Instructor i = new _Instructor();
                i.IdInstructor = idSeleccionado;
                i.Nombre = nombre;
                i.Apellido = apellido;
                i.Especialidad = especialidad;
                i.Telefono = telefono;

                //TODO: Actualización real en la base de datos, en su versión async.
                bool resultado = await instructorCD.EditarAsync(i);
                if (resultado)
                {
                    MessageBox.Show("Instructor actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync();
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
            finally
            {
                btnActualizar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //TODO: Botón ELIMINAR. Primero valida (regla de negocio existente) que el
        //TODO: instructor no tenga matrículas asociadas antes de borrar.
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un instructor de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnEliminar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                //TODO: Regla de negocio ya existente en el proyecto: no eliminar si tiene matrículas.
                bool tieneMatriculas = instructorCD.TieneMatriculas(idSeleccionado);
                if (tieneMatriculas)
                {
                    MessageBox.Show("No se puede eliminar: el instructor tiene matrículas registradas.",
                                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //TODO: Eliminación real en la base de datos, en su versión async.
                bool resultado = await instructorCD.EliminarAsync(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Instructor eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarDatosAsync();
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

        //TODO: Botón BUSCAR (NUEVO - Requisito 1). Filtra por nombre, apellido o
        //TODO: especialidad. Si el texto está vacío, simplemente recarga todo.
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

                List<_Instructor> resultado = await instructorCD.BuscarAsync(texto);
                dgvInstructores.DataSource = resultado;
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

        //TODO: Al hacer clic en una fila de la grilla, se cargan sus datos en los
        //TODO: cuadros de texto para poder editar o eliminar ese registro.
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

        //TODO: Restringe el campo Teléfono a solo números mientras se escribe.
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //TODO: Restringe el campo Nombre a solo letras y espacios mientras se escribe.
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //TODO: Restringe el campo Apellido a solo letras y espacios mientras se escribe.
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }


    }


}
