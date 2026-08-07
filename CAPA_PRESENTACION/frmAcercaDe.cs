using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmAcercaDe : Form
    {
        private Panel pnlTarjetaPrincipal = null!;

        public frmAcercaDe()
        {
            InitializeComponent();
            ConfigurarDiseno();
        }

        private void ConfigurarDiseno()
        {
            SuspendLayout();

            try
            {
                // =====================================================
                // FORMULARIO
                // =====================================================

                BackColor = Color.FromArgb(4, 18, 47);
                ForeColor = Color.White;

                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.Manual;

                AutoScroll = false;
                DoubleBuffered = true;

                Margin = Padding.Empty;
                Padding = Padding.Empty;


                // =====================================================
                // CABECERA
                // =====================================================

                pnlCabeceraAcercaDe.Dock = DockStyle.Top;
                pnlCabeceraAcercaDe.Height = 88;

                pnlCabeceraAcercaDe.BackColor =
                    Color.FromArgb(4, 20, 51);


                // TITULO
                lblTitulo.Parent = pnlCabeceraAcercaDe;

                lblTitulo.Text = "Acerca de";
                lblTitulo.AutoSize = true;

                lblTitulo.Location =
                    new Point(26, 11);

                lblTitulo.Font =
                    new Font(
                        "Segoe UI",
                        20F,
                        FontStyle.Bold
                    );

                lblTitulo.ForeColor = Color.White;
                lblTitulo.BackColor = Color.Transparent;


                // SUBTITULO
                lblSubtitulo.Parent =
                    pnlCabeceraAcercaDe;

                lblSubtitulo.Text =
                    "Información general del sistema";

                lblSubtitulo.AutoSize = true;

                lblSubtitulo.Location =
                    new Point(29, 50);

                lblSubtitulo.Font =
                    new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Regular
                    );

                lblSubtitulo.ForeColor =
                    Color.FromArgb(145, 167, 199);

                lblSubtitulo.BackColor =
                    Color.Transparent;


                // =====================================================
                // CONTENIDO
                // =====================================================

                pnlContenido.Dock = DockStyle.Fill;

                pnlContenido.BackColor =
                    Color.FromArgb(4, 18, 47);

                pnlContenido.Padding =
                    new Padding(24);


                // =====================================================
                // TARJETA PRINCIPAL
                // =====================================================

                pnlTarjetaPrincipal = new Panel
                {
                    Parent = pnlContenido,

                    Location = new Point(24, 85),

                    Size = new Size(
        Math.Max(
            700,
            pnlContenido.ClientSize.Width - 48
        ),
        545
    ),

                    Anchor =
        AnchorStyles.Top |
        AnchorStyles.Left |
        AnchorStyles.Right,

                    BackColor =
        Color.FromArgb(13, 34, 73)
                };
                // =====================================================
                // ACENTO IZQUIERDO
                // =====================================================

                Panel pnlAcento = new Panel
                {
                    Parent = pnlTarjetaPrincipal,
                    Dock = DockStyle.Left,
                    Width = 5,

                    BackColor =
                        Color.FromArgb(44, 190, 255)
                };


                // =====================================================
                // NOMBRE DEL SISTEMA
                // =====================================================

                lblNombreSistema.Parent =
                    pnlTarjetaPrincipal;

                lblNombreSistema.Text =
                    "LEXBRIDGE ACADEMY";

                lblNombreSistema.AutoSize = true;

                lblNombreSistema.Location =
                    new Point(35, 35);

                lblNombreSistema.Font =
                    new Font(
                        "Segoe UI",
                        19F,
                        FontStyle.Bold
                    );

                lblNombreSistema.ForeColor =
                    Color.FromArgb(44, 190, 255);

                lblNombreSistema.BackColor =
                    Color.Transparent;

                lblNombreSistema.BringToFront();


                // =====================================================
                // DESCRIPCION
                // =====================================================

                lblDescripcion.Parent =
                    pnlTarjetaPrincipal;

                lblDescripcion.Text =
                    "Sistema de Gestión Académica de Inglés\r\n" +
                    "Plataforma para administrar alumnos, instructores, " +
                    "niveles, matrículas, pagos y reportes.";

                lblDescripcion.Location =
                    new Point(38, 90);

                lblDescripcion.Size =
                    new Size(900, 60);

                lblDescripcion.Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right;

                lblDescripcion.Font =
                    new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Regular
                    );

                lblDescripcion.ForeColor =
                    Color.FromArgb(184, 200, 224);

                lblDescripcion.BackColor =
                    Color.Transparent;

                lblDescripcion.BringToFront();


                // =====================================================
                // SEPARADOR
                // =====================================================

                Panel pnlSeparador = new Panel
                {
                    Parent = pnlTarjetaPrincipal,

                    Location =
                        new Point(38, 165),

                    Size =
                        new Size(
                            Math.Max(
                                500,
                                pnlTarjetaPrincipal.Width - 76
                            ),
                            1
                        ),

                    Anchor =
                        AnchorStyles.Top |
                        AnchorStyles.Left |
                        AnchorStyles.Right,

                    BackColor =
                        Color.FromArgb(39, 65, 119)
                };


                // =====================================================
                // TITULO PARTICIPANTES
                // =====================================================

                lblIntegrantesTitulo.Parent =
                    pnlTarjetaPrincipal;

                lblIntegrantesTitulo.Text =
                    "Participantes del proyecto";

                lblIntegrantesTitulo.AutoSize = true;

                lblIntegrantesTitulo.Location =
                    new Point(38, 195);

                lblIntegrantesTitulo.Font =
                    new Font(
                        "Segoe UI",
                        14F,
                        FontStyle.Bold
                    );

                lblIntegrantesTitulo.ForeColor =
                    Color.White;

                lblIntegrantesTitulo.BackColor =
                    Color.Transparent;

                lblIntegrantesTitulo.BringToFront();


                // =====================================================
                // PARTICIPANTES - COLUMNA IZQUIERDA
                // =====================================================

                ConfigurarIntegrante(
                    lblIntegrante1,
                    "Frankeny Castillo, 2025-0794",
                    38,
                    245
                );

                ConfigurarIntegrante(
                    lblIntegrante2,
                    "Kimberly Ashley, 2025-0505",
                    38,
                    305
                );

                ConfigurarIntegrante(
                    lblIntegrante3,
                    "Emmanuel Chalas, 2024-2062",
                    38,
                    365
                );


                // =====================================================
                // PARTICIPANTES - COLUMNA DERECHA PQ SE VE MEJOR
                // =====================================================

                ConfigurarIntegrante(
                    lblIntegrante4,
                    "Dariel Casilla, 2024 -2074",
                    410,
                    245
                );

                ConfigurarIntegrante(
                    lblIntegrante5,
                    "Andres Sanchez, 2025 -0561",
                    410,
                    305
                );


                // =====================================================
                // VERSION FICTICIA
                // =====================================================

                lblVersion.Parent =
                    pnlTarjetaPrincipal;

                lblVersion.Text =
                    "Versión 1.0  •  2026";

                lblVersion.AutoSize = true;

                lblVersion.Location =
                    new Point(38, 465);

                lblVersion.Font =
                    new Font(
                        "Segoe UI",
                        8.5F,
                        FontStyle.Regular
                    );

                lblVersion.ForeColor =
                    Color.FromArgb(125, 148, 181);

                lblVersion.BackColor =
                    Color.Transparent;

                lblVersion.BringToFront();


                // =====================================================
                // REDIMENSIONAMIENTO ESPERO QUE FUNCIONE
                // =====================================================

                pnlContenido.Resize +=
                    (sender, e) =>
                    {
                        if (pnlTarjetaPrincipal == null)
                            return;

                        pnlTarjetaPrincipal.Width =
                            Math.Max(
                                700,
                                pnlContenido.ClientSize.Width - 48
                            );

                        RedondearControlAcercaDe(
                            pnlTarjetaPrincipal,
                            18
                        );
                    };


                // =====================================================
                // ORDEN VISUAL
                // =====================================================

                pnlContenido.BringToFront();
                pnlCabeceraAcercaDe.BringToFront();
            }
            finally
            {
                ResumeLayout(false);
            }
        }


        // =============================================================
        // DISENO DE LOS INTEGRANTES
        // =============================================================

        private void ConfigurarIntegrante(
            Label label,
            string nombre,
            int x,
            int y)
        {
            if (label == null ||
                pnlTarjetaPrincipal == null)
            {
                return;
            }

            label.Parent =
                pnlTarjetaPrincipal;

            label.Text =
                "●   " + nombre;

            label.Location =
                new Point(x, y);

            label.Size =
                new Size(335, 44);

            label.AutoSize = false;

            label.TextAlign =
                ContentAlignment.MiddleLeft;

            label.Padding =
                new Padding(14, 0, 8, 0);

            label.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Regular
                );

            label.ForeColor =
                Color.FromArgb(220, 230, 245);

            label.BackColor =
                Color.FromArgb(23, 48, 92);

            label.BringToFront();
        }


        // =============================================================
        // MODO INTEGRADO CON frmPrincipal.Extras
        // =============================================================

        public void PrepararModoIntegrado()
        {
            FormBorderStyle =
                FormBorderStyle.None;

            StartPosition =
                FormStartPosition.Manual;

            MinimumSize =
                Size.Empty;

            MaximumSize =
                Size.Empty;

            Margin =
                Padding.Empty;

            Padding =
                Padding.Empty;

            Dock =
                DockStyle.Fill;


            if (pnlCabeceraAcercaDe != null)
            {
                pnlCabeceraAcercaDe.Visible =
                    true;

                pnlCabeceraAcercaDe.Dock =
                    DockStyle.Top;
            }


            if (pnlContenido != null)
            {
                pnlContenido.Visible =
                    true;

                pnlContenido.Dock =
                    DockStyle.Fill;
            }


            pnlContenido?.BringToFront();
            pnlCabeceraAcercaDe?.BringToFront();
        }


        // =============================================================
        // BORDES REDONDEADOS
        // =============================================================

        private static void RedondearControlAcercaDe(
            Control control,
            int radio)
        {
            if (control == null ||
                control.Width <= 0 ||
                control.Height <= 0)
            {
                return;
            }

            int diametro =
                radio * 2;

            Rectangle rect =
                new Rectangle(
                    0,
                    0,
                    control.Width,
                    control.Height
                );


            using (GraphicsPath path =
                new GraphicsPath())
            {
                path.StartFigure();

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

                control.Region =
                    new Region(path);
            }
        }


        // =============================================================
        // EVENTOS DEL DESIGNER
        // =============================================================

        private void lblIntegrantesTitulo_Click(
            object sender,
            EventArgs e)
        {
        }


        private void frmAcercaDe_Load(
            object sender,
            EventArgs e)
        {
        }
    }
}