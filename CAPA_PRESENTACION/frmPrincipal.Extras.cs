using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmPrincipal
    {
        // =========================================================
        // VARIABLES
        // =========================================================

        private bool disenoInicializado;
        private bool formularioMostrado;

        private System.Windows.Forms.Timer timerExtras = null!;

        private int indiceContenidoEnglish;
        private string ultimoEstadoDashboard = string.Empty;

        private Panel pnlMenuModerno = null!;
        private Panel pnlContenidoModerno = null!;
        private Panel pnlEncabezadoModerno = null!;
        private Panel pnlCuerpoModerno = null!;

        private Panel pnlContenedorModulos = null!;

        // Vista permanente del Dashboard.
        // Se crea una sola vez y luego solo se muestra/oculta.
        private Panel pnlDashboardPrincipal = null!;
        private Form? formularioModuloActual;

        // Los módulos se crean una sola vez y permanecen cargados.
        // No son static porque los controles WinForms deben pertenecer
        // a una instancia concreta del formulario principal.
        private readonly Dictionary<Type, Form> modulosCargados =
            new Dictionary<Type, Form>();

        private Panel pnlMarca = null!;
        private Panel pnlSalir = null!;

        private FlowLayoutPanel flpMenu = null!;
        private Button btnDashboardPrincipal = null!;

        private TableLayoutPanel tlpPrincipal = null!;
        private TableLayoutPanel tlpCentro = null!;
        private TableLayoutPanel tlpDerecha = null!;
        private TableLayoutPanel tlpIndicadores = null!;
        private TableLayoutPanel tlpAccesos = null!;
        private TableLayoutPanel tlpNotificaciones = null!;

        private Panel pnlResumen = null!;
        private Panel pnlAccesosRapidos = null!;
        private Panel pnlEstadoSistema = null!;

        private Label lblTituloPagina = null!;
        private Label lblSubtituloPagina = null!;

        private Label lblNombreMarca = null!;
        private Label lblSubtituloMarca = null!;

        private Label lblTituloResumen = null!;
        private Label lblTituloAccesos = null!;

        private Label lblTituloEstado = null!;
        private Label lblEstadoGeneral = null!;
        private Label lblDetalleEstado = null!;

        private Label lblNumeroContenido = null!;
        private Button btnAnteriorContenido = null!;

        private readonly List<Panel> filasNotificacion =
            new List<Panel>();

        private readonly List<Panel> barrasNotificacion =
            new List<Panel>();

        private readonly List<Label> textosNotificacion =
            new List<Label>();

        // =========================================================
        // CONTENIDO ENGLISH WORLD
        // =========================================================

        private readonly List<ContenidoEnglish> contenidosEnglish =
            new List<ContenidoEnglish>
            {
                new ContenidoEnglish(
                    "PALABRA DEL DIA",
                    "Bridge",
                    "Significa puente.\r\n\r\n" +
                    "Ejemplo:\r\n" +
                    "Learning English can be a bridge to new opportunities."
                ),

                new ContenidoEnglish(
                    "EXPRESION UTIL",
                    "Break the ice",
                    "Se utiliza para iniciar una conversacion y hacer que las " +
                    "personas se sientan mas comodas.\r\n\r\n" +
                    "Ejemplo:\r\n" +
                    "The teacher told a joke to break the ice."
                ),

                new ContenidoEnglish(
                    "PRONUNCIACION",
                    "Think",
                    "Para pronunciar el sonido TH, coloca suavemente la punta " +
                    "de la lengua entre los dientes y deja salir el aire."
                ),

                new ContenidoEnglish(
                    "VOCABULARIO",
                    "Opportunity",
                    "Significa oportunidad.\r\n\r\n" +
                    "Ejemplo:\r\n" +
                    "English creates new opportunities."
                ),

                new ContenidoEnglish(
                    "CONSEJO DE ESTUDIO",
                    "Practice every day",
                    "Practicar durante 15 minutos todos los dias puede ser mas " +
                    "efectivo que estudiar muchas horas una sola vez."
                ),

                new ContenidoEnglish(
                    "EXPRESION UTIL",
                    "Piece of cake",
                    "Se utiliza cuando algo es muy facil.\r\n\r\n" +
                    "Ejemplo:\r\n" +
                    "The exam was a piece of cake."
                ),

                new ContenidoEnglish(
                    "DATO CULTURAL",
                    "British and American English",
                    "En ingles britanico se utiliza flat. En ingles " +
                    "estadounidense se utiliza apartment."
                )
            };

        private sealed class ContenidoEnglish
        {
            public string Tipo { get; }
            public string Titulo { get; }
            public string Detalle { get; }

            public ContenidoEnglish(
                string tipo,
                string titulo,
                string detalle)
            {
                Tipo = tipo;
                Titulo = titulo;
                Detalle = detalle;
            }
        }

        private sealed class NotificacionDashboard
        {
            public string Texto { get; }
            public Color Color { get; }

            public NotificacionDashboard(
                string texto,
                Color color)
            {
                Texto = texto;
                Color = color;
            }
        }

        // =========================================================
        // REDUCIR PARPADEO
        // =========================================================

        protected override CreateParams CreateParams
        {
            get
            {
                return base.CreateParams;
            }
        }

        // =========================================================
        // CREAR ANTES DE MOSTRAR
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoInicializado)
            {
                disenoInicializado = true;

                try
                {
                    InicializarDisenoModerno();
                    InicializarFuncionesExtras();
                    AjustarContenidoDesplazable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar el formulario:\r\n" +
                        ex.Message,
                        "Panel principal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

            base.SetVisibleCore(value);

            if (value)
            {
                formularioMostrado = true;

                // Dashboard ya construido y listo.
                pnlDashboardPrincipal.Visible = true;
                pnlContenedorModulos.Visible = false;
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        private void InicializarDisenoModerno()
        {
            ConfigurarFormulario();
            OcultarControlesOriginalesNoUsados();
            CrearEstructuraPrincipal();
            ConfigurarMenuLateral();
            ConfigurarNavegacionIntegrada();
            ConfigurarEncabezado();
            ConfigurarColumnaCentral();
            ConfigurarIndicadores();
            ConfigurarAccesosRapidos();
            ConfigurarEstadoSistema();
            ConfigurarColumnaDerecha();
            ConfigurarNotificaciones();
            ConfigurarEnglishWorld();

            AjustarContenidoDesplazable();

            pnlMenuModerno.BringToFront();
            pnlContenidoModerno.BringToFront();

            Invalidate(true);
        }

        private void ConfigurarFormulario()
        {
            ClientSize = new Size(1440, 800);
            MinimumSize = new Size(1180, 700);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;

            BackColor = Color.FromArgb(4, 18, 47);
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
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            UpdateStyles();
        }

        private void OcultarControlesOriginalesNoUsados()
        {
            lblTitulo.Visible = false;
            pnlDashboard.Visible = false;
            lstNotificaciones.Visible = false;
            label1.Visible = false;

            lblTituloAlumnos.Visible = false;
            lblTituloMatriculas.Visible = false;
            lblTituloInstructores.Visible = false;
            lblTituloIngresos.Visible = false;
            lblTituloPendiente.Visible = false;
            lblTituloNivel.Visible = false;
        }

        // =========================================================
        // ESTRUCTURA PRINCIPAL
        // =========================================================

        private void CrearEstructuraPrincipal()
        {
            pnlMenuModerno = new Panel
            {
                Name = "pnlMenuModerno",
                Dock = DockStyle.Left,
                Width = 210,
                BackColor = Color.FromArgb(3, 16, 41),
                Padding = new Padding(12, 16, 12, 16)
            };

            pnlContenidoModerno = new Panel
            {
                Name = "pnlContenidoModerno",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlEncabezadoModerno = new Panel
            {
                Name = "pnlEncabezadoModerno",
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(4, 20, 51)
            };

            pnlCuerpoModerno = new Panel
            {
                Name = "pnlCuerpoModerno",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                AutoScroll = true
            };

            pnlCuerpoModerno.HorizontalScroll.Enabled = false;
            pnlCuerpoModerno.HorizontalScroll.Visible = false;

            pnlCuerpoModerno.Resize +=
                (sender, e) =>
                {
                    AjustarContenidoDesplazable();
                };

            pnlContenidoModerno.Controls.Add(pnlCuerpoModerno);
            pnlContenidoModerno.Controls.Add(pnlEncabezadoModerno);

            // Panel permanente que contiene TODO el Dashboard.
            pnlDashboardPrincipal = new Panel
            {
                Name = "pnlDashboardPrincipal",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.Transparent,
                Visible = true
            };

            // El encabezado y el cuerpo del Dashboard viven aquí.
            pnlDashboardPrincipal.Controls.Add(pnlCuerpoModerno);
            pnlDashboardPrincipal.Controls.Add(pnlEncabezadoModerno);

            pnlContenedorModulos = new Panel
            {
                Name = "pnlContenedorModulos",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.FromArgb(4, 18, 47),
                Visible = false
            };

            pnlContenidoModerno.Controls.Clear();
            pnlContenidoModerno.Controls.Add(pnlContenedorModulos);
            pnlContenidoModerno.Controls.Add(pnlDashboardPrincipal);

            Controls.Add(pnlContenidoModerno);
            Controls.Add(pnlMenuModerno);

            pnlDashboardPrincipal.BringToFront();

            CrearTablaPrincipal();
        }

        private void CrearTablaPrincipal()
        {
            tlpPrincipal = new TableLayoutPanel
            {
                Parent = pnlCuerpoModerno,
                Dock = DockStyle.None,
                Location = new Point(20, 18),

                Size = new Size(
                    Math.Max(
                        900,
                        pnlCuerpoModerno.ClientSize.Width - 40
                    ),
                    690
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor = Color.Transparent,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            tlpPrincipal.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 70F)
            );

            tlpPrincipal.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 30F)
            );

            tlpPrincipal.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F)
            );

            tlpCentro = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                ColumnCount = 1,
                RowCount = 3,
                Margin = new Padding(0, 0, 10, 0),
                Padding = new Padding(0)
            };

            tlpCentro.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 220F)
            );

            tlpCentro.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 230F)
            );

            tlpCentro.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 210F)
            );

            tlpDerecha = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(10, 0, 0, 0),
                Padding = new Padding(0)
            };

            tlpDerecha.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 330F)
            );

            tlpDerecha.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 330F)
            );

            tlpPrincipal.Controls.Add(tlpCentro, 0, 0);
            tlpPrincipal.Controls.Add(tlpDerecha, 1, 0);

            pnlCuerpoModerno.AutoScrollMinSize =
                new Size(0, tlpPrincipal.Bottom + 20);
        }

        private void AjustarContenidoDesplazable()
        {
            if (pnlCuerpoModerno == null ||
                tlpPrincipal == null)
            {
                return;
            }

            int anchoDisponible =
                pnlCuerpoModerno.ClientSize.Width - 40;

            if (pnlCuerpoModerno.VerticalScroll.Visible)
            {
                anchoDisponible -=
                    SystemInformation.VerticalScrollBarWidth;
            }

            tlpPrincipal.Location = new Point(20, 18);

            // El Dashboard sigue el ancho real disponible.
            // No se fuerza un ancho mínimo artificial durante resize/maximizado.
            tlpPrincipal.Width = Math.Max(1, anchoDisponible);
            tlpPrincipal.Height = 690;

            pnlCuerpoModerno.AutoScrollMinSize =
                new Size(0, tlpPrincipal.Bottom + 20);
        }

        // =========================================================
        // MENU LATERAL
        // =========================================================

        private void ConfigurarMenuLateral()
        {
            pnlMarca = new Panel
            {
                Dock = DockStyle.Top,
                Height = 78,
                BackColor = Color.Transparent
            };

            Panel pnlSeparador = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = Color.FromArgb(29, 50, 89)
            };

            pnlSalir = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 12, 0, 10)
            };

            flpMenu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 14, 0, 0),
                Margin = new Padding(0)
            };

            pnlMenuModerno.Controls.Clear();

            pnlMenuModerno.Controls.Add(flpMenu);
            pnlMenuModerno.Controls.Add(pnlSalir);
            pnlMenuModerno.Controls.Add(pnlSeparador);
            pnlMenuModerno.Controls.Add(pnlMarca);

            picLogo.Parent = pnlMarca;
            picLogo.Location = new Point(2, 7);
            picLogo.Size = new Size(48, 48);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.Transparent;

            CargarLogoDesdeRecursos();

            lblNombreMarca = new Label
            {
                Parent = pnlMarca,
                Text = "LEXBRIDGE",
                Location = new Point(57, 8),
                Size = new Size(135, 27),
                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    11.5F,
                    FontStyle.Bold
                )
            };

            lblSubtituloMarca = new Label
            {
                Parent = pnlMarca,
                Text = "ACADEMIA DE INGLES",
                Location = new Point(58, 35),
                Size = new Size(130, 18),
                ForeColor = Color.FromArgb(139, 161, 193),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.5F)
            };

            btnDashboardPrincipal = new Button();

            ConfigurarBotonMenu(
                btnDashboardPrincipal,
                "\uE80F",
                "Dashboard"
            );

            // Alumnos.
            ConfigurarBotonMenu(
                btnAlumnos,
                "\uE77B",
                "Alumnos"
            );

            ConfigurarBotonMenu(
                btnNiveles,
                "\uE8EF",
                "Niveles"
            );

            ConfigurarBotonMenu(
                btnInstructores,
                "\uE716",
                "Instructores"
            );

            ConfigurarBotonMenu(
                btnMatriculas,
                "\uE787",
                "Matriculas"
            );

            ConfigurarBotonMenu(
                btnPagos,
                "\uE8C7",
                "Pagos"
            );

            ConfigurarBotonMenu(
                btnReportes,
                "\uE9D2",
                "Reportes"
            );

            ConfigurarBotonMenu(
                btnConsultaMatriculas,
                "\uE721",
                "Consulta"
            );

            btnSalir.Parent = pnlSalir;
            btnSalir.Dock = DockStyle.Fill;
            btnSalir.Text = "Cerrar aplicacion";
            btnSalir.TextAlign = ContentAlignment.MiddleCenter;
            btnSalir.Padding = new Padding(0);
            btnSalir.Margin = new Padding(0);

            btnSalir.BackColor =
                Color.FromArgb(88, 30, 57);

            btnSalir.ForeColor =
                Color.FromArgb(255, 207, 218);

            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.FlatAppearance.BorderSize = 0;

            btnSalir.Font = new Font(
                "Segoe UI",
                8.7F,
                FontStyle.Bold
            );

            btnSalir.Cursor = Cursors.Hand;

            RedondearControl(btnSalir, 12);
        }

        private void ConfigurarBotonMenu(
            Button boton,
            string icono,
            string texto)
        {
            Panel contenedor = new Panel
            {
                Size = new Size(186, 43),
                Margin = new Padding(0, 0, 0, 6),
                BackColor = Color.Transparent
            };

            flpMenu.Controls.Add(contenedor);

            boton.Parent = contenedor;
            boton.Location = new Point(0, 0);
            boton.Size = contenedor.Size;

            boton.Text = texto;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Padding = new Padding(45, 0, 0, 0);

            boton.BackColor = Color.Transparent;

            boton.ForeColor =
                Color.FromArgb(207, 218, 238);

            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;

            boton.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(39, 34, 91);

            boton.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(62, 38, 117);

            boton.Font = new Font(
                "Segoe UI",
                9F
            );

            boton.Cursor = Cursors.Hand;

            Label lblIcono = new Label
            {
                Parent = contenedor,
                Text = icono,
                Location = new Point(11, 0),
                Size = new Size(31, 43),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe MDL2 Assets",
                    10.5F
                ),

                ForeColor =
                    Color.FromArgb(170, 195, 230),

                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            lblIcono.Click +=
                (sender, e) =>
                {
                    boton.PerformClick();
                };

            RedondearControl(boton, 11);

            boton.SendToBack();
            lblIcono.BringToFront();
        }


        // =========================================================
        // NAVEGACION INTEGRADA
        // =========================================================

        private void ConfigurarNavegacionIntegrada()
        {
            QuitarEventosClick(btnDashboardPrincipal);
            QuitarEventosClick(btnAlumnos);
            QuitarEventosClick(btnNiveles);
            QuitarEventosClick(btnInstructores);
            QuitarEventosClick(btnMatriculas);
            QuitarEventosClick(btnPagos);
            QuitarEventosClick(btnReportes);
            QuitarEventosClick(btnConsultaMatriculas);

            btnDashboardPrincipal.Click += (sender, e) =>
            {
                MostrarDashboardPrincipal();
            };

            btnAlumnos.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmAlumnos());
            };

            btnNiveles.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmNiveles());
            };

            btnInstructores.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmInstructores());
            };

            btnMatriculas.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmMatriculas());
            };

            btnPagos.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmPagos());
            };

            btnReportes.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(new frmReportes());
            };

            btnConsultaMatriculas.Click += (sender, e) =>
            {
                AbrirModuloEnPrincipal(
                    new frmConsultaMatriculas()
                );
            };

            picLogo.Cursor = Cursors.Hand;
            picLogo.Click += (sender, e) =>
            {
                MostrarDashboardPrincipal();
            };

            lblNombreMarca.Cursor = Cursors.Hand;
            lblNombreMarca.Click += (sender, e) =>
            {
                MostrarDashboardPrincipal();
            };
        }

        private void AbrirModuloEnPrincipal(Form formularioSolicitado)
        {
            try
            {
                Type tipoModulo = formularioSolicitado.GetType();

                Form modulo;

                // Reutilizar el módulo si ya fue abierto.
                if (modulosCargados.TryGetValue(
                        tipoModulo,
                        out Form? moduloExistente) &&
                    !moduloExistente.IsDisposed)
                {
                    modulo = moduloExistente;
                    formularioSolicitado.Dispose();
                }
                else
                {
                    modulo = formularioSolicitado;

                    modulo.TopLevel = false;
                    modulo.FormBorderStyle = FormBorderStyle.None;
                    modulo.WindowState = FormWindowState.Normal;
                    modulo.StartPosition = FormStartPosition.Manual;
                    modulo.MinimumSize = Size.Empty;
                    modulo.MaximumSize = Size.Empty;
                    modulo.Margin = Padding.Empty;
                    modulo.Padding = Padding.Empty;
                    modulo.Dock = DockStyle.Fill;

                    PrepararModuloEspecifico(modulo);

                    pnlContenedorModulos.Controls.Add(modulo);
                    modulosCargados[tipoModulo] = modulo;
                }

                // Si ya es el módulo actual, no hacer nada.
                if (formularioModuloActual == modulo &&
                    modulo.Visible)
                {
                    modulo.Focus();
                    return;
                }

                // Ocultar módulo anterior.
                if (formularioModuloActual != null &&
                    !formularioModuloActual.IsDisposed &&
                    formularioModuloActual != modulo)
                {
                    formularioModuloActual.Hide();
                }

                formularioModuloActual = modulo;

                // CAMBIO DIRECTO:
                // Dashboard OFF / módulos ON.
                pnlDashboardPrincipal.Visible = false;

                pnlContenedorModulos.Visible = true;
                pnlContenedorModulos.Dock = DockStyle.Fill;

                modulo.Dock = DockStyle.Fill;

                if (!modulo.Visible)
                {
                    modulo.Show();
                }

                modulo.BringToFront();
                modulo.Focus();
            }
            catch (Exception ex)
            {
                if (!formularioSolicitado.IsDisposed)
                {
                    formularioSolicitado.Dispose();
                }

                MessageBox.Show(
                    "No se pudo abrir el módulo:\r\n" +
                    ex.Message,
                    "Lexbridge",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Nueva mejora:
        // Permite congelar el redibujado durante el cambio de modulo.
        private static void CambiarRedibujado(
            Control control,
            bool habilitar)
        {
            if (control == null || !control.IsHandleCreated)
            {
                return;
            }

            SendMessage(
                control.Handle,
                0x000B,
                habilitar ? new IntPtr(1) : IntPtr.Zero,
                IntPtr.Zero
            );
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam
        );

        private void PrepararModuloEspecifico(
            Form formulario)
        {
            switch (formulario)
            {
                case frmAlumnos alumnos:
                    alumnos.PrepararModoIntegrado();
                    break;

                case frmNiveles niveles:
                    niveles.PrepararModoIntegrado();
                    break;

                case frmInstructores instructores:
                    instructores.PrepararModoIntegrado();
                    break;

                case frmMatriculas matriculas:
                    matriculas.PrepararModoIntegrado();
                    break;

                case frmPagos pagos:
                    pagos.PrepararModoIntegrado();
                    break;

                case frmReportes reportes:
                    reportes.PrepararModoIntegrado();
                    break;

                case frmConsultaMatriculas consulta:
                    consulta.PrepararModoIntegrado();
                    break;

                default:
                    PrepararFormularioIntegrado(formulario);
                    break;
            }
        }

        private void PrepararFormularioIntegrado(Form formulario)
        {
            formulario.SuspendLayout();

            try
            {
                OcultarPanelDeFormulario(
                    formulario,
                    "pnlMenuLateral",
                    "pnlMenuPagos",
                    "pnlMenuReportes",
                    "pnlMenuConsulta",
                    "pnlMenuModerno"
                );

                OcultarPanelDeFormulario(
                    formulario,
                    "pnlEncabezado",
                    "pnlEncabezadoPagos",
                    "pnlEncabezadoReportes",
                    "pnlEncabezadoConsulta",
                    "pnlEncabezadoModerno"
                );

                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.MinimumSize = Size.Empty;
                formulario.Padding = Padding.Empty;
                formulario.Dock = DockStyle.Fill;
            }
            finally
            {
                // Nueva correccion:
                // No fuerza un repintado completo del formulario.
                formulario.ResumeLayout(false);
            }
        }

        private static void OcultarPanelDeFormulario(
            Form formulario,
            params string[] nombresCampos)
        {
            Type? tipo = formulario.GetType();

            while (tipo != null)
            {
                foreach (string nombreCampo in nombresCampos)
                {
                    FieldInfo? campo = tipo.GetField(
                        nombreCampo,
                        BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.Public
                    );

                    if (campo?.GetValue(formulario) is Panel panel)
                    {
                        panel.Visible = false;
                        panel.Dock = DockStyle.None;
                        panel.Width = 0;
                        panel.Height = 0;
                    }
                }

                tipo = tipo.BaseType;
            }
        }

        private static void QuitarEventosClick(Button boton)
        {
            FieldInfo? campoEventos =
                typeof(Component).GetField(
                    "s_eventClick",
                    BindingFlags.Static |
                    BindingFlags.NonPublic
                );

            campoEventos ??=
                typeof(Control).GetField(
                    "s_clickEvent",
                    BindingFlags.Static |
                    BindingFlags.NonPublic
                );

            PropertyInfo? propiedadEventos =
                typeof(Component).GetProperty(
                    "Events",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic
                );

            if (campoEventos == null ||
                propiedadEventos == null)
            {
                return;
            }

            object? clave = campoEventos.GetValue(null);

            if (clave == null)
                return;

            if (propiedadEventos.GetValue(boton) is
                EventHandlerList listaEventos)
            {
                listaEventos.RemoveHandler(
                    clave,
                    listaEventos[clave]
                );
            }
        }

        private void MostrarDashboardPrincipal()
        {
            // Ocultar el módulo actual, sin destruirlo.
            if (formularioModuloActual != null &&
                !formularioModuloActual.IsDisposed)
            {
                formularioModuloActual.Hide();
            }

            formularioModuloActual = null;

            // CAMBIO DIRECTO TIPO PESTAÑA:
            // módulos OFF / Dashboard ON.
            pnlContenedorModulos.Visible = false;

            pnlDashboardPrincipal.Visible = true;
            pnlDashboardPrincipal.Dock = DockStyle.Fill;
            pnlDashboardPrincipal.BringToFront();

            // No reconstruir.
            // No BeginInvoke.
            // No Refresh.
            // No Update.
            // No Invalidate.
        }


        private void CerrarModuloActual()
        {
            if (formularioModuloActual == null)
                return;

            if (!formularioModuloActual.IsDisposed)
            {
                formularioModuloActual.Hide();
            }

            formularioModuloActual = null;
        }

        private void LiberarModulosCargados()
        {
            formularioModuloActual = null;

            foreach (Form modulo in modulosCargados.Values)
            {
                try
                {
                    if (!modulo.IsDisposed)
                    {
                        modulo.Close();
                        modulo.Dispose();
                    }
                }
                catch
                {
                    // Continúa liberando los demás módulos.
                }
            }

            modulosCargados.Clear();

            if (pnlContenedorModulos != null)
            {
                pnlContenedorModulos.Controls.Clear();
            }
        }

        private void CargarLogoDesdeRecursos()
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

                    Image? imagen = recurso as Image;

                    if (imagen != null)
                    {
                        picLogo.Image = imagen;
                        return;
                    }
                }

                picLogo.Image = null;
            }
            catch
            {
                picLogo.Image = null;
            }
        }

        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void ConfigurarEncabezado()
        {
            lblTituloPagina = new Label
            {
                Parent = pnlEncabezadoModerno,
                Text = "Panel Principal",
                Location = new Point(26, 11),
                Size = new Size(430, 39),
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
                Parent = pnlEncabezadoModerno,
                Text = "Resumen general de la Academia de Ingles",
                Location = new Point(29, 50),
                Size = new Size(460, 22),
                ForeColor = Color.FromArgb(145, 167, 199),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            lblFechaHora.Parent = pnlEncabezadoModerno;
            lblFechaHora.Dock = DockStyle.Right;
            lblFechaHora.Width = 340;
            lblFechaHora.Padding = new Padding(0, 10, 22, 0);
            lblFechaHora.AutoSize = false;

            lblFechaHora.TextAlign =
                ContentAlignment.MiddleRight;

            lblFechaHora.ForeColor =
                Color.FromArgb(211, 221, 239);

            lblFechaHora.BackColor =
                Color.Transparent;

            lblFechaHora.Font =
                new Font("Segoe UI", 9F);
        }

        // =========================================================
        // COLUMNA CENTRAL
        // =========================================================

        private void ConfigurarColumnaCentral()
        {
            pnlResumen = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = Color.Transparent
            };

            pnlAccesosRapidos = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 8, 0, 8),
                BackColor = Color.Transparent
            };

            pnlEstadoSistema = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 8, 0, 0),
                BackColor = Color.FromArgb(13, 34, 73)
            };

            tlpCentro.Controls.Add(pnlResumen, 0, 0);
            tlpCentro.Controls.Add(pnlAccesosRapidos, 0, 1);
            tlpCentro.Controls.Add(pnlEstadoSistema, 0, 2);

            RedondearControl(pnlEstadoSistema, 17);
        }

        // =========================================================
        // INDICADORES
        // =========================================================

        private void ConfigurarIndicadores()
        {
            lblTituloResumen = new Label
            {
                Parent = pnlResumen,
                Text = "Resumen del sistema",
                Location = new Point(0, 0),

                Size = new Size(
                    pnlResumen.Width,
                    34
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    13.5F,
                    FontStyle.Bold
                )
            };

            tlpIndicadores = new TableLayoutPanel
            {
                Parent = pnlResumen,
                Location = new Point(0, 36),

                Size = new Size(
                    pnlResumen.Width,
                    Math.Max(
                        100,
                        pnlResumen.Height - 36
                    )
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                ColumnCount = 3,
                RowCount = 2,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            for (int i = 0; i < 3; i++)
            {
                tlpIndicadores.ColumnStyles.Add(
                    new ColumnStyle(
                        SizeType.Percent,
                        33.333F
                    )
                );
            }

            tlpIndicadores.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F)
            );

            tlpIndicadores.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F)
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Alumnos",
                    lblTotalAlumnos,
                    Color.FromArgb(44, 190, 255)
                ),
                0,
                0
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Matriculas",
                    lblTotalMatriculas,
                    Color.FromArgb(146, 83, 255)
                ),
                1,
                0
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Instructores",
                    lblTotalInstructores,
                    Color.FromArgb(48, 216, 160)
                ),
                2,
                0
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Ingresos",
                    lblTotalIngresos,
                    Color.FromArgb(45, 218, 131)
                ),
                0,
                1
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Pendiente",
                    lblTotalPendiente,
                    Color.FromArgb(255, 177, 40)
                ),
                1,
                1
            );

            tlpIndicadores.Controls.Add(
                CrearTarjetaIndicador(
                    "Nivel popular",
                    lblNivelPopular,
                    Color.FromArgb(55, 205, 255)
                ),
                2,
                1
            );

            lblTituloResumen.BringToFront();
        }

        private Panel CrearTarjetaIndicador(
            string titulo,
            Label lblValor,
            Color color)
        {
            Panel tarjeta = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 12),
                BackColor = Color.FromArgb(14, 36, 76)
            };

            Panel barra = new Panel
            {
                Parent = tarjeta,
                Dock = DockStyle.Left,
                Width = 4,
                BackColor = color
            };

            Label lblNombre = new Label
            {
                Parent = tarjeta,
                Text = titulo,
                Location = new Point(20, 12),
                Size = new Size(190, 21),
                ForeColor = Color.FromArgb(177, 195, 222),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.7F)
            };

            lblValor.Parent = tarjeta;
            lblValor.AutoSize = false;
            lblValor.Location = new Point(19, 35);
            lblValor.Size = new Size(215, 40);
            lblValor.TextAlign = ContentAlignment.MiddleLeft;
            lblValor.BackColor = Color.Transparent;
            lblValor.ForeColor = color;

            lblValor.Font = new Font(
                "Segoe UI",
                titulo == "Ingresos" ||
                titulo == "Pendiente"
                    ? 13F
                    : 17F,
                FontStyle.Bold
            );

            tarjeta.Resize +=
                (sender, e) =>
                {
                    RedondearControl(tarjeta, 15);
                };

            RedondearControl(tarjeta, 15);

            return tarjeta;
        }

        // =========================================================
        // ACCESOS RAPIDOS
        // =========================================================

        private void ConfigurarAccesosRapidos()
        {
            lblTituloAccesos = new Label
            {
                Parent = pnlAccesosRapidos,
                Text = "Accesos rapidos",
                Location = new Point(0, 0),

                Size = new Size(
                    pnlAccesosRapidos.Width,
                    34
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    13.5F,
                    FontStyle.Bold
                )
            };

            tlpAccesos = new TableLayoutPanel
            {
                Parent = pnlAccesosRapidos,
                Location = new Point(0, 36),

                Size = new Size(
                    pnlAccesosRapidos.Width,
                    Math.Max(
                        100,
                        pnlAccesosRapidos.Height - 36
                    )
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                ColumnCount = 3,
                RowCount = 2,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            for (int i = 0; i < 3; i++)
            {
                tlpAccesos.ColumnStyles.Add(
                    new ColumnStyle(
                        SizeType.Percent,
                        33.333F
                    )
                );
            }

            tlpAccesos.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F)
            );

            tlpAccesos.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F)
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Alumnos",
                    "Registrar y consultar alumnos",
                    btnAlumnos
                ),
                0,
                0
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Niveles",
                    "Administrar niveles academicos",
                    btnNiveles
                ),
                1,
                0
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Instructores",
                    "Gestionar personal docente",
                    btnInstructores
                ),
                2,
                0
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Matriculas",
                    "Registrar nuevas matriculas",
                    btnMatriculas
                ),
                0,
                1
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Pagos",
                    "Registrar y consultar pagos",
                    btnPagos
                ),
                1,
                1
            );

            tlpAccesos.Controls.Add(
                CrearAccesoRapido(
                    "Reportes",
                    "Consultar reportes generales",
                    btnReportes
                ),
                2,
                1
            );

            lblTituloAccesos.BringToFront();
        }

        private Button CrearAccesoRapido(
            string titulo,
            string descripcion,
            Button botonOriginal)
        {
            Button boton = new Button
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 12),

                Text =
                    titulo +
                    Environment.NewLine +
                    descripcion,

                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(17, 6, 10, 6),
                BackColor = Color.FromArgb(14, 36, 76),
                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    8.7F,
                    FontStyle.Bold
                ),

                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            boton.FlatAppearance.BorderColor =
                Color.FromArgb(39, 65, 119);

            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(34, 50, 104);

            boton.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(55, 42, 119);

            boton.Click +=
                (sender, e) =>
                {
                    botonOriginal.PerformClick();
                };

            boton.Resize +=
                (sender, e) =>
                {
                    RedondearControl(boton, 14);
                };

            RedondearControl(boton, 14);

            return boton;
        }

        // =========================================================
        // ESTADO DEL SISTEMA
        // =========================================================

        private void ConfigurarEstadoSistema()
        {
            lblTituloEstado = new Label
            {
                Parent = pnlEstadoSistema,
                Text = "Estado del sistema",
                Location = new Point(20, 14),
                Size = new Size(230, 28),
                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            lblEstadoGeneral = new Label
            {
                Parent = pnlEstadoSistema,
                Text = "Sistema preparado",
                Location = new Point(20, 45),
                Size = new Size(240, 24),
                ForeColor = Color.FromArgb(48, 216, 150),
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold
                )
            };

            lblDetalleEstado = new Label
            {
                Parent = pnlEstadoSistema,
                Text = "Los modulos principales estan disponibles.",
                Location = new Point(258, 21),
                Size = new Size(390, 55),
                ForeColor = Color.FromArgb(184, 200, 224),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.8F),

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            lblEstadoDashboard.Parent = pnlEstadoSistema;
            lblEstadoDashboard.Location = new Point(20, 74);
            lblEstadoDashboard.AutoSize = true;
            lblEstadoDashboard.Font = new Font("Segoe UI", 8F);

            btnRefrescarDashboard.Parent = pnlEstadoSistema;

            btnRefrescarDashboard.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnRefrescarDashboard.Size = new Size(128, 34);

            btnRefrescarDashboard.Location =
                new Point(
                    pnlEstadoSistema.Width - 150,
                    40
                );

            btnRefrescarDashboard.Text = "Actualizar datos";
            btnRefrescarDashboard.BackColor =
                Color.FromArgb(20, 151, 165);

            btnRefrescarDashboard.ForeColor = Color.White;
            btnRefrescarDashboard.FlatStyle = FlatStyle.Flat;
            btnRefrescarDashboard.FlatAppearance.BorderSize = 0;

            btnRefrescarDashboard.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold
            );

            pnlEstadoSistema.Resize +=
                (sender, e) =>
                {
                    btnRefrescarDashboard.Left =
                        pnlEstadoSistema.Width -
                        btnRefrescarDashboard.Width -
                        20;

                    lblDetalleEstado.Width =
                        Math.Max(
                            240,
                            pnlEstadoSistema.Width - 450
                        );

                    RedondearControl(
                        pnlEstadoSistema,
                        17
                    );
                };

            RedondearControl(btnRefrescarDashboard, 11);
        }

        // =========================================================
        // COLUMNA DERECHA
        // =========================================================

        private void ConfigurarColumnaDerecha()
        {
            pnlNotificaciones.Parent = tlpDerecha;
            pnlNotificaciones.Dock = DockStyle.Fill;
            pnlNotificaciones.Margin = new Padding(0, 0, 0, 8);

            pnlEnglishWorld.Parent = tlpDerecha;
            pnlEnglishWorld.Dock = DockStyle.Fill;
            pnlEnglishWorld.Margin = new Padding(0, 8, 0, 0);

            tlpDerecha.Controls.Add(
                pnlNotificaciones,
                0,
                0
            );

            tlpDerecha.Controls.Add(
                pnlEnglishWorld,
                0,
                1
            );
        }

        // =========================================================
        // NOTIFICACIONES
        // =========================================================

        private void ConfigurarNotificaciones()
        {
            pnlNotificaciones.BackColor =
                Color.FromArgb(13, 34, 73);

            lblTituloNotificaciones.Parent = pnlNotificaciones;
            lblTituloNotificaciones.Text = "Notificaciones";
            lblTituloNotificaciones.Location = new Point(18, 14);
            lblTituloNotificaciones.Size = new Size(240, 32);
            lblTituloNotificaciones.ForeColor = Color.White;
            lblTituloNotificaciones.BackColor = Color.Transparent;

            lblTituloNotificaciones.Font = new Font(
                "Segoe UI",
                14F,
                FontStyle.Bold
            );

            lblCantidadNotificaciones.Parent = pnlNotificaciones;
            lblCantidadNotificaciones.AutoSize = false;
            lblCantidadNotificaciones.Size = new Size(28, 28);

            lblCantidadNotificaciones.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblCantidadNotificaciones.Location =
                new Point(
                    pnlNotificaciones.Width - 46,
                    16
                );

            lblCantidadNotificaciones.TextAlign =
                ContentAlignment.MiddleCenter;

            lblCantidadNotificaciones.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold
            );

            tlpNotificaciones = new TableLayoutPanel
            {
                Parent = pnlNotificaciones,
                Location = new Point(16, 58),

                Size = new Size(
                    pnlNotificaciones.Width - 32,
                    pnlNotificaciones.Height - 74
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                ColumnCount = 1,
                RowCount = 6,
                BackColor = Color.FromArgb(8, 25, 57),
                Padding = new Padding(7),
                Margin = new Padding(0)
            };

            tlpNotificaciones.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    100F
                )
            );

            for (int i = 0; i < 6; i++)
            {
                tlpNotificaciones.RowStyles.Add(
                    new RowStyle(
                        SizeType.Percent,
                        16.666F
                    )
                );

                CrearFilaNotificacion(i);
            }

            pnlNotificaciones.Resize +=
                (sender, e) =>
                {
                    lblCantidadNotificaciones.Left =
                        pnlNotificaciones.Width -
                        lblCantidadNotificaciones.Width -
                        18;

                    RedondearControl(
                        pnlNotificaciones,
                        17
                    );

                    RedondearControl(
                        tlpNotificaciones,
                        12
                    );
                };

            RedondearControl(pnlNotificaciones, 17);
            RedondearControl(lblCantidadNotificaciones, 14);
            RedondearControl(tlpNotificaciones, 12);
        }

        private void CrearFilaNotificacion(int indice)
        {
            Panel fila = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 4),
                BackColor = Color.FromArgb(14, 37, 77)
            };

            Panel barra = new Panel
            {
                Parent = fila,
                Dock = DockStyle.Left,
                Width = 4,
                BackColor = Color.FromArgb(61, 181, 255)
            };

            Label texto = new Label
            {
                Parent = fila,
                Dock = DockStyle.Fill,
                Padding = new Padding(11, 0, 7, 0),
                Text = "Sin informacion",
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(222, 231, 245),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            fila.Resize +=
                (sender, e) =>
                {
                    RedondearControl(fila, 8);
                };

            filasNotificacion.Add(fila);
            barrasNotificacion.Add(barra);
            textosNotificacion.Add(texto);

            tlpNotificaciones.Controls.Add(fila, 0, indice);

            RedondearControl(fila, 8);
        }

        // =========================================================
        // ENGLISH WORLD
        // =========================================================

        private void ConfigurarEnglishWorld()
        {
            pnlEnglishWorld.BackColor =
                Color.FromArgb(13, 34, 73);

            lblTituloEnglishWorld.Parent = pnlEnglishWorld;
            lblTituloEnglishWorld.Text = "English World";
            lblTituloEnglishWorld.Location = new Point(18, 14);
            lblTituloEnglishWorld.Size = new Size(220, 32);
            lblTituloEnglishWorld.ForeColor = Color.White;
            lblTituloEnglishWorld.BackColor = Color.Transparent;

            lblTituloEnglishWorld.Font = new Font(
                "Segoe UI",
                14F,
                FontStyle.Bold
            );

            lblNumeroContenido = new Label
            {
                Parent = pnlEnglishWorld,
                Size = new Size(52, 24),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEnglishWorld.Width - 70,
                    17
                ),

                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(184, 198, 221),
                BackColor = Color.FromArgb(23, 48, 92),

                Font = new Font(
                    "Segoe UI",
                    7.8F,
                    FontStyle.Bold
                )
            };

            lblTipoContenido.Parent = pnlEnglishWorld;
            lblTipoContenido.AutoSize = false;
            lblTipoContenido.Location = new Point(18, 58);
            lblTipoContenido.Size = new Size(300, 21);
            lblTipoContenido.ForeColor = Color.FromArgb(255, 190, 30);
            lblTipoContenido.BackColor = Color.Transparent;

            lblTipoContenido.Font = new Font(
                "Segoe UI",
                8.2F,
                FontStyle.Bold
            );

            lblTituloContenido.Parent = pnlEnglishWorld;
            lblTituloContenido.AutoSize = false;
            lblTituloContenido.Location = new Point(18, 82);

            lblTituloContenido.Size =
                new Size(
                    pnlEnglishWorld.Width - 36,
                    41
                );

            lblTituloContenido.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            lblTituloContenido.ForeColor = Color.White;
            lblTituloContenido.BackColor = Color.Transparent;

            lblTituloContenido.Font = new Font(
                "Segoe UI",
                17F,
                FontStyle.Bold
            );

            lblDetalleContenido.Parent = pnlEnglishWorld;
            lblDetalleContenido.AutoSize = false;
            lblDetalleContenido.Location = new Point(18, 128);

            lblDetalleContenido.Size =
                new Size(
                    pnlEnglishWorld.Width - 36,
                    125
                );

            lblDetalleContenido.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            lblDetalleContenido.ForeColor =
                Color.FromArgb(211, 222, 239);

            lblDetalleContenido.BackColor =
                Color.Transparent;

            lblDetalleContenido.Font =
                new Font("Segoe UI", 9F);

            btnAnteriorContenido = new Button
            {
                Parent = pnlEnglishWorld,
                Size = new Size(98, 34),

                Anchor =
                    AnchorStyles.Bottom |
                    AnchorStyles.Left,

                Location = new Point(
                    18,
                    pnlEnglishWorld.Height - 52
                ),

                Text = "Anterior",
                BackColor = Color.FromArgb(24, 48, 92),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            btnAnteriorContenido.FlatAppearance.BorderColor =
                Color.FromArgb(50, 75, 125);

            btnAnteriorContenido.FlatAppearance.BorderSize = 1;

            btnAnteriorContenido.Click +=
                btnAnteriorContenido_Click;

            btnSiguienteContenido.Parent = pnlEnglishWorld;
            btnSiguienteContenido.Size = new Size(98, 34);

            btnSiguienteContenido.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            btnSiguienteContenido.Location =
                new Point(
                    pnlEnglishWorld.Width - 116,
                    pnlEnglishWorld.Height - 52
                );

            btnSiguienteContenido.Text = "Siguiente";
            btnSiguienteContenido.BackColor =
                Color.FromArgb(20, 151, 165);

            btnSiguienteContenido.ForeColor = Color.White;
            btnSiguienteContenido.FlatStyle = FlatStyle.Flat;
            btnSiguienteContenido.FlatAppearance.BorderSize = 0;

            btnSiguienteContenido.Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold
            );

            btnSiguienteContenido.Cursor = Cursors.Hand;

            pnlEnglishWorld.Resize +=
                (sender, e) =>
                {
                    lblNumeroContenido.Left =
                        pnlEnglishWorld.Width -
                        lblNumeroContenido.Width -
                        18;

                    lblDetalleContenido.Height =
                        Math.Max(
                            55,
                            pnlEnglishWorld.Height - 190
                        );

                    btnAnteriorContenido.Top =
                        pnlEnglishWorld.Height -
                        btnAnteriorContenido.Height -
                        18;

                    btnSiguienteContenido.Left =
                        pnlEnglishWorld.Width -
                        btnSiguienteContenido.Width -
                        18;

                    btnSiguienteContenido.Top =
                        pnlEnglishWorld.Height -
                        btnSiguienteContenido.Height -
                        18;

                    RedondearControl(
                        pnlEnglishWorld,
                        17
                    );
                };

            RedondearControl(pnlEnglishWorld, 17);
            RedondearControl(lblNumeroContenido, 11);
            RedondearControl(btnAnteriorContenido, 11);
            RedondearControl(btnSiguienteContenido, 11);
        }

        // =========================================================
        // FUNCIONES EXTRAS
        // =========================================================

        private void InicializarFuncionesExtras()
        {
            MostrarContenidoEnglish();
            ActualizarFechaHora();
            ActualizarNotificacionesDesdeDashboard();
            ActualizarEstadoSistema();

            ultimoEstadoDashboard =
                ObtenerEstadoDashboard();

            btnSiguienteContenido.Click -=
                btnSiguienteContenido_Click;

            btnSiguienteContenido.Click +=
                btnSiguienteContenido_Click;

            timerExtras =
                new System.Windows.Forms.Timer
                {
                    Interval = 1000
                };

            timerExtras.Tick += TimerExtras_Tick;
            timerExtras.Start();
        }

        private void TimerExtras_Tick(
            object? sender,
            EventArgs e)
        {
            ActualizarFechaHora();

            string estadoActual =
                ObtenerEstadoDashboard();

            if (estadoActual != ultimoEstadoDashboard)
            {
                ultimoEstadoDashboard = estadoActual;

                ActualizarNotificacionesDesdeDashboard();
                ActualizarEstadoSistema();
            }
        }

        private string ObtenerEstadoDashboard()
        {
            return
                lblTotalAlumnos.Text + "|" +
                lblTotalMatriculas.Text + "|" +
                lblTotalInstructores.Text + "|" +
                lblTotalIngresos.Text + "|" +
                lblTotalPendiente.Text + "|" +
                lblNivelPopular.Text;
        }

        private void ActualizarFechaHora()
        {
            CultureInfo cultura =
                new CultureInfo("es-DO");

            lblFechaHora.Text =
                DateTime.Now.ToString(
                    "dddd, dd 'de' MMMM 'de' yyyy",
                    cultura
                ) +
                Environment.NewLine +
                DateTime.Now.ToString(
                    "hh:mm:ss tt",
                    cultura
                );
        }

        // =========================================================
        // NOTIFICACIONES
        // =========================================================

        private void ActualizarNotificacionesDesdeDashboard()
        {
            int totalAlumnos =
                ConvertirEntero(lblTotalAlumnos.Text);

            int totalMatriculas =
                ConvertirEntero(lblTotalMatriculas.Text);

            int totalInstructores =
                ConvertirEntero(lblTotalInstructores.Text);

            decimal totalIngresos =
                ConvertirMoneda(lblTotalIngresos.Text);

            decimal totalPendiente =
                ConvertirMoneda(lblTotalPendiente.Text);

            string nivelPopular =
                lblNivelPopular.Text?.Trim() ??
                string.Empty;

            List<NotificacionDashboard> notificaciones =
                new List<NotificacionDashboard>();

            notificaciones.Add(
                totalPendiente > 0
                    ? new NotificacionDashboard(
                        "Pagos pendientes: RD$" +
                        totalPendiente.ToString("N2"),
                        Color.FromArgb(255, 177, 40)
                    )
                    : new NotificacionDashboard(
                        "No existen pagos pendientes.",
                        Color.FromArgb(48, 216, 150)
                    )
            );

            notificaciones.Add(
                totalAlumnos > 0
                    ? new NotificacionDashboard(
                        "Alumnos registrados: " +
                        totalAlumnos,
                        Color.FromArgb(61, 181, 255)
                    )
                    : new NotificacionDashboard(
                        "No hay alumnos registrados.",
                        Color.FromArgb(61, 181, 255)
                    )
            );

            notificaciones.Add(
                totalMatriculas > 0
                    ? new NotificacionDashboard(
                        "Matriculas activas: " +
                        totalMatriculas,
                        Color.FromArgb(150, 96, 255)
                    )
                    : new NotificacionDashboard(
                        "No hay matriculas activas.",
                        Color.FromArgb(150, 96, 255)
                    )
            );

            notificaciones.Add(
                totalInstructores > 0
                    ? new NotificacionDashboard(
                        "Instructores registrados: " +
                        totalInstructores,
                        Color.FromArgb(48, 216, 150)
                    )
                    : new NotificacionDashboard(
                        "Debes registrar instructores.",
                        Color.FromArgb(255, 94, 118)
                    )
            );

            notificaciones.Add(
                EsNivelValido(nivelPopular)
                    ? new NotificacionDashboard(
                        "Nivel mas popular: " +
                        nivelPopular,
                        Color.FromArgb(55, 205, 255)
                    )
                    : new NotificacionDashboard(
                        "No hay datos del nivel popular.",
                        Color.FromArgb(55, 205, 255)
                    )
            );

            notificaciones.Add(
                totalIngresos > 0
                    ? new NotificacionDashboard(
                        "Ingresos registrados: RD$" +
                        totalIngresos.ToString("N2"),
                        Color.FromArgb(48, 216, 150)
                    )
                    : new NotificacionDashboard(
                        "No se han registrado ingresos.",
                        Color.FromArgb(150, 170, 200)
                    )
            );

            lblCantidadNotificaciones.Text =
                notificaciones.Count.ToString();

            lblCantidadNotificaciones.BackColor =
                totalPendiente > 0 ||
                totalInstructores == 0 ||
                totalAlumnos == 0
                    ? Color.FromArgb(231, 43, 84)
                    : Color.FromArgb(35, 177, 115);

            for (int i = 0;
                 i < filasNotificacion.Count;
                 i++)
            {
                if (i < notificaciones.Count)
                {
                    filasNotificacion[i].Visible = true;

                    textosNotificacion[i].Text =
                        notificaciones[i].Texto;

                    barrasNotificacion[i].BackColor =
                        notificaciones[i].Color;
                }
                else
                {
                    filasNotificacion[i].Visible = false;
                }
            }
        }

        // =========================================================
        // ESTADO
        // =========================================================

        private void ActualizarEstadoSistema()
        {
            int totalAlumnos =
                ConvertirEntero(lblTotalAlumnos.Text);

            int totalMatriculas =
                ConvertirEntero(lblTotalMatriculas.Text);

            int totalInstructores =
                ConvertirEntero(lblTotalInstructores.Text);

            if (totalAlumnos == 0 &&
                totalInstructores == 0)
            {
                lblEstadoGeneral.Text =
                    "Configuracion inicial pendiente";

                lblEstadoGeneral.ForeColor =
                    Color.FromArgb(255, 177, 40);

                lblDetalleEstado.Text =
                    "Comienza registrando instructores y alumnos. " +
                    "Luego podras crear matriculas y registrar pagos.";
            }
            else if (totalAlumnos == 0)
            {
                lblEstadoGeneral.Text =
                    "Faltan alumnos";

                lblEstadoGeneral.ForeColor =
                    Color.FromArgb(61, 181, 255);

                lblDetalleEstado.Text =
                    "Debes registrar alumnos antes de crear sus matriculas.";
            }
            else if (totalInstructores == 0)
            {
                lblEstadoGeneral.Text =
                    "Faltan instructores";

                lblEstadoGeneral.ForeColor =
                    Color.FromArgb(255, 177, 40);

                lblDetalleEstado.Text =
                    "Registra instructores para poder asignarlos a las matriculas.";
            }
            else if (totalMatriculas == 0)
            {
                lblEstadoGeneral.Text =
                    "Faltan matriculas";

                lblEstadoGeneral.ForeColor =
                    Color.FromArgb(55, 205, 255);

                lblDetalleEstado.Text =
                    "Ya existen alumnos e instructores. Puedes registrar matriculas.";
            }
            else
            {
                lblEstadoGeneral.Text =
                    "Sistema preparado";

                lblEstadoGeneral.ForeColor =
                    Color.FromArgb(48, 216, 150);

                lblDetalleEstado.Text =
                    "La informacion principal esta disponible para trabajar.";
            }
        }

        // =========================================================
        // ENGLISH WORLD
        // =========================================================

        private void MostrarContenidoEnglish()
        {
            if (contenidosEnglish.Count == 0)
                return;

            ContenidoEnglish contenido =
                contenidosEnglish[indiceContenidoEnglish];

            lblTipoContenido.Text = contenido.Tipo;
            lblTituloContenido.Text = contenido.Titulo;
            lblDetalleContenido.Text = contenido.Detalle;

            lblNumeroContenido.Text =
                (indiceContenidoEnglish + 1) +
                " / " +
                contenidosEnglish.Count;
        }

        private void btnSiguienteContenido_Click(
            object? sender,
            EventArgs e)
        {
            indiceContenidoEnglish++;

            if (indiceContenidoEnglish >=
                contenidosEnglish.Count)
            {
                indiceContenidoEnglish = 0;
            }

            MostrarContenidoEnglish();
        }

        private void btnAnteriorContenido_Click(
            object? sender,
            EventArgs e)
        {
            indiceContenidoEnglish--;

            if (indiceContenidoEnglish < 0)
            {
                indiceContenidoEnglish =
                    contenidosEnglish.Count - 1;
            }

            MostrarContenidoEnglish();
        }

        // =========================================================
        // CONVERSIONES
        // =========================================================

        private static int ConvertirEntero(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            string limpio =
                texto.Replace(",", string.Empty)
                     .Trim();

            return int.TryParse(
                limpio,
                out int resultado)
                    ? resultado
                    : 0;
        }

        private static decimal ConvertirMoneda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0m;

            string limpio =
                texto.Replace("RD$", string.Empty)
                     .Replace("$", string.Empty)
                     .Trim();

            if (decimal.TryParse(
                limpio,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal resultado))
            {
                return resultado;
            }

            if (decimal.TryParse(
                limpio,
                NumberStyles.Number,
                new CultureInfo("es-DO"),
                out resultado))
            {
                return resultado;
            }

            return 0m;
        }

        private static bool EsNivelValido(string nivel)
        {
            if (string.IsNullOrWhiteSpace(nivel))
                return false;

            if (nivel == "-")
                return false;

            return !nivel.Equals(
                "Sin datos",
                StringComparison.OrdinalIgnoreCase
            );
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
                       Color.FromArgb(4, 18, 48),
                       Color.FromArgb(58, 22, 113),
                       0F))
            {
                ColorBlend mezcla = new ColorBlend
                {
                    Colors = new[]
                    {
                        Color.FromArgb(4, 18, 48),
                        Color.FromArgb(7, 29, 73),
                        Color.FromArgb(29, 30, 94),
                        Color.FromArgb(64, 24, 116)
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

                e.Graphics.FillRectangle(fondo, area);
            }

            DibujarCurvaInferior(e.Graphics);
        }

        private void DibujarCurvaInferior(Graphics graphics)
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
                    alto + 42,
                    ancho * 3 / 4,
                    alto + 57,
                    ancho + 120,
                    alto - 87
                );

                using (Pen lapiz = new Pen(
                    Color.FromArgb(
                        110,
                        99,
                        70,
                        255
                    ),
                    2.2F))
                {
                    graphics.DrawPath(lapiz, curva);
                }
            }
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
        // CIERRE
        // =========================================================

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            LiberarModulosCargados();

            if (timerExtras != null)
            {
                timerExtras.Stop();
                timerExtras.Tick -= TimerExtras_Tick;
                timerExtras.Dispose();
            }

            base.OnFormClosed(e);
        }
    }
}