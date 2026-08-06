using CAPA_NEGOCIOS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmMatriculas
    {
        // =========================================================
        // VARIABLES DEL DISENO
        // =========================================================

        private bool disenoMatriculasInicializado = false;
        private bool formularioMatriculasMostrado = false;
        private bool modoIntegradoSolicitado;

        private Panel pnlMenuLateral = null!;
        private Panel pnlContenido = null!;
        private Panel pnlEncabezado = null!;
        private Panel pnlCuerpo = null!;

        private Panel pnlMarca = null!;
        private Panel pnlMenuOpciones = null!;
        private Panel pnlPieMenu = null!;

        private Panel pnlFormulario = null!;
        private Panel pnlLista = null!;
        private Panel pnlCabeceraFormulario = null!;
        private Panel pnlCabeceraLista = null!;
        private Panel pnlAcciones = null!;
        private Panel pnlBuscador = null!;
        private Panel pnlContenedorGrid = null!;

        private Label lblMarca = null!;
        private Label lblMarcaSubtitulo = null!;
        private Label lblTituloPagina = null!;
        private Label lblSubtituloPagina = null!;
        private Label lblTituloFormulario = null!;
        private Label lblTituloLista = null!;
        private Label lblTotalMatriculas = null!;
        private Label lblPieIzquierdo = null!;
        private Label lblVersion = null!;

        private PictureBox picLogoMenu = null!;

        private Button btnMenuDashboard = null!;
        private Button btnMenuNiveles = null!;
        private Button btnMenuInstructores = null!;
        private Button btnMenuMatriculas = null!;
        private Button btnMenuPagos = null!;
        private Button btnMenuReportes = null!;
        private Button btnMenuConsulta = null!;

        private Button btnCerrarVentana = null!;
        private TextBox txtBuscarMatricula = null!;
        private Button btnBuscarMatricula = null!;
        private Button btnLimpiarBusqueda = null!;

        private object? fuenteOriginalMatriculas;

        private readonly ServicioCorreo servicioCorreo =
            new ServicioCorreo();

        private bool eliminacionCorreoConfigurada;
        private bool actualizandoDatosMatricula;

        // =========================================================
        // REDUCIR PARPADEO
        // =========================================================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parametros = base.CreateParams;

                // Evita el efecto pesado o extraño al incrustar el formulario.
                if (!modoIntegradoSolicitado)
                {
                    parametros.ExStyle |= 0x02000000;
                }

                return parametros;
            }
        }

        // =========================================================
        // CREAR EL DISENO ANTES DE MOSTRAR EL FORMULARIO
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoMatriculasInicializado)
            {
                disenoMatriculasInicializado = true;

                SuspendLayout();

                try
                {
                    InicializarDisenoMatriculas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar el diseno de matriculas:\r\n" +
                        ex.Message,
                        "Gestion de Matriculas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                finally
                {
                    ResumeLayout(true);
                    PerformLayout();
                }
            }

            base.SetVisibleCore(value);

            if (value && !formularioMatriculasMostrado)
            {
                formularioMatriculasMostrado = true;

                BeginInvoke(new Action(() =>
                {
                    AjustarDisenoResponsivo();

                    if (modoIntegradoSolicitado)
                    {
                        AplicarModoIntegrado();
                    }
                    ActualizarTotalMatriculas();
                    Invalidate(true);
                    Update();
                }));
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        private void InicializarDisenoMatriculas()
        {
            ConfigurarFormulario();
            CrearEstructuraPrincipal();
            CrearMenuLateral();
            CrearEncabezado();
            CrearPanelFormulario();
            CrearPanelLista();
            ConfigurarControlesFormulario();
            ConfigurarBotonesAccion();
            ConfigurarEliminacionMatriculaConCorreo();

            ConfigurarBuscador();
            ConfigurarDataGridView();
            ConfigurarEventosVisuales();

            AjustarDisenoResponsivo();

            pnlMenuLateral.BringToFront();
            pnlContenido.BringToFront();

            if (modoIntegradoSolicitado)
            {
                AplicarModoIntegrado();
            }
        }

        private void ConfigurarFormulario()
        {
            Text = "Gestion de Matriculas - Lexbridge";

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1160, 700);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;

            AutoScaleMode = AutoScaleMode.Dpi;

            BackColor = Color.FromArgb(4, 17, 44);
            ForeColor = Color.White;

            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );

            DoubleBuffered = true;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );

            UpdateStyles();
        }

        // =========================================================
        // ESTRUCTURA PRINCIPAL
        // =========================================================

        private void CrearEstructuraPrincipal()
        {
            pnlMenuLateral = new Panel
            {
                Name = "pnlMenuLateralModerno",
                Dock = DockStyle.Left,
                Width = 235,
                Padding = new Padding(12),
                BackColor = Color.FromArgb(3, 14, 39)
            };

            pnlContenido = new Panel
            {
                Name = "pnlContenidoModerno",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlEncabezado = new Panel
            {
                Name = "pnlEncabezadoModerno",
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(5, 20, 52)
            };

            pnlCuerpo = new Panel
            {
                Name = "pnlCuerpoModerno",
                Dock = DockStyle.Fill,
                Padding = new Padding(18),
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            pnlContenido.Controls.Add(pnlCuerpo);
            pnlContenido.Controls.Add(pnlEncabezado);

            Controls.Add(pnlContenido);
            Controls.Add(pnlMenuLateral);

            pnlCuerpo.Resize += (sender, e) =>
            {
                AjustarDisenoResponsivo();
            };

            Resize += (sender, e) =>
            {
                AjustarDisenoResponsivo();
            };
        }

        // =========================================================
        // MENU LATERAL
        // =========================================================

        private void CrearMenuLateral()
        {
            pnlMarca = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            pnlPieMenu = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = Color.Transparent
            };

            pnlMenuOpciones = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 0),
                BackColor = Color.Transparent
            };

            pnlMenuLateral.Controls.Add(pnlMenuOpciones);
            pnlMenuLateral.Controls.Add(pnlPieMenu);
            pnlMenuLateral.Controls.Add(pnlMarca);

            picLogoMenu = new PictureBox
            {
                Parent = pnlMarca,
                Location = new Point(3, 7),
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            CargarLogoDesdeRecursos();

            lblMarca = new Label
            {
                Parent = pnlMarca,
                Text = "LEXBRIDGE",
                Location = new Point(59, 9),
                Size = new Size(150, 27),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            lblMarcaSubtitulo = new Label
            {
                Parent = pnlMarca,
                Text = "ACADEMIA DE INGLES",
                Location = new Point(60, 36),
                Size = new Size(145, 19),
                ForeColor = Color.FromArgb(153, 171, 201),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.8F)
            };

            btnMenuDashboard = CrearBotonMenu(
                "Dashboard", "\uE80F", 8, false
            );

            btnMenuNiveles = CrearBotonMenu(
                "Niveles", "\uE8EF", 60, false
            );

            btnMenuInstructores = CrearBotonMenu(
                "Instructores", "\uE77B", 112, false
            );

            btnMenuMatriculas = CrearBotonMenu(
                "Matriculas", "\uE787", 164, true
            );

            btnMenuPagos = CrearBotonMenu(
                "Pagos", "\uE8C7", 216, false
            );

            btnMenuReportes = CrearBotonMenu(
                "Reportes", "\uE9D2", 268, false
            );

            btnMenuConsulta = CrearBotonMenu(
                "Consulta", "\uE721", 320, false
            );

            btnMenuDashboard.Click += (sender, e) =>
            {
                VolverAlDashboard();
            };

            btnMenuNiveles.Click += (sender, e) =>
            {
                AbrirFormulario(new frmNiveles());
            };

            btnMenuInstructores.Click += (sender, e) =>
            {
                AbrirFormulario(new frmInstructores());
            };

            btnMenuMatriculas.Click += (sender, e) =>
            {
                btnNuevo.PerformClick();
                cmbAlumno.Focus();
            };

            btnMenuPagos.Click += (sender, e) =>
            {
                AbrirFormulario(new frmPagos());
            };

            btnMenuReportes.Click += (sender, e) =>
            {
                AbrirFormulario(new frmReportes());
            };

            btnMenuConsulta.Click += (sender, e) =>
            {
                AbrirFormulario(new frmConsultaMatriculas());
            };

            lblPieIzquierdo = new Label
            {
                Parent = pnlPieMenu,
                Text = "(c) 2026 Lexbridge",
                Location = new Point(5, 27),
                Size = new Size(132, 22),
                ForeColor = Color.FromArgb(185, 197, 219),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            lblVersion = new Label
            {
                Parent = pnlPieMenu,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),
                ForeColor = Color.FromArgb(185, 197, 219),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };
        }

        private Button CrearBotonMenu(
            string texto,
            string icono,
            int top,
            bool seleccionado)
        {
            Button boton = new Button
            {
                Parent = pnlMenuOpciones,
                Location = new Point(0, top),

                Size = new Size(
                    Math.Max(180, pnlMenuOpciones.ClientSize.Width),
                    44
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                Text = texto,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(49, 0, 0, 0),

                BackColor = seleccionado
                    ? Color.FromArgb(72, 26, 124)
                    : Color.Transparent,

                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9.5F,
                    seleccionado
                        ? FontStyle.Bold
                        : FontStyle.Regular
                ),

                Cursor = Cursors.Hand
            };

            boton.FlatAppearance.BorderSize = 0;

            boton.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(47, 31, 94);

            boton.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(71, 34, 122);

            Label lblIcono = new Label
            {
                Parent = pnlMenuOpciones,
                Location = new Point(10, top),
                Size = new Size(35, 44),
                Text = icono,

                TextAlign =
                    ContentAlignment.MiddleCenter,

                ForeColor = seleccionado
                    ? Color.FromArgb(255, 187, 31)
                    : Color.FromArgb(166, 190, 226),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe MDL2 Assets",
                    11F
                ),

                Cursor = Cursors.Hand
            };

            if (seleccionado)
            {
                Panel barra = new Panel
                {
                    Parent = pnlMenuOpciones,
                    Location = new Point(0, top),
                    Size = new Size(4, 44),

                    BackColor =
                        Color.FromArgb(255, 181, 25)
                };

                barra.BringToFront();
            }

            lblIcono.Click += (sender, e) =>
            {
                boton.PerformClick();
            };

            RedondearControl(boton, 11);

            boton.BringToFront();
            lblIcono.BringToFront();

            return boton;
        }

        private void CargarLogoDesdeRecursos()
        {
            try
            {
                string[] nombresPosibles =
                {
                    "logolexbridge",
                    "logoLexbridge",
                    "LogoLexbridge",
                    "LOGOLEXBRIDGE"
                };

                foreach (string nombre in nombresPosibles)
                {
                    object? recurso =
                        Properties.Resources
                            .ResourceManager
                            .GetObject(nombre);

                    Image? imagen = recurso as Image;

                    if (imagen != null)
                    {
                        picLogoMenu.Image = imagen;
                        return;
                    }
                }

                picLogoMenu.Image = null;
            }
            catch
            {
                picLogoMenu.Image = null;
            }
        }

        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void CrearEncabezado()
        {
            lblTitulo.Visible = false;

            lblTituloPagina = new Label
            {
                Parent = pnlEncabezado,
                Text = "Gestion de Matriculas",
                Location = new Point(32, 16),
                Size = new Size(520, 38),
                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    20F,
                    FontStyle.Bold
                )
            };

            lblSubtituloPagina = new Label
            {
                Parent = pnlEncabezado,

                Text =
                    "Registra alumnos en sus niveles e instructores correspondientes",

                Location = new Point(35, 55),
                Size = new Size(600, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            btnNuevo.Parent = pnlEncabezado;

            btnNuevo.Size = new Size(145, 40);

            btnNuevo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnNuevo.Location = new Point(
                pnlEncabezado.ClientSize.Width - 310,
                24
            );

            btnNuevo.Text = "Nueva matricula";

            btnNuevo.BackColor =
                Color.FromArgb(14, 36, 76);

            btnNuevo.ForeColor =
                Color.FromArgb(255, 189, 36);

            btnNuevo.FlatStyle = FlatStyle.Flat;

            btnNuevo.FlatAppearance.BorderColor =
                Color.FromArgb(255, 174, 20);

            btnNuevo.FlatAppearance.BorderSize = 1;

            btnNuevo.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            btnNuevo.Cursor = Cursors.Hand;

            btnCerrarVentana = new Button
            {
                Parent = pnlEncabezado,
                Size = new Size(115, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEncabezado.ClientSize.Width - 145,
                    24
                ),

                Text = "Cerrar",

                BackColor =
                    Color.FromArgb(24, 43, 82),

                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnCerrarVentana.FlatAppearance.BorderColor =
                Color.FromArgb(71, 92, 139);

            btnCerrarVentana.FlatAppearance.BorderSize = 1;

            btnCerrarVentana.Click += (sender, e) =>
            {
                Close();
            };

            RedondearControl(btnNuevo, 11);
            RedondearControl(btnCerrarVentana, 11);
        }

        // =========================================================
        // PANEL IZQUIERDO
        // =========================================================

        private void CrearPanelFormulario()
        {
            pnlFormulario = new Panel
            {
                Parent = pnlCuerpo,
                Location = new Point(18, 18),
                Size = new Size(350, 640),

                BackColor =
                    Color.FromArgb(10, 29, 67),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left
            };

            pnlCabeceraFormulario = new Panel
            {
                Parent = pnlFormulario,
                Dock = DockStyle.Top,
                Height = 56,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloFormulario = new Label
            {
                Parent = pnlCabeceraFormulario,
                Text = "Informacion de la matricula",
                Location = new Point(20, 15),
                Size = new Size(305, 28),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            pnlAcciones = new Panel
            {
                Parent = pnlFormulario,
                Location = new Point(20, 450),
                Size = new Size(310, 165),
                BackColor = Color.Transparent
            };

            RedondearControl(pnlFormulario, 18);
        }

        private void ConfigurarControlesFormulario()
        {
            ConfigurarEtiqueta(
                lblAlumno,
                "Alumno *",
                new Point(20, 72)
            );

            ConfigurarComboBox(
                cmbAlumno,
                new Point(20, 99)
            );

            ConfigurarEtiqueta(
                lblNivel,
                "Nivel *",
                new Point(20, 153)
            );

            ConfigurarComboBox(
                cmbNivel,
                new Point(20, 180)
            );

            ConfigurarEtiqueta(
                lblInstructor,
                "Instructor *",
                new Point(20, 234)
            );

            ConfigurarComboBox(
                cmbInstructor,
                new Point(20, 261)
            );

            ConfigurarEtiqueta(
                lblFecha,
                "Fecha de matricula *",
                new Point(20, 315)
            );

            dtpFechaMatricula.Parent = pnlFormulario;
            dtpFechaMatricula.Location = new Point(20, 342);
            dtpFechaMatricula.Size = new Size(310, 34);

            dtpFechaMatricula.Font = new Font(
                "Segoe UI",
                9.5F
            );

            dtpFechaMatricula.Format =
                DateTimePickerFormat.Short;

            dtpFechaMatricula.CalendarMonthBackground =
                Color.FromArgb(7, 23, 57);

            dtpFechaMatricula.CalendarForeColor =
                Color.White;

            lblMensaje.Parent = pnlFormulario;
            lblMensaje.AutoSize = false;
            lblMensaje.Location = new Point(20, 382);
            lblMensaje.Size = new Size(310, 48);
            lblMensaje.BackColor = Color.Transparent;

            lblMensaje.Font = new Font(
                "Segoe UI",
                8.5F
            );

            lblMensaje.TextAlign =
                ContentAlignment.MiddleLeft;

            lblEstado.Parent = pnlFormulario;
            lblEstado.AutoSize = false;
            lblEstado.Location = new Point(20, 424);
            lblEstado.Size = new Size(310, 24);
            lblEstado.BackColor = Color.Transparent;

            lblEstado.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold
            );
        }

        private void ConfigurarEtiqueta(
            Label etiqueta,
            string texto,
            Point posicion)
        {
            etiqueta.Parent = pnlFormulario;
            etiqueta.AutoSize = false;
            etiqueta.Location = posicion;
            etiqueta.Size = new Size(300, 23);
            etiqueta.Text = texto;
            etiqueta.ForeColor = Color.White;
            etiqueta.BackColor = Color.Transparent;

            etiqueta.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );
        }

        private void ConfigurarComboBox(
            ComboBox combo,
            Point posicion)
        {
            combo.Parent = pnlFormulario;
            combo.Location = posicion;
            combo.Size = new Size(310, 34);

            combo.Font = new Font(
                "Segoe UI",
                9.5F
            );

            combo.BackColor =
                Color.FromArgb(7, 23, 57);

            combo.ForeColor = Color.White;

            combo.FlatStyle =
                FlatStyle.Flat;

            combo.DropDownStyle =
                ComboBoxStyle.DropDownList;
        }

        // =========================================================
        // BOTONES DE ACCION
        // =========================================================

        private void ConfigurarBotonesAccion()
        {
            btnGuardar.Parent = pnlAcciones;
            btnLimpiar.Parent = pnlAcciones;
            btnEliminar.Parent = pnlAcciones;

            ConfigurarBotonAccion(
                btnGuardar,
                "Guardar",
                new Point(0, 0),
                new Size(310, 43),
                Color.FromArgb(10, 146, 153),
                Color.FromArgb(27, 207, 190)
            );

            ConfigurarBotonAccion(
                btnLimpiar,
                "Limpiar",
                new Point(0, 53),
                new Size(150, 41),
                Color.FromArgb(25, 43, 82),
                Color.FromArgb(78, 102, 155)
            );

            ConfigurarBotonAccion(
                btnEliminar,
                "Eliminar",
                new Point(160, 53),
                new Size(150, 41),
                Color.FromArgb(190, 15, 65),
                Color.FromArgb(235, 42, 92)
            );

            Label lblAyuda = new Label
            {
                Parent = pnlAcciones,

                Text =
                    "Selecciona una fila para eliminar una matricula.",

                Location = new Point(0, 108),
                Size = new Size(310, 42),

                ForeColor =
                    Color.FromArgb(154, 176, 208),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    8F
                )
            };
        }

        private void ConfigurarBotonAccion(
            Button boton,
            string texto,
            Point posicion,
            Size tamano,
            Color fondo,
            Color borde)
        {
            boton.Location = posicion;
            boton.Size = tamano;

            boton.Text = texto;

            boton.TextAlign =
                ContentAlignment.MiddleCenter;

            boton.Padding = new Padding(0);

            boton.BackColor = fondo;
            boton.ForeColor = Color.White;

            boton.FlatStyle = FlatStyle.Flat;

            boton.FlatAppearance.BorderColor = borde;
            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                AclararColor(fondo, 18);

            boton.FlatAppearance.MouseDownBackColor =
                OscurecerColor(fondo, 15);

            boton.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            boton.Cursor = Cursors.Hand;

            RedondearControl(boton, 11);
        }

        // =========================================================
        // PANEL DE LA TABLA
        // =========================================================

        private void CrearPanelLista()
        {
            pnlLista = new Panel
            {
                Parent = pnlCuerpo,
                Location = new Point(386, 18),
                Size = new Size(760, 640),

                BackColor =
                    Color.FromArgb(10, 29, 67),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            pnlCabeceraLista = new Panel
            {
                Parent = pnlLista,
                Dock = DockStyle.Top,
                Height = 64,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloLista = new Label
            {
                Parent = pnlCabeceraLista,
                Text = "Lista de matriculas",
                Location = new Point(20, 17),
                Size = new Size(250, 30),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            pnlBuscador = new Panel
            {
                Parent = pnlCabeceraLista,
                Size = new Size(350, 36),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlCabeceraLista.Width - 368,
                    14
                ),

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            pnlContenedorGrid = new Panel
            {
                Parent = pnlLista,
                Location = new Point(16, 78),

                Size = new Size(
                    pnlLista.Width - 32,
                    pnlLista.Height - 120
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(6, 22, 54)
            };

            lblTotalMatriculas = new Label
            {
                Parent = pnlLista,
                AutoSize = false,
                Location = new Point(
                    20,
                    pnlLista.Height - 32
                ),

                Size = new Size(430, 24),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Left,

                ForeColor =
                    Color.FromArgb(175, 191, 219),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    8.5F
                )
            };

            RedondearControl(pnlLista, 18);
            RedondearControl(pnlContenedorGrid, 12);
        }

        // =========================================================
        // BUSCADOR
        // =========================================================

        private void ConfigurarBuscador()
        {
            txtBuscarMatricula = new TextBox
            {
                Parent = pnlBuscador,
                Location = new Point(12, 7),
                Size = new Size(230, 24),

                BorderStyle =
                    BorderStyle.None,

                BackColor =
                    Color.FromArgb(7, 23, 57),

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    8.8F
                ),

                PlaceholderText =
                    "Buscar alumno, nivel o instructor..."
            };

            btnBuscarMatricula = new Button
            {
                Parent = pnlBuscador,
                Location = new Point(248, 3),
                Size = new Size(48, 30),
                Text = "Ir",

                BackColor =
                    Color.FromArgb(20, 76, 120),

                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnBuscarMatricula.FlatAppearance.BorderSize = 0;

            btnLimpiarBusqueda = new Button
            {
                Parent = pnlBuscador,
                Location = new Point(301, 3),
                Size = new Size(45, 30),
                Text = "X",

                BackColor =
                    Color.FromArgb(24, 43, 82),

                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnLimpiarBusqueda.FlatAppearance.BorderSize = 0;

            btnBuscarMatricula.Click += (sender, e) =>
            {
                BuscarMatriculasEnGrilla();
            };

            btnLimpiarBusqueda.Click += (sender, e) =>
            {
                txtBuscarMatricula.Clear();
                RestaurarFuenteMatriculas();
            };

            txtBuscarMatricula.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BuscarMatriculasEnGrilla();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    txtBuscarMatricula.Clear();
                    RestaurarFuenteMatriculas();
                }
            };

            RedondearControl(pnlBuscador, 10);
            RedondearControl(btnBuscarMatricula, 8);
            RedondearControl(btnLimpiarBusqueda, 8);
        }

        private void BuscarMatriculasEnGrilla()
        {
            string texto =
                txtBuscarMatricula.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                RestaurarFuenteMatriculas();
                return;
            }

            try
            {
                if (fuenteOriginalMatriculas == null)
                {
                    fuenteOriginalMatriculas =
                        dgvMatriculas.DataSource;
                }

                if (fuenteOriginalMatriculas is DataTable tabla)
                {
                    DataView vista =
                        new DataView(tabla);

                    string valor =
                        texto.Replace("'", "''");

                    vista.RowFilter =
                        ConstruirFiltroDataTable(
                            tabla,
                            valor
                        );

                    dgvMatriculas.DataSource = vista;
                }
                else if (fuenteOriginalMatriculas is DataView vistaOriginal)
                {
                    DataView vista =
                        new DataView(vistaOriginal.Table);

                    string valor =
                        texto.Replace("'", "''");

                    vista.RowFilter =
                        ConstruirFiltroDataTable(
                            vistaOriginal.Table,
                            valor
                        );

                    dgvMatriculas.DataSource = vista;
                }
                else
                {
                    BuscarVisualmenteEnFilas(texto);
                }

                ActualizarTotalMatriculas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo aplicar la busqueda:\r\n" +
                    ex.Message,
                    "Buscar matriculas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private static string ConstruirFiltroDataTable(
            DataTable tabla,
            string valor)
        {
            string[] columnasPreferidas =
            {
                "NombreAlumno",
                "NombreNivel",
                "NombreInstructor",
                "FechaMatricula",
                "IdMatricula"
            };

            var filtros =
                columnasPreferidas
                    .Where(nombre =>
                        tabla.Columns.Contains(nombre))
                    .Select(nombre =>
                    {
                        DataColumn columna =
                            tabla.Columns[nombre];

                        if (columna.DataType == typeof(string))
                        {
                            return
                                $"[{nombre}] LIKE '%{valor}%'";
                        }

                        return
                            $"CONVERT([{nombre}], 'System.String') LIKE '%{valor}%'";
                    })
                    .ToArray();

            if (filtros.Length == 0)
            {
                return "1 = 1";
            }

            return string.Join(" OR ", filtros);
        }

        private void BuscarVisualmenteEnFilas(string texto)
        {
            string busqueda =
                texto.ToLowerInvariant();

            foreach (DataGridViewRow fila in dgvMatriculas.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                bool coincide =
                    fila.Cells
                        .Cast<DataGridViewCell>()
                        .Any(celda =>
                            Convert.ToString(celda.Value)?
                                .ToLowerInvariant()
                                .Contains(busqueda) == true);

                try
                {
                    fila.Visible = coincide;
                }
                catch
                {
                    // Algunas fuentes enlazadas no permiten ocultar filas.
                }
            }
        }

        private void RestaurarFuenteMatriculas()
        {
            try
            {
                if (fuenteOriginalMatriculas != null)
                {
                    dgvMatriculas.DataSource =
                        fuenteOriginalMatriculas;
                }
                else
                {
                    foreach (DataGridViewRow fila in dgvMatriculas.Rows)
                    {
                        fila.Visible = true;
                    }
                }

                ActualizarTotalMatriculas();
            }
            catch
            {
                // La restauracion visual no afecta la logica original.
            }
        }

        // =========================================================
        // DATAGRIDVIEW
        // =========================================================

        private void ConfigurarDataGridView()
        {
            dgvMatriculas.Parent =
                pnlContenedorGrid;

            dgvMatriculas.Dock =
                DockStyle.Fill;

            dgvMatriculas.BorderStyle =
                BorderStyle.None;

            dgvMatriculas.BackgroundColor =
                Color.FromArgb(6, 22, 54);

            dgvMatriculas.GridColor =
                Color.FromArgb(32, 56, 103);

            dgvMatriculas.EnableHeadersVisualStyles =
                false;

            dgvMatriculas.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvMatriculas.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(15, 39, 83);

            dgvMatriculas.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvMatriculas.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold
                );

            dgvMatriculas.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvMatriculas.ColumnHeadersHeight = 42;

            dgvMatriculas.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvMatriculas.DefaultCellStyle.BackColor =
                Color.FromArgb(8, 27, 63);

            dgvMatriculas.DefaultCellStyle.ForeColor =
                Color.FromArgb(226, 233, 245);

            dgvMatriculas.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(60, 33, 112);

            dgvMatriculas.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvMatriculas.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.3F
                );

            dgvMatriculas.DefaultCellStyle.Padding =
                new Padding(6, 0, 6, 0);

            dgvMatriculas.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(10, 31, 70);

            dgvMatriculas.RowTemplate.Height = 40;

            dgvMatriculas.RowHeadersVisible = false;

            dgvMatriculas.AllowUserToAddRows = false;
            dgvMatriculas.AllowUserToDeleteRows = false;
            dgvMatriculas.AllowUserToResizeRows = false;

            dgvMatriculas.MultiSelect = false;
            dgvMatriculas.ReadOnly = true;

            dgvMatriculas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvMatriculas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvMatriculas.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvMatriculas.DataBindingComplete +=
                dgvMatriculas_DataBindingComplete;
        }

        private void dgvMatriculas_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (fuenteOriginalMatriculas == null)
                {
                    fuenteOriginalMatriculas =
                        dgvMatriculas.DataSource;
                }

                ConfigurarColumna(
                    "IdMatricula",
                    "ID",
                    35
                );

                ConfigurarColumna(
                    "IdAlumno",
                    "Id alumno",
                    45
                );

                ConfigurarColumna(
                    "IdNivel",
                    "Id nivel",
                    40
                );

                ConfigurarColumna(
                    "IdInstructor",
                    "Id instructor",
                    48
                );

                ConfigurarColumna(
                    "FechaMatricula",
                    "Fecha",
                    65
                );

                ConfigurarColumna(
                    "NombreAlumno",
                    "Alumno",
                    100
                );

                ConfigurarColumna(
                    "NombreNivel",
                    "Nivel",
                    70
                );

                ConfigurarColumna(
                    "NombreInstructor",
                    "Instructor",
                    90
                );

                if (dgvMatriculas.Columns["FechaMatricula"] != null)
                {
                    dgvMatriculas.Columns["FechaMatricula"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                dgvMatriculas.ClearSelection();
                ActualizarTotalMatriculas();
            }
            catch
            {
                // El formulario sigue funcionando si cambia una columna.
            }
        }

        private void ConfigurarColumna(
            string nombre,
            string encabezado,
            float peso)
        {
            if (dgvMatriculas.Columns[nombre] == null)
                return;

            dgvMatriculas.Columns[nombre].HeaderText =
                encabezado;

            dgvMatriculas.Columns[nombre].FillWeight =
                peso;
        }

        private void ActualizarTotalMatriculas()
        {
            if (lblTotalMatriculas == null ||
                dgvMatriculas == null)
            {
                return;
            }

            int cantidad = dgvMatriculas.Rows
                .Cast<DataGridViewRow>()
                .Count(fila =>
                    !fila.IsNewRow &&
                    fila.Visible);

            lblTotalMatriculas.Text =
                cantidad == 1
                    ? "Mostrando 1 matricula"
                    : $"Mostrando {cantidad} matriculas";
        }


        // =========================================================
        // ELIMINACION DE MATRICULA CON CORREO DE CANCELACION
        // =========================================================

        private void ConfigurarEliminacionMatriculaConCorreo()
        {
            if (eliminacionCorreoConfigurada)
                return;

            eliminacionCorreoConfigurada = true;

            /*
             * Se conectan una sola vez los eventos definitivos.
             * El botón Nuevo actualiza los ComboBox antes de habilitar
             * el formulario, evitando depender de VisibleChanged.
             */

            btnNuevo.Click -= btnNuevo_Click;
            btnNuevo.Click -= btnNuevoConActualizacion_Click;
            btnNuevo.Click += btnNuevoConActualizacion_Click;

            btnGuardar.Click -= btnGuardar_Click;
            btnGuardar.Click += btnGuardar_Click;

            btnLimpiar.Click -= btnLimpiar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            btnEliminar.Click -= btnEliminar_Click;
            btnEliminar.Click -= btnEliminarConCorreo_Click;
            btnEliminar.Click += btnEliminarConCorreo_Click;

            dgvMatriculas.SelectionChanged -=
                dgvMatriculas_SelectionChanged;

            dgvMatriculas.SelectionChanged +=
                dgvMatriculas_SelectionChanged;
        }

        private async void btnNuevoConActualizacion_Click(
            object? sender,
            EventArgs e)
        {
            if (actualizandoDatosMatricula)
                return;

            actualizandoDatosMatricula = true;
            btnNuevo.Enabled = false;

            try
            {
                lblEstado.Text =
                    "Actualizando alumnos, niveles e instructores...";

                lblEstado.ForeColor =
                    Color.Orange;

                await CargarCombosAsync();

                fuenteOriginalMatriculas =
                    dgvMatriculas.DataSource;

                /*
                 * Ejecuta la lógica original de frmMatriculas.cs:
                 * habilita campos y prepara una matrícula nueva.
                 */
                btnNuevo_Click(sender!, e);

                lblEstado.Text =
                    "Datos actualizados. Puede registrar la matrícula.";

                lblEstado.ForeColor =
                    Color.Green;
            }
            catch (Exception ex)
            {
                lblEstado.Text =
                    "No se pudieron actualizar los datos";

                lblEstado.ForeColor =
                    Color.Red;

                MessageBox.Show(
                    "No se pudieron actualizar los alumnos, " +
                    "niveles e instructores:\r\n\r\n" +
                    ex.Message,
                    "Matrículas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            finally
            {
                btnNuevo.Enabled = true;
                actualizandoDatosMatricula = false;
            }
        }

        private async void btnEliminarConCorreo_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0 ||
                    dgvMatriculas.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Seleccione una matrícula de la grilla para eliminar.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                DataGridViewRow fila =
                    dgvMatriculas.SelectedRows[0];

                int idAlumno = ObtenerEnteroCeldaMatricula(
                    fila,
                    "IdAlumno"
                );

                string nombreAlumno =
                    ObtenerTextoCeldaMatricula(
                        fila,
                        "NombreAlumno",
                        "Alumno",
                        "Nombre"
                    );

                string nombreNivel =
                    ObtenerTextoCeldaMatricula(
                        fila,
                        "NombreNivel",
                        "Nivel"
                    );

                string correoAlumno =
                    ObtenerTextoCeldaMatricula(
                        fila,
                        "Correo",
                        "CorreoAlumno",
                        "Email"
                    );

                if (idAlumno > 0 &&
                    (string.IsNullOrWhiteSpace(nombreAlumno) ||
                     string.IsNullOrWhiteSpace(correoAlumno)))
                {
                    CompletarDatosAlumnoMatricula(
                        idAlumno,
                        ref nombreAlumno,
                        ref correoAlumno
                    );
                }

                if (string.IsNullOrWhiteSpace(nombreAlumno))
                {
                    nombreAlumno = "Alumno seleccionado";
                }

                if (string.IsNullOrWhiteSpace(nombreNivel))
                {
                    nombreNivel = "Nivel registrado";
                }

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

                DialogResult confirmacion =
                    MessageBox.Show(
                        "¿Desea cancelar esta matrícula?\r\n\r\n" +
                        "Alumno: " + nombreAlumno + "\r\n" +
                        "Nivel: " + nombreNivel + "\r\n\r\n" +
                        "Al confirmar, se enviará un correo de cancelación.",
                        "Confirmar cancelación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (confirmacion != DialogResult.Yes)
                    return;

                btnEliminar.Enabled = false;

                lblEstado.Text =
                    "Cancelando matrícula...";

                lblEstado.ForeColor =
                    Color.Orange;

                bool resultado =
                    matriculaCD.Eliminar(idSeleccionado);

                if (!resultado)
                {
                    MessageBox.Show(
                        "Error al eliminar la matrícula.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                bool correoEnviado = false;
                string errorCorreo = string.Empty;

                if (!string.IsNullOrWhiteSpace(correoAlumno))
                {
                    try
                    {
                        await servicioCorreo
                            .EnviarCancelacionMatriculaAsync(
                                correoAlumno,
                                nombreAlumno,
                                nombreNivel
                            );

                        correoEnviado = true;
                    }
                    catch (Exception exCorreo)
                    {
                        errorCorreo = exCorreo.Message;
                    }
                }

                CargarGrilla();
                LimpiarCampos();

                lblEstado.Text = "Listo";
                lblEstado.ForeColor = Color.Green;

                if (correoEnviado)
                {
                    MessageBox.Show(
                        "Matrícula cancelada correctamente.\r\n" +
                        "El correo de cancelación fue enviado.",
                        "Cancelación completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else if (string.IsNullOrWhiteSpace(correoAlumno))
                {
                    MessageBox.Show(
                        "Matrícula cancelada correctamente.\r\n\r\n" +
                        "No se envió el correo porque el alumno " +
                        "no tiene un correo registrado.",
                        "Cancelación completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show(
                        "La matrícula fue cancelada, pero el correo " +
                        "no pudo enviarse.\r\n\r\n" +
                        errorCorreo,
                        "Advertencia de correo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cancelar la matrícula:\r\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                if (idSeleccionado != 0)
                {
                    btnEliminar.Enabled = true;
                }
            }
        }

        private void CompletarDatosAlumnoMatricula(
            int idAlumno,
            ref string nombreAlumno,
            ref string correoAlumno)
        {
            object? alumnos =
                alumnoCD.ObtenerTodos();

            if (alumnos is not IEnumerable lista)
                return;

            foreach (object? alumno in lista)
            {
                if (alumno == null)
                    continue;

                int idActual =
                    ObtenerEnteroPropiedadMatricula(
                        alumno,
                        "IdAlumno"
                    );

                if (idActual != idAlumno)
                    continue;

                if (string.IsNullOrWhiteSpace(nombreAlumno))
                {
                    string nombre =
                        ObtenerTextoPropiedadMatricula(
                            alumno,
                            "Nombre"
                        );

                    string apellido =
                        ObtenerTextoPropiedadMatricula(
                            alumno,
                            "Apellido"
                        );

                    nombreAlumno =
                        (nombre + " " + apellido).Trim();
                }

                if (string.IsNullOrWhiteSpace(correoAlumno))
                {
                    correoAlumno =
                        ObtenerTextoPropiedadMatricula(
                            alumno,
                            "Correo",
                            "Email"
                        );
                }

                break;
            }
        }

        private static int ObtenerEnteroCeldaMatricula(
            DataGridViewRow fila,
            params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                if (!fila.DataGridView.Columns.Contains(nombre))
                    continue;

                object? valor =
                    fila.Cells[nombre].Value;

                if (valor != null &&
                    int.TryParse(
                        valor.ToString(),
                        out int numero))
                {
                    return numero;
                }
            }

            return 0;
        }

        private static string ObtenerTextoCeldaMatricula(
            DataGridViewRow fila,
            params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                if (!fila.DataGridView.Columns.Contains(nombre))
                    continue;

                string texto =
                    fila.Cells[nombre].Value?
                        .ToString()?
                        .Trim()
                    ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;
            }

            return string.Empty;
        }

        private static int ObtenerEnteroPropiedadMatricula(
            object objeto,
            string nombre)
        {
            PropertyInfo? propiedad =
                objeto.GetType().GetProperty(
                    nombre,
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase
                );

            if (propiedad == null)
                return 0;

            object? valor =
                propiedad.GetValue(objeto);

            return int.TryParse(
                valor?.ToString(),
                out int numero
            )
                ? numero
                : 0;
        }

        private static string ObtenerTextoPropiedadMatricula(
            object objeto,
            params string[] nombres)
        {
            foreach (string nombre in nombres)
            {
                PropertyInfo? propiedad =
                    objeto.GetType().GetProperty(
                        nombre,
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.IgnoreCase
                    );

                if (propiedad == null)
                    continue;

                string texto =
                    propiedad.GetValue(objeto)?
                        .ToString()?
                        .Trim()
                    ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;
            }

            return string.Empty;
        }

        // =========================================================
        // EVENTOS VISUALES
        // =========================================================

        private void ConfigurarEventosVisuales()
        {
            pnlFormulario.Resize += (sender, e) =>
            {
                pnlAcciones.Width =
                    pnlFormulario.ClientSize.Width - 40;

                pnlAcciones.Left = 20;

                btnGuardar.Width =
                    pnlAcciones.Width;

                int anchoMitad =
                    (pnlAcciones.Width - 10) / 2;

                btnLimpiar.Width = anchoMitad;
                btnEliminar.Width = anchoMitad;
                btnEliminar.Left = anchoMitad + 10;

                RedondearControl(
                    pnlFormulario,
                    18
                );
            };

            pnlLista.Resize += (sender, e) =>
            {
                pnlBuscador.Left =
                    pnlCabeceraLista.ClientSize.Width -
                    pnlBuscador.Width -
                    18;

                pnlContenedorGrid.Size =
                    new Size(
                        pnlLista.ClientSize.Width - 32,
                        pnlLista.ClientSize.Height - 120
                    );

                lblTotalMatriculas.Top =
                    pnlLista.ClientSize.Height - 32;

                RedondearControl(
                    pnlLista,
                    18
                );
            };

            pnlEncabezado.Resize += (sender, e) =>
            {
                btnCerrarVentana.Left =
                    pnlEncabezado.ClientSize.Width -
                    btnCerrarVentana.Width -
                    20;

                btnNuevo.Left =
                    btnCerrarVentana.Left -
                    btnNuevo.Width -
                    20;
            };

            dgvMatriculas.DataSourceChanged += (sender, e) =>
            {
                ActualizarTotalMatriculas();
            };

            cmbAlumno.Enter += CampoCombo_Enter;
            cmbNivel.Enter += CampoCombo_Enter;
            cmbInstructor.Enter += CampoCombo_Enter;
            txtBuscarMatricula.Enter += CampoTexto_Enter;

            cmbAlumno.Leave += CampoCombo_Leave;
            cmbNivel.Leave += CampoCombo_Leave;
            cmbInstructor.Leave += CampoCombo_Leave;
            txtBuscarMatricula.Leave += CampoTexto_Leave;
        }

        private void CampoCombo_Enter(
            object? sender,
            EventArgs e)
        {
            if (sender is ComboBox combo)
            {
                combo.BackColor =
                    Color.FromArgb(10, 31, 72);
            }
        }

        private void CampoCombo_Leave(
            object? sender,
            EventArgs e)
        {
            if (sender is ComboBox combo)
            {
                combo.BackColor =
                    Color.FromArgb(7, 23, 57);
            }
        }

        private void CampoTexto_Enter(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox caja)
            {
                caja.BackColor =
                    Color.FromArgb(10, 31, 72);
            }
        }

        private void CampoTexto_Leave(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox caja)
            {
                caja.BackColor =
                    Color.FromArgb(7, 23, 57);
            }
        }

        // =========================================================
        // DIMENSIONES
        // =========================================================

        private void AjustarDisenoResponsivo()
        {
            if (!disenoMatriculasInicializado ||
                pnlCuerpo == null ||
                pnlFormulario == null ||
                pnlLista == null)
            {
                return;
            }

            const int margen = 18;
            const int separacion = 18;
            const int anchoFormulario = 350;
            const int anchoMinimoLista = 650;
            const int altoMinimoPaneles = 640;

            int anchoInterior =
                pnlCuerpo.ClientSize.Width -
                (margen * 2);

            int anchoLista =
                anchoInterior -
                anchoFormulario -
                separacion;

            int altoDisponible =
                pnlCuerpo.ClientSize.Height -
                (margen * 2);

            int altoPaneles =
                Math.Max(
                    altoMinimoPaneles,
                    altoDisponible
                );

            pnlFormulario.Location =
                new Point(margen, margen);

            pnlFormulario.Size =
                new Size(
                    anchoFormulario,
                    altoPaneles
                );

            pnlLista.Location =
                new Point(
                    margen +
                    anchoFormulario +
                    separacion,
                    margen
                );

            if (anchoLista >= anchoMinimoLista)
            {
                pnlLista.Size =
                    new Size(
                        anchoLista,
                        altoPaneles
                    );

                pnlCuerpo.AutoScrollMinSize =
                    new Size(
                        0,
                        altoPaneles +
                        (margen * 2)
                    );
            }
            else
            {
                pnlLista.Size =
                    new Size(
                        anchoMinimoLista,
                        altoPaneles
                    );

                pnlCuerpo.AutoScrollMinSize =
                    new Size(
                        margen +
                        anchoFormulario +
                        separacion +
                        anchoMinimoLista +
                        margen,

                        altoPaneles +
                        (margen * 2)
                    );
            }

            pnlContenedorGrid.Location =
                new Point(16, 78);

            pnlContenedorGrid.Size =
                new Size(
                    pnlLista.ClientSize.Width - 32,
                    pnlLista.ClientSize.Height - 120
                );

            lblTotalMatriculas.Location =
                new Point(
                    20,
                    pnlLista.ClientSize.Height - 32
                );

            pnlBuscador.Left =
                pnlCabeceraLista.ClientSize.Width -
                pnlBuscador.Width -
                18;

            pnlAcciones.Location =
                new Point(
                    20,
                    pnlFormulario.ClientSize.Height -
                    pnlAcciones.Height -
                    20
                );

            btnCerrarVentana.Left =
                pnlEncabezado.ClientSize.Width -
                btnCerrarVentana.Width -
                20;

            btnNuevo.Left =
                btnCerrarVentana.Left -
                btnNuevo.Width -
                20;

            RedondearControl(pnlFormulario, 18);
            RedondearControl(pnlLista, 18);
            RedondearControl(pnlContenedorGrid, 12);
        }

        // =========================================================
        // NAVEGACION
        // =========================================================

        private void VolverAlDashboard()
        {
            frmPrincipal? principal =
                Application.OpenForms
                    .OfType<frmPrincipal>()
                    .FirstOrDefault();

            if (principal != null)
            {
                principal.Show();

                principal.WindowState =
                    FormWindowState.Normal;

                principal.BringToFront();
                principal.Activate();

                Close();
                return;
            }

            frmPrincipal nuevoPrincipal =
                new frmPrincipal();

            nuevoPrincipal.Show();
            Close();
        }

        private void AbrirFormulario(
            Form formulario)
        {
            try
            {
                Form? formularioAbierto =
                    Application.OpenForms
                        .Cast<Form>()
                        .FirstOrDefault(
                            f => f.GetType() ==
                                 formulario.GetType()
                        );

                if (formularioAbierto != null)
                {
                    formulario.Dispose();

                    formularioAbierto.WindowState =
                        FormWindowState.Normal;

                    formularioAbierto.BringToFront();
                    formularioAbierto.Activate();

                    return;
                }

                formulario.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo abrir el formulario:\r\n" +
                    ex.Message,
                    "Navegacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                formulario.Dispose();
            }
        }

        // =========================================================
        // COLORES
        // =========================================================

        private static Color AclararColor(
            Color color,
            int cantidad)
        {
            return Color.FromArgb(
                color.A,
                Math.Min(255, color.R + cantidad),
                Math.Min(255, color.G + cantidad),
                Math.Min(255, color.B + cantidad)
            );
        }

        private static Color OscurecerColor(
            Color color,
            int cantidad)
        {
            return Color.FromArgb(
                color.A,
                Math.Max(0, color.R - cantidad),
                Math.Max(0, color.G - cantidad),
                Math.Max(0, color.B - cantidad)
            );
        }

        // =========================================================
        // BORDES REDONDEADOS
        // =========================================================

        private static void RedondearControl(
            Control control,
            int radio)
        {
            if (control == null ||
                control.Width <= 0 ||
                control.Height <= 0)
            {
                return;
            }

            int diametro = radio * 2;

            diametro = Math.Min(
                diametro,
                control.Width
            );

            diametro = Math.Min(
                diametro,
                control.Height
            );

            Rectangle arco = new Rectangle(
                0,
                0,
                diametro,
                diametro
            );

            using (GraphicsPath ruta =
                   new GraphicsPath())
            {
                ruta.AddArc(arco, 180, 90);

                arco.X =
                    control.Width - diametro;

                ruta.AddArc(arco, 270, 90);

                arco.Y =
                    control.Height - diametro;

                ruta.AddArc(arco, 0, 90);

                arco.X = 0;

                ruta.AddArc(arco, 90, 90);

                ruta.CloseFigure();

                Region? regionAnterior =
                    control.Region;

                control.Region =
                    new Region(ruta);

                regionAnterior?.Dispose();
            }
        }

        // =========================================================
        // FONDO
        // =========================================================

        protected override void OnPaintBackground(
            PaintEventArgs e)
        {
            Rectangle area = ClientRectangle;

            using (LinearGradientBrush fondo =
                   new LinearGradientBrush(
                       area,
                       Color.FromArgb(3, 16, 43),
                       Color.FromArgb(57, 20, 107),
                       0F))
            {
                ColorBlend mezcla =
                    new ColorBlend
                    {
                        Colors = new[]
                        {
                            Color.FromArgb(3, 16, 43),
                            Color.FromArgb(6, 28, 70),
                            Color.FromArgb(27, 29, 91),
                            Color.FromArgb(61, 20, 110)
                        },

                        Positions = new[]
                        {
                            0F,
                            0.38F,
                            0.72F,
                            1F
                        }
                    };

                fondo.InterpolationColors = mezcla;

                e.Graphics.FillRectangle(
                    fondo,
                    area
                );
            }

            DibujarCurvaInferior(e.Graphics);
        }

        private void DibujarCurvaInferior(
            Graphics graphics)
        {
            graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;

            using (GraphicsPath curva =
                   new GraphicsPath())
            {
                curva.AddBezier(
                    -100,
                    alto - 95,
                    ancho / 4,
                    alto + 45,
                    ancho * 3 / 4,
                    alto + 60,
                    ancho + 120,
                    alto - 90
                );

                using (Pen lapiz =
                       new Pen(
                           Color.FromArgb(
                               125,
                               93,
                               58,
                               255
                           ),
                           2.3F))
                {
                    graphics.DrawPath(
                        lapiz,
                        curva
                    );
                }
            }
        }

        // =========================================================
        // MODO INTEGRADO EN FRMPRINCIPAL
        // =========================================================

        public void PrepararModoIntegrado()
        {
            modoIntegradoSolicitado = true;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.Manual;

            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            AutoScaleMode = AutoScaleMode.None;

            Dock = DockStyle.Fill;
            Margin = new Padding(0);
            Padding = new Padding(0);

            AplicarModoIntegrado();
        }

        private void AplicarModoIntegrado()
        {
            if (!modoIntegradoSolicitado)
                return;

            SuspendLayout();

            try
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                MinimumSize = Size.Empty;
                MaximumSize = Size.Empty;
                AutoScaleMode = AutoScaleMode.None;

                if (pnlMenuLateral != null)
                {
                    pnlMenuLateral.Visible = false;
                    pnlMenuLateral.Dock = DockStyle.None;
                    pnlMenuLateral.Width = 0;
                }

                if (pnlEncabezado != null)
                {
                    pnlEncabezado.Visible = false;
                    pnlEncabezado.Dock = DockStyle.None;
                    pnlEncabezado.Height = 0;
                }

                if (pnlContenido != null)
                {
                    pnlContenido.Visible = true;
                    pnlContenido.Dock = DockStyle.Fill;
                    pnlContenido.Location = Point.Empty;
                    pnlContenido.Margin = new Padding(0);
                    pnlContenido.Padding = new Padding(0);
                    pnlContenido.BringToFront();
                }

                if (pnlCuerpo != null)
                {
                    pnlCuerpo.Visible = true;
                    pnlCuerpo.Dock = DockStyle.Fill;
                    pnlCuerpo.Location = Point.Empty;
                    pnlCuerpo.Margin = new Padding(0);
                }

                PerformLayout();

                if (pnlCuerpo != null)
                {
                    AjustarDisenoResponsivo();
                }

                Invalidate(true);
            }
            finally
            {
                ResumeLayout(true);
            }
        }

    }
}