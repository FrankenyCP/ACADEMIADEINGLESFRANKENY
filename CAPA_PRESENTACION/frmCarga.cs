using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{

    public partial class frmCarga : Form
    {
        private bool cargaIniciada = false;
        private int progresoActual = 0;

        // Impide abrir mas de un login.
        private static int loginAbierto = 0;

        public frmCarga()
        {
            InitializeComponent();

            ConfigurarFormulario();
            ConfigurarControles();
            OrganizarControles();
        }

        private void ConfigurarFormulario()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            ClientSize = new Size(1200, 750);

            DoubleBuffered = true;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            UpdateStyles();
        }

        private void ConfigurarControles()
        {
            // Logo
            picLogo.Size = new Size(520, 400);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.Transparent;

            // El PNG tiene mucho margen transparente.
            // Este método lo recorta automáticamente.
            if (picLogo.Image != null)
            {
                picLogo.Image = RecortarMargenTransparente(
                    picLogo.Image
                );
            }

            // Texto de estado
            lblEstado.AutoSize = false;
            lblEstado.Size = new Size(500, 40);
            lblEstado.BackColor = Color.Transparent;
            lblEstado.ForeColor = Color.White;
            lblEstado.Font = new Font(
                "Segoe UI",
                14F,
                FontStyle.Regular
            );
            lblEstado.TextAlign = ContentAlignment.MiddleCenter;
            lblEstado.Text = "Inicializando sistema...";

            // Barra gris
            pnlBarraFondo.Size = new Size(600, 16);
            pnlBarraFondo.BackColor =
                Color.FromArgb(70, 79, 111);

            // Barra amarilla
            pnlProgreso.Width = 0;
            pnlProgreso.Height = pnlBarraFondo.Height;
            pnlProgreso.BackColor =
                Color.FromArgb(255, 184, 0);

            // Porcentaje
            lblPorcentaje.AutoSize = false;
            lblPorcentaje.Size = new Size(75, 32);
            lblPorcentaje.BackColor = Color.Transparent;
            lblPorcentaje.ForeColor =
                Color.FromArgb(255, 205, 0);
            lblPorcentaje.Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Regular
            );
            lblPorcentaje.TextAlign =
                ContentAlignment.MiddleLeft;
            lblPorcentaje.Text = "0%";

            pnlProgreso.BringToFront();

            lblCopyright.Text = "© 2026 Lexbridge";
            lblCopyright.AutoSize = true;
            lblCopyright.BackColor = Color.Transparent;
            lblCopyright.ForeColor = Color.White;
            lblCopyright.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );

            lblPunto.Text = "•";
            lblPunto.AutoSize = true;
            lblPunto.BackColor = Color.Transparent;
            lblPunto.ForeColor = Color.FromArgb(255, 190, 0);
            lblPunto.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold
            );

            lblVersion.Text = "Versión 1.0";
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.ForeColor = Color.White;
            lblVersion.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular
            );
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (cargaIniciada)
                return;

            cargaIniciada = true;

            // Da tiempo para que el formulario se dibuje.
            await Task.Delay(350);

            await EjecutarCargaAsync();

            AbrirLoginUnaVez();
        }

        private async Task EjecutarCargaAsync()
        {
            progresoActual = 0;
            ActualizarProgreso(0);

            await AnimarProgresoAsync(
                0,
                15,
                850,
                "Preparando componentes..."
            );

            await AnimarProgresoAsync(
                15,
                35,
                950,
                "Inicializando sistema..."
            );

            await AnimarProgresoAsync(
                35,
                58,
                1000,
                "Verificando conexión..."
            );

            await AnimarProgresoAsync(
                58,
                78,
                900,
                "Cargando información..."
            );

            await AnimarProgresoAsync(
                78,
                93,
                800,
                "Preparando formularios..."
            );

            await AnimarProgresoAsync(
                93,
                100,
                650,
                "Carga completada"
            );

            await Task.Delay(550);
        }

        private async Task AnimarProgresoAsync(
            int inicio,
            int final,
            int duracion,
            string mensaje)
        {
            lblEstado.Text = mensaje;

            Stopwatch reloj = Stopwatch.StartNew();

            while (reloj.ElapsedMilliseconds < duracion)
            {
                double tiempo =
                    reloj.ElapsedMilliseconds /
                    (double)duracion;

                // Animacion suave.
                double suavizado =
                    tiempo * tiempo *
                    (3.0 - 2.0 * tiempo);

                double valor =
                    inicio +
                    ((final - inicio) * suavizado);

                ActualizarProgreso(
                    (int)Math.Round(valor)
                );

                // Aproximadamente 60 FPS.
                await Task.Delay(16);
            }

            ActualizarProgreso(final);

            await Task.Delay(120);
        }

        private void ActualizarProgreso(int porcentaje)
        {
            progresoActual = Math.Max(
                0,
                Math.Min(100, porcentaje)
            );

            int ancho =
                pnlBarraFondo.Width *
                progresoActual / 100;

            pnlProgreso.Width = ancho;
            lblPorcentaje.Text =
                progresoActual + "%";

            RedondearControl(
                pnlBarraFondo,
                pnlBarraFondo.Height
            );

            RedondearControl(
                pnlProgreso,
                pnlProgreso.Height
            );

            pnlProgreso.BringToFront();
        }

        private void AbrirLoginUnaVez()
        {
            // Solo la primera llamada puede pasar.
            if (Interlocked.CompareExchange(
                    ref loginAbierto,
                    1,
                    0) != 0)
            {
                return;
            }

            frmLogin loginExistente = null;

            // Comprueba si ya existe un frmLogin abierto.
            foreach (Form formulario in Application.OpenForms)
            {
                if (formulario is frmLogin encontrado)
                {
                    loginExistente = encontrado;
                    break;
                }
            }

            if (loginExistente != null)
            {
                loginExistente.Show();
                loginExistente.BringToFront();
                loginExistente.Activate();

                Hide();
                return;
            }

            frmLogin login = new frmLogin();

            login.StartPosition =
                FormStartPosition.CenterScreen;

            login.FormClosed += (s, e) =>
            {
                Interlocked.Exchange(
                    ref loginAbierto,
                    0
                );
            };

            login.Show();
            login.BringToFront();
            login.Activate();

            Hide();
        }

        private void OrganizarControles()
        {
            /*
             * El PictureBox sigue midiendo 700 × 600,
             * pero el logo recortado llenara mejor el espacio.
             */
            picLogo.Left =
         (ClientSize.Width - picLogo.Width) / 2;

            picLogo.Top = 45;

            // Texto de carga
            lblEstado.Left =
                (ClientSize.Width - lblEstado.Width) / 2;

            lblEstado.Top = 500;

            // Barra de progreso
            pnlBarraFondo.Left =
                (ClientSize.Width - pnlBarraFondo.Width) / 2;

            pnlBarraFondo.Top = 555;

            pnlProgreso.Left = pnlBarraFondo.Left;
            pnlProgreso.Top = pnlBarraFondo.Top;
            pnlProgreso.Height = pnlBarraFondo.Height;

            // Porcentaje
            lblPorcentaje.Left =
                pnlBarraFondo.Right + 20;

            lblPorcentaje.Top =
                pnlBarraFondo.Top -
                ((lblPorcentaje.Height -
                  pnlBarraFondo.Height) / 2);

            // Pie de página
            lblCopyright.Left = 30;
            lblCopyright.Top = ClientSize.Height - 38;

            lblPunto.Left = lblCopyright.Right + 10;
            lblPunto.Top = lblCopyright.Top - 1;

            lblVersion.Left = lblPunto.Right + 8;
            lblVersion.Top = lblCopyright.Top;

            pnlProgreso.BringToFront();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!IsHandleCreated)
                return;

            OrganizarControles();
            ActualizarProgreso(progresoActual);
            Invalidate();
        }

        /*
         * Aqui se dibuja el fondo que faltaba:
         * azul oscuro, morado y brillo lateral.
         */
        protected override void OnPaintBackground(
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle area = ClientRectangle;

            using (LinearGradientBrush fondo =
                   new LinearGradientBrush(
                       area,
                       Color.FromArgb(3, 20, 45),
                       Color.FromArgb(69, 30, 117),
                       0F))
            {
                ColorBlend mezcla =
                    new ColorBlend();

                mezcla.Colors = new[]
                {
                    Color.FromArgb(3, 20, 45),
                    Color.FromArgb(10, 31, 76),
                    Color.FromArgb(45, 35, 110),
                    Color.FromArgb(85, 31, 128)
                };

                mezcla.Positions = new[]
                {
                    0F,
                    0.35F,
                    0.72F,
                    1F
                };

                fondo.InterpolationColors = mezcla;

                g.FillRectangle(fondo, area);
            }

            DibujarBrilloMorado(g);
            DibujarCurvasInferiores(g);
        }

        private void DibujarBrilloMorado(Graphics g)
        {
            Rectangle zonaBrillo = new Rectangle(
                ClientSize.Width - 470,
                -100,
                620,
                ClientSize.Height + 200
            );

            using (GraphicsPath forma =
                   new GraphicsPath())
            {
                forma.AddEllipse(zonaBrillo);

                using (PathGradientBrush brillo =
                       new PathGradientBrush(forma))
                {
                    brillo.CenterColor =
                        Color.FromArgb(
                            90,
                            136,
                            45,
                            205
                        );

                    brillo.SurroundColors =
                        new[]
                        {
                            Color.FromArgb(
                                0,
                                35,
                                18,
                                80
                            )
                        };

                    g.FillPath(brillo, forma);
                }
            }
        }

        private void DibujarCurvasInferiores(
            Graphics g)
        {
            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;

            using (GraphicsPath curva =
                   new GraphicsPath())
            {
                curva.AddBezier(
                    -160,
                    alto - 245,
                    ancho / 4,
                    alto + 60,
                    ancho * 3 / 4,
                    alto + 75,
                    ancho + 150,
                    alto - 150
                );

                using (Pen lapiz = new Pen(
                    Color.FromArgb(
                        115,
                        73,
                        103,
                        255
                    ),
                    2.2F))
                {
                    g.DrawPath(lapiz, curva);
                }
            }

            using (GraphicsPath resplandor =
                   new GraphicsPath())
            {
                resplandor.AddBezier(
                    -160,
                    alto - 248,
                    ancho / 4,
                    alto + 55,
                    ancho * 3 / 4,
                    alto + 70,
                    ancho + 150,
                    alto - 153
                );

                using (Pen lapiz = new Pen(
                    Color.FromArgb(
                        35,
                        105,
                        67,
                        255
                    ),
                    7F))
                {
                    g.DrawPath(lapiz, resplandor);
                }
            }
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

            Rectangle rectangulo = new Rectangle(
                0,
                0,
                control.Width,
                control.Height
            );

            using (GraphicsPath forma =
                   CrearRectanguloRedondeado(
                       rectangulo,
                       radio
                   ))
            {
                Region regionAnterior =
                    control.Region;

                control.Region =
                    new Region(forma);

                regionAnterior?.Dispose();
            }
        }

        private GraphicsPath CrearRectanguloRedondeado(
            Rectangle rectangulo,
            int radio)
        {
            GraphicsPath forma =
                new GraphicsPath();

            int diametro = Math.Min(
                radio,
                Math.Min(
                    rectangulo.Width,
                    rectangulo.Height
                )
            );

            if (diametro <= 1)
            {
                forma.AddRectangle(rectangulo);
                return forma;
            }

            Rectangle arco = new Rectangle(
                rectangulo.X,
                rectangulo.Y,
                diametro,
                diametro
            );

            forma.AddArc(arco, 180, 90);

            arco.X =
                rectangulo.Right - diametro;

            forma.AddArc(arco, 270, 90);

            arco.Y =
                rectangulo.Bottom - diametro;

            forma.AddArc(arco, 0, 90);

            arco.X = rectangulo.Left;

            forma.AddArc(arco, 90, 90);

            forma.CloseFigure();

            return forma;
        }

        /*
         * Elimina automaticamente el espacio transparente
         * alrededor del logo.
         */
        private Bitmap RecortarMargenTransparente(
            Image imagenOriginal)
        {
            Bitmap original =
                new Bitmap(imagenOriginal);

            int izquierda = original.Width;
            int derecha = 0;
            int arriba = original.Height;
            int abajo = 0;

            bool encontrado = false;

            for (int y = 0;
                 y < original.Height;
                 y++)
            {
                for (int x = 0;
                     x < original.Width;
                     x++)
                {
                    Color pixel =
                        original.GetPixel(x, y);

                    if (pixel.A > 15)
                    {
                        encontrado = true;

                        if (x < izquierda)
                            izquierda = x;

                        if (x > derecha)
                            derecha = x;

                        if (y < arriba)
                            arriba = y;

                        if (y > abajo)
                            abajo = y;
                    }
                }
            }

            if (!encontrado)
                return original;

            Rectangle zona = Rectangle.FromLTRB(
                izquierda,
                arriba,
                derecha + 1,
                abajo + 1
            );

            Bitmap recortada = new Bitmap(
                zona.Width,
                zona.Height
            );

            using (Graphics g =
                   Graphics.FromImage(recortada))
            {
                g.DrawImage(
                    original,
                    new Rectangle(
                        0,
                        0,
                        recortada.Width,
                        recortada.Height
                    ),
                    zona,
                    GraphicsUnit.Pixel
                );
            }

            original.Dispose();

            return recortada;
        }
    }
}