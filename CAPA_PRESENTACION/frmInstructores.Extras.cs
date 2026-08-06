using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmInstructores
    {
        // =========================================================
        // VARIABLES DEL DISENO
        // =========================================================

        private bool disenoModernoInicializado = false;
        private bool formularioYaMostrado = false;
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

        private Button btnNuevoInstructor = null!;
        private Button btnCerrarVentana = null!;

        // =========================================================
        // REDUCIR PARPADEO
        // =========================================================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parametros = base.CreateParams;

                // WS_EX_COMPOSITED
                parametros.ExStyle |= 0x02000000;

                return parametros;
            }
        }

        // =========================================================
        // PREPARAR ANTES DE MOSTRAR
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoModernoInicializado)
            {
                disenoModernoInicializado = true;

                SuspendLayout();

                try
                {
                    InicializarDisenoModerno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar el diseno de instructores:\r\n" +
                        ex.Message,
                        "Gestion de Instructores",
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

            if (value && !formularioYaMostrado)
            {
                formularioYaMostrado = true;

                BeginInvoke(new Action(() =>
                {
                    AjustarDisenoResponsivo();

                    if (modoIntegradoSolicitado)
                    {
                        AplicarModoIntegrado();
                    }
                    Invalidate(true);
                    Update();
                }));
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        private void InicializarDisenoModerno()
        {
            ConfigurarFormulario();
            CrearEstructuraPrincipal();
            CrearMenuLateral();
            CrearEncabezado();
            CrearPanelFormulario();
            CrearPanelLista();
            ConfigurarControlesFormulario();
            ConfigurarBotonesAccion();
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
            Text = "Gestion de Instructores - Lexbridge";

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1160, 700);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;

            BackColor = Color.FromArgb(4, 17, 44);
            ForeColor = Color.White;

            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );

            AutoScaleMode = AutoScaleMode.Dpi;
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
                Font = new Font(
                    "Segoe UI",
                    6.8F
                )
            };

            btnMenuDashboard = CrearBotonMenu(
                "Dashboard",
                "\uE80F",
                8,
                false
            );

            btnMenuNiveles = CrearBotonMenu(
                "Niveles",
                "\uE8EF",
                60,
                false
            );

            btnMenuInstructores = CrearBotonMenu(
                "Instructores",
                "\uE77B",
                112,
                true
            );

            btnMenuMatriculas = CrearBotonMenu(
                "Matriculas",
                "\uE787",
                164,
                false
            );

            btnMenuPagos = CrearBotonMenu(
                "Pagos",
                "\uE8C7",
                216,
                false
            );

            btnMenuReportes = CrearBotonMenu(
                "Reportes",
                "\uE9D2",
                268,
                false
            );

            btnMenuConsulta = CrearBotonMenu(
                "Consulta",
                "\uE721",
                320,
                false
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
                txtNombre.Focus();
            };

            btnMenuMatriculas.Click += (sender, e) =>
            {
                AbrirFormulario(new frmMatriculas());
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
                Font = new Font(
                    "Segoe UI",
                    8F
                )
            };

            lblVersion = new Label
            {
                Parent = pnlPieMenu,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),
                ForeColor = Color.FromArgb(185, 197, 219),
                BackColor = Color.Transparent,
                Font = new Font(
                    "Segoe UI",
                    8F
                )
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
                TextAlign = ContentAlignment.MiddleCenter,

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
                    BackColor = Color.FromArgb(255, 181, 25)
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
                Text = "Gestion de Instructores",
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
                    "Administra los datos del personal docente de la academia",

                Location = new Point(35, 55),
                Size = new Size(540, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    9F
                )
            };

            btnNuevoInstructor = new Button
            {
                Parent = pnlEncabezado,
                Size = new Size(150, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEncabezado.ClientSize.Width - 315,
                    24
                ),

                Text = "Nuevo instructor",

                BackColor =
                    Color.FromArgb(14, 36, 76),

                ForeColor =
                    Color.FromArgb(255, 189, 36),

                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnNuevoInstructor.FlatAppearance.BorderColor =
                Color.FromArgb(255, 174, 20);

            btnNuevoInstructor.FlatAppearance.BorderSize = 1;

            btnNuevoInstructor.Click += (sender, e) =>
            {
                btnLimpiar.PerformClick();
                txtNombre.Focus();
            };

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

            RedondearControl(btnNuevoInstructor, 11);
            RedondearControl(btnCerrarVentana, 11);
        }

        // =========================================================
        // PANEL DE INFORMACION
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

                Text = "Informacion del instructor",

                Location = new Point(20, 15),
                Size = new Size(300, 28),

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
                Location = new Point(20, 458),
                Size = new Size(310, 160),
                BackColor = Color.Transparent
            };

            RedondearControl(pnlFormulario, 18);
        }

        private void ConfigurarControlesFormulario()
        {
            ConfigurarEtiqueta(
                lblNombre,
                "Nombre *",
                new Point(20, 72),
                300
            );

            ConfigurarTextBox(
                txtNombre,
                new Point(20, 99),
                310
            );

            txtNombre.PlaceholderText =
                "Ingrese el nombre";

            ConfigurarEtiqueta(
                lblApellido,
                "Apellido *",
                new Point(20, 145),
                300
            );

            ConfigurarTextBox(
                txtApellido,
                new Point(20, 172),
                310
            );

            txtApellido.PlaceholderText =
                "Ingrese el apellido";

            ConfigurarEtiqueta(
                lblEspecialidad,
                "Especialidad *",
                new Point(20, 218),
                300
            );

            ConfigurarTextBox(
                txtEspecialidad,
                new Point(20, 245),
                310
            );

            txtEspecialidad.PlaceholderText =
                "Ej. Conversacion";

            ConfigurarEtiqueta(
                lblTelefono,
                "Telefono *",
                new Point(20, 291),
                300
            );

            ConfigurarTextBox(
                txtTelefono,
                new Point(20, 318),
                310
            );

            txtTelefono.PlaceholderText =
                "Ej. 8095551234";

            lblMensaje.Parent = pnlFormulario;
            lblMensaje.AutoSize = false;

            lblMensaje.Location =
                new Point(20, 358);

            lblMensaje.Size =
                new Size(310, 84);

            lblMensaje.BackColor =
                Color.Transparent;

            lblMensaje.Font = new Font(
                "Segoe UI",
                8.5F
            );

            lblMensaje.TextAlign =
                ContentAlignment.MiddleLeft;
        }

        private void ConfigurarEtiqueta(
            Label etiqueta,
            string texto,
            Point posicion,
            int ancho)
        {
            etiqueta.Parent = pnlFormulario;
            etiqueta.AutoSize = false;

            etiqueta.Location = posicion;
            etiqueta.Size = new Size(ancho, 23);

            etiqueta.Text = texto;

            etiqueta.ForeColor = Color.White;
            etiqueta.BackColor = Color.Transparent;

            etiqueta.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );
        }

        private void ConfigurarTextBox(
            TextBox caja,
            Point posicion,
            int ancho)
        {
            caja.Parent = pnlFormulario;
            caja.Location = posicion;
            caja.Size = new Size(ancho, 34);

            caja.Font = new Font(
                "Segoe UI",
                9.5F
            );

            caja.BackColor =
                Color.FromArgb(7, 23, 57);

            caja.ForeColor = Color.White;

            caja.BorderStyle =
                BorderStyle.FixedSingle;

            caja.Margin = new Padding(0);
        }

        // =========================================================
        // BOTONES DE ACCION
        // =========================================================

        private void ConfigurarBotonesAccion()
        {
            btnGuardar.Parent = pnlAcciones;
            btnActualizar.Parent = pnlAcciones;
            btnLimpiar.Parent = pnlAcciones;
            btnEliminar.Parent = pnlAcciones;

            ConfigurarBotonAccion(
                btnGuardar,
                "Guardar",
                new Point(0, 0),
                new Size(310, 42),
                Color.FromArgb(10, 146, 153),
                Color.FromArgb(27, 207, 190)
            );

            ConfigurarBotonAccion(
                btnActualizar,
                "Actualizar",
                new Point(0, 51),
                new Size(150, 40),
                Color.FromArgb(17, 86, 135),
                Color.FromArgb(46, 170, 220)
            );

            ConfigurarBotonAccion(
                btnLimpiar,
                "Limpiar",
                new Point(160, 51),
                new Size(150, 40),
                Color.FromArgb(25, 43, 82),
                Color.FromArgb(78, 102, 155)
            );

            ConfigurarBotonAccion(
                btnEliminar,
                "Eliminar",
                new Point(0, 100),
                new Size(310, 42),
                Color.FromArgb(190, 15, 65),
                Color.FromArgb(235, 42, 92)
            );
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
        // PANEL DE LISTA
        // =========================================================

        private void CrearPanelLista()
        {
            pnlLista = new Panel
            {
                Parent = pnlCuerpo,

                Location =
                    new Point(386, 18),

                Size =
                    new Size(760, 640),

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

                Text = "Lista de instructores",

                Location = new Point(20, 17),
                Size = new Size(260, 30),

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

                Size = new Size(330, 36),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlCabeceraLista.Width - 348,
                    14
                ),

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            pnlContenedorGrid = new Panel
            {
                Parent = pnlLista,

                Location =
                    new Point(16, 78),

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

            lblTotal.Parent = pnlLista;
            lblTotal.AutoSize = false;

            lblTotal.Location = new Point(
                20,
                pnlLista.Height - 32
            );

            lblTotal.Size = new Size(410, 24);

            lblTotal.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Left;

            lblTotal.ForeColor =
                Color.FromArgb(175, 191, 219);

            lblTotal.BackColor =
                Color.Transparent;

            lblTotal.Font = new Font(
                "Segoe UI",
                8.5F
            );

            RedondearControl(pnlLista, 18);
            RedondearControl(pnlContenedorGrid, 12);
        }

        // =========================================================
        // BUSCADOR
        // =========================================================

        private void ConfigurarBuscador()
        {
            lblBuscar.Visible = false;

            txtBuscar.Parent = pnlBuscador;

            txtBuscar.Location =
                new Point(12, 7);

            txtBuscar.Size =
                new Size(255, 24);

            txtBuscar.BorderStyle =
                BorderStyle.None;

            txtBuscar.BackColor =
                Color.FromArgb(7, 23, 57);

            txtBuscar.ForeColor =
                Color.White;

            txtBuscar.Font = new Font(
                "Segoe UI",
                8.8F
            );

            txtBuscar.PlaceholderText =
                "Nombre, apellido o especialidad...";

            btnBuscar.Parent = pnlBuscador;

            btnBuscar.Location =
                new Point(274, 3);

            btnBuscar.Size =
                new Size(52, 30);

            btnBuscar.Text = "Ir";

            btnBuscar.BackColor =
                Color.FromArgb(20, 76, 120);

            btnBuscar.ForeColor =
                Color.White;

            btnBuscar.FlatStyle =
                FlatStyle.Flat;

            btnBuscar.FlatAppearance.BorderSize = 0;

            btnBuscar.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold
            );

            btnBuscar.Cursor =
                Cursors.Hand;

            txtBuscar.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnBuscar.PerformClick();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    txtBuscar.Clear();
                    btnBuscar.PerformClick();
                }
            };

            RedondearControl(pnlBuscador, 10);
            RedondearControl(btnBuscar, 8);
        }

        // =========================================================
        // DATAGRIDVIEW
        // =========================================================

        private void ConfigurarDataGridView()
        {
            dgvInstructores.Parent =
                pnlContenedorGrid;

            dgvInstructores.Dock =
                DockStyle.Fill;

            dgvInstructores.BorderStyle =
                BorderStyle.None;

            dgvInstructores.BackgroundColor =
                Color.FromArgb(6, 22, 54);

            dgvInstructores.GridColor =
                Color.FromArgb(32, 56, 103);

            dgvInstructores.EnableHeadersVisualStyles =
                false;

            dgvInstructores.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvInstructores.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(15, 39, 83);

            dgvInstructores.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvInstructores.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold
                );

            dgvInstructores.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvInstructores.ColumnHeadersHeight = 42;

            dgvInstructores.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvInstructores.DefaultCellStyle.BackColor =
                Color.FromArgb(8, 27, 63);

            dgvInstructores.DefaultCellStyle.ForeColor =
                Color.FromArgb(226, 233, 245);

            dgvInstructores.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(60, 33, 112);

            dgvInstructores.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvInstructores.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.8F
                );

            dgvInstructores.DefaultCellStyle.Padding =
                new Padding(7, 0, 7, 0);

            dgvInstructores.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(10, 31, 70);

            dgvInstructores.RowTemplate.Height = 40;

            dgvInstructores.RowHeadersVisible = false;

            dgvInstructores.AllowUserToAddRows = false;
            dgvInstructores.AllowUserToDeleteRows = false;
            dgvInstructores.AllowUserToResizeRows = false;

            dgvInstructores.MultiSelect = false;
            dgvInstructores.ReadOnly = true;

            dgvInstructores.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvInstructores.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvInstructores.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvInstructores.DataBindingComplete +=
                dgvInstructores_DataBindingComplete;
        }

        private void dgvInstructores_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (dgvInstructores.Columns["IdInstructor"] != null)
                {
                    dgvInstructores.Columns["IdInstructor"].HeaderText =
                        "ID";

                    dgvInstructores.Columns["IdInstructor"].FillWeight =
                        24;
                }

                if (dgvInstructores.Columns["Nombre"] != null)
                {
                    dgvInstructores.Columns["Nombre"].HeaderText =
                        "Nombre";

                    dgvInstructores.Columns["Nombre"].FillWeight =
                        65;
                }

                if (dgvInstructores.Columns["Apellido"] != null)
                {
                    dgvInstructores.Columns["Apellido"].HeaderText =
                        "Apellido";

                    dgvInstructores.Columns["Apellido"].FillWeight =
                        65;
                }

                if (dgvInstructores.Columns["Especialidad"] != null)
                {
                    dgvInstructores.Columns["Especialidad"].HeaderText =
                        "Especialidad";

                    dgvInstructores.Columns["Especialidad"].FillWeight =
                        90;
                }

                if (dgvInstructores.Columns["Telefono"] != null)
                {
                    dgvInstructores.Columns["Telefono"].HeaderText =
                        "Telefono";

                    dgvInstructores.Columns["Telefono"].FillWeight =
                        65;
                }

                dgvInstructores.ClearSelection();
            }
            catch
            {
                // No detiene el formulario si cambia alguna columna.
            }
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

                btnActualizar.Width =
                    anchoMitad;

                btnLimpiar.Width =
                    anchoMitad;

                btnLimpiar.Left =
                    anchoMitad + 10;

                btnEliminar.Width =
                    pnlAcciones.Width;

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

                lblTotal.Top =
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

                btnNuevoInstructor.Left =
                    btnCerrarVentana.Left -
                    btnNuevoInstructor.Width -
                    20;
            };

            txtNombre.Enter += Campo_Enter;
            txtApellido.Enter += Campo_Enter;
            txtEspecialidad.Enter += Campo_Enter;
            txtTelefono.Enter += Campo_Enter;
            txtBuscar.Enter += Campo_Enter;

            txtNombre.Leave += Campo_Leave;
            txtApellido.Leave += Campo_Leave;
            txtEspecialidad.Leave += Campo_Leave;
            txtTelefono.Leave += Campo_Leave;
            txtBuscar.Leave += Campo_Leave;
        }

        private void Campo_Enter(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox caja)
            {
                caja.BackColor =
                    Color.FromArgb(10, 31, 72);
            }
        }

        private void Campo_Leave(
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
        // REDIMENSIONAMIENTO
        // =========================================================

        private void AjustarDisenoResponsivo()
        {
            if (!disenoModernoInicializado ||
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
                new Point(
                    margen,
                    margen
                );

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

            lblTotal.Location =
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

            btnNuevoInstructor.Left =
                btnCerrarVentana.Left -
                btnNuevoInstructor.Width -
                20;

            RedondearControl(
                pnlFormulario,
                18
            );

            RedondearControl(
                pnlLista,
                18
            );

            RedondearControl(
                pnlContenedorGrid,
                12
            );
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
                ruta.AddArc(
                    arco,
                    180,
                    90
                );

                arco.X =
                    control.Width -
                    diametro;

                ruta.AddArc(
                    arco,
                    270,
                    90
                );

                arco.Y =
                    control.Height -
                    diametro;

                ruta.AddArc(
                    arco,
                    0,
                    90
                );

                arco.X = 0;

                ruta.AddArc(
                    arco,
                    90,
                    90
                );

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
            Rectangle area =
                ClientRectangle;

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

                fondo.InterpolationColors =
                    mezcla;

                e.Graphics.FillRectangle(
                    fondo,
                    area
                );
            }

            DibujarCurvaInferior(
                e.Graphics
            );
        }

        private void DibujarCurvaInferior(
            Graphics graphics)
        {
            graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            int ancho =
                ClientSize.Width;

            int alto =
                ClientSize.Height;

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