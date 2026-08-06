using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmConsultaMatriculas
    {
        // =========================================================
        // VARIABLES
        // =========================================================

        private bool disenoConsultaInicializado;
        private bool formularioConsultaMostrado;
        private bool modoIntegradoSolicitado;

        private Panel pnlMenuConsulta = null!;
        private Panel pnlContenidoConsulta = null!;
        private Panel pnlEncabezadoConsulta = null!;
        private Panel pnlCuerpoConsulta = null!;

        private Panel pnlMarcaConsulta = null!;
        private Panel pnlOpcionesConsulta = null!;
        private Panel pnlPieConsulta = null!;

        private Panel pnlConsultaPrincipal = null!;
        private Panel pnlCabeceraListaConsulta = null!;
        private Panel pnlBuscadorConsulta = null!;
        private Panel pnlContenedorGridConsulta = null!;
        private Panel pnlDetalleModerno = null!;

        private Panel pnlTarjetaAlumno = null!;
        private Panel pnlTarjetaNivel = null!;
        private Panel pnlTarjetaCosto = null!;
        private Panel pnlTarjetaPagado = null!;
        private Panel pnlTarjetaPendiente = null!;
        private Panel pnlTarjetaEstado = null!;

        private PictureBox picLogoConsulta = null!;

        private Label lblMarcaConsulta = null!;
        private Label lblSubtituloMarcaConsulta = null!;

        private Label lblTituloPaginaConsulta = null!;
        private Label lblSubtituloPaginaConsulta = null!;

        private Label lblTituloListaConsulta = null!;
        private Label lblTituloDetalleConsulta = null!;
        private Label lblCantidadConsulta = null!;

        private Label lblPieConsulta = null!;
        private Label lblVersionConsulta = null!;

        private Button btnMenuDashboardConsulta = null!;
        private Button btnMenuAlumnosConsulta = null!;
        private Button btnMenuNivelesConsulta = null!;
        private Button btnMenuInstructoresConsulta = null!;
        private Button btnMenuMatriculasConsulta = null!;
        private Button btnMenuPagosConsulta = null!;
        private Button btnMenuReportesConsulta = null!;
        private Button btnMenuConsultaActual = null!;

        private Button btnCerrarConsulta = null!;

        // =========================================================
        // REDUCIR PARPADEO
        // =========================================================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parametros = base.CreateParams;
                parametros.ExStyle |= 0x02000000;
                return parametros;
            }
        }

        // =========================================================
        // PREPARAR ANTES DE MOSTRAR
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoConsultaInicializado)
            {
                disenoConsultaInicializado = true;

                SuspendLayout();

                try
                {
                    InicializarDisenoConsulta();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar la consulta:\r\n" +
                        ex.Message,
                        "Consulta de matriculas",
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

            if (value && !formularioConsultaMostrado)
            {
                formularioConsultaMostrado = true;

                BeginInvoke(new Action(() =>
                {
                    AjustarDisenoConsulta();

                    if (modoIntegradoSolicitado)
                    {
                        AplicarModoIntegrado();
                    }
                    ActualizarCantidadConsulta();
                    Invalidate(true);
                    Update();
                }));
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        private void InicializarDisenoConsulta()
        {
            ConfigurarFormularioConsulta();
            CrearEstructuraConsulta();
            CrearMenuLateralConsulta();
            CrearEncabezadoConsulta();
            CrearPanelConsultaPrincipal();
            ConfigurarBuscadorConsulta();
            ConfigurarGridConsulta();
            ConfigurarDetalleConsulta();
            ConfigurarEventosVisualesConsulta();

            AjustarDisenoConsulta();

            pnlMenuConsulta.BringToFront();
            pnlContenidoConsulta.BringToFront();

            if (modoIntegradoSolicitado)
            {
                AplicarModoIntegrado();
            }
        }

        private void ConfigurarFormularioConsulta()
        {
            Text =
                "Consulta de Matriculas y Pagos - Lexbridge";

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1120, 680);

            StartPosition =
                FormStartPosition.CenterScreen;

            FormBorderStyle =
                FormBorderStyle.Sizable;

            MaximizeBox = true;

            BackColor =
                Color.FromArgb(4, 17, 44);

            ForeColor = Color.White;

            Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Regular
            );

            AutoScaleMode =
                AutoScaleMode.Dpi;

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
        // ESTRUCTURA
        // =========================================================

        private void CrearEstructuraConsulta()
        {
            pnlMenuConsulta = new Panel
            {
                Dock = DockStyle.Left,
                Width = 235,
                Padding = new Padding(12),
                BackColor = Color.FromArgb(3, 14, 39)
            };

            pnlContenidoConsulta = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlEncabezadoConsulta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(5, 20, 52)
            };

            pnlCuerpoConsulta = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            pnlContenidoConsulta.Controls.Add(
                pnlCuerpoConsulta
            );

            pnlContenidoConsulta.Controls.Add(
                pnlEncabezadoConsulta
            );

            Controls.Add(pnlContenidoConsulta);
            Controls.Add(pnlMenuConsulta);

            pnlCuerpoConsulta.Resize +=
                (sender, e) =>
                {
                    AjustarDisenoConsulta();
                };

            Resize +=
                (sender, e) =>
                {
                    AjustarDisenoConsulta();
                };
        }

        // =========================================================
        // MENU
        // =========================================================

        private void CrearMenuLateralConsulta()
        {
            pnlMarcaConsulta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            pnlPieConsulta = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = Color.Transparent
            };

            pnlOpcionesConsulta = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlMenuConsulta.Controls.Add(
                pnlOpcionesConsulta
            );

            pnlMenuConsulta.Controls.Add(
                pnlPieConsulta
            );

            pnlMenuConsulta.Controls.Add(
                pnlMarcaConsulta
            );

            picLogoConsulta = new PictureBox
            {
                Parent = pnlMarcaConsulta,
                Location = new Point(3, 7),
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            CargarLogoConsulta();

            lblMarcaConsulta = new Label
            {
                Parent = pnlMarcaConsulta,
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

            lblSubtituloMarcaConsulta = new Label
            {
                Parent = pnlMarcaConsulta,
                Text = "ACADEMIA DE INGLES",
                Location = new Point(60, 36),
                Size = new Size(145, 19),

                ForeColor =
                    Color.FromArgb(153, 171, 201),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.8F)
            };

            btnMenuDashboardConsulta =
                CrearBotonMenuConsulta(
                    "Dashboard",
                    "\uE80F",
                    8,
                    false
                );

            btnMenuAlumnosConsulta =
                CrearBotonMenuConsulta(
                    "Alumnos",
                    "\uE77B",
                    60,
                    false
                );

            btnMenuNivelesConsulta =
                CrearBotonMenuConsulta(
                    "Niveles",
                    "\uE8EF",
                    112,
                    false
                );

            btnMenuInstructoresConsulta =
                CrearBotonMenuConsulta(
                    "Instructores",
                    "\uE716",
                    164,
                    false
                );

            btnMenuMatriculasConsulta =
                CrearBotonMenuConsulta(
                    "Matriculas",
                    "\uE787",
                    216,
                    false
                );

            btnMenuPagosConsulta =
                CrearBotonMenuConsulta(
                    "Pagos",
                    "\uE8C7",
                    268,
                    false
                );

            btnMenuReportesConsulta =
                CrearBotonMenuConsulta(
                    "Reportes",
                    "\uE9D2",
                    320,
                    false
                );

            btnMenuConsultaActual =
                CrearBotonMenuConsulta(
                    "Consulta",
                    "\uE721",
                    372,
                    true
                );

            btnMenuDashboardConsulta.Click +=
                (sender, e) =>
                {
                    VolverDashboardConsulta();
                };

            btnMenuAlumnosConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmAlumnos()
                    );
                };

            btnMenuNivelesConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmNiveles()
                    );
                };

            btnMenuInstructoresConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmInstructores()
                    );
                };

            btnMenuMatriculasConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmMatriculas()
                    );
                };

            btnMenuPagosConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmPagos()
                    );
                };

            btnMenuReportesConsulta.Click +=
                (sender, e) =>
                {
                    AbrirFormularioConsulta(
                        new frmReportes()
                    );
                };

            btnMenuConsultaActual.Click +=
                (sender, e) =>
                {
                    txtBuscar.Focus();
                };

            lblPieConsulta = new Label
            {
                Parent = pnlPieConsulta,
                Text = "(c) 2026 Lexbridge",
                Location = new Point(5, 27),
                Size = new Size(132, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            lblVersionConsulta = new Label
            {
                Parent = pnlPieConsulta,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };
        }

        private Button CrearBotonMenuConsulta(
            string texto,
            string icono,
            int top,
            bool seleccionado)
        {
            Button boton = new Button
            {
                Parent = pnlOpcionesConsulta,
                Location = new Point(0, top),

                Size = new Size(
                    Math.Max(
                        180,
                        pnlOpcionesConsulta.ClientSize.Width
                    ),
                    44
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                Text = texto,
                TextAlign =
                    ContentAlignment.MiddleLeft,

                Padding =
                    new Padding(49, 0, 0, 0),

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
                Parent = pnlOpcionesConsulta,
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
                    Parent = pnlOpcionesConsulta,
                    Location = new Point(0, top),
                    Size = new Size(4, 44),

                    BackColor =
                        Color.FromArgb(255, 181, 25)
                };

                barra.BringToFront();
            }

            lblIcono.Click +=
                (sender, e) =>
                {
                    boton.PerformClick();
                };

            RedondearConsulta(boton, 11);

            boton.BringToFront();
            lblIcono.BringToFront();

            return boton;
        }

        private void CargarLogoConsulta()
        {
            try
            {
                string[] nombres =
                {
                    "logolexbridge",
                    "logoLexbridge",
                    "LogoLexbridge",
                    "LOGOLEXBRIDGE"
                };

                foreach (string nombre in nombres)
                {
                    object? recurso =
                        Properties.Resources
                            .ResourceManager
                            .GetObject(nombre);

                    if (recurso is Image imagen)
                    {
                        picLogoConsulta.Image = imagen;
                        return;
                    }
                }

                picLogoConsulta.Image = null;
            }
            catch
            {
                picLogoConsulta.Image = null;
            }
        }

        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void CrearEncabezadoConsulta()
        {
            lblTitulo.Visible = false;

            lblTituloPaginaConsulta = new Label
            {
                Parent = pnlEncabezadoConsulta,

                Text =
                    "Consulta de Matriculas y Pagos",

                Location = new Point(32, 16),
                Size = new Size(580, 38),

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    20F,
                    FontStyle.Bold
                )
            };

            lblSubtituloPaginaConsulta = new Label
            {
                Parent = pnlEncabezadoConsulta,

                Text =
                    "Consulta el nivel y el estado financiero de cada alumno",

                Location = new Point(35, 55),
                Size = new Size(600, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            btnCerrarConsulta = new Button
            {
                Parent = pnlEncabezadoConsulta,
                Size = new Size(115, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEncabezadoConsulta.ClientSize.Width -
                    145,
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

            btnCerrarConsulta.FlatAppearance.BorderColor =
                Color.FromArgb(71, 92, 139);

            btnCerrarConsulta.FlatAppearance.BorderSize = 1;

            btnCerrarConsulta.Click +=
                (sender, e) =>
                {
                    Close();
                };

            RedondearConsulta(
                btnCerrarConsulta,
                11
            );
        }

        // =========================================================
        // PANEL PRINCIPAL
        // =========================================================

        private void CrearPanelConsultaPrincipal()
        {
            pnlConsultaPrincipal = new Panel
            {
                Parent = pnlCuerpoConsulta,
                Location = new Point(18, 18),
                Size = new Size(1090, 620),

                BackColor =
                    Color.FromArgb(10, 29, 67),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            pnlCabeceraListaConsulta = new Panel
            {
                Parent = pnlConsultaPrincipal,
                Dock = DockStyle.Top,
                Height = 70,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloListaConsulta = new Label
            {
                Parent = pnlCabeceraListaConsulta,
                Text = "Matriculas registradas",
                Location = new Point(20, 17),
                Size = new Size(300, 30),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            pnlBuscadorConsulta = new Panel
            {
                Parent = pnlCabeceraListaConsulta,

                Size = new Size(480, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlCabeceraListaConsulta.Width - 500,
                    15
                ),

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            pnlContenedorGridConsulta = new Panel
            {
                Parent = pnlConsultaPrincipal,
                Location = new Point(18, 86),

                Size = new Size(
                    pnlConsultaPrincipal.Width - 36,
                    265
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(6, 22, 54)
            };

            lblCantidadConsulta = new Label
            {
                Parent = pnlConsultaPrincipal,

                Location = new Point(20, 355),
                Size = new Size(350, 24),

                ForeColor =
                    Color.FromArgb(171, 189, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.5F)
            };

            pnlDetalleModerno = new Panel
            {
                Parent = pnlConsultaPrincipal,
                Location = new Point(18, 386),

                Size = new Size(
                    pnlConsultaPrincipal.Width - 36,
                    215
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            RedondearConsulta(
                pnlConsultaPrincipal,
                18
            );

            RedondearConsulta(
                pnlContenedorGridConsulta,
                12
            );

            RedondearConsulta(
                pnlDetalleModerno,
                14
            );
        }

        // =========================================================
        // BUSCADOR
        // =========================================================

        private void ConfigurarBuscadorConsulta()
        {
            lblBuscar.Visible = false;

            txtBuscar.Parent = pnlBuscadorConsulta;
            txtBuscar.Location = new Point(13, 9);
            txtBuscar.Size = new Size(270, 24);

            txtBuscar.BorderStyle =
                BorderStyle.None;

            txtBuscar.BackColor =
                Color.FromArgb(7, 23, 57);

            txtBuscar.ForeColor = Color.White;

            txtBuscar.Font =
                new Font("Segoe UI", 9F);

            txtBuscar.PlaceholderText =
                "Buscar alumno...";

            btnBuscar.Parent = pnlBuscadorConsulta;
            btnBuscar.Location = new Point(293, 4);
            btnBuscar.Size = new Size(85, 32);

            btnBuscar.Text = "Buscar";

            btnBuscar.BackColor =
                Color.FromArgb(16, 117, 176);

            btnBuscar.ForeColor = Color.White;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;

            btnBuscar.Font = new Font(
                "Segoe UI",
                8.3F,
                FontStyle.Bold
            );

            btnLimpiarBusqueda.Parent =
                pnlBuscadorConsulta;

            btnLimpiarBusqueda.Location =
                new Point(385, 4);

            btnLimpiarBusqueda.Size =
                new Size(90, 32);

            btnLimpiarBusqueda.Text = "Limpiar";

            btnLimpiarBusqueda.BackColor =
                Color.FromArgb(24, 43, 82);

            btnLimpiarBusqueda.ForeColor =
                Color.White;

            btnLimpiarBusqueda.FlatStyle =
                FlatStyle.Flat;

            btnLimpiarBusqueda
                .FlatAppearance.BorderSize = 0;

            btnLimpiarBusqueda.Font = new Font(
                "Segoe UI",
                8.3F,
                FontStyle.Bold
            );

            txtBuscar.KeyDown +=
                (sender, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        btnBuscar.PerformClick();
                    }

                    if (e.KeyCode == Keys.Escape)
                    {
                        btnLimpiarBusqueda.PerformClick();
                    }
                };

            RedondearConsulta(
                pnlBuscadorConsulta,
                10
            );

            RedondearConsulta(
                btnBuscar,
                8
            );

            RedondearConsulta(
                btnLimpiarBusqueda,
                8
            );
        }

        // =========================================================
        // TABLA
        // =========================================================

        private void ConfigurarGridConsulta()
        {
            dgvConsulta.Parent =
                pnlContenedorGridConsulta;

            dgvConsulta.Dock =
                DockStyle.Fill;

            dgvConsulta.BorderStyle =
                BorderStyle.None;

            dgvConsulta.BackgroundColor =
                Color.FromArgb(6, 22, 54);

            dgvConsulta.GridColor =
                Color.FromArgb(32, 56, 103);

            dgvConsulta.EnableHeadersVisualStyles =
                false;

            dgvConsulta.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvConsulta.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(15, 39, 83);

            dgvConsulta.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvConsulta.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold
                );

            dgvConsulta.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvConsulta.ColumnHeadersHeight = 42;

            dgvConsulta.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvConsulta.DefaultCellStyle.BackColor =
                Color.FromArgb(8, 27, 63);

            dgvConsulta.DefaultCellStyle.ForeColor =
                Color.FromArgb(226, 233, 245);

            dgvConsulta.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(60, 33, 112);

            dgvConsulta.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvConsulta.DefaultCellStyle.Font =
                new Font("Segoe UI", 8.4F);

            dgvConsulta.DefaultCellStyle.Padding =
                new Padding(6, 0, 6, 0);

            dgvConsulta.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(10, 31, 70);

            dgvConsulta.RowTemplate.Height = 40;
            dgvConsulta.RowHeadersVisible = false;

            dgvConsulta.AllowUserToAddRows = false;
            dgvConsulta.AllowUserToDeleteRows = false;
            dgvConsulta.AllowUserToResizeRows = false;

            dgvConsulta.ReadOnly = true;
            dgvConsulta.MultiSelect = false;

            dgvConsulta.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvConsulta.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvConsulta.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvConsulta.DataBindingComplete +=
                dgvConsulta_DataBindingCompleteExtra;
        }

        private void dgvConsulta_DataBindingCompleteExtra(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                ConfigurarColumnaConsulta(
                    "IdMatricula",
                    "ID",
                    30
                );

                ConfigurarColumnaConsulta(
                    "IdAlumno",
                    "Id alumno",
                    40
                );

                ConfigurarColumnaConsulta(
                    "IdNivel",
                    "Id nivel",
                    35
                );

                ConfigurarColumnaConsulta(
                    "IdInstructor",
                    "Id instructor",
                    45
                );

                ConfigurarColumnaConsulta(
                    "FechaMatricula",
                    "Fecha",
                    60
                );

                ConfigurarColumnaConsulta(
                    "NombreAlumno",
                    "Alumno",
                    100
                );

                ConfigurarColumnaConsulta(
                    "NombreNivel",
                    "Nivel",
                    70
                );

                ConfigurarColumnaConsulta(
                    "NombreInstructor",
                    "Instructor",
                    90
                );

                if (dgvConsulta.Columns["FechaMatricula"] != null)
                {
                    dgvConsulta.Columns["FechaMatricula"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";
                }

                dgvConsulta.ClearSelection();
                ActualizarCantidadConsulta();
            }
            catch
            {
            }
        }

        private void ConfigurarColumnaConsulta(
            string nombre,
            string encabezado,
            float peso)
        {
            if (dgvConsulta.Columns[nombre] == null)
                return;

            dgvConsulta.Columns[nombre].HeaderText =
                encabezado;

            dgvConsulta.Columns[nombre].FillWeight =
                peso;
        }

        private void ActualizarCantidadConsulta()
        {
            if (lblCantidadConsulta == null ||
                dgvConsulta == null)
            {
                return;
            }

            int cantidad =
                dgvConsulta.Rows
                    .Cast<DataGridViewRow>()
                    .Count(fila =>
                        !fila.IsNewRow &&
                        fila.Visible);

            lblCantidadConsulta.Text =
                cantidad == 1
                    ? "Mostrando 1 matricula"
                    : "Mostrando " +
                      cantidad +
                      " matriculas";
        }

        // =========================================================
        // DETALLE
        // =========================================================

        private void ConfigurarDetalleConsulta()
        {
            lblDetalle.Visible = false;
            pnlDetalle.Visible = false;

            lblTituloDetalleConsulta = new Label
            {
                Parent = pnlDetalleModerno,

                Text =
                    "Detalle financiero del alumno",

                Location = new Point(20, 12),
                Size = new Size(370, 30),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold
                )
            };

            pnlTarjetaAlumno =
                CrearTarjetaDetalle(
                    "Alumno",
                    lblValorNombreAlumno,
                    Color.FromArgb(61, 181, 255)
                );

            pnlTarjetaNivel =
                CrearTarjetaDetalle(
                    "Nivel",
                    lblValorNivel,
                    Color.FromArgb(150, 96, 255)
                );

            pnlTarjetaCosto =
                CrearTarjetaDetalle(
                    "Costo del nivel",
                    lblValorCosto,
                    Color.FromArgb(225, 232, 242)
                );

            pnlTarjetaPagado =
                CrearTarjetaDetalle(
                    "Total pagado",
                    lblValorPagado,
                    Color.FromArgb(46, 213, 115)
                );

            pnlTarjetaPendiente =
                CrearTarjetaDetalle(
                    "Saldo pendiente",
                    lblValorPendiente,
                    Color.FromArgb(255, 177, 40)
                );

            pnlTarjetaEstado =
                CrearTarjetaEstado();

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaAlumno
            );

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaNivel
            );

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaCosto
            );

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaPagado
            );

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaPendiente
            );

            pnlDetalleModerno.Controls.Add(
                pnlTarjetaEstado
            );

            OrganizarTarjetasDetalle();
        }

        private Panel CrearTarjetaDetalle(
            string titulo,
            Label valor,
            Color colorValor)
        {
            Panel tarjeta = new Panel
            {
                Size = new Size(200, 70),

                BackColor =
                    Color.FromArgb(13, 34, 73)
            };

            Label etiqueta = new Label
            {
                Parent = tarjeta,
                Text = titulo,
                Location = new Point(14, 9),
                Size = new Size(170, 20),

                ForeColor =
                    Color.FromArgb(158, 181, 213),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            valor.Parent = tarjeta;
            valor.AutoSize = false;
            valor.Location = new Point(14, 29);
            valor.Size = new Size(175, 31);

            valor.ForeColor = colorValor;
            valor.BackColor = Color.Transparent;

            valor.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );

            valor.TextAlign =
                ContentAlignment.MiddleLeft;

            tarjeta.Resize +=
                (sender, e) =>
                {
                    valor.Width =
                        tarjeta.ClientSize.Width - 28;

                    RedondearConsulta(
                        tarjeta,
                        12
                    );
                };

            RedondearConsulta(tarjeta, 12);

            return tarjeta;
        }

        private Panel CrearTarjetaEstado()
        {
            Panel tarjeta = new Panel
            {
                Size = new Size(320, 70),

                BackColor =
                    Color.FromArgb(13, 34, 73)
            };

            Label etiqueta = new Label
            {
                Parent = tarjeta,
                Text = "Estado de pago",
                Location = new Point(14, 8),
                Size = new Size(160, 20),

                ForeColor =
                    Color.FromArgb(158, 181, 213),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            picEstado.Parent = tarjeta;
            picEstado.Location = new Point(15, 36);
            picEstado.Size = new Size(17, 17);

            lblValorEstado.Parent = tarjeta;
            lblValorEstado.AutoSize = false;
            lblValorEstado.Location =
                new Point(42, 28);

            lblValorEstado.Size =
                new Size(260, 34);

            lblValorEstado.BackColor =
                Color.Transparent;

            lblValorEstado.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            lblValorEstado.TextAlign =
                ContentAlignment.MiddleLeft;

            tarjeta.Resize +=
                (sender, e) =>
                {
                    lblValorEstado.Width =
                        tarjeta.ClientSize.Width - 55;

                    RedondearConsulta(
                        tarjeta,
                        12
                    );
                };

            RedondearConsulta(tarjeta, 12);

            return tarjeta;
        }

        private void OrganizarTarjetasDetalle()
        {
            if (pnlDetalleModerno == null ||
                pnlTarjetaAlumno == null)
            {
                return;
            }

            const int margen = 20;
            const int separacion = 12;
            const int topPrimeraFila = 50;
            const int topSegundaFila = 130;

            int anchoDisponible =
                pnlDetalleModerno.ClientSize.Width -
                (margen * 2);

            int anchoPrimera =
                (anchoDisponible - separacion) / 2;

            int anchoSegunda =
                (anchoDisponible -
                 (separacion * 3)) / 4;

            pnlTarjetaAlumno.Location =
                new Point(
                    margen,
                    topPrimeraFila
                );

            pnlTarjetaAlumno.Size =
                new Size(
                    anchoPrimera,
                    66
                );

            pnlTarjetaNivel.Location =
                new Point(
                    margen +
                    anchoPrimera +
                    separacion,
                    topPrimeraFila
                );

            pnlTarjetaNivel.Size =
                new Size(
                    anchoPrimera,
                    66
                );

            pnlTarjetaCosto.Location =
                new Point(
                    margen,
                    topSegundaFila
                );

            pnlTarjetaCosto.Size =
                new Size(
                    anchoSegunda,
                    66
                );

            pnlTarjetaPagado.Location =
                new Point(
                    margen +
                    anchoSegunda +
                    separacion,
                    topSegundaFila
                );

            pnlTarjetaPagado.Size =
                new Size(
                    anchoSegunda,
                    66
                );

            pnlTarjetaPendiente.Location =
                new Point(
                    margen +
                    ((anchoSegunda + separacion) * 2),
                    topSegundaFila
                );

            pnlTarjetaPendiente.Size =
                new Size(
                    anchoSegunda,
                    66
                );

            pnlTarjetaEstado.Location =
                new Point(
                    margen +
                    ((anchoSegunda + separacion) * 3),
                    topSegundaFila
                );

            pnlTarjetaEstado.Size =
                new Size(
                    anchoSegunda,
                    66
                );
        }

        // =========================================================
        // EVENTOS
        // =========================================================

        private void ConfigurarEventosVisualesConsulta()
        {
            dgvConsulta.DataSourceChanged +=
                (sender, e) =>
                {
                    ActualizarCantidadConsulta();
                };

            pnlConsultaPrincipal.Resize +=
                (sender, e) =>
                {
                    pnlBuscadorConsulta.Left =
                        pnlCabeceraListaConsulta.ClientSize.Width -
                        pnlBuscadorConsulta.Width -
                        20;

                    pnlContenedorGridConsulta.Width =
                        pnlConsultaPrincipal.ClientSize.Width -
                        36;

                    pnlDetalleModerno.Width =
                        pnlConsultaPrincipal.ClientSize.Width -
                        36;

                    OrganizarTarjetasDetalle();

                    RedondearConsulta(
                        pnlConsultaPrincipal,
                        18
                    );
                };

            pnlEncabezadoConsulta.Resize +=
                (sender, e) =>
                {
                    btnCerrarConsulta.Left =
                        pnlEncabezadoConsulta.ClientSize.Width -
                        btnCerrarConsulta.Width -
                        20;
                };

            txtBuscar.Enter +=
                (sender, e) =>
                {
                    txtBuscar.BackColor =
                        Color.FromArgb(10, 31, 72);
                };

            txtBuscar.Leave +=
                (sender, e) =>
                {
                    txtBuscar.BackColor =
                        Color.FromArgb(7, 23, 57);
                };
        }

        // =========================================================
        // DIMENSIONES
        // =========================================================

        private void AjustarDisenoConsulta()
        {
            if (!disenoConsultaInicializado ||
                pnlCuerpoConsulta == null ||
                pnlConsultaPrincipal == null)
            {
                return;
            }

            const int margen = 18;
            const int anchoMinimo = 820;
            const int altoMinimo = 620;

            int anchoDisponible =
                pnlCuerpoConsulta.ClientSize.Width -
                (margen * 2);

            int altoDisponible =
                pnlCuerpoConsulta.ClientSize.Height -
                (margen * 2);

            int anchoPanel =
                Math.Max(
                    anchoMinimo,
                    anchoDisponible
                );

            int altoPanel =
                Math.Max(
                    altoMinimo,
                    altoDisponible
                );

            pnlConsultaPrincipal.Location =
                new Point(margen, margen);

            pnlConsultaPrincipal.Size =
                new Size(
                    anchoPanel,
                    altoPanel
                );

            pnlCuerpoConsulta.AutoScrollMinSize =
                new Size(
                    anchoPanel + (margen * 2),
                    altoPanel + (margen * 2)
                );

            int alturaGrid =
                Math.Max(
                    220,
                    (altoPanel - 150) / 2
                );

            pnlContenedorGridConsulta.Location =
                new Point(18, 86);

            pnlContenedorGridConsulta.Size =
                new Size(
                    anchoPanel - 36,
                    alturaGrid
                );

            lblCantidadConsulta.Location =
                new Point(
                    20,
                    pnlContenedorGridConsulta.Bottom + 5
                );

            pnlDetalleModerno.Location =
                new Point(
                    18,
                    lblCantidadConsulta.Bottom + 7
                );

            pnlDetalleModerno.Size =
                new Size(
                    anchoPanel - 36,
                    Math.Max(
                        215,
                        altoPanel -
                        lblCantidadConsulta.Bottom -
                        25
                    )
                );

            pnlBuscadorConsulta.Left =
                pnlCabeceraListaConsulta.ClientSize.Width -
                pnlBuscadorConsulta.Width -
                20;

            btnCerrarConsulta.Left =
                pnlEncabezadoConsulta.ClientSize.Width -
                btnCerrarConsulta.Width -
                20;

            OrganizarTarjetasDetalle();

            RedondearConsulta(
                pnlConsultaPrincipal,
                18
            );

            RedondearConsulta(
                pnlContenedorGridConsulta,
                12
            );

            RedondearConsulta(
                pnlDetalleModerno,
                14
            );
        }

        // =========================================================
        // NAVEGACION
        // =========================================================

        private void VolverDashboardConsulta()
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

            frmPrincipal nuevo =
                new frmPrincipal();

            nuevo.Show();
            Close();
        }

        private void AbrirFormularioConsulta(
            Form formulario)
        {
            try
            {
                Form? abierto =
                    Application.OpenForms
                        .Cast<Form>()
                        .FirstOrDefault(
                            f => f.GetType() ==
                                 formulario.GetType()
                        );

                if (abierto != null)
                {
                    formulario.Dispose();

                    abierto.WindowState =
                        FormWindowState.Normal;

                    abierto.BringToFront();
                    abierto.Activate();

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
        // REDONDEAR
        // =========================================================

        private static void RedondearConsulta(
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

            using GraphicsPath ruta =
                new GraphicsPath();

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

            Region? anterior =
                control.Region;

            control.Region =
                new Region(ruta);

            anterior?.Dispose();
        }

        // =========================================================
        // FONDO
        // =========================================================

        protected override void OnPaintBackground(
            PaintEventArgs e)
        {
            Rectangle area = ClientRectangle;

            using LinearGradientBrush fondo =
                new LinearGradientBrush(
                    area,
                    Color.FromArgb(3, 16, 43),
                    Color.FromArgb(57, 20, 107),
                    0F
                );

            ColorBlend mezcla = new ColorBlend
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

                if (pnlMenuConsulta != null)
                {
                    pnlMenuConsulta.Visible = false;
                    pnlMenuConsulta.Dock = DockStyle.None;
                    pnlMenuConsulta.Width = 0;
                }

                if (pnlEncabezadoConsulta != null)
                {
                    pnlEncabezadoConsulta.Visible = false;
                    pnlEncabezadoConsulta.Dock = DockStyle.None;
                    pnlEncabezadoConsulta.Height = 0;
                }

                if (pnlContenidoConsulta != null)
                {
                    pnlContenidoConsulta.Visible = true;
                    pnlContenidoConsulta.Dock = DockStyle.Fill;
                    pnlContenidoConsulta.Location = Point.Empty;
                    pnlContenidoConsulta.Margin = new Padding(0);
                    pnlContenidoConsulta.Padding = new Padding(0);
                    pnlContenidoConsulta.BringToFront();
                }

                if (pnlCuerpoConsulta != null)
                {
                    pnlCuerpoConsulta.Visible = true;
                    pnlCuerpoConsulta.Dock = DockStyle.Fill;
                    pnlCuerpoConsulta.Location = Point.Empty;
                    pnlCuerpoConsulta.Margin = new Padding(0);
                }

                PerformLayout();

                if (pnlCuerpoConsulta != null)
                {
                    AjustarDisenoConsulta();
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