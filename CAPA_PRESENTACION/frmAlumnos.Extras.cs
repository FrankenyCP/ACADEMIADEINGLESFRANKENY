using CAPA_DATOS;
using CAPA_NEGOCIOS;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    // TODO: Arquitectura en Capas - Continuación parcial (partial class) de frmAlumnos, separa la lógica de negocio/eventos (frmAlumnos.cs) del diseño visual construido por código (este archivo)
    public partial class frmAlumnos
    {
        // =========================================================
        // VARIABLES DEL DISENO
        // =========================================================

        private bool disenoAlumnosInicializado;
        private bool formularioAlumnosMostrado;
        private bool modoIntegrado;

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

        private Panel pnlAccionesPrincipales = null!;
        private Panel pnlAccionesAlumno = null!;
        private Panel pnlBuscador = null!;
        private Panel pnlContenedorGrid = null!;

        private Label lblMarca = null!;
        private Label lblMarcaSubtitulo = null!;
        private Label lblTituloPagina = null!;
        private Label lblSubtituloPagina = null!;

        private Label lblTituloFormulario = null!;
        private Label lblTituloLista = null!;
        private Label lblTotalAlumnos = null!;
        private Label lblPieIzquierdo = null!;
        private Label lblVersion = null!;

        // TODO: Logo - PictureBox donde se carga el logo de la academia (Lexbridge) dentro del menú lateral
        private PictureBox picLogoMenu = null!;

        // TODO: MenuStrip u otra alternativa - Botones que actúan como alternativa al MenuStrip tradicional, forman el menú lateral con las opciones principales del sistema (Dashboard, Alumnos, Niveles, Instructores, Matrículas, Pagos, Reportes, Consulta)
        private Button btnMenuDashboard = null!;
        private Button btnMenuAlumnos = null!;
        private Button btnMenuNiveles = null!;
        private Button btnMenuInstructores = null!;
        private Button btnMenuMatriculas = null!;
        private Button btnMenuPagos = null!;
        private Button btnMenuReportes = null!;
        private Button btnMenuConsulta = null!;

        private Button btnCerrarVentana = null!;

        // =========================================================
        // VARIABLES DEL CORREO
        // =========================================================

        private string correoAntesDeGuardar = string.Empty;
        private string nombreAntesDeGuardar = string.Empty;
        private string apellidoAntesDeGuardar = string.Empty;

        private int cantidadAlumnosAntesDeGuardar;

        private bool verificandoCorreoRegistro;
        private bool datosCorreoCapturados;

        // =========================================================
        // REDUCIR PARPADEO
        // =========================================================

        protected override CreateParams CreateParams
        {
            get
            {
                // No usar WS_EX_COMPOSITED cuando frmAlumnos se integra
                // dentro de frmPrincipal. Puede impedir el repintado del Form hijo.
                return base.CreateParams;
            }
        }

        // =========================================================
        // PREPARAR ANTES DE MOSTRAR
        // =========================================================

        // TODO: Captura de error (try-catch) - Envuelve la inicialización y el ajuste del diseño en try-catch, evitando que un fallo al mostrar el formulario cierre la aplicación de forma forzada
        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            try
            {
                // En modo normal, esta es la inicializacion de respaldo.
                // En modo integrado ya se inicializa en PrepararModoIntegrado().
                if (value && !disenoAlumnosInicializado)
                {
                    InicializarAlumnosSeguro();
                }

                base.SetVisibleCore(value);

                if (!value || IsDisposed)
                    return;

                if (modoIntegrado)
                {
                    AplicarModoIntegrado();
                }
                else
                {
                    AjustarDisenoResponsivo();
                }

                formularioAlumnosMostrado = true;

                PerformLayout();
                Invalidate(true);
                Update();
            }
            catch (Exception ex)
            {
                try
                {
                    base.SetVisibleCore(value);
                }
                catch
                {
                }

                MessageBox.Show(
                    "No se pudo mostrar el modulo de Alumnos:\r\n\r\n" +
                    ex.Message,
                    "Lexbridge - Alumnos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // TODO: Captura de error (try-catch) - Si la inicialización del diseño falla, revierte la bandera disenoAlumnosInicializado para permitir un nuevo intento, evitando dejar el formulario en un estado inconsistente
        private void InicializarAlumnosSeguro()
        {
            if (disenoAlumnosInicializado)
                return;

            SuspendLayout();

            try
            {
                // Solo se marca como inicializado cuando realmente comienza
                // la construccion del diseño.
                disenoAlumnosInicializado = true;

                InicializarDisenoAlumnos();

                PerformLayout();
            }
            catch
            {
                // Permite volver a intentar si la inicializacion falla.
                disenoAlumnosInicializado = false;
                throw;
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        // =========================================================
        // INICIALIZACION
        // =========================================================

        // TODO: Clases creadas según su uso, sin código ajeno - Método orquestador que arma el diseño completo del formulario, delega cada parte a métodos específicos (menú, encabezado, formulario, lista)
        private void InicializarDisenoAlumnos()
        {
            // Configuración de doble buffer movida aquí para evitar
            // duplicar el constructor definido en frmAlumnos.cs.
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );
            this.UpdateStyles();

            ConfigurarFormulario();
            CrearEstructuraPrincipal();
            CrearMenuLateral();
            CrearEncabezado();
            CrearPanelFormulario();
            CrearPanelLista();

            ConfigurarControlesFormulario();
            ConfigurarBotonesPrincipales();
            ConfigurarBotonesEspeciales();
            ConfigurarBuscador();
            ConfigurarDataGridView();
            ConfigurarEventosVisuales();
            ConfigurarRestriccionesEntradas();
            ConfigurarCorreoRegistro();

            AjustarDisenoResponsivo();

            pnlMenuLateral.BringToFront();
            pnlContenido.BringToFront();

            if (modoIntegrado)
            {
                AplicarModoIntegrado();
            }
        }

        private void ConfigurarFormulario()
        {
            Text = "Gestion de Alumnos - Lexbridge";

            if (!modoIntegrado)
            {
                ClientSize = new Size(1420, 800);
                MinimumSize = new Size(1220, 720);
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.Sizable;
                MaximizeBox = true;
            }
            else
            {
                MinimumSize = Size.Empty;
                MaximumSize = Size.Empty;
                StartPosition = FormStartPosition.Manual;
                FormBorderStyle = FormBorderStyle.None;
                MaximizeBox = false;
                WindowState = FormWindowState.Normal;
            }

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

            Controls.Add(pnlMenuLateral);
            Controls.Add(pnlContenido);

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

        // TODO: MenuStrip u otra alternativa - Construye el menú lateral (alternativa visual al MenuStrip) con más de cinco opciones principales: Dashboard, Alumnos, Niveles, Instructores, Matrículas, Pagos, Reportes y Consulta
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

                ForeColor =
                    Color.FromArgb(153, 171, 201),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.8F)
            };

            btnMenuDashboard = CrearBotonMenu(
                "Dashboard",
                "\uE80F",
                8,
                false
            );

            btnMenuAlumnos = CrearBotonMenu(
                "Alumnos",
                "\uE77B",
                60,
                true
            );

            btnMenuNiveles = CrearBotonMenu(
                "Niveles",
                "\uE8EF",
                112,
                false
            );

            btnMenuInstructores = CrearBotonMenu(
                "Instructores",
                "\uE716",
                164,
                false
            );

            btnMenuMatriculas = CrearBotonMenu(
                "Matriculas",
                "\uE787",
                216,
                false
            );

            btnMenuPagos = CrearBotonMenu(
                "Pagos",
                "\uE8C7",
                268,
                false
            );

            btnMenuReportes = CrearBotonMenu(
                "Reportes",
                "\uE9D2",
                320,
                false
            );

            btnMenuConsulta = CrearBotonMenu(
                "Consulta",
                "\uE721",
                372,
                false
            );

            btnMenuDashboard.Click += (sender, e) =>
            {
                VolverAlDashboard();
            };

            // TODO: Opción de entrada - Navegación desde el menú lateral hacia los formularios que permiten agregar datos (Alumnos, Niveles, Instructores, Matrículas, Pagos)
            btnMenuAlumnos.Click += (sender, e) =>
            {
                btnNuevo.PerformClick();
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

            // TODO: Opción consulta - Navegación desde el menú lateral hacia el formulario que permite dar un vistazo a los datos ya guardados (frmConsultaMatriculas)
            btnMenuConsulta.Click += (sender, e) =>
            {
                AbrirFormulario(
                    new frmConsultaMatriculas()
                );
            };

            lblPieIzquierdo = new Label
            {
                Parent = pnlPieMenu,
                Text = "(c) 2026 Lexbridge",
                Location = new Point(5, 27),
                Size = new Size(132, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            lblVersion = new Label
            {
                Parent = pnlPieMenu,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };
        }

        // TODO: Clases creadas según su uso, sin código ajeno - Método fábrica reutilizable que crea cada botón del menú lateral con su ícono y estado (seleccionado o no)
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
                    Math.Max(
                        180,
                        pnlMenuOpciones.ClientSize.Width
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

        // TODO: Logo - Carga el logo de Lexbridge desde los recursos del proyecto y lo asigna al PictureBox del menú lateral
        // TODO: Captura de error (try-catch) - Si el recurso del logo no existe o falla la carga, evita que la aplicación se cierre de forma forzada y deja el PictureBox sin imagen
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

                    if (recurso is Image imagen)
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
                Text = "Gestion de Alumnos",
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
                    "Administra los alumnos, su informacion y progreso academico",

                Location = new Point(35, 55),
                Size = new Size(600, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            btnNuevo.Parent = pnlEncabezado;
            btnNuevo.Size = new Size(140, 40);

            btnNuevo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnNuevo.Location = new Point(
                pnlEncabezado.ClientSize.Width - 305,
                24
            );

            btnNuevo.Text = "Nuevo alumno";

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
        // PANEL FORMULARIO
        // =========================================================

        private void CrearPanelFormulario()
        {
            pnlFormulario = new Panel
            {
                Parent = pnlCuerpo,
                Location = new Point(18, 18),
                Size = new Size(370, 650),

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
                Text = "Informacion del alumno",
                Location = new Point(20, 15),
                Size = new Size(320, 28),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    12.5F,
                    FontStyle.Bold
                )
            };

            pnlAccionesPrincipales = new Panel
            {
                Parent = pnlFormulario,
                Location = new Point(20, 485),
                Size = new Size(330, 145),
                BackColor = Color.Transparent
            };

            RedondearControl(pnlFormulario, 18);
        }

        private void ConfigurarControlesFormulario()
        {
            ConfigurarEtiqueta(
                lblNombre,
                "Nombre *",
                new Point(20, 72)
            );

            ConfigurarTextBox(
                txtNombre,
                new Point(20, 99)
            );

            txtNombre.PlaceholderText =
                "Ingrese el nombre";

            ConfigurarEtiqueta(
                lblApellido,
                "Apellido *",
                new Point(20, 145)
            );

            ConfigurarTextBox(
                txtApellido,
                new Point(20, 172)
            );

            txtApellido.PlaceholderText =
                "Ingrese el apellido";

            ConfigurarEtiqueta(
                lblTelefono,
                "Telefono *",
                new Point(20, 218)
            );

            ConfigurarTextBox(
                txtTelefono,
                new Point(20, 245)
            );

            txtTelefono.PlaceholderText =
                "Ej. 8095551234";

            ConfigurarEtiqueta(
                lblCorreo,
                "Correo *",
                new Point(20, 291)
            );

            ConfigurarTextBox(
                txtCorreo,
                new Point(20, 318)
            );

            txtCorreo.PlaceholderText =
                "correo@ejemplo.com";

            ConfigurarEtiqueta(
                lblFechaNacimiento,
                "Fecha de nacimiento *",
                new Point(20, 364)
            );

            dtpFechaNacimiento.Parent = pnlFormulario;

            dtpFechaNacimiento.Location =
                new Point(20, 391);

            dtpFechaNacimiento.Size =
                new Size(330, 34);

            dtpFechaNacimiento.Font =
                new Font("Segoe UI", 9.5F);

            dtpFechaNacimiento.Format =
                DateTimePickerFormat.Short;

            chkIntensivo.Parent = pnlFormulario;

            chkIntensivo.Location =
                new Point(20, 437);

            chkIntensivo.Size =
                new Size(330, 30);

            chkIntensivo.Text =
                "Modalidad intensiva";

            chkIntensivo.ForeColor = Color.White;
            chkIntensivo.BackColor = Color.Transparent;

            chkIntensivo.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            lblMensaje.Parent = pnlFormulario;
            lblMensaje.AutoSize = false;
            lblMensaje.Location = new Point(20, 463);
            lblMensaje.Size = new Size(330, 22);
            lblMensaje.BackColor = Color.Transparent;

            lblMensaje.Font =
                new Font("Segoe UI", 8F);

            lblMensaje.TextAlign =
                ContentAlignment.MiddleLeft;
        }

        private void ConfigurarEtiqueta(
            Label etiqueta,
            string texto,
            Point posicion)
        {
            etiqueta.Parent = pnlFormulario;
            etiqueta.AutoSize = false;
            etiqueta.Location = posicion;
            etiqueta.Size = new Size(330, 23);
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
            Point posicion)
        {
            caja.Parent = pnlFormulario;
            caja.Location = posicion;
            caja.Size = new Size(330, 34);

            caja.Font =
                new Font("Segoe UI", 9.5F);

            caja.BackColor =
                Color.FromArgb(7, 23, 57);

            caja.ForeColor = Color.White;

            caja.BorderStyle =
                BorderStyle.FixedSingle;
        }

        // =========================================================
        // BOTONES PRINCIPALES
        // =========================================================

        private void ConfigurarBotonesPrincipales()
        {
            btnGuardar.Parent = pnlAccionesPrincipales;
            btnActualizar.Parent = pnlAccionesPrincipales;
            btnLimpiar.Parent = pnlAccionesPrincipales;
            btnEliminar.Parent = pnlAccionesPrincipales;

            ConfigurarBotonAccion(
                btnGuardar,
                "Guardar",
                new Point(0, 0),
                new Size(330, 42),
                Color.FromArgb(10, 146, 153),
                Color.FromArgb(27, 207, 190)
            );

            ConfigurarBotonAccion(
                btnActualizar,
                "Actualizar",
                new Point(0, 52),
                new Size(160, 40),
                Color.FromArgb(17, 86, 135),
                Color.FromArgb(46, 170, 220)
            );

            ConfigurarBotonAccion(
                btnLimpiar,
                "Limpiar",
                new Point(170, 52),
                new Size(160, 40),
                Color.FromArgb(25, 43, 82),
                Color.FromArgb(78, 102, 155)
            );

            ConfigurarBotonAccion(
                btnEliminar,
                "Eliminar",
                new Point(0, 102),
                new Size(330, 42),
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
            boton.UseVisualStyleBackColor = false;

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
        // PANEL LISTA
        // =========================================================

        private void CrearPanelLista()
        {
            pnlLista = new Panel
            {
                Parent = pnlCuerpo,
                Location = new Point(406, 18),
                Size = new Size(760, 650),

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
                Height = 112,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloLista = new Label
            {
                Parent = pnlCabeceraLista,
                Text = "Lista de alumnos",
                Location = new Point(20, 15),
                Size = new Size(240, 30),

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
                Size = new Size(315, 36),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlCabeceraLista.Width - 333,
                    13
                ),

                BackColor =
                    Color.FromArgb(7, 23, 57)
            };

            pnlAccionesAlumno = new Panel
            {
                Parent = pnlCabeceraLista,
                Location = new Point(20, 59),

                Size = new Size(
                    pnlCabeceraLista.Width - 40,
                    41
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor = Color.Transparent
            };

            pnlContenedorGrid = new Panel
            {
                Parent = pnlLista,
                Location = new Point(16, 126),

                Size = new Size(
                    pnlLista.Width - 32,
                    pnlLista.Height - 168
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(6, 22, 54)
            };

            lblTotalAlumnos = new Label
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
                Font = new Font("Segoe UI", 8.5F)
            };

            RedondearControl(pnlLista, 18);
            RedondearControl(pnlContenedorGrid, 12);
        }

        // =========================================================
        // BOTONES ESPECIALES
        // =========================================================

        private void ConfigurarBotonesEspeciales()
        {
            btnInfoAlumno.Parent = pnlAccionesAlumno;
            btnPromover.Parent = pnlAccionesAlumno;
            btnVerNivel.Parent = pnlAccionesAlumno;

            ConfigurarBotonSecundario(
                btnInfoAlumno,
                "Ver informacion",
                new Point(0, 0),
                new Size(155, 38),
                Color.FromArgb(17, 86, 135)
            );

            ConfigurarBotonSecundario(
                btnPromover,
                "Promover",
                new Point(165, 0),
                new Size(125, 38),
                Color.FromArgb(72, 42, 132)
            );

            ConfigurarBotonSecundario(
                btnVerNivel,
                "Ver nivel",
                new Point(300, 0),
                new Size(125, 38),
                Color.FromArgb(25, 101, 121)
            );
        }

        private void ConfigurarBotonSecundario(
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
                AclararColor(fondo, 30);

            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                AclararColor(fondo, 15);

            boton.FlatAppearance.MouseDownBackColor =
                OscurecerColor(fondo, 12);

            boton.Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold
            );

            boton.Cursor = Cursors.Hand;

            RedondearControl(boton, 10);
        }

        // =========================================================
        // BUSCADOR
        // =========================================================

        private void ConfigurarBuscador()
        {
            lblBuscar.Visible = false;

            txtBuscar.Parent = pnlBuscador;
            txtBuscar.Location = new Point(12, 7);
            txtBuscar.Size = new Size(290, 24);

            txtBuscar.BorderStyle =
                BorderStyle.None;

            txtBuscar.BackColor =
                Color.FromArgb(7, 23, 57);

            txtBuscar.ForeColor = Color.White;

            txtBuscar.Font =
                new Font("Segoe UI", 8.8F);

            txtBuscar.PlaceholderText =
                "Buscar por nombre o apellido...";

            RedondearControl(pnlBuscador, 10);
        }

        // =========================================================
        // DATAGRIDVIEW
        // =========================================================

        // TODO: Opción consulta - Configura la grilla que permite dar un vistazo a los datos ya guardados (encabezados, colores, selección de solo lectura)
        private void ConfigurarDataGridView()
        {
            dgvAlumnos.Parent = pnlContenedorGrid;
            dgvAlumnos.Dock = DockStyle.Fill;

            dgvAlumnos.BorderStyle =
                BorderStyle.None;

            dgvAlumnos.BackgroundColor =
                Color.FromArgb(6, 22, 54);

            dgvAlumnos.GridColor =
                Color.FromArgb(32, 56, 103);

            dgvAlumnos.EnableHeadersVisualStyles =
                false;

            dgvAlumnos.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvAlumnos.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(15, 39, 83);

            dgvAlumnos.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvAlumnos.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold
                );

            dgvAlumnos.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvAlumnos.ColumnHeadersHeight = 42;

            dgvAlumnos.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvAlumnos.DefaultCellStyle.BackColor =
                Color.FromArgb(8, 27, 63);

            dgvAlumnos.DefaultCellStyle.ForeColor =
                Color.FromArgb(226, 233, 245);

            dgvAlumnos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(60, 33, 112);

            dgvAlumnos.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvAlumnos.DefaultCellStyle.Font =
                new Font("Segoe UI", 8.5F);

            dgvAlumnos.DefaultCellStyle.Padding =
                new Padding(6, 0, 6, 0);

            dgvAlumnos.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(10, 31, 70);

            dgvAlumnos.RowTemplate.Height = 40;
            dgvAlumnos.RowHeadersVisible = false;

            dgvAlumnos.AllowUserToAddRows = false;
            dgvAlumnos.AllowUserToDeleteRows = false;
            dgvAlumnos.AllowUserToResizeRows = false;

            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.ReadOnly = true;

            dgvAlumnos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvAlumnos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvAlumnos.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvAlumnos.DataBindingComplete +=
                dgvAlumnos_DataBindingComplete;
        }

        // TODO: Captura de error (try-catch) - Envuelve la configuración de columnas en try-catch para que un error de formato en la grilla no interrumpa el formulario
        private void dgvAlumnos_DataBindingComplete(
            object? sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                ConfigurarColumna(
                    "IdAlumno",
                    "ID",
                    28
                );

                ConfigurarColumna(
                    "Nombre",
                    "Nombre",
                    65
                );

                ConfigurarColumna(
                    "Apellido",
                    "Apellido",
                    65
                );

                ConfigurarColumna(
                    "FechaNacimiento",
                    "Fecha de nacimiento",
                    75
                );

                ConfigurarColumna(
                    "Telefono",
                    "Telefono",
                    65
                );

                ConfigurarColumna(
                    "Correo",
                    "Correo",
                    105
                );

                if (dgvAlumnos.Columns["FechaNacimiento"] != null)
                {
                    dgvAlumnos.Columns["FechaNacimiento"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";
                }

                dgvAlumnos.ClearSelection();
                ActualizarTotalAlumnos();
            }
            catch
            {
                // No interfiere con el formulario.
            }
        }

        private void ConfigurarColumna(
            string nombre,
            string encabezado,
            float peso)
        {
            if (dgvAlumnos.Columns[nombre] == null)
                return;

            dgvAlumnos.Columns[nombre].HeaderText =
                encabezado;

            dgvAlumnos.Columns[nombre].FillWeight =
                peso;
        }

        private void ActualizarTotalAlumnos()
        {
            if (lblTotalAlumnos == null ||
                dgvAlumnos == null)
            {
                return;
            }

            int cantidad =
                dgvAlumnos.Rows
                    .Cast<DataGridViewRow>()
                    .Count(fila =>
                        !fila.IsNewRow &&
                        fila.Visible);

            lblTotalAlumnos.Text =
                cantidad == 1
                    ? "Mostrando 1 alumno"
                    : "Mostrando " +
                      cantidad +
                      " alumnos";
        }

        // =========================================================
        // CONFIGURACION DEL CORREO
        // =========================================================


        // =========================================================
        // RESTRICCIONES DE ENTRADA
        // =========================================================

        private bool aplicandoRestriccionEntrada;

        // TODO: Clases creadas según su uso, sin código ajeno - Reemplaza los manejadores originales de Guardar/Actualizar por versiones "seguras" que primero validan, sin duplicar la lógica ya definida en frmAlumnos.cs
        private void ConfigurarRestriccionesEntradas()
        {
            txtNombre.MaxLength = 50;
            txtApellido.MaxLength = 50;
            txtTelefono.MaxLength = 10;
            txtCorreo.MaxLength = 100;

            txtNombre.TextChanged +=
                (sender, e) =>
                {
                    FiltrarSoloLetras(
                        txtNombre,
                        "El nombre solo puede contener letras."
                    );
                };

            txtApellido.TextChanged +=
                (sender, e) =>
                {
                    FiltrarSoloLetras(
                        txtApellido,
                        "El apellido solo puede contener letras."
                    );
                };

            txtTelefono.TextChanged +=
                (sender, e) =>
                {
                    FiltrarSoloNumeros(
                        txtTelefono,
                        "El teléfono solo puede contener números."
                    );
                };

            /*
             * Sustituye únicamente la entrada a Guardar y Actualizar.
             * La lógica original sigue estando en frmAlumnos.cs,
             * pero ahora solo se ejecuta después de validar.
             */
            btnGuardar.Click -= btnGuardar_Click;
            btnGuardar.Click += btnGuardarSeguro_Click;

            btnActualizar.Click -= btnActualizar_Click;
            btnActualizar.Click += btnActualizarSeguro_Click;
        }

        // TODO: Opción de entrada - Punto de entrada seguro que valida los datos extra antes de invocar el guardado real definido en frmAlumnos.cs
        private void btnGuardarSeguro_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidarDatosAlumnoExtra())
                return;

            btnGuardar_Click(sender, e);
        }

        private void btnActualizarSeguro_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidarDatosAlumnoExtra())
                return;

            btnActualizar_Click(sender, e);
        }

        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado de validación extra (nombre, apellido, teléfono, correo) antes de permitir guardar/actualizar
        private bool ValidarDatosAlumnoExtra()
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return MostrarErrorYEnfocar(
                    txtNombre,
                    "El nombre es obligatorio."
                );
            }

            if (!nombre.All(caracter =>
                    char.IsLetter(caracter) ||
                    char.IsWhiteSpace(caracter)))
            {
                return MostrarErrorYEnfocar(
                    txtNombre,
                    "El nombre solo puede contener letras."
                );
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                return MostrarErrorYEnfocar(
                    txtApellido,
                    "El apellido es obligatorio."
                );
            }

            if (!apellido.All(caracter =>
                    char.IsLetter(caracter) ||
                    char.IsWhiteSpace(caracter)))
            {
                return MostrarErrorYEnfocar(
                    txtApellido,
                    "El apellido solo puede contener letras."
                );
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                return MostrarErrorYEnfocar(
                    txtTelefono,
                    "El teléfono es obligatorio."
                );
            }

            if (!telefono.All(char.IsDigit))
            {
                return MostrarErrorYEnfocar(
                    txtTelefono,
                    "El teléfono solo puede contener números."
                );
            }

            if (telefono.Length != 10)
            {
                return MostrarErrorYEnfocar(
                    txtTelefono,
                    "El teléfono debe contener exactamente 10 números."
                );
            }

            if (string.IsNullOrWhiteSpace(correo))
            {
                return MostrarErrorYEnfocar(
                    txtCorreo,
                    "El correo es obligatorio."
                );
            }

            LimpiarMensajeRestriccion();
            return true;
        }

        private bool MostrarErrorYEnfocar(
            Control control,
            string mensaje)
        {
            MostrarMensajeRestriccion(mensaje);
            control.Focus();
            return false;
        }

        private void FiltrarSoloLetras(
            TextBox caja,
            string mensaje)
        {
            if (aplicandoRestriccionEntrada)
                return;

            string textoOriginal = caja.Text;

            string textoFiltrado = new string(
                textoOriginal
                    .Where(caracter =>
                        char.IsLetter(caracter) ||
                        char.IsWhiteSpace(caracter))
                    .ToArray()
            );

            if (textoOriginal == textoFiltrado)
                return;

            aplicandoRestriccionEntrada = true;

            int posicionCursor = caja.SelectionStart;

            caja.Text = textoFiltrado;
            caja.SelectionStart = Math.Min(
                posicionCursor,
                caja.Text.Length
            );

            aplicandoRestriccionEntrada = false;

            MostrarMensajeRestriccion(mensaje);
        }

        private void FiltrarSoloNumeros(
            TextBox caja,
            string mensaje)
        {
            if (aplicandoRestriccionEntrada)
                return;

            string textoOriginal = caja.Text;

            string textoFiltrado = new string(
                textoOriginal
                    .Where(char.IsDigit)
                    .Take(10)
                    .ToArray()
            );

            if (textoOriginal == textoFiltrado)
                return;

            aplicandoRestriccionEntrada = true;

            int posicionCursor = caja.SelectionStart;

            caja.Text = textoFiltrado;
            caja.SelectionStart = Math.Min(
                posicionCursor,
                caja.Text.Length
            );

            aplicandoRestriccionEntrada = false;

            MostrarMensajeRestriccion(mensaje);
        }

        private void MostrarMensajeRestriccion(
            string mensaje)
        {
            if (lblMensaje == null)
                return;

            lblMensaje.ForeColor =
                Color.FromArgb(255, 105, 135);

            lblMensaje.Text = mensaje;
        }

        private void LimpiarMensajeRestriccion()
        {
            if (lblMensaje == null)
                return;

            lblMensaje.Text = string.Empty;
        }

        // TODO: Interfaces Y Asincrónicos - Conecta eventos adicionales (MouseDown, Click, KeyDown) del botón Guardar para disparar el envío de correo de confirmación tras el registro, usando ServicioCorreo (CAPA_NEGOCIOS)
        private void ConfigurarCorreoRegistro()
        {
            /*
             * MouseDown captura los datos antes de que el evento
             * original de Guardar pueda limpiar los campos.
             */
            btnGuardar.MouseDown +=
                btnGuardar_MouseDownCorreo;

            /*
             * Este Click adicional no sustituye el evento original.
             * Se limita a comprobar si el alumno fue registrado.
             */
            btnGuardar.Click +=
                btnGuardar_ClickCorreoExtra;

            /*
             * Permite capturar los datos si Guardar se activa con
             * el teclado en lugar de hacer clic con el mouse.
             */
            btnGuardar.KeyDown +=
                btnGuardar_KeyDownCorreo;
        }

        private void btnGuardar_MouseDownCorreo(
            object? sender,
            MouseEventArgs e)
        {
            CapturarDatosPreviosCorreo();
        }

        private void btnGuardar_KeyDownCorreo(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Space)
            {
                CapturarDatosPreviosCorreo();
            }
        }

        // TODO: Captura de error (try-catch) - Si la captura de datos previos falla, limpia las variables temporales en vez de dejar el formulario en un estado inconsistente
        private void CapturarDatosPreviosCorreo()
        {
            try
            {
                correoAntesDeGuardar =
                    txtCorreo.Text.Trim();

                nombreAntesDeGuardar =
                    txtNombre.Text.Trim();

                apellidoAntesDeGuardar =
                    txtApellido.Text.Trim();

                cantidadAlumnosAntesDeGuardar =
                    alumnoCD.ObtenerTodos().Count;

                datosCorreoCapturados = true;
            }
            catch
            {
                correoAntesDeGuardar = string.Empty;
                nombreAntesDeGuardar = string.Empty;
                apellidoAntesDeGuardar = string.Empty;

                cantidadAlumnosAntesDeGuardar = 0;
                datosCorreoCapturados = false;
            }
        }

        // TODO: Llamadas asíncronas - Evento async void que espera (mediante EsperarRegistroAlumnoAsync) a que termine el guardado original antes de enviar el correo de confirmación con await
        // TODO: Captura de error (try-catch) - Doble manejo de errores: uno específico para el envío de correo (no bloquea el registro ya exitoso) y otro general para la verificación completa
        private async void btnGuardar_ClickCorreoExtra(
            object? sender,
            EventArgs e)
        {
            if (verificandoCorreoRegistro)
                return;

            /*
             * Si Guardar fue ejecutado por PerformClick u otra forma,
             * intenta capturar los datos en este momento.
             */
            if (!datosCorreoCapturados)
            {
                CapturarDatosPreviosCorreo();
            }

            if (string.IsNullOrWhiteSpace(
                    correoAntesDeGuardar))
            {
                LimpiarDatosTemporalesCorreo();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                    nombreAntesDeGuardar))
            {
                LimpiarDatosTemporalesCorreo();
                return;
            }

            verificandoCorreoRegistro = true;

            try
            {
                bool registrado =
                    await EsperarRegistroAlumnoAsync();

                if (!registrado)
                {
                    /*
                     * No se envia correo si el guardado original
                     * fallo, fue cancelado o encontro duplicados.
                     */
                    return;
                }

                string nombreCompleto =
                    (nombreAntesDeGuardar + " " +
                     apellidoAntesDeGuardar).Trim();

                try
                {
                    await servicioCorreoRegistro
                    .EnviarRegistroExitosoAsync(
                   correoAntesDeGuardar,
                    nombreCompleto
                       );

                    MessageBox.Show(
                        "La confirmacion de registro fue enviada a:\r\n\r\n" +
                        correoAntesDeGuardar,
                        "Correo enviado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception exCorreo)
                {
                    MessageBox.Show(
                        "El alumno fue registrado correctamente, " +
                        "pero no se pudo enviar el correo de confirmacion.\r\n\r\n" +
                        "Motivo: " +
                        exCorreo.Message,
                        "Correo no enviado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo verificar la confirmacion por correo:\r\n" +
                    ex.Message,
                    "Confirmacion de registro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            finally
            {
                verificandoCorreoRegistro = false;
                LimpiarDatosTemporalesCorreo();
            }
        }

        // TODO: Llamadas asíncronas - Método asíncrono que usa Task.Delay en un bucle de reintentos (polling) para esperar a que el evento async void original termine de insertar el alumno en la base de datos
        // TODO: Captura de error (try-catch) - Ignora errores puntuales durante el sondeo, ya que la operación original todavía podría estar en curso
        private async Task<bool> EsperarRegistroAlumnoAsync()
        {
            /*
             * El evento original de Guardar es async void.
             * Por eso este evento adicional debe esperar a que
             * termine la operacion de base de datos.
             */

            const int intentosMaximos = 20;
            const int esperaMilisegundos = 300;

            for (int intento = 0;
                 intento < intentosMaximos;
                 intento++)
            {
                await Task.Delay(
                    esperaMilisegundos
                );

                try
                {
                    int cantidadActual =
                        alumnoCD.ObtenerTodos().Count;

                    bool cantidadAumento =
                        cantidadActual >
                        cantidadAlumnosAntesDeGuardar;

                    if (!cantidadAumento)
                        continue;

                    bool alumnoExiste =
                        await alumnoCD.ExisteDuplicadoAsync(
                            nombreAntesDeGuardar,
                            apellidoAntesDeGuardar,
                            correoAntesDeGuardar,
                            0
                        );

                    if (alumnoExiste)
                        return true;
                }
                catch
                {
                    /*
                     * Se vuelve a intentar mientras el evento
                     * original termina la operacion.
                     */
                }
            }

            return false;
        }

        private void LimpiarDatosTemporalesCorreo()
        {
            correoAntesDeGuardar = string.Empty;
            nombreAntesDeGuardar = string.Empty;
            apellidoAntesDeGuardar = string.Empty;

            cantidadAlumnosAntesDeGuardar = 0;
            datosCorreoCapturados = false;
        }

        // =========================================================
        // EVENTOS VISUALES
        // =========================================================

        private bool ajustandoLayoutAlumnos;

        private void ConfigurarEventosVisuales()
        {
            pnlCuerpo.Resize += (sender, e) =>
            {
                if (ajustandoLayoutAlumnos ||
                    !IsHandleCreated ||
                    WindowState == FormWindowState.Minimized)
                {
                    return;
                }

                ajustandoLayoutAlumnos = true;

                try
                {
                    AjustarDisenoResponsivo();
                }
                finally
                {
                    ajustandoLayoutAlumnos = false;
                }
            };

            dgvAlumnos.DataSourceChanged +=
                (sender, e) =>
                {
                    ActualizarTotalAlumnos();
                };

            txtNombre.Enter += CampoTexto_Enter;
            txtApellido.Enter += CampoTexto_Enter;
            txtTelefono.Enter += CampoTexto_Enter;
            txtCorreo.Enter += CampoTexto_Enter;
            txtBuscar.Enter += CampoTexto_Enter;

            txtNombre.Leave += CampoTexto_Leave;
            txtApellido.Leave += CampoTexto_Leave;
            txtTelefono.Leave += CampoTexto_Leave;
            txtCorreo.Leave += CampoTexto_Leave;
            txtBuscar.Leave += CampoTexto_Leave;
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
        // DIMENSIONES RESPONSIVAS
        // =========================================================

        private void AjustarDisenoResponsivo()
        {
            if (!disenoAlumnosInicializado ||
                pnlCuerpo == null ||
                pnlFormulario == null ||
                pnlLista == null)
            {
                return;
            }

            const int margen = 18;
            const int separacion = 18;
            const int anchoMinimoFormulario = 360;
            const int anchoMaximoFormulario = 500;
            const int anchoMinimoLista = 620;
            const int altoMinimoPaneles = 620;

            int anchoInterior = Math.Max(
                980,
                pnlCuerpo.ClientSize.Width -
                (margen * 2)
            );

            int anchoFormulario = Math.Max(
                anchoMinimoFormulario,
                Math.Min(
                    anchoMaximoFormulario,
                    (int)(anchoInterior * 0.34)
                )
            );

            int anchoLista = Math.Max(
                anchoMinimoLista,
                anchoInterior -
                anchoFormulario -
                separacion
            );

            int altoPaneles = Math.Max(
                altoMinimoPaneles,
                pnlCuerpo.ClientSize.Height -
                (margen * 2)
            );

            pnlFormulario.SetBounds(
                margen,
                margen,
                anchoFormulario,
                altoPaneles
            );

            pnlLista.SetBounds(
                margen + anchoFormulario + separacion,
                margen,
                anchoLista,
                altoPaneles
            );

            int anchoTotalNecesario =
                margen +
                anchoFormulario +
                separacion +
                anchoLista +
                margen;

            pnlCuerpo.AutoScrollMinSize = new Size(
                anchoTotalNecesario >
                pnlCuerpo.ClientSize.Width
                    ? anchoTotalNecesario
                    : 0,
                altoPaneles + (margen * 2)
            );

            int anchoCampo =
                pnlFormulario.ClientSize.Width - 40;

            foreach (Control control in
                     pnlFormulario.Controls)
            {
                if (control is TextBox ||
                    control is ComboBox ||
                    control is DateTimePicker)
                {
                    control.Width = anchoCampo;
                }
            }

            pnlAccionesPrincipales.Left = 20;
            pnlAccionesPrincipales.Width = anchoCampo;

            AjustarBotonesPrincipalesIntegrados();

            pnlAccionesPrincipales.Top =
                pnlFormulario.ClientSize.Height -
                pnlAccionesPrincipales.Height -
                18;

            AjustarNuevoJuntoAlBuscador();

            pnlContenedorGrid.SetBounds(
                16,
                126,
                Math.Max(
                    300,
                    pnlLista.ClientSize.Width - 32
                ),
                Math.Max(
                    220,
                    pnlLista.ClientSize.Height - 168
                )
            );

            lblTotalAlumnos.Location = new Point(
                20,
                pnlLista.ClientSize.Height - 32
            );

            RedondearControl(pnlFormulario, 18);
            RedondearControl(pnlLista, 18);
            RedondearControl(pnlContenedorGrid, 12);
        }

        private void AjustarBotonesPrincipalesIntegrados()
        {
            if (pnlAccionesPrincipales == null)
                return;

            const int separacionHorizontal = 12;
            const int separacionVertical = 10;
            const int alto = 42;

            int anchoTotal =
                pnlAccionesPrincipales.ClientSize.Width;

            if (anchoTotal <= 0)
                return;

            int anchoMitad =
                (anchoTotal - separacionHorizontal) / 2;

            btnGuardar.SetBounds(
                0,
                0,
                anchoTotal,
                alto
            );

            btnActualizar.SetBounds(
                0,
                alto + separacionVertical,
                anchoMitad,
                alto
            );

            btnLimpiar.SetBounds(
                anchoMitad + separacionHorizontal,
                alto + separacionVertical,
                anchoMitad,
                alto
            );

            btnEliminar.SetBounds(
                0,
                (alto * 2) +
                (separacionVertical * 2),
                anchoTotal,
                alto
            );

            pnlAccionesPrincipales.Height =
                btnEliminar.Bottom;

            RedondearControl(btnGuardar, 11);
            RedondearControl(btnActualizar, 11);
            RedondearControl(btnLimpiar, 11);
            RedondearControl(btnEliminar, 11);
        }

        private void AjustarNuevoJuntoAlBuscador()
        {
            if (!modoIntegrado ||
                btnNuevo == null ||
                pnlCabeceraLista == null ||
                pnlBuscador == null)
            {
                return;
            }

            const int margenDerecho = 18;
            const int separacion = 10;
            const int anchoNuevo = 145;
            const int alto = 36;

            btnNuevo.Parent = pnlCabeceraLista;
            btnNuevo.Visible = true;
            btnNuevo.Enabled = true;
            btnNuevo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            pnlBuscador.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            pnlBuscador.Left =
                pnlCabeceraLista.ClientSize.Width -
                pnlBuscador.Width -
                margenDerecho;

            pnlBuscador.Top = 13;

            btnNuevo.SetBounds(
                pnlBuscador.Left -
                anchoNuevo -
                separacion,
                13,
                anchoNuevo,
                alto
            );

            btnNuevo.Text = "Nuevo alumno";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.BringToFront();

            RedondearControl(btnNuevo, 10);
        }

        private void AjustarBloqueAccionesEquilibrado()
        {
            if (modoIntegrado)
            {
                AjustarDisenoResponsivo();
            }
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

        // TODO: Captura de error (try-catch) - Envuelve la apertura de otros formularios en try-catch para evitar el cierre forzado si la navegación falla
        // TODO: Clases creadas según su uso, sin código ajeno - Método genérico reutilizable para abrir cualquier formulario del sistema, evitando duplicar instancias ya abiertas
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

            Region? regionAnterior =
                control.Region;

            control.Region =
                new Region(ruta);

            regionAnterior?.Dispose();
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

            DibujarCurvaInferior(
                e.Graphics
            );
        }

        private void DibujarCurvaInferior(
            Graphics graphics)
        {
            graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;

            using GraphicsPath curva =
                new GraphicsPath();

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

            using Pen lapiz =
                new Pen(
                    Color.FromArgb(
                        125,
                        93,
                        58,
                        255
                    ),
                    2.3F
                );

            graphics.DrawPath(
                lapiz,
                curva
            );
        }


        private void AjustarBotonesResponsivos(
            Panel panel,
            params Button[] botones)
        {
            if (panel == null ||
                panel.IsDisposed ||
                botones == null ||
                botones.Length < 4)
            {
                return;
            }

            Button guardar = botones[0];
            Button actualizar = botones[1];
            Button limpiar = botones[2];
            Button eliminar = botones[3];

            if (guardar == null ||
                actualizar == null ||
                limpiar == null ||
                eliminar == null)
            {
                return;
            }

            const int separacionHorizontal = 12;
            const int separacionVertical = 10;
            const int altoBoton = 42;

            int anchoTotal =
                panel.ClientSize.Width;

            if (anchoTotal <= 0)
                return;

            int anchoMitad =
                (anchoTotal - separacionHorizontal) / 2;

            guardar.SetBounds(
                0,
                0,
                anchoTotal,
                altoBoton
            );

            actualizar.SetBounds(
                0,
                altoBoton + separacionVertical,
                anchoMitad,
                altoBoton
            );

            limpiar.SetBounds(
                anchoMitad + separacionHorizontal,
                altoBoton + separacionVertical,
                anchoMitad,
                altoBoton
            );

            eliminar.SetBounds(
                0,
                (altoBoton * 2) +
                (separacionVertical * 2),
                anchoTotal,
                altoBoton
            );

            ConfigurarBotonFinal(guardar);
            ConfigurarBotonFinal(actualizar);
            ConfigurarBotonFinal(limpiar);
            ConfigurarBotonFinal(eliminar);

            panel.Height = eliminar.Bottom;
        }

        private void ConfigurarBotonFinal(
            Button boton)
        {
            if (boton == null ||
                boton.IsDisposed)
            {
                return;
            }

            boton.AutoSize = false;
            boton.Margin = Padding.Empty;
            boton.Padding = Padding.Empty;

            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;

            boton.UseVisualStyleBackColor = false;

            boton.TextAlign =
                ContentAlignment.MiddleCenter;

            boton.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold
            );

            boton.ForeColor = Color.White;
            boton.Cursor = Cursors.Hand;
        }

        private void ConfigurarBotonIntegrado(
            Button boton,
            Point posicion,
            Size tamano)
        {
            if (boton == null ||
                boton.IsDisposed)
            {
                return;
            }

            boton.SuspendLayout();

            boton.Location = posicion;
            boton.Size = tamano;

            boton.AutoSize = false;
            boton.Margin = Padding.Empty;
            boton.Padding = Padding.Empty;

            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 1;

            boton.UseVisualStyleBackColor = false;
            boton.TextAlign =
                ContentAlignment.MiddleCenter;

            boton.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Bold
            );

            boton.ForeColor = Color.White;
            boton.Cursor = Cursors.Hand;

            boton.ResumeLayout(true);
        }

        private void AjustarBotonNuevoIntegrado()
        {
            if (modoIntegrado)
            {
                AjustarDisenoResponsivo();
            }
        }

        private void ReajustarModoIntegradoAlMostrar()
        {
            if (modoIntegrado)
            {
                AjustarDisenoResponsivo();
            }
        }

        // =========================================================
        // *** CORRECCIÓN PRINCIPAL: MODO INTEGRADO ***
        // =========================================================

        // TODO: Captura de error (try-catch) - Envuelve la preparación del modo integrado (embebido dentro de frmPrincipal) en try-catch, relanzando el error tras notificar al usuario
        public void PrepararModoIntegrado()
        {
            try
            {
                modoIntegrado = true;

                TopLevel = false;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                StartPosition = FormStartPosition.Manual;

                MinimumSize = Size.Empty;
                MaximumSize = Size.Empty;

                Margin = Padding.Empty;
                Padding = Padding.Empty;

                AutoScaleMode = AutoScaleMode.Dpi;
                AutoScroll = false;

                // CLAVE:
                // Inicializar AHORA, antes de que frmPrincipal llame Show().
                // Así Alumnos no depende de SetVisibleCore para existir visualmente.
                if (!disenoAlumnosInicializado)
                {
                    InicializarAlumnosSeguro();
                }

                AplicarModoIntegrado();

                Dock = DockStyle.Fill;
                Visible = true;

                BringToFront();
                PerformLayout();
                Invalidate(true);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo preparar Alumnos para el panel principal:\r\n\r\n" +
                    ex.Message,
                    "Lexbridge - Alumnos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                throw;
            }
        }

        // TODO: Arquitectura en Capas - Ajusta la interfaz cuando frmAlumnos se embebe dentro de frmPrincipal (formulario principal), ocultando el menú lateral y encabezado propios para no duplicar la navegación
        private void AplicarModoIntegrado()
        {
            if (!modoIntegrado)
                return;

            SuspendLayout();

            try
            {
                // 1. Configurar el formulario principal
                this.MinimumSize = Size.Empty;
                this.MaximumSize = Size.Empty;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.Manual;
                this.AutoScaleMode = AutoScaleMode.Dpi;
                this.Dock = DockStyle.Fill;
                this.Margin = Padding.Empty;
                this.Padding = Padding.Empty;

                // 2. *** OCULTAR COMPLETAMENTE EL MENÚ LATERAL ***
                if (pnlMenuLateral != null)
                {
                    pnlMenuLateral.Visible = false;
                    pnlMenuLateral.Enabled = false;
                    pnlMenuLateral.Dock = DockStyle.None;
                    pnlMenuLateral.Width = 0;
                    pnlMenuLateral.Height = 0;
                    pnlMenuLateral.SendToBack();
                }

                // 3. *** OCULTAR COMPLETAMENTE EL ENCABEZADO ***
                if (pnlEncabezado != null)
                {
                    pnlEncabezado.Visible = false;
                    pnlEncabezado.Enabled = false;
                    pnlEncabezado.Dock = DockStyle.None;
                    pnlEncabezado.Height = 0;
                    pnlEncabezado.SendToBack();
                }

                // 4. Configurar el panel de contenido para que ocupe TODO el espacio
                if (pnlContenido != null)
                {
                    pnlContenido.Visible = true;
                    pnlContenido.Enabled = true;
                    pnlContenido.Dock = DockStyle.Fill;
                    pnlContenido.Location = Point.Empty;
                    pnlContenido.Margin = Padding.Empty;
                    pnlContenido.Padding = Padding.Empty;
                    pnlContenido.BringToFront();
                }

                // 5. Configurar el cuerpo para que ocupe TODO el espacio
                if (pnlCuerpo != null)
                {
                    pnlCuerpo.Visible = true;
                    pnlCuerpo.Enabled = true;
                    pnlCuerpo.Dock = DockStyle.Fill;
                    pnlCuerpo.Location = Point.Empty;
                    pnlCuerpo.Margin = Padding.Empty;
                    pnlCuerpo.Padding = new Padding(18);
                    pnlCuerpo.BringToFront();
                }

                // 6. Forzar que los botones de navegación del módulo NO se muestren
                btnMenuDashboard.Visible = false;
                btnMenuAlumnos.Visible = false;
                btnMenuNiveles.Visible = false;
                btnMenuInstructores.Visible = false;
                btnMenuMatriculas.Visible = false;
                btnMenuPagos.Visible = false;
                btnMenuReportes.Visible = false;
                btnMenuConsulta.Visible = false;

                // 7. Ocultar el botón de cerrar ventana
                if (btnCerrarVentana != null)
                {
                    btnCerrarVentana.Visible = false;
                }

                // 8. Ajustar el diseño responsivo
                AjustarDisenoResponsivo();

                // 9. Forzar actualización visual
                this.Refresh();
                this.Invalidate(true);
                this.Update();
            }
            finally
            {
                ResumeLayout(true);
            }
        }
    }
}