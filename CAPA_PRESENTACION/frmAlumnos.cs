using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Arquitectura en Capas - Formulario perteneciente a CAPA_PRESENTACION, la opción de "entrada" para gestionar alumnos (agregar, editar, eliminar)
    // TODO: Opción de entrada - Formulario donde se permiten agregar datos a la base de datos (alta, edición y eliminación de alumnos)
    public partial class frmAlumnos : Form
    {
        // TODO: Arquitectura en Capas - Instancias de las clases de CAPA_DATOS usadas por este formulario para acceder a la información
        private AlumnoCD alumnoCD = new AlumnoCD();
        private MatriculaCD matriculaCD = new MatriculaCD();
        private int idSeleccionado = 0;
        // TODO: Arquitectura en Capas - Instancia de la clase de negocio ServicioCorreo (CAPA_NEGOCIOS), usada para notificaciones por correo
        private readonly ServicioCorreo servicioCorreoRegistro =
         new ServicioCorreo();

        public frmAlumnos()
        {
            InitializeComponent();
        }

        // TODO: Llamadas asíncronas - Evento Load declarado "async void" porque es un manejador de eventos de WinForms, permite usar await sin bloquear la interfaz gráfica
        // TODO: Captura de error (try-catch) - Envuelve la carga inicial en try-catch para evitar que un fallo de conexión cierre la aplicación de forma forzada
        // TODO: Formularios de entrada deshabilitados - Al cargar el formulario, se llama a DeshabilitarCampos() para que los campos inicien bloqueados
        // async void porque es un evento de WinForms (Load)
        private async void frmAlumnos_Load(object sender, EventArgs e)
        {
            try
            {
                await CargarGrillaAsync(); // espera a que carguen los alumnos
                DeshabilitarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Llamadas asíncronas - Método asíncrono que trae todos los alumnos desde CAPA_DATOS mediante await, sin bloquear la interfaz gráfica
        // TODO: Opción consulta - Alimenta la grilla que permite dar un vistazo a los datos ya guardados
        // Trae todos los alumnos y los pone en la grilla
        private async Task CargarGrillaAsync()
        {
            dgvAlumnos.DataSource = await alumnoCD.ObtenerTodosAsync();
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que limpia todos los campos del formulario y reinicia el Id seleccionado
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

        // TODO: Formularios de entrada deshabilitados - Método que bloquea todos los campos de entrada, cumple el requisito de que el formulario cargue deshabilitado
        // Deja los campos bloqueados: así se evita editar por accidente.
        private void DeshabilitarCampos()
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
            dtpFechaNacimiento.Enabled = false;
            chkIntensivo.Enabled = false;
            btnGuardar.Enabled = false;
        }

        // TODO: Botón habilitar campos - Método invocado por el botón que habilita los campos para permitir el ingreso de datos, cumple el requisito de habilitación manual
        private void HabilitarCampos()
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            chkIntensivo.Enabled = true;
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado de validación, revisa que los campos obligatorios no estén vacíos antes de guardar/actualizar
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

        // TODO: Botón habilitar campos - Evento del botón "Nuevo" que habilita los campos para permitir el ingreso de datos de un nuevo alumno
        // Botón "Nuevo": habilita los campos y prepara el formulario para un registro nuevo.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos();
            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            txtNombre.Focus();
        }

        // TODO: Llamadas asíncronas - Evento async void que inserta un nuevo alumno usando await sobre los métodos asíncronos de AlumnoCD
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de guardado en try-catch para evitar el cierre forzado de la aplicación
        // TODO: Opción de entrada - Inserta un nuevo registro de alumno en la base de datos
        // TODO: Clases y herencia - Instancia un objeto Alumno (CAPA_NEGOCIOS) para invocar EvaluarNivel() y mostrar la modalidad calculada según sea Intensiva o Regular
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();

                bool duplicado = await alumnoCD.ExisteDuplicadoAsync(nombre, apellido, correo, 0); // valida que no exista ya
                if (duplicado)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Ya existe un alumno con ese nombre/apellido o correo.";
                    return;
                }

                _Alumno a = new _Alumno();
                a.Nombre = nombre;
                a.Apellido = apellido;
                a.FechaNacimiento = dtpFechaNacimiento.Value;
                a.Telefono = txtTelefono.Text.Trim();
                a.Correo = correo;

                bool resultado = await alumnoCD.InsertarAsync(a); // inserta en la BD
                if (resultado)
                {
                    Alumno alumnoNeg = new Alumno(a.Nombre, a.Apellido, a.Telefono,
                                                  a.FechaNacimiento, chkIntensivo.Checked);
                    MessageBox.Show(alumnoNeg.EvaluarNivel(), "Modalidad del Alumno",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarGrillaAsync(); // refresca la grilla
                    LimpiarCampos();
                    DeshabilitarCampos();
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

        // TODO: Llamadas asíncronas - Evento async void que actualiza un alumno usando await sobre EditarAsync de AlumnoCD
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de actualización en try-catch para evitar el cierre forzado de la aplicación
        // TODO: Opción de entrada - Actualiza un registro de alumno existente en la base de datos
        private async void btnActualizar_Click(object sender, EventArgs e)
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

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();

                bool duplicado = await alumnoCD.ExisteDuplicadoAsync(nombre, apellido, correo, idSeleccionado); // excluye el propio registro
                if (duplicado)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Ya existe otro alumno con ese nombre/apellido o correo.";
                    return;
                }

                _Alumno a = new _Alumno();
                a.IdAlumno = idSeleccionado;
                a.Nombre = nombre;
                a.Apellido = apellido;
                a.FechaNacimiento = dtpFechaNacimiento.Value;
                a.Telefono = txtTelefono.Text.Trim();
                a.Correo = correo;

                bool resultado = await alumnoCD.EditarAsync(a);
                if (resultado)
                {
                    MessageBox.Show("Alumno actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarGrillaAsync();
                    LimpiarCampos();
                    DeshabilitarCampos();
                    btnActualizar.Enabled = false;
                    btnEliminar.Enabled = false;
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

        // TODO: Llamadas asíncronas - Evento async void que elimina un alumno usando await sobre EliminarAsync de AlumnoCD
        // TODO: Captura de error (try-catch) - Envuelve todo el proceso de eliminación en try-catch para evitar el cierre forzado de la aplicación
        // TODO: Opción de entrada - Elimina un registro de alumno de la base de datos, validando antes que no tenga matrículas asociadas
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un alumno de la grilla para eliminar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool tieneMatriculas = await alumnoCD.TieneMatriculasAsync(idSeleccionado); // no dejar eliminar si tiene matrículas
                if (tieneMatriculas)
                {
                    MessageBox.Show("No se puede eliminar: el alumno tiene matrículas registradas.",
                                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = await alumnoCD.EliminarAsync(idSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Alumno eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarGrillaAsync();
                    LimpiarCampos();
                    DeshabilitarCampos();
                    btnActualizar.Enabled = false;
                    btnEliminar.Enabled = false;
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

        // TODO: Formularios de entrada deshabilitados - Botón "Limpiar" que vuelve a bloquear los campos, restaurando el estado inicial deshabilitado
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            DeshabilitarCampos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        // TODO: Arquitectura en Capas - Consulta a MatriculaCD (CAPA_DATOS) para mostrar el nivel actual del alumno seleccionado
        // TODO: Métodos, métodos abstractos y métodos virtuales - Método síncrono, MatriculaCD todavía no tiene versión asíncrona
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

                // MatriculaCD (módulo del Integrante 4) sigue siendo síncrono por ahora.
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

        // TODO: Clases y herencia - Instancia un objeto Alumno (CAPA_NEGOCIOS) e invoca PromoverAlumno(), método propio de la subclase
        // TODO: Arquitectura en Capas - Combina lógica de negocio (Alumno.PromoverAlumno) con acceso a datos (NivelCD y MatriculaCD) para completar la promoción de nivel
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
                int idMatriculaActual = 0;
                for (int i = 0; i < matriculas.Count; i++)
                {
                    if (matriculas[i].IdAlumno == idSeleccionado)
                    {
                        idMatriculaActual = matriculas[i].IdMatricula;
                    }
                }

                if (idMatriculaActual == 0)
                {
                    MessageBox.Show("No se encontró matrícula activa para este alumno.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultado = matriculaCD.ActualizarNivel(idMatriculaActual, idNivelNuevo);
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

        // TODO: Botón habilitar campos - Al seleccionar una fila de la grilla, se habilitan los campos para permitir editar ese registro específico
        // TODO: Captura de error (try-catch) - Envuelve la lectura de la fila seleccionada en try-catch para evitar errores si algún dato viene inválido
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

                    // Seleccionar una fila habilita edición sobre ESE registro.
                    HabilitarCampos();
                    btnGuardar.Enabled = false;
                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Llamadas asíncronas - Evento async void que filtra la grilla en tiempo real usando await sobre BuscarAsync de AlumnoCD
        // TODO: Opción consulta - Permite dar un vistazo filtrado a los datos ya guardados según lo que escribe el usuario
        // TODO: Captura de error (try-catch) - Envuelve la búsqueda en try-catch para evitar el cierre forzado de la aplicación
        // Búsqueda por nombre o apellido mientras el usuario escribe.
        // Se dispara cada vez que escribes una letra en el buscador
        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.Trim();
                if (filtro == string.Empty)
                {
                    await CargarGrillaAsync(); // sin texto, muestra todos
                }
                else
                {
                    dgvAlumnos.DataSource = await alumnoCD.BuscarAsync(filtro); // filtra por nombre/apellido
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Validación de entrada, solo permite dígitos en el campo teléfono
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Validación de entrada, solo permite letras y espacios en el campo nombre
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Validación de entrada, solo permite letras y espacios en el campo apellido
        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // TODO: Clases y herencia - Ejemplo explícito de polimorfismo: una variable de tipo base Persona referencia un objeto de tipo Alumno
        // TODO: Métodos, métodos abstractos y métodos virtuales - Al llamar ObtenerInformacion() se ejecuta la versión sobrescrita en Alumno, no la de Persona, por el mecanismo de override/virtual
        // TODO: Captura de error (try-catch) - Envuelve la construcción del objeto y la muestra de información en try-catch
        private void btnInfoAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un alumno de la grilla.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Polimorfismo: referencia de tipo base Persona apunta a objeto Alumno
                Persona persona = new Alumno(
                    txtNombre.Text, txtApellido.Text, txtTelefono.Text,
                    dtpFechaNacimiento.Value, chkIntensivo.Checked);

                // Invoca ObtenerInformacion() del tipo real (Alumno), no de Persona
                MessageBox.Show(persona.ObtenerInformacion(), "Información del Alumno",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}