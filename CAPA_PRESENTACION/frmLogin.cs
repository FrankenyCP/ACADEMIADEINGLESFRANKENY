using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmLogin : Form
    {
        // Paneles principales
        private Panel pnlBarraSuperior;
        private Panel pnlMarca;
        private Panel pnlTarjeta;

        // Marca
        private PictureBox picLogo;
        private Label lblCopyright;

        // Inicio con Windows
        private Button btnWindows;
        private Label lblSeparador;

        // Ventana
        private Button btnCerrar;
        private Button btnMinimizar;

        private bool disenoAplicado;
        private bool formularioPrincipalAbierto;

        // Permite mover el formulario sin bordes
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            int lParam
        );

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public frmLogin()
        {
            InitializeComponent();
            AplicarDiseno();
        }

        private void AplicarDiseno()
        {
            if (disenoAplicado)
                return;

            disenoAplicado = true;

            SuspendLayout();

            Text = "Lexbridge - Inicio de sesión";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1000, 650);

            BackColor = Color.FromArgb(5, 21, 53);
            DoubleBuffered = true;

            // Permite presionar Enter para ingresar.
            AcceptButton = btnIngresar;

            CrearBarraSuperior();
            CrearPanelMarca();
            CrearTarjetaLogin();
            ConfigurarControlesOriginales();
            CrearBotonWindows();
            OrganizarControles();

            Resize += frmLogin_Resize;

            ResumeLayout(false);
        }

        // ====================================================
        // BARRA SUPERIOR
        // ====================================================

        private void CrearBarraSuperior()
        {
            pnlBarraSuperior = new Panel
            {
                Height = 42,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(6, 18, 48)
            };

            Controls.Add(pnlBarraSuperior);
            pnlBarraSuperior.BringToFront();

            Label lblAplicacion = new Label
            {
                Text = "Lexbridge Academia de Inglés",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold
                ),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(18, 11)
            };

            pnlBarraSuperior.Controls.Add(lblAplicacion);

            btnCerrar = new Button
            {
                Text = "✕",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(48, 42),
                Cursor = Cursors.Hand,
                TabStop = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnCerrar.FlatAppearance.BorderSize = 0;

            btnCerrar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(220, 45, 65);

            btnCerrar.Click += (_, _) =>
            {
                Application.Exit();
            };

            pnlBarraSuperior.Controls.Add(btnCerrar);

            btnMinimizar = new Button
            {
                Text = "—",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 11F),
                Size = new Size(48, 42),
                Cursor = Cursors.Hand,
                TabStop = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnMinimizar.FlatAppearance.BorderSize = 0;

            btnMinimizar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 53, 100);

            btnMinimizar.Click += (_, _) =>
            {
                WindowState = FormWindowState.Minimized;
            };

            pnlBarraSuperior.Controls.Add(btnMinimizar);

            pnlBarraSuperior.MouseDown += MoverFormulario;
            lblAplicacion.MouseDown += MoverFormulario;
        }

        private void MoverFormulario(
            object sender,
            MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            ReleaseCapture();

            SendMessage(
                Handle,
                WM_NCLBUTTONDOWN,
                HT_CAPTION,
                0
            );
        }

        // ====================================================
        // PANEL IZQUIERDO CON EL LOGO
        // ====================================================

        private void CrearPanelMarca()
        {
            pnlMarca = new Panel
            {
                BackColor = Color.FromArgb(7, 25, 60)
            };

            Controls.Add(pnlMarca);

            picLogo = new PictureBox
            {
                // Debe existir en Properties/Resources.resx
                Image = Properties.Resources.LogoLexbridge,

                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            pnlMarca.Controls.Add(picLogo);

            lblCopyright = new Label
            {
                Text = "© 2026 Lexbridge   •   Versión 1.0",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Regular
                ),
                ForeColor = Color.FromArgb(202, 214, 239),
                BackColor = Color.Transparent
            };

            pnlMarca.Controls.Add(lblCopyright);
        }

        // ====================================================
        // TARJETA DEL LOGIN
        // ====================================================

        private void CrearTarjetaLogin()
        {
            pnlTarjeta = new Panel
            {
                BackColor = Color.FromArgb(18, 39, 85),
                Padding = new Padding(38)
            };

            Controls.Add(pnlTarjeta);

            // Mueve tus controles existentes a la tarjeta.
            lblTitulo.Parent = pnlTarjeta;
            lblSubtitulo.Parent = pnlTarjeta;
            lblUsuario.Parent = pnlTarjeta;
            lblContrasena.Parent = pnlTarjeta;
            lblMensaje.Parent = pnlTarjeta;

            txtUsuario.Parent = pnlTarjeta;
            txtContrasena.Parent = pnlTarjeta;

            btnIngresar.Parent = pnlTarjeta;
        }

        private void ConfigurarControlesOriginales()
        {
            lblTitulo.Text = "Inicia sesión";
            lblTitulo.AutoSize = false;
            lblTitulo.Font = new Font(
                "Segoe UI",
                25F,
                FontStyle.Bold
            );
            lblTitulo.ForeColor = Color.White;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;

            lblSubtitulo.Text =
                "Ingresa tus credenciales para continuar";

            lblSubtitulo.AutoSize = false;
            lblSubtitulo.Font = new Font(
                "Segoe UI",
                10.5F,
                FontStyle.Regular
            );
            lblSubtitulo.ForeColor =
                Color.FromArgb(182, 198, 229);
            lblSubtitulo.BackColor = Color.Transparent;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleLeft;

            ConfigurarLabelCampo(
                lblUsuario,
                "Usuario"
            );

            ConfigurarLabelCampo(
                lblContrasena,
                "Contraseña"
            );

            ConfigurarTextBox(txtUsuario);
            ConfigurarTextBox(txtContrasena);

            txtUsuario.PlaceholderText =
                "Ingrese su usuario";

            txtContrasena.PlaceholderText =
                "Ingrese su contraseña";

            txtContrasena.UseSystemPasswordChar = true;

            txtUsuario.TabIndex = 0;
            txtContrasena.TabIndex = 1;
            btnIngresar.TabIndex = 2;

            btnIngresar.Text = "Ingresar";
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.FlatAppearance.BorderSize = 0;

            btnIngresar.BackColor =
                Color.FromArgb(24, 163, 178);

            btnIngresar.ForeColor = Color.White;

            btnIngresar.Font = new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold
            );

            btnIngresar.Cursor = Cursors.Hand;

            btnIngresar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(31, 185, 199);

            btnIngresar.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(18, 137, 150);

            lblMensaje.AutoSize = false;
            lblMensaje.BackColor = Color.Transparent;

            lblMensaje.ForeColor =
                Color.FromArgb(255, 112, 133);

            lblMensaje.Font = new Font(
                "Segoe UI",
                9.5F,
                FontStyle.Regular
            );

            lblMensaje.TextAlign =
                ContentAlignment.MiddleCenter;
        }

        private void ConfigurarLabelCampo(
            Label label,
            string texto)
        {
            label.Text = texto;
            label.AutoSize = false;

            label.Font = new Font(
                "Segoe UI",
                10.5F,
                FontStyle.Bold
            );

            label.ForeColor = Color.White;
            label.BackColor = Color.Transparent;

            label.TextAlign =
                ContentAlignment.MiddleLeft;
        }

        private void ConfigurarTextBox(
            TextBox textBox)
        {
            textBox.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Regular
            );

            textBox.ForeColor = Color.White;

            textBox.BackColor =
                Color.FromArgb(11, 29, 69);

            textBox.BorderStyle =
                BorderStyle.FixedSingle;
        }

        // ====================================================
        // BOTÓN DE WINDOWS
        // ====================================================

        private void CrearBotonWindows()
        {
            lblSeparador = new Label
            {
                Text = "────────────   o   ────────────",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Regular
                ),
                ForeColor = Color.FromArgb(140, 157, 194),
                BackColor = Color.Transparent
            };

            pnlTarjeta.Controls.Add(lblSeparador);

            btnWindows = new Button
            {
                Text = "⊞  Continuar con Windows",
                FlatStyle = FlatStyle.Flat,

                BackColor =
                    Color.FromArgb(11, 29, 69),

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand,
                TabIndex = 3
            };

            btnWindows.FlatAppearance.BorderSize = 1;

            btnWindows.FlatAppearance.BorderColor =
                Color.FromArgb(75, 99, 154);

            btnWindows.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(27, 48, 98);

            btnWindows.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(36, 58, 110);

            btnWindows.Click += btnWindows_Click;

            pnlTarjeta.Controls.Add(btnWindows);
        }

        private void btnWindows_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                using WindowsIdentity identidad =
                    WindowsIdentity.GetCurrent();

                string usuarioWindows =
                    identidad.Name ?? string.Empty;

                if (string.IsNullOrWhiteSpace(usuarioWindows))
                {
                    MostrarError(
                        "No se pudo obtener el usuario de Windows."
                    );

                    return;
                }

                DialogResult respuesta =
                    MessageBox.Show(
                        "Se detectó la sesión de Windows:\n\n" +
                        usuarioWindows +
                        "\n\n¿Deseas continuar con esta cuenta?",
                        "Continuar con Windows",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (respuesta != DialogResult.Yes)
                    return;

                /*
                 * Esta versión utiliza la sesión de Windows
                 * que ya está iniciada en la computadora.
                 *
                 * No solicita contraseña de Microsoft ni MFA.
                 */
                AbrirFormularioPrincipal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo iniciar sesión con Windows:\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ====================================================
        // POSICIONES
        // ====================================================

        private void OrganizarControles()
        {
            if (pnlMarca == null ||
                pnlTarjeta == null)
            {
                return;
            }

            int alturaDisponible =
                ClientSize.Height -
                pnlBarraSuperior.Height;

            int anchoMarca =
                Math.Max(
                    400,
                    ClientSize.Width * 43 / 100
                );

            pnlMarca.SetBounds(
                0,
                pnlBarraSuperior.Height,
                anchoMarca,
                alturaDisponible
            );

            int anchoDerecho =
                ClientSize.Width - anchoMarca;

            // Se aumentó la altura para el botón de Windows.
            pnlTarjeta.Size =
                new Size(460, 590);

            pnlTarjeta.Left =
                anchoMarca +
                (anchoDerecho -
                 pnlTarjeta.Width) / 2;

            pnlTarjeta.Top =
                pnlBarraSuperior.Height +
                (alturaDisponible -
                 pnlTarjeta.Height) / 2;

            btnCerrar.Left =
                pnlBarraSuperior.ClientSize.Width -
                btnCerrar.Width;

            btnMinimizar.Left =
                btnCerrar.Left -
                btnMinimizar.Width;

            // Logo
            picLogo.SetBounds(
                45,
                65,
                anchoMarca - 90,
                465
            );

            lblCopyright.Left = 25;

            lblCopyright.Top =
                pnlMarca.Height -
                lblCopyright.Height -
                22;

            // Tarjeta
            lblTitulo.SetBounds(
                38,
                35,
                384,
                52
            );

            lblSubtitulo.SetBounds(
                38,
                84,
                384,
                30
            );

            lblUsuario.SetBounds(
                38,
                130,
                384,
                25
            );

            txtUsuario.SetBounds(
                38,
                158,
                384,
                40
            );

            lblContrasena.SetBounds(
                38,
                218,
                384,
                25
            );

            txtContrasena.SetBounds(
                38,
                246,
                384,
                40
            );

            btnIngresar.SetBounds(
                38,
                315,
                384,
                50
            );

            lblSeparador.SetBounds(
                38,
                385,
                384,
                30
            );

            btnWindows.SetBounds(
                38,
                425,
                384,
                50
            );

            lblMensaje.SetBounds(
                38,
                495,
                384,
                55
            );

            RedondearControl(
                pnlTarjeta,
                22
            );

            RedondearControl(
                btnIngresar,
                11
            );

            RedondearControl(
                btnWindows,
                11
            );
        }

        private void frmLogin_Resize(
            object sender,
            EventArgs e)
        {
            OrganizarControles();
            Invalidate();
        }

        // ====================================================
        // FONDO
        // ====================================================

        protected override void OnPaintBackground(
            PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 ||
                ClientRectangle.Height <= 0)
            {
                base.OnPaintBackground(e);
                return;
            }

            using LinearGradientBrush fondo =
                new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(5, 21, 53),
                    Color.FromArgb(79, 35, 133),
                    0F
                );

            ColorBlend mezcla = new ColorBlend
            {
                Colors = new[]
                {
                    Color.FromArgb(5, 21, 53),
                    Color.FromArgb(9, 31, 75),
                    Color.FromArgb(39, 38, 104),
                    Color.FromArgb(82, 34, 135)
                },

                Positions = new[]
                {
                    0F,
                    0.38F,
                    0.73F,
                    1F
                }
            };

            fondo.InterpolationColors = mezcla;

            e.Graphics.FillRectangle(
                fondo,
                ClientRectangle
            );

            DibujarCurvas(e.Graphics);
        }

        private void DibujarCurvas(Graphics g)
        {
            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            using GraphicsPath curva =
                new GraphicsPath();

            curva.AddBezier(
                -100,
                ClientSize.Height - 90,
                ClientSize.Width / 3,
                ClientSize.Height + 60,
                ClientSize.Width * 2 / 3,
                ClientSize.Height + 55,
                ClientSize.Width + 100,
                ClientSize.Height - 95
            );

            using Pen linea = new Pen(
                Color.FromArgb(
                    105,
                    72,
                    82,
                    255
                ),
                3F
            );

            g.DrawPath(linea, curva);

            using Pen brillo = new Pen(
                Color.FromArgb(
                    38,
                    180,
                    60,
                    255
                ),
                7F
            );

            g.DrawPath(brillo, curva);
        }

        private void RedondearControl(
            Control control,
            int radio)
        {
            if (control.Width <= 0 ||
                control.Height <= 0)
            {
                return;
            }

            Rectangle rect = new Rectangle(
                0,
                0,
                control.Width,
                control.Height
            );

            using GraphicsPath path =
                new GraphicsPath();

            int diametro = radio * 2;

            path.AddArc(
                rect.X,
                rect.Y,
                diametro,
                diametro,
                180,
                90
            );

            path.AddArc(
                rect.Right - diametro,
                rect.Y,
                diametro,
                diametro,
                270,
                90
            );

            path.AddArc(
                rect.Right - diametro,
                rect.Bottom - diametro,
                diametro,
                diametro,
                0,
                90
            );

            path.AddArc(
                rect.X,
                rect.Bottom - diametro,
                diametro,
                diametro,
                90,
                90
            );

            path.CloseFigure();

            Region regionAnterior =
                control.Region;

            control.Region =
                new Region(path);

            regionAnterior?.Dispose();
        }

        // ====================================================
        // APERTURA DEL FORMULARIO PRINCIPAL
        // ====================================================

        private void AbrirFormularioPrincipal()
        {
            if (formularioPrincipalAbierto)
                return;

            formularioPrincipalAbierto = true;

            frmPrincipal principal =
                new frmPrincipal();

            principal.FormClosed += (_, _) =>
            {
                Close();
            };

            principal.Show();
            Hide();
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.ForeColor =
                Color.FromArgb(255, 112, 133);

            lblMensaje.Text = mensaje;
        }

        // ====================================================
        // VALIDACIÓN ORIGINAL
        // ====================================================

        private void btnIngresar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string usuario =
                    txtUsuario.Text.Trim();

                string contrasena =
                    txtContrasena.Text.Trim();

                if (usuario == string.Empty ||
                    contrasena == string.Empty)
                {
                    MostrarError(
                        "Por favor ingrese usuario y contraseña."
                    );

                    return;
                }

                GestorLogin gestor =
                    new GestorLogin();

                bool acceso =
                    gestor.ValidarAcceso(
                        usuario,
                        contrasena
                    );

                if (acceso)
                {
                    AbrirFormularioPrincipal();
                }
                else
                {
                    MostrarError(
                        "Usuario o contraseña incorrectos."
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void frmLogin_Load(
            object sender,
            EventArgs e)
        {
        }
    }
}