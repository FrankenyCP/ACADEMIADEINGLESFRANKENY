using CAPA_DATOS;
using CAPA_NEGOCIOS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmPagos
    {
        // =========================================================
        // VARIABLES DEL DISENO
        // =========================================================

        private bool disenoPagosInicializado;
        private bool formularioPagosMostrado;
        private bool modoIntegradoSolicitado;

        private Panel pnlMenuPagos = null!;
        private Panel pnlContenidoPagos = null!;
        private Panel pnlEncabezadoPagos = null!;
        private Panel pnlCuerpoPagos = null!;

        private Panel pnlMarcaPagos = null!;
        private Panel pnlOpcionesPagos = null!;
        private Panel pnlPiePagos = null!;

        private Panel pnlFormularioPago = null!;
        private Panel pnlListaPagos = null!;

        private Panel pnlCabeceraFormularioPago = null!;
        private Panel pnlCabeceraListaPagos = null!;

        private Panel pnlAccionesPago = null!;
        private Panel pnlHerramientasPago = null!;
        private Panel pnlContenedorGridPagos = null!;
        private Panel pnlBuscarPagos = null!;

        private Panel pnlFondoBanco = null!;
        private Panel pnlTarjetaBanco = null!;

        private PictureBox picLogoPagos = null!;

        private Label lblMarcaPagos = null!;
        private Label lblMarcaSubtituloPagos = null!;

        private Label lblTituloPaginaPagos = null!;
        private Label lblSubtituloPaginaPagos = null!;

        private Label lblTituloFormularioPago = null!;
        private Label lblTituloListaPagos = null!;

        private Label lblCantidadPagos = null!;
        private Label lblTotalRecaudado = null!;

        private Label lblPiePagos = null!;
        private Label lblVersionPagos = null!;

        private Label lblBuscarPagos = null!;
        private TextBox txtBuscarPagos = null!;
        private Button btnLimpiarBusquedaPagos = null!;

        private Button btnMenuDashboardPagos = null!;
        private Button btnMenuAlumnosPagos = null!;
        private Button btnMenuNivelesPagos = null!;
        private Button btnMenuInstructoresPagos = null!;
        private Button btnMenuMatriculasPagos = null!;
        private Button btnMenuPagosActual = null!;
        private Button btnMenuReportesPagos = null!;
        private Button btnMenuConsultaPagos = null!;

        private Button btnCerrarFormularioPagos = null!;
        private Button btnMostrarDatosBanco = null!;

        private readonly ServicioCorreo servicioCorreoPago =
            new ServicioCorreo();

        private readonly AlumnoCD alumnoCDCorreoPago =
            new AlumnoCD();

        private bool correoPagoConfigurado;
        private bool procesandoCorreoPago;

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
        // INICIALIZACION ANTES DE MOSTRAR
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoPagosInicializado)
            {
                disenoPagosInicializado = true;

                SuspendLayout();

                try
                {
                    InicializarDisenoPagos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar el diseno de pagos:\r\n" +
                        ex.Message,
                        "Gestion de Pagos",
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

            if (value && !formularioPagosMostrado)
            {
                formularioPagosMostrado = true;

                BeginInvoke(new Action(() =>
                {
                    AjustarDisenoPagos();

                    if (modoIntegradoSolicitado)
                    {
                        AplicarModoIntegrado();
                    }
                    ActualizarResumenPagos();
                    Invalidate(true);
                    Update();
                }));
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        private void InicializarDisenoPagos()
        {
            ConfigurarFormularioPagos();
            CrearEstructuraPagos();
            CrearMenuLateralPagos();
            CrearEncabezadoPagos();
            CrearPanelFormularioPago();
            CrearPanelListaPagos();

            ConfigurarCamposPago();
            ConfigurarBotonesPago();
            ConfigurarCorreoConfirmacionPago();
            ConfigurarHerramientasPagos();
            ConfigurarBuscadorPagos();
            ConfigurarGridPagos();
            ConfigurarPanelBancarioModerno();
            ConfigurarEventosVisualesPagos();

            AjustarDisenoPagos();

            pnlMenuPagos.BringToFront();
            pnlContenidoPagos.BringToFront();

            if (modoIntegradoSolicitado)
            {
                AplicarModoIntegrado();
            }
        }

        private void ConfigurarFormularioPagos()
        {
            Text = "Gestion de Pagos - Lexbridge";

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1180, 720);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;

            BackColor = Color.FromArgb(4, 17, 44);
            ForeColor = Color.White;

            Font = new Font(
                "Segoe UI",
                9F,
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
        // ESTRUCTURA
        // =========================================================

        private void CrearEstructuraPagos()
        {
            pnlMenuPagos = new Panel
            {
                Dock = DockStyle.Left,
                Width = 235,
                Padding = new Padding(12),
                BackColor = Color.FromArgb(3, 14, 39)
            };

            pnlContenidoPagos = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlEncabezadoPagos = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(5, 20, 52)
            };

            pnlCuerpoPagos = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            pnlContenidoPagos.Controls.Add(pnlCuerpoPagos);
            pnlContenidoPagos.Controls.Add(pnlEncabezadoPagos);

            Controls.Add(pnlContenidoPagos);
            Controls.Add(pnlMenuPagos);

            pnlCuerpoPagos.Resize += (sender, e) =>
            {
                AjustarDisenoPagos();
            };

            Resize += (sender, e) =>
            {
                AjustarDisenoPagos();
            };
        }

        // =========================================================
        // MENU
        // =========================================================

        private void CrearMenuLateralPagos()
        {
            pnlMarcaPagos = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            pnlPiePagos = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = Color.Transparent
            };

            pnlOpcionesPagos = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlMenuPagos.Controls.Add(pnlOpcionesPagos);
            pnlMenuPagos.Controls.Add(pnlPiePagos);
            pnlMenuPagos.Controls.Add(pnlMarcaPagos);

            picLogoPagos = new PictureBox
            {
                Parent = pnlMarcaPagos,
                Location = new Point(3, 7),
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            CargarLogoPagos();

            lblMarcaPagos = new Label
            {
                Parent = pnlMarcaPagos,
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

            lblMarcaSubtituloPagos = new Label
            {
                Parent = pnlMarcaPagos,
                Text = "ACADEMIA DE INGLES",
                Location = new Point(60, 36),
                Size = new Size(145, 19),

                ForeColor =
                    Color.FromArgb(153, 171, 201),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.8F)
            };

            btnMenuDashboardPagos = CrearBotonMenuPagos(
                "Dashboard",
                "\uE80F",
                8,
                false
            );

            btnMenuAlumnosPagos = CrearBotonMenuPagos(
                "Alumnos",
                "\uE77B",
                60,
                false
            );

            btnMenuNivelesPagos = CrearBotonMenuPagos(
                "Niveles",
                "\uE8EF",
                112,
                false
            );

            btnMenuInstructoresPagos = CrearBotonMenuPagos(
                "Instructores",
                "\uE716",
                164,
                false
            );

            btnMenuMatriculasPagos = CrearBotonMenuPagos(
                "Matriculas",
                "\uE787",
                216,
                false
            );

            btnMenuPagosActual = CrearBotonMenuPagos(
                "Pagos",
                "\uE8C7",
                268,
                true
            );

            btnMenuReportesPagos = CrearBotonMenuPagos(
                "Reportes",
                "\uE9D2",
                320,
                false
            );

            btnMenuConsultaPagos = CrearBotonMenuPagos(
                "Consulta",
                "\uE721",
                372,
                false
            );

            btnMenuDashboardPagos.Click += (sender, e) =>
            {
                VolverDashboardPagos();
            };

            btnMenuAlumnosPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(new frmAlumnos());
            };

            btnMenuNivelesPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(new frmNiveles());
            };

            btnMenuInstructoresPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(new frmInstructores());
            };

            btnMenuMatriculasPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(new frmMatriculas());
            };

            btnMenuPagosActual.Click += (sender, e) =>
            {
                txtMonto.Focus();
            };

            btnMenuReportesPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(new frmReportes());
            };

            btnMenuConsultaPagos.Click += (sender, e) =>
            {
                AbrirFormularioPagos(
                    new frmConsultaMatriculas()
                );
            };

            lblPiePagos = new Label
            {
                Parent = pnlPiePagos,
                Text = "(c) 2026 Lexbridge",
                Location = new Point(5, 27),
                Size = new Size(132, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            lblVersionPagos = new Label
            {
                Parent = pnlPiePagos,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };
        }

        private Button CrearBotonMenuPagos(
            string texto,
            string icono,
            int top,
            bool seleccionado)
        {
            Button boton = new Button
            {
                Parent = pnlOpcionesPagos,
                Location = new Point(0, top),

                Size = new Size(
                    Math.Max(
                        180,
                        pnlOpcionesPagos.ClientSize.Width
                    ),
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
                Parent = pnlOpcionesPagos,
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
                    Parent = pnlOpcionesPagos,
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

            RedondearControlPagos(boton, 11);

            boton.BringToFront();
            lblIcono.BringToFront();

            return boton;
        }

        private void CargarLogoPagos()
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
                        picLogoPagos.Image = imagen;
                        return;
                    }
                }

                picLogoPagos.Image = null;
            }
            catch
            {
                picLogoPagos.Image = null;
            }
        }

        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void CrearEncabezadoPagos()
        {
            lblTitulo.Visible = false;

            lblTituloPaginaPagos = new Label
            {
                Parent = pnlEncabezadoPagos,
                Text = "Gestion de Pagos",
                Location = new Point(32, 16),
                Size = new Size(500, 38),
                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    20F,
                    FontStyle.Bold
                )
            };

            lblSubtituloPaginaPagos = new Label
            {
                Parent = pnlEncabezadoPagos,

                Text =
                    "Registra pagos, consulta saldos y genera comprobantes",

                Location = new Point(35, 55),
                Size = new Size(550, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            btnCerrarFormularioPagos = new Button
            {
                Parent = pnlEncabezadoPagos,
                Size = new Size(115, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEncabezadoPagos.ClientSize.Width - 145,
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

            btnCerrarFormularioPagos
                .FlatAppearance.BorderColor =
                    Color.FromArgb(71, 92, 139);

            btnCerrarFormularioPagos
                .FlatAppearance.BorderSize = 1;

            btnCerrarFormularioPagos.Click +=
                (sender, e) =>
                {
                    Close();
                };

            RedondearControlPagos(
                btnCerrarFormularioPagos,
                11
            );
        }

        // =========================================================
        // PANEL IZQUIERDO
        // =========================================================

        private void CrearPanelFormularioPago()
        {
            pnlFormularioPago = new Panel
            {
                Parent = pnlCuerpoPagos,
                Location = new Point(18, 18),
                Size = new Size(360, 650),

                BackColor =
                    Color.FromArgb(10, 29, 67),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left
            };

            pnlCabeceraFormularioPago = new Panel
            {
                Parent = pnlFormularioPago,
                Dock = DockStyle.Top,
                Height = 56,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloFormularioPago = new Label
            {
                Parent = pnlCabeceraFormularioPago,
                Text = "Informacion del pago",
                Location = new Point(20, 15),
                Size = new Size(310, 28),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            pnlAccionesPago = new Panel
            {
                Parent = pnlFormularioPago,
                Location = new Point(20, 475),
                Size = new Size(320, 155),
                BackColor = Color.Transparent
            };

            RedondearControlPagos(
                pnlFormularioPago,
                18
            );
        }

        private void ConfigurarCamposPago()
        {
            ConfigurarEtiquetaPago(
                lblMatricula,
                "Matricula *",
                new Point(20, 75)
            );

            ConfigurarComboPago(
                cmbMatricula,
                new Point(20, 103)
            );

            ConfigurarEtiquetaPago(
                lblFechaPago,
                "Fecha de pago *",
                new Point(20, 157)
            );

            dtpFechaPago.Parent = pnlFormularioPago;
            dtpFechaPago.Location = new Point(20, 185);
            dtpFechaPago.Size = new Size(320, 34);

            dtpFechaPago.Font =
                new Font("Segoe UI", 9.5F);

            dtpFechaPago.Format =
                DateTimePickerFormat.Short;

            ConfigurarEtiquetaPago(
                lblMonto,
                "Monto *",
                new Point(20, 239)
            );

            txtMonto.Parent = pnlFormularioPago;
            txtMonto.Location = new Point(20, 267);
            txtMonto.Size = new Size(320, 34);

            txtMonto.Font =
                new Font("Segoe UI", 9.5F);

            txtMonto.BackColor =
                Color.FromArgb(7, 23, 57);

            txtMonto.ForeColor = Color.White;

            txtMonto.BorderStyle =
                BorderStyle.FixedSingle;

            txtMonto.PlaceholderText =
                "Ej. 2500.00";

            ConfigurarEtiquetaPago(
                lblMetodoPago,
                "Metodo de pago *",
                new Point(20, 321)
            );

            ConfigurarComboPago(
                cmbMetodoPago,
                new Point(20, 349)
            );

            lblSaldo.Parent = pnlFormularioPago;
            lblSaldo.AutoSize = false;
            lblSaldo.Location = new Point(20, 399);
            lblSaldo.Size = new Size(320, 43);

            lblSaldo.ForeColor =
                Color.FromArgb(67, 222, 163);

            lblSaldo.BackColor =
                Color.Transparent;

            lblSaldo.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold
            );

            lblSaldo.TextAlign =
                ContentAlignment.MiddleLeft;

            lblMensaje.Parent = pnlFormularioPago;
            lblMensaje.AutoSize = false;
            lblMensaje.Location = new Point(20, 441);
            lblMensaje.Size = new Size(320, 34);

            lblMensaje.BackColor =
                Color.Transparent;

            lblMensaje.Font =
                new Font("Segoe UI", 8F);

            lblMensaje.TextAlign =
                ContentAlignment.MiddleLeft;
        }

        private void ConfigurarEtiquetaPago(
            Label etiqueta,
            string texto,
            Point posicion)
        {
            etiqueta.Parent = pnlFormularioPago;
            etiqueta.AutoSize = false;
            etiqueta.Location = posicion;
            etiqueta.Size = new Size(320, 23);

            etiqueta.Text = texto;
            etiqueta.ForeColor = Color.White;
            etiqueta.BackColor = Color.Transparent;

            etiqueta.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );
        }

        private void ConfigurarComboPago(
            ComboBox combo,
            Point posicion)
        {
            combo.Parent = pnlFormularioPago;
            combo.Location = posicion;
            combo.Size = new Size(320, 34);

            combo.Font =
                new Font("Segoe UI", 9.5F);

            combo.BackColor =
                Color.FromArgb(7, 23, 57);

            combo.ForeColor = Color.White;

            combo.DropDownStyle =
                ComboBoxStyle.DropDownList;

            combo.FlatStyle = FlatStyle.Flat;
        }

        // =========================================================
        // BOTONES DEL FORMULARIO
        // =========================================================

        private void ConfigurarBotonesPago()
        {
            btnGuardar.Parent = pnlAccionesPago;
            btnLimpiar.Parent = pnlAccionesPago;
            btnEliminar.Parent = pnlAccionesPago;

            ConfigurarBotonAccionPago(
                btnGuardar,
                "Guardar pago",
                new Point(0, 0),
                new Size(320, 43),
                Color.FromArgb(10, 146, 153),
                Color.FromArgb(27, 207, 190)
            );

            ConfigurarBotonAccionPago(
                btnLimpiar,
                "Limpiar",
                new Point(0, 53),
                new Size(155, 40),
                Color.FromArgb(25, 43, 82),
                Color.FromArgb(78, 102, 155)
            );

            ConfigurarBotonAccionPago(
                btnEliminar,
                "Eliminar",
                new Point(165, 53),
                new Size(155, 40),
                Color.FromArgb(190, 15, 65),
                Color.FromArgb(235, 42, 92)
            );

            btnMostrarDatosBanco = new Button
            {
                Parent = pnlAccionesPago,
                Location = new Point(0, 103),
                Size = new Size(320, 40),

                Text = "Ver datos bancarios",

                BackColor =
                    Color.FromArgb(55, 39, 116),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8.8F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnMostrarDatosBanco.FlatAppearance.BorderColor =
                Color.FromArgb(118, 89, 210);

            btnMostrarDatosBanco.FlatAppearance.BorderSize = 1;

            btnMostrarDatosBanco.Click +=
                (sender, e) =>
                {
                    MostrarPanelBancoPagos();
                };

            RedondearControlPagos(
                btnMostrarDatosBanco,
                11
            );
        }

        private void ConfigurarBotonAccionPago(
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

            boton.Padding = new Padding(0);

            boton.TextAlign =
                ContentAlignment.MiddleCenter;

            boton.BackColor = fondo;
            boton.ForeColor = Color.White;

            boton.FlatStyle = FlatStyle.Flat;

            boton.FlatAppearance.BorderColor = borde;
            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                AclararColorPagos(fondo, 18);

            boton.FlatAppearance.MouseDownBackColor =
                OscurecerColorPagos(fondo, 15);

            boton.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            boton.Cursor = Cursors.Hand;

            RedondearControlPagos(boton, 11);
        }

        // =========================================================
        // PANEL DERECHO
        // =========================================================

        private void CrearPanelListaPagos()
        {
            pnlListaPagos = new Panel
            {
                Parent = pnlCuerpoPagos,
                Location = new Point(396, 18),
                Size = new Size(760, 650),

                BackColor =
                    Color.FromArgb(10, 29, 67),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            pnlCabeceraListaPagos = new Panel
            {
                Parent = pnlListaPagos,
                Dock = DockStyle.Top,
                Height = 112,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloListaPagos = new Label
            {
                Parent = pnlCabeceraListaPagos,
                Text = "Pagos registrados",
                Location = new Point(20, 15),
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

            pnlBuscarPagos = new Panel
            {
                Parent = pnlCabeceraListaPagos,
                Size = new Size(310, 36),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlCabeceraListaPagos.Width - 328,
                    13
                ),

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            pnlHerramientasPago = new Panel
            {
                Parent = pnlCabeceraListaPagos,
                Location = new Point(20, 59),

                Size = new Size(
                    pnlCabeceraListaPagos.Width - 40,
                    41
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor = Color.Transparent
            };

            pnlContenedorGridPagos = new Panel
            {
                Parent = pnlListaPagos,
                Location = new Point(16, 126),

                Size = new Size(
                    pnlListaPagos.Width - 32,
                    pnlListaPagos.Height - 178
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(6, 22, 54)
            };

            lblCantidadPagos = new Label
            {
                Parent = pnlListaPagos,
                AutoSize = false,

                Location = new Point(
                    20,
                    pnlListaPagos.Height - 41
                ),

                Size = new Size(260, 25),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Left,

                ForeColor =
                    Color.FromArgb(175, 191, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.5F)
            };

            lblTotalRecaudado = new Label
            {
                Parent = pnlListaPagos,
                AutoSize = false,

                Location = new Point(
                    pnlListaPagos.Width - 340,
                    pnlListaPagos.Height - 41
                ),

                Size = new Size(320, 25),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Right,

                ForeColor =
                    Color.FromArgb(67, 222, 163),

                BackColor = Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleRight,

                Font = new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold
                )
            };

            RedondearControlPagos(
                pnlListaPagos,
                18
            );

            RedondearControlPagos(
                pnlContenedorGridPagos,
                12
            );
        }

        // =========================================================
        // EXPORTACIONES
        // =========================================================

        private void ConfigurarHerramientasPagos()
        {
            btn_ExportarPDF.Parent = pnlHerramientasPago;
            btn_ExportarExcelfrmPagos.Parent =
                pnlHerramientasPago;

            ConfigurarBotonHerramientaPago(
                btn_ExportarPDF,
                "Exportar PDF",
                new Point(0, 0),
                new Size(145, 38),
                Color.FromArgb(154, 37, 69)
            );

            ConfigurarBotonHerramientaPago(
                btn_ExportarExcelfrmPagos,
                "Exportar Excel",
                new Point(155, 0),
                new Size(145, 38),
                Color.FromArgb(19, 118, 73)
            );
        }

        private void ConfigurarBotonHerramientaPago(
            Button boton,
            string texto,
            Point posicion,
            Size tamano,
            Color fondo)
        {
            boton.Location = posicion;
            boton.Size = tamano;
            boton.Text = texto;

            boton.BackColor = fondo;
            boton.ForeColor = Color.White;

            boton.FlatStyle = FlatStyle.Flat;

            boton.FlatAppearance.BorderColor =
                AclararColorPagos(fondo, 28);

            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                AclararColorPagos(fondo, 15);

            boton.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold
            );

            boton.Cursor = Cursors.Hand;

            RedondearControlPagos(
                boton,
                10
            );
        }

        // =========================================================
        // BUSCADOR VISUAL
        // =========================================================

        private void ConfigurarBuscadorPagos()
        {
            lblBuscarPagos = new Label
            {
                Parent = pnlBuscarPagos,
                Text = "\uE721",
                Location = new Point(8, 0),
                Size = new Size(32, 36),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                ForeColor =
                    Color.FromArgb(168, 190, 225),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe MDL2 Assets",
                    10F
                )
            };

            txtBuscarPagos = new TextBox
            {
                Parent = pnlBuscarPagos,
                Location = new Point(41, 7),
                Size = new Size(220, 24),

                BorderStyle =
                    BorderStyle.None,

                BackColor =
                    Color.FromArgb(7, 23, 57),

                ForeColor = Color.White,

                Font =
                    new Font("Segoe UI", 8.8F),

                PlaceholderText =
                    "Buscar en la tabla..."
            };

            btnLimpiarBusquedaPagos = new Button
            {
                Parent = pnlBuscarPagos,
                Location = new Point(267, 3),
                Size = new Size(39, 30),

                Text = "X",

                BackColor =
                    Color.FromArgb(24, 43, 82),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnLimpiarBusquedaPagos
                .FlatAppearance.BorderSize = 0;

            txtBuscarPagos.TextChanged +=
                (sender, e) =>
                {
                    FiltrarPagosVisualmente();
                };

            btnLimpiarBusquedaPagos.Click +=
                (sender, e) =>
                {
                    txtBuscarPagos.Clear();
                };

            RedondearControlPagos(
                pnlBuscarPagos,
                10
            );

            RedondearControlPagos(
                btnLimpiarBusquedaPagos,
                8
            );
        }

        private void FiltrarPagosVisualmente()
        {
            string filtro =
                txtBuscarPagos.Text
                    .Trim()
                    .ToLowerInvariant();

            CurrencyManager? administrador = null;

            try
            {
                administrador =
                    BindingContext[dgvPagos.DataSource]
                    as CurrencyManager;

                administrador?.SuspendBinding();

                foreach (DataGridViewRow fila in dgvPagos.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    bool visible =
                        string.IsNullOrWhiteSpace(filtro) ||
                        fila.Cells
                            .Cast<DataGridViewCell>()
                            .Any(celda =>
                                Convert.ToString(celda.Value)?
                                    .ToLowerInvariant()
                                    .Contains(filtro) == true);

                    fila.Visible = visible;
                }
            }
            catch
            {
                // Algunas fuentes enlazadas no permiten ocultar filas.
            }
            finally
            {
                administrador?.ResumeBinding();
                ActualizarResumenPagos();
            }
        }

        // =========================================================
        // DATAGRIDVIEW
        // =========================================================

        private void ConfigurarGridPagos()
        {
            dgvPagos.Parent =
                pnlContenedorGridPagos;

            dgvPagos.Dock = DockStyle.Fill;

            dgvPagos.BorderStyle =
                BorderStyle.None;

            dgvPagos.BackgroundColor =
                Color.FromArgb(6, 22, 54);

            dgvPagos.GridColor =
                Color.FromArgb(32, 56, 103);

            dgvPagos.EnableHeadersVisualStyles =
                false;

            dgvPagos.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvPagos.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(15, 39, 83);

            dgvPagos.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvPagos.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold
                );

            dgvPagos.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvPagos.ColumnHeadersHeight = 42;

            dgvPagos.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvPagos.DefaultCellStyle.BackColor =
                Color.FromArgb(8, 27, 63);

            dgvPagos.DefaultCellStyle.ForeColor =
                Color.FromArgb(226, 233, 245);

            dgvPagos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(60, 33, 112);

            dgvPagos.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvPagos.DefaultCellStyle.Font =
                new Font("Segoe UI", 8.5F);

            dgvPagos.DefaultCellStyle.Padding =
                new Padding(6, 0, 6, 0);

            dgvPagos.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(10, 31, 70);

            dgvPagos.RowTemplate.Height = 40;
            dgvPagos.RowHeadersVisible = false;

            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.AllowUserToDeleteRows = false;
            dgvPagos.AllowUserToResizeRows = false;

            dgvPagos.MultiSelect = false;
            dgvPagos.ReadOnly = true;

            dgvPagos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPagos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvPagos.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvPagos.DataBindingComplete +=
                dgvPagos_DataBindingCompleteExtra;
        }

        private void dgvPagos_DataBindingCompleteExtra(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                ConfigurarColumnaPago(
                    "IdPago",
                    "ID",
                    35
                );

                ConfigurarColumnaPago(
                    "IdMatricula",
                    "Matricula",
                    45
                );

                ConfigurarColumnaPago(
                    "FechaPago",
                    "Fecha",
                    60
                );

                ConfigurarColumnaPago(
                    "Monto",
                    "Monto",
                    65
                );

                ConfigurarColumnaPago(
                    "MetodoPago",
                    "Metodo",
                    65
                );

                ConfigurarColumnaPago(
                    "InfoMatricula",
                    "Alumno y nivel",
                    130
                );

                if (dgvPagos.Columns["FechaPago"] != null)
                {
                    dgvPagos.Columns["FechaPago"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";
                }

                if (dgvPagos.Columns["Monto"] != null)
                {
                    dgvPagos.Columns["Monto"]
                        .DefaultCellStyle.Format =
                            "N2";

                    dgvPagos.Columns["Monto"]
                        .DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleRight;
                }

                dgvPagos.ClearSelection();
                ActualizarResumenPagos();
            }
            catch
            {
                // No interfiere con la logica original.
            }
        }

        private void ConfigurarColumnaPago(
            string nombre,
            string encabezado,
            float peso)
        {
            if (dgvPagos.Columns[nombre] == null)
                return;

            dgvPagos.Columns[nombre].HeaderText =
                encabezado;

            dgvPagos.Columns[nombre].FillWeight =
                peso;
        }

        private void ActualizarResumenPagos()
        {
            if (lblCantidadPagos == null ||
                lblTotalRecaudado == null ||
                dgvPagos == null)
            {
                return;
            }

            int cantidad = 0;
            decimal total = 0m;

            foreach (DataGridViewRow fila in dgvPagos.Rows)
            {
                if (fila.IsNewRow || !fila.Visible)
                    continue;

                cantidad++;

                object? valorMonto = null;

                if (dgvPagos.Columns["Monto"] != null)
                {
                    valorMonto =
                        fila.Cells["Monto"].Value;
                }

                if (valorMonto != null &&
                    decimal.TryParse(
                        Convert.ToString(valorMonto),
                        NumberStyles.Any,
                        CultureInfo.CurrentCulture,
                        out decimal monto))
                {
                    total += monto;
                }
            }

            lblCantidadPagos.Text =
                cantidad == 1
                    ? "Mostrando 1 pago"
                    : "Mostrando " +
                      cantidad +
                      " pagos";

            lblTotalRecaudado.Text =
                "Total mostrado: RD$" +
                total.ToString("N2");
        }

        // =========================================================
        // PANEL BANCARIO
        // =========================================================

        private void ConfigurarPanelBancarioModerno()
        {
            pnlDatosBancarios.Visible = false;

            pnlFondoBanco = new Panel
            {
                Parent = pnlContenidoPagos,
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.FromArgb(4, 12, 32)
            };

            pnlTarjetaBanco = new Panel
            {
                Parent = pnlFondoBanco,
                Size = new Size(620, 430),

                BackColor =
                    Color.FromArgb(12, 32, 72)
            };

            label1.Parent = pnlTarjetaBanco;
            label1.AutoSize = false;
            label1.Location = new Point(35, 28);
            label1.Size = new Size(550, 48);

            label1.Text = "Datos Bancarios";

            label1.ForeColor =
                Color.FromArgb(255, 187, 31);

            label1.BackColor =
                Color.Transparent;

            label1.Font = new Font(
                "Segoe UI",
                20F,
                FontStyle.Bold
            );

            label1.TextAlign =
                ContentAlignment.MiddleCenter;

            lblBanco.Parent = pnlTarjetaBanco;
            lblBanco.AutoSize = false;
            lblBanco.Location = new Point(35, 95);
            lblBanco.Size = new Size(550, 34);

            lblBanco.Text =
                "Banco Popular Dominicano";

            lblBanco.TextAlign =
                ContentAlignment.MiddleCenter;

            lblBanco.ForeColor = Color.White;
            lblBanco.BackColor = Color.Transparent;

            lblBanco.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold
            );

            ConfigurarLineaBanco(
                lblCuenta,
                label2,
                "Numero de cuenta:",
                label2.Text,
                155
            );

            ConfigurarLineaBanco(
                lblBeneficiario,
                label3,
                "Beneficiario:",
                label3.Text,
                220
            );

            ConfigurarLineaBanco(
                lblDocumento,
                label4,
                "Documento:",
                label4.Text,
                285
            );

            btnCerrarPanel.Parent = pnlTarjetaBanco;
            btnCerrarPanel.Location = new Point(555, 18);
            btnCerrarPanel.Size = new Size(42, 36);

            btnCerrarPanel.Text = "X";

            btnCerrarPanel.BackColor =
                Color.FromArgb(185, 24, 67);

            btnCerrarPanel.ForeColor = Color.White;
            btnCerrarPanel.FlatStyle = FlatStyle.Flat;
            btnCerrarPanel.FlatAppearance.BorderSize = 0;

            btnCerrarPanel.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            btnCerrarPanel.Cursor = Cursors.Hand;

            Button btnCopiarCuenta = new Button
            {
                Parent = pnlTarjetaBanco,
                Location = new Point(185, 350),
                Size = new Size(250, 44),

                Text = "Copiar numero de cuenta",

                BackColor =
                    Color.FromArgb(15, 145, 155),

                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnCopiarCuenta.FlatAppearance.BorderSize = 0;

            btnCopiarCuenta.Click +=
                (sender, e) =>
                {
                    try
                    {
                        Clipboard.SetText(
                            label2.Text.Trim()
                        );

                        MessageBox.Show(
                            "Numero de cuenta copiado.",
                            "Datos bancarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "No se pudo copiar: " +
                            ex.Message,
                            "Datos bancarios",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                };

            btnCerrarPanel.Click +=
                (sender, e) =>
                {
                    OcultarPanelBancoPagos();
                };

            pnlFondoBanco.Click +=
                (sender, e) =>
                {
                    if (sender == pnlFondoBanco)
                    {
                        OcultarPanelBancoPagos();
                    }
                };

            pnlFondoBanco.Resize +=
                (sender, e) =>
                {
                    CentrarTarjetaBanco();
                };

            RedondearControlPagos(
                pnlTarjetaBanco,
                20
            );

            RedondearControlPagos(
                btnCerrarPanel,
                10
            );

            RedondearControlPagos(
                btnCopiarCuenta,
                11
            );
        }

        private void ConfigurarLineaBanco(
            Label etiqueta,
            Label valor,
            string textoEtiqueta,
            string textoValor,
            int top)
        {
            etiqueta.Parent = pnlTarjetaBanco;
            etiqueta.AutoSize = false;

            etiqueta.Location =
                new Point(55, top);

            etiqueta.Size =
                new Size(190, 31);

            etiqueta.Text = textoEtiqueta;

            etiqueta.ForeColor =
                Color.FromArgb(255, 187, 31);

            etiqueta.BackColor =
                Color.Transparent;

            etiqueta.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );

            valor.Parent = pnlTarjetaBanco;
            valor.AutoSize = false;

            valor.Location =
                new Point(245, top);

            valor.Size =
                new Size(315, 31);

            valor.Text = textoValor;

            valor.ForeColor = Color.White;
            valor.BackColor = Color.Transparent;

            valor.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold
            );
        }

        private void MostrarPanelBancoPagos()
        {
            pnlFondoBanco.Visible = true;
            pnlFondoBanco.BringToFront();

            CentrarTarjetaBanco();
        }

        private void OcultarPanelBancoPagos()
        {
            pnlFondoBanco.Visible = false;
        }

        private void CentrarTarjetaBanco()
        {
            if (pnlFondoBanco == null ||
                pnlTarjetaBanco == null)
            {
                return;
            }

            pnlTarjetaBanco.Left =
                Math.Max(
                    20,
                    (pnlFondoBanco.ClientSize.Width -
                     pnlTarjetaBanco.Width) / 2
                );

            pnlTarjetaBanco.Top =
                Math.Max(
                    20,
                    (pnlFondoBanco.ClientSize.Height -
                     pnlTarjetaBanco.Height) / 2
                );
        }


        // =========================================================
        // CORREO DE CONFIRMACION DE PAGO
        // =========================================================

        private void ConfigurarCorreoConfirmacionPago()
        {
            if (correoPagoConfigurado)
                return;

            correoPagoConfigurado = true;

            btnGuardar.Click -= btnGuardar_Click;
            btnGuardar.Click -= btnGuardarPagoConCorreo_Click;
            btnGuardar.Click += btnGuardarPagoConCorreo_Click;
        }

        private async void btnGuardarPagoConCorreo_Click(
            object? sender,
            EventArgs e)
        {
            if (procesandoCorreoPago)
                return;

            procesandoCorreoPago = true;

            int idMatricula = 0;
            decimal monto = 0M;
            DateTime fechaPago = dtpFechaPago.Value;
            string metodoPago =
                cmbMetodoPago.SelectedItem?.ToString()
                ?? string.Empty;

            string nombreAlumno = string.Empty;
            string correoAlumno = string.Empty;
            string nivelAlumno = string.Empty;
            string codigoMatricula = string.Empty;

            int cantidadPagosAntes = 0;

            try
            {
                if (cmbMatricula.SelectedValue != null)
                {
                    int.TryParse(
                        cmbMatricula.SelectedValue.ToString(),
                        out idMatricula
                    );
                }

                decimal.TryParse(
                    txtMonto.Text.Trim(),
                    out monto
                );

                if (idMatricula > 0)
                {
                    cantidadPagosAntes =
                        pagoCD.ObtenerPorMatricula(
                            idMatricula
                        ).Count;

                    ObtenerDatosReciboPago(
                        idMatricula,
                        ref nombreAlumno,
                        ref correoAlumno,
                        ref nivelAlumno,
                        ref codigoMatricula
                    );
                }

                btnGuardar_Click(sender!, e);

                if (idMatricula <= 0 || monto <= 0M)
                    return;

                int cantidadPagosDespues =
                    pagoCD.ObtenerPorMatricula(
                        idMatricula
                    ).Count;

                if (cantidadPagosDespues <= cantidadPagosAntes)
                    return;

                if (string.IsNullOrWhiteSpace(correoAlumno))
                {
                    MessageBox.Show(
                        "El pago fue registrado, pero el alumno no " +
                        "tiene un correo guardado para enviar el recibo.",
                        "Recibo no enviado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                try
                {
                    await servicioCorreoPago
                        .EnviarConfirmacionPagoAsync(
                            correoAlumno,
                            nombreAlumno,
                            codigoMatricula,
                            nivelAlumno,
                            fechaPago,
                            monto,
                            metodoPago
                        );

                    MessageBox.Show(
                        "El recibo de confirmacion fue enviado a:\r\n\r\n" +
                        correoAlumno,
                        "Recibo enviado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception exCorreo)
                {
                    MessageBox.Show(
                        "El pago fue registrado correctamente, pero " +
                        "no se pudo enviar el recibo.\r\n\r\n" +
                        "Motivo: " + exCorreo.Message,
                        "Recibo no enviado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo preparar el recibo del pago:\r\n" +
                    ex.Message,
                    "Confirmacion de pago",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            finally
            {
                procesandoCorreoPago = false;
            }
        }

        private void ObtenerDatosReciboPago(
            int idMatricula,
            ref string nombreAlumno,
            ref string correoAlumno,
            ref string nivelAlumno,
            ref string codigoMatricula)
        {
            object? matriculas =
                matriculaCD.ObtenerTodos();

            if (matriculas is not IEnumerable listaMatriculas)
                return;

            int idAlumno = 0;

            foreach (object? matricula in listaMatriculas)
            {
                if (matricula == null)
                    continue;

                int idActual =
                    ObtenerEnteroPropiedadPago(
                        matricula,
                        "IdMatricula"
                    );

                if (idActual != idMatricula)
                    continue;

                idAlumno =
                    ObtenerEnteroPropiedadPago(
                        matricula,
                        "IdAlumno"
                    );

                nombreAlumno =
                    ObtenerTextoPropiedadPago(
                        matricula,
                        "NombreAlumno",
                        "Alumno",
                        "Nombre"
                    );

                correoAlumno =
                    ObtenerTextoPropiedadPago(
                        matricula,
                        "CorreoAlumno",
                        "Correo",
                        "Email"
                    );

                nivelAlumno =
                    ObtenerTextoPropiedadPago(
                        matricula,
                        "NombreNivel",
                        "Nivel"
                    );

                codigoMatricula =
                    idMatricula.ToString();

                break;
            }

            if (idAlumno <= 0)
                return;

            object? alumnos =
                alumnoCDCorreoPago.ObtenerTodos();

            if (alumnos is not IEnumerable listaAlumnos)
                return;

            foreach (object? alumno in listaAlumnos)
            {
                if (alumno == null)
                    continue;

                int idActual =
                    ObtenerEnteroPropiedadPago(
                        alumno,
                        "IdAlumno"
                    );

                if (idActual != idAlumno)
                    continue;

                if (string.IsNullOrWhiteSpace(nombreAlumno))
                {
                    string nombre =
                        ObtenerTextoPropiedadPago(
                            alumno,
                            "Nombre"
                        );

                    string apellido =
                        ObtenerTextoPropiedadPago(
                            alumno,
                            "Apellido"
                        );

                    nombreAlumno =
                        (nombre + " " + apellido).Trim();
                }

                if (string.IsNullOrWhiteSpace(correoAlumno))
                {
                    correoAlumno =
                        ObtenerTextoPropiedadPago(
                            alumno,
                            "Correo",
                            "Email"
                        );
                }

                break;
            }
        }

        private static int ObtenerEnteroPropiedadPago(
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

            return int.TryParse(
                propiedad.GetValue(objeto)?
                    .ToString(),
                out int valor
            )
                ? valor
                : 0;
        }

        private static string ObtenerTextoPropiedadPago(
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

                string valor =
                    propiedad.GetValue(objeto)?
                        .ToString()?
                        .Trim()
                    ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(valor))
                    return valor;
            }

            return string.Empty;
        }

        // =========================================================
        // EVENTOS VISUALES
        // =========================================================

        private void ConfigurarEventosVisualesPagos()
        {
            pnlFormularioPago.Resize +=
                (sender, e) =>
                {
                    pnlAccionesPago.Width =
                        pnlFormularioPago.ClientSize.Width -
                        40;

                    btnGuardar.Width =
                        pnlAccionesPago.Width;

                    int anchoMitad =
                        (pnlAccionesPago.Width - 10) / 2;

                    btnLimpiar.Width = anchoMitad;
                    btnEliminar.Width = anchoMitad;

                    btnEliminar.Left =
                        anchoMitad + 10;

                    btnMostrarDatosBanco.Width =
                        pnlAccionesPago.Width;

                    RedondearControlPagos(
                        pnlFormularioPago,
                        18
                    );
                };

            pnlListaPagos.Resize +=
                (sender, e) =>
                {
                    pnlBuscarPagos.Left =
                        pnlCabeceraListaPagos.ClientSize.Width -
                        pnlBuscarPagos.Width -
                        18;

                    pnlHerramientasPago.Width =
                        pnlCabeceraListaPagos.ClientSize.Width -
                        40;

                    pnlContenedorGridPagos.Size =
                        new Size(
                            pnlListaPagos.ClientSize.Width - 32,
                            pnlListaPagos.ClientSize.Height - 178
                        );

                    lblCantidadPagos.Top =
                        pnlListaPagos.ClientSize.Height - 41;

                    lblTotalRecaudado.Left =
                        pnlListaPagos.ClientSize.Width -
                        lblTotalRecaudado.Width -
                        20;

                    lblTotalRecaudado.Top =
                        pnlListaPagos.ClientSize.Height - 41;

                    RedondearControlPagos(
                        pnlListaPagos,
                        18
                    );
                };

            pnlEncabezadoPagos.Resize +=
                (sender, e) =>
                {
                    btnCerrarFormularioPagos.Left =
                        pnlEncabezadoPagos.ClientSize.Width -
                        btnCerrarFormularioPagos.Width -
                        20;
                };

            dgvPagos.DataSourceChanged +=
                (sender, e) =>
                {
                    ActualizarResumenPagos();
                };

            txtMonto.Enter += CampoPago_Enter;
            txtMonto.Leave += CampoPago_Leave;
            txtBuscarPagos.Enter += CampoPago_Enter;
            txtBuscarPagos.Leave += CampoPago_Leave;

            cmbMetodoPago.SelectedIndexChanged +=
                (sender, e) =>
                {
                    btnMostrarDatosBanco.Visible =
                        cmbMetodoPago.SelectedItem?
                            .ToString() ==
                        "Transferencia";
                };
        }

        private void CampoPago_Enter(
            object? sender,
            EventArgs e)
        {
            if (sender is TextBox caja)
            {
                caja.BackColor =
                    Color.FromArgb(10, 31, 72);
            }
        }

        private void CampoPago_Leave(
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

        private void AjustarDisenoPagos()
        {
            if (!disenoPagosInicializado ||
                pnlCuerpoPagos == null ||
                pnlFormularioPago == null ||
                pnlListaPagos == null)
            {
                return;
            }

            const int margen = 18;
            const int separacion = 18;

            const int anchoFormulario = 360;
            const int anchoMinimoLista = 650;
            const int altoMinimo = 650;

            int anchoInterior =
                pnlCuerpoPagos.ClientSize.Width -
                (margen * 2);

            int anchoLista =
                anchoInterior -
                anchoFormulario -
                separacion;

            int altoDisponible =
                pnlCuerpoPagos.ClientSize.Height -
                (margen * 2);

            int altoPaneles =
                Math.Max(
                    altoMinimo,
                    altoDisponible
                );

            pnlFormularioPago.Location =
                new Point(margen, margen);

            pnlFormularioPago.Size =
                new Size(
                    anchoFormulario,
                    altoPaneles
                );

            pnlListaPagos.Location =
                new Point(
                    margen +
                    anchoFormulario +
                    separacion,
                    margen
                );

            if (anchoLista >= anchoMinimoLista)
            {
                pnlListaPagos.Size =
                    new Size(
                        anchoLista,
                        altoPaneles
                    );

                pnlCuerpoPagos.AutoScrollMinSize =
                    new Size(
                        0,
                        altoPaneles +
                        (margen * 2)
                    );
            }
            else
            {
                pnlListaPagos.Size =
                    new Size(
                        anchoMinimoLista,
                        altoPaneles
                    );

                pnlCuerpoPagos.AutoScrollMinSize =
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

            pnlAccionesPago.Location =
                new Point(
                    20,
                    pnlFormularioPago.ClientSize.Height -
                    pnlAccionesPago.Height -
                    20
                );

            pnlBuscarPagos.Left =
                pnlCabeceraListaPagos.ClientSize.Width -
                pnlBuscarPagos.Width -
                18;

            pnlContenedorGridPagos.Size =
                new Size(
                    pnlListaPagos.ClientSize.Width - 32,
                    pnlListaPagos.ClientSize.Height - 178
                );

            lblCantidadPagos.Top =
                pnlListaPagos.ClientSize.Height - 41;

            lblTotalRecaudado.Left =
                pnlListaPagos.ClientSize.Width -
                lblTotalRecaudado.Width -
                20;

            lblTotalRecaudado.Top =
                pnlListaPagos.ClientSize.Height - 41;

            btnCerrarFormularioPagos.Left =
                pnlEncabezadoPagos.ClientSize.Width -
                btnCerrarFormularioPagos.Width -
                20;

            CentrarTarjetaBanco();

            RedondearControlPagos(
                pnlFormularioPago,
                18
            );

            RedondearControlPagos(
                pnlListaPagos,
                18
            );

            RedondearControlPagos(
                pnlContenedorGridPagos,
                12
            );
        }

        // =========================================================
        // NAVEGACION
        // =========================================================

        private void VolverDashboardPagos()
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

        private void AbrirFormularioPagos(
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
        // COLORES
        // =========================================================

        private static Color AclararColorPagos(
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

        private static Color OscurecerColorPagos(
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
        // BORDES
        // =========================================================

        private static void RedondearControlPagos(
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

            Region? anterior = control.Region;

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

                if (pnlMenuPagos != null)
                {
                    pnlMenuPagos.Visible = false;
                    pnlMenuPagos.Dock = DockStyle.None;
                    pnlMenuPagos.Width = 0;
                }

                if (pnlEncabezadoPagos != null)
                {
                    pnlEncabezadoPagos.Visible = false;
                    pnlEncabezadoPagos.Dock = DockStyle.None;
                    pnlEncabezadoPagos.Height = 0;
                }

                if (pnlContenidoPagos != null)
                {
                    pnlContenidoPagos.Visible = true;
                    pnlContenidoPagos.Dock = DockStyle.Fill;
                    pnlContenidoPagos.Location = Point.Empty;
                    pnlContenidoPagos.Margin = new Padding(0);
                    pnlContenidoPagos.Padding = new Padding(0);
                    pnlContenidoPagos.BringToFront();
                }

                if (pnlCuerpoPagos != null)
                {
                    pnlCuerpoPagos.Visible = true;
                    pnlCuerpoPagos.Dock = DockStyle.Fill;
                    pnlCuerpoPagos.Location = Point.Empty;
                    pnlCuerpoPagos.Margin = new Padding(0);
                }

                PerformLayout();

                if (pnlCuerpoPagos != null)
                {
                    AjustarDisenoPagos();
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