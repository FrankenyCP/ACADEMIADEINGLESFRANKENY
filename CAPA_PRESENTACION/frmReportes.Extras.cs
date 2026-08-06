using CAPA_NEGOCIOS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CAPA_PRESENTACION
{
    public partial class frmReportes
    {
        // =========================================================
        // VARIABLES
        // =========================================================

        private bool disenoReportesInicializado;
        private bool formularioReportesMostrado;
        private bool modoIntegradoSolicitado;

        private Panel pnlMenuReportes = null!;
        private Panel pnlContenidoReportes = null!;
        private Panel pnlEncabezadoReportes = null!;
        private Panel pnlCuerpoReportes = null!;

        private Panel pnlMarcaReportes = null!;
        private Panel pnlOpcionesReportes = null!;
        private Panel pnlPieReportes = null!;

        private Panel pnlReportePrincipal = null!;
        private Panel pnlCabeceraReporte = null!;
        private Panel pnlContenedorTexto = null!;
        private Panel pnlHerramientasReporte = null!;

        private PictureBox picLogoReportes = null!;

        private Label lblMarcaReportes = null!;
        private Label lblMarcaSubtituloReportes = null!;
        private Label lblTituloPaginaReportes = null!;
        private Label lblSubtituloPaginaReportes = null!;
        private Label lblTituloContenidoReporte = null!;
        private Label lblFechaGeneracionReporte = null!;
        private Label lblPieReportes = null!;
        private Label lblVersionReportes = null!;

        private Button btnMenuDashboardReportes = null!;
        private Button btnMenuAlumnosReportes = null!;
        private Button btnMenuNivelesReportes = null!;
        private Button btnMenuInstructoresReportes = null!;
        private Button btnMenuMatriculasReportes = null!;
        private Button btnMenuPagosReportes = null!;
        private Button btnMenuReportesActual = null!;
        private Button btnMenuConsultaReportes = null!;

        private Button btnCerrarReportes = null!;
        private Button btnActualizarHerramienta = null!;
        private Button btnExportarPdfReporte = null!;
        private Button btnExportarExcelReporte = null!;
        private Button btnCopiarReporte = null!;

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
        // PREPARAR DISEÑO
        // =========================================================

        protected override void SetVisibleCore(bool value)
        {
            if (LicenseManager.UsageMode ==
                LicenseUsageMode.Designtime)
            {
                base.SetVisibleCore(value);
                return;
            }

            if (value && !disenoReportesInicializado)
            {
                disenoReportesInicializado = true;

                SuspendLayout();

                try
                {
                    InicializarDisenoReportes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al preparar el diseño de reportes:\r\n" +
                        ex.Message,
                        "Reportes",
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

            if (value && !formularioReportesMostrado)
            {
                formularioReportesMostrado = true;

                BeginInvoke(new Action(() =>
                {
                    AjustarDisenoReportes();

                    if (modoIntegradoSolicitado)
                    {
                        AplicarModoIntegrado();
                    }
                    ActualizarFechaReporte();
                    Invalidate(true);
                    Update();
                }));
            }
        }

        // =========================================================
        // INICIALIZACIÓN
        // =========================================================

        private void InicializarDisenoReportes()
        {
            ConfigurarFormularioReportes();
            CrearEstructuraReportes();
            CrearMenuLateralReportes();
            CrearEncabezadoReportes();
            CrearPanelReporte();
            ConfigurarTextoReporte();
            ConfigurarHerramientasReporte();
            ConfigurarEventosReportes();

            AjustarDisenoReportes();

            pnlMenuReportes.BringToFront();
            pnlContenidoReportes.BringToFront();

            if (modoIntegradoSolicitado)
            {
                AplicarModoIntegrado();
            }
        }

        private void ConfigurarFormularioReportes()
        {
            Text = "Reportes Generales - Lexbridge";

            ClientSize = new Size(1400, 800);
            MinimumSize = new Size(1120, 680);

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

        private void CrearEstructuraReportes()
        {
            pnlMenuReportes = new Panel
            {
                Dock = DockStyle.Left,
                Width = 235,
                Padding = new Padding(12),
                BackColor = Color.FromArgb(3, 14, 39)
            };

            pnlContenidoReportes = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlEncabezadoReportes = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(5, 20, 52)
            };

            pnlCuerpoReportes = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            pnlContenidoReportes.Controls.Add(pnlCuerpoReportes);
            pnlContenidoReportes.Controls.Add(pnlEncabezadoReportes);

            Controls.Add(pnlContenidoReportes);
            Controls.Add(pnlMenuReportes);

            pnlCuerpoReportes.Resize += (sender, e) =>
            {
                AjustarDisenoReportes();
            };

            Resize += (sender, e) =>
            {
                AjustarDisenoReportes();
            };
        }

        // =========================================================
        // MENÚ LATERAL
        // =========================================================

        private void CrearMenuLateralReportes()
        {
            pnlMarcaReportes = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            pnlPieReportes = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 68,
                BackColor = Color.Transparent
            };

            pnlOpcionesReportes = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            pnlMenuReportes.Controls.Add(pnlOpcionesReportes);
            pnlMenuReportes.Controls.Add(pnlPieReportes);
            pnlMenuReportes.Controls.Add(pnlMarcaReportes);

            picLogoReportes = new PictureBox
            {
                Parent = pnlMarcaReportes,
                Location = new Point(3, 7),
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            CargarLogoReportes();

            lblMarcaReportes = new Label
            {
                Parent = pnlMarcaReportes,
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

            lblMarcaSubtituloReportes = new Label
            {
                Parent = pnlMarcaReportes,
                Text = "ACADEMIA DE INGLES",
                Location = new Point(60, 36),
                Size = new Size(145, 19),

                ForeColor =
                    Color.FromArgb(153, 171, 201),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 6.8F)
            };

            btnMenuDashboardReportes = CrearBotonMenuReportes(
                "Dashboard", "\uE80F", 8, false
            );

            btnMenuAlumnosReportes = CrearBotonMenuReportes(
                "Alumnos", "\uE77B", 60, false
            );

            btnMenuNivelesReportes = CrearBotonMenuReportes(
                "Niveles", "\uE8EF", 112, false
            );

            btnMenuInstructoresReportes = CrearBotonMenuReportes(
                "Instructores", "\uE716", 164, false
            );

            btnMenuMatriculasReportes = CrearBotonMenuReportes(
                "Matriculas", "\uE787", 216, false
            );

            btnMenuPagosReportes = CrearBotonMenuReportes(
                "Pagos", "\uE8C7", 268, false
            );

            btnMenuReportesActual = CrearBotonMenuReportes(
                "Reportes", "\uE9D2", 320, true
            );

            btnMenuConsultaReportes = CrearBotonMenuReportes(
                "Consulta", "\uE721", 372, false
            );

            btnMenuDashboardReportes.Click += (sender, e) =>
            {
                VolverDashboardReportes();
            };

            btnMenuAlumnosReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(new frmAlumnos());
            };

            btnMenuNivelesReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(new frmNiveles());
            };

            btnMenuInstructoresReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(new frmInstructores());
            };

            btnMenuMatriculasReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(new frmMatriculas());
            };

            btnMenuPagosReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(new frmPagos());
            };

            btnMenuReportesActual.Click += (sender, e) =>
            {
                btnActualizar.PerformClick();
            };

            btnMenuConsultaReportes.Click += (sender, e) =>
            {
                AbrirFormularioReportes(
                    new frmConsultaMatriculas()
                );
            };

            lblPieReportes = new Label
            {
                Parent = pnlPieReportes,
                Text = "(c) 2026 Lexbridge",
                Location = new Point(5, 27),
                Size = new Size(132, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            lblVersionReportes = new Label
            {
                Parent = pnlPieReportes,
                Text = "Version 1.0",
                Location = new Point(143, 27),
                Size = new Size(75, 22),

                ForeColor =
                    Color.FromArgb(185, 197, 219),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };
        }

        private Button CrearBotonMenuReportes(
            string texto,
            string icono,
            int top,
            bool seleccionado)
        {
            Button boton = new Button
            {
                Parent = pnlOpcionesReportes,
                Location = new Point(0, top),

                Size = new Size(
                    Math.Max(
                        180,
                        pnlOpcionesReportes.ClientSize.Width
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
                Parent = pnlOpcionesReportes,
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
                    Parent = pnlOpcionesReportes,
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

            RedondearControlReportes(boton, 11);

            boton.BringToFront();
            lblIcono.BringToFront();

            return boton;
        }

        private void CargarLogoReportes()
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
                        picLogoReportes.Image = imagen;
                        return;
                    }
                }

                picLogoReportes.Image = null;
            }
            catch
            {
                picLogoReportes.Image = null;
            }
        }

        // =========================================================
        // ENCABEZADO
        // =========================================================

        private void CrearEncabezadoReportes()
        {
            lblTitulo.Visible = false;

            lblTituloPaginaReportes = new Label
            {
                Parent = pnlEncabezadoReportes,
                Text = "Reportes Generales",
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

            lblSubtituloPaginaReportes = new Label
            {
                Parent = pnlEncabezadoReportes,

                Text =
                    "Consulta el estado general y los resultados de la academia",

                Location = new Point(35, 55),
                Size = new Size(600, 22),

                ForeColor =
                    Color.FromArgb(148, 169, 202),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F)
            };

            btnActualizar.Parent = pnlEncabezadoReportes;
            btnActualizar.Size = new Size(145, 40);

            btnActualizar.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            btnActualizar.Location = new Point(
                pnlEncabezadoReportes.ClientSize.Width - 310,
                24
            );

            btnActualizar.Text = "Actualizar reporte";

            btnActualizar.BackColor =
                Color.FromArgb(14, 145, 155);

            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.FlatAppearance.BorderSize = 0;

            btnActualizar.Font = new Font(
                "Segoe UI",
                8.8F,
                FontStyle.Bold
            );

            btnCerrarReportes = new Button
            {
                Parent = pnlEncabezadoReportes,
                Size = new Size(115, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                Location = new Point(
                    pnlEncabezadoReportes.ClientSize.Width - 145,
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

            btnCerrarReportes.FlatAppearance.BorderColor =
                Color.FromArgb(71, 92, 139);

            btnCerrarReportes.FlatAppearance.BorderSize = 1;

            btnCerrarReportes.Click += (sender, e) =>
            {
                Close();
            };

            RedondearControlReportes(btnActualizar, 11);
            RedondearControlReportes(btnCerrarReportes, 11);
        }

        // =========================================================
        // PANEL DEL REPORTE
        // =========================================================

        private void CrearPanelReporte()
        {
            pnlReportePrincipal = new Panel
            {
                Parent = pnlCuerpoReportes,
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

            pnlCabeceraReporte = new Panel
            {
                Parent = pnlReportePrincipal,
                Dock = DockStyle.Top,
                Height = 70,

                BackColor =
                    Color.FromArgb(12, 33, 74)
            };

            lblTituloContenidoReporte = new Label
            {
                Parent = pnlCabeceraReporte,
                Text = "Resumen institucional",
                Location = new Point(22, 13),
                Size = new Size(330, 30),

                ForeColor =
                    Color.FromArgb(255, 187, 31),

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    13F,
                    FontStyle.Bold
                )
            };

            lblFechaGeneracionReporte = new Label
            {
                Parent = pnlCabeceraReporte,
                Text = "Actualizado:",
                Location = new Point(24, 43),
                Size = new Size(430, 20),

                ForeColor =
                    Color.FromArgb(164, 183, 214),

                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8F)
            };

            pnlHerramientasReporte = new Panel
            {
                Parent = pnlCabeceraReporte,
                Size = new Size(454, 40),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left,

                // Queda cerca del título y completamente visible.
                Location = new Point(430, 15),

                BackColor = Color.Transparent
            };

            pnlContenedorTexto = new Panel
            {
                Parent = pnlReportePrincipal,
                Location = new Point(18, 88),

                Size = new Size(
                    pnlReportePrincipal.Width - 36,
                    pnlReportePrincipal.Height - 106
                ),

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,

                BackColor =
                    Color.FromArgb(6, 22, 54)
            };

            RedondearControlReportes(
                pnlReportePrincipal,
                18
            );

            RedondearControlReportes(
                pnlContenedorTexto,
                12
            );
        }

        // =========================================================
        // TEXTO DEL REPORTE
        // =========================================================

        private void ConfigurarTextoReporte()
        {
            txtReporte.Parent = pnlContenedorTexto;
            txtReporte.Dock = DockStyle.Fill;

            txtReporte.Multiline = true;
            txtReporte.ReadOnly = true;

            txtReporte.ScrollBars =
                ScrollBars.Vertical;

            txtReporte.BorderStyle =
                BorderStyle.None;

            txtReporte.BackColor =
                Color.FromArgb(6, 22, 54);

            txtReporte.ForeColor =
                Color.FromArgb(225, 234, 247);

            txtReporte.Font = new Font(
                "Consolas",
                11F,
                FontStyle.Regular
            );

            txtReporte.Padding =
                new Padding(20);

            txtReporte.WordWrap = true;
        }

        // =========================================================
        // HERRAMIENTAS
        // =========================================================

        private void ConfigurarHerramientasReporte()
        {
            /*
             * Se crea un botón exclusivo para la barra.
             * Así no hereda posiciones ni anclajes del botón original.
             */
            btnActualizar.Visible = false;

            btnActualizarHerramienta =
                CrearBotonHerramientaReporte(
                    "Actualizar",
                    new Point(0, 0),
                    new Size(100, 38),
                    Color.FromArgb(14, 145, 155)
                );

            btnExportarPdfReporte =
                CrearBotonHerramientaReporte(
                    "Exportar PDF",
                    new Point(108, 0),
                    new Size(115, 38),
                    Color.FromArgb(154, 37, 69)
                );

            btnExportarExcelReporte =
                CrearBotonHerramientaReporte(
                    "Exportar Excel",
                    new Point(231, 0),
                    new Size(125, 38),
                    Color.FromArgb(19, 118, 73)
                );

            btnCopiarReporte =
                CrearBotonHerramientaReporte(
                    "Copiar",
                    new Point(364, 0),
                    new Size(90, 38),
                    Color.FromArgb(33, 80, 132)
                );

            btnActualizarHerramienta.Click +=
                (sender, e) =>
                {
                    btnActualizar_Click(sender, e);
                    ActualizarFechaReporte();
                };

            btnExportarPdfReporte.Click +=
                btnExportarPdfReporte_Click;

            btnExportarExcelReporte.Click +=
                btnExportarExcelReporte_Click;

            btnCopiarReporte.Click +=
                btnCopiarReporte_Click;
        }

        private Button CrearBotonHerramientaReporte(
            string texto,
            Point posicion,
            Size tamano,
            Color fondo)
        {
            Button boton = new Button
            {
                Parent = pnlHerramientasReporte,
                Location = posicion,
                Size = tamano,
                Text = texto,

                BackColor = fondo,
                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Font = new Font(
                    "Segoe UI",
                    8.3F,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand
            };

            boton.FlatAppearance.BorderColor =
                AclararColorReportes(fondo, 28);

            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.MouseOverBackColor =
                AclararColorReportes(fondo, 15);

            RedondearControlReportes(boton, 10);

            return boton;
        }

        private async void btnExportarPdfReporte_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReporte.Text))
                {
                    MessageBox.Show(
                        "No hay información para exportar.",
                        "Reportes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                using SaveFileDialog dialogo =
                    new SaveFileDialog
                    {
                        Filter =
                            "Archivo PDF (*.pdf)|*.pdf",

                        FileName =
                            "Reporte_General_" +
                            DateTime.Now.ToString("yyyyMMdd") +
                            ".pdf"
                    };

                if (dialogo.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                iExportador exportador =
                    new ExportadorPdf();

                bool resultado =
                    await exportador.ExportarAsync(
                        "Reporte General Lexbridge",
                        ObtenerLineasReporte(),
                        dialogo.FileName
                    );

                if (resultado)
                {
                    AbrirArchivoReporte(dialogo.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al exportar el reporte a PDF: " +
                    ex.Message,
                    "Reportes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void btnExportarExcelReporte_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReporte.Text))
                {
                    MessageBox.Show(
                        "No hay información para exportar.",
                        "Reportes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                using SaveFileDialog dialogo =
                    new SaveFileDialog
                    {
                        Filter =
                            "Archivo Excel (*.xlsx)|*.xlsx",

                        FileName =
                            "Reporte_General_" +
                            DateTime.Now.ToString("yyyyMMdd") +
                            ".xlsx"
                    };

                if (dialogo.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                iExportador exportador =
                    new ExportadorExcel();

                bool resultado =
                    await exportador.ExportarAsync(
                        "Reporte General Lexbridge",
                        ObtenerLineasReporte(),
                        dialogo.FileName
                    );

                if (resultado)
                {
                    AbrirArchivoReporte(dialogo.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al exportar el reporte a Excel: " +
                    ex.Message,
                    "Reportes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCopiarReporte_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReporte.Text))
                {
                    MessageBox.Show(
                        "No hay información para copiar.",
                        "Reportes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                Clipboard.SetText(txtReporte.Text);

                MessageBox.Show(
                    "Reporte copiado al portapapeles.",
                    "Reportes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo copiar el reporte: " +
                    ex.Message,
                    "Reportes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private List<string> ObtenerLineasReporte()
        {
            return txtReporte.Lines
                .Where(linea =>
                    !string.IsNullOrWhiteSpace(linea))
                .ToList();
        }

        private static void AbrirArchivoReporte(
            string ruta)
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(ruta)
                {
                    UseShellExecute = true
                }
            );
        }

        // =========================================================
        // EVENTOS VISUALES
        // =========================================================

        private void ConfigurarEventosReportes()
        {
            /*
             * Reconecta explícitamente el evento original que genera
             * nuevamente el contenido de txtReporte.
             */
            btnActualizar.Click -= btnActualizar_Click;
            btnActualizar.Click += btnActualizar_Click;

            /*
             * Este evento adicional solo actualiza la fecha visual.
             */
            btnActualizar.Click += (sender, e) =>
            {
                ActualizarFechaReporte();
            };

            pnlReportePrincipal.Resize += (sender, e) =>
            {
                AjustarPosicionHerramientasReporte();

                pnlContenedorTexto.Size =
                    new Size(
                        pnlReportePrincipal.ClientSize.Width - 36,
                        pnlReportePrincipal.ClientSize.Height - 106
                    );

                RedondearControlReportes(
                    pnlReportePrincipal,
                    18
                );
            };

            pnlEncabezadoReportes.Resize += (sender, e) =>
            {
                btnCerrarReportes.Left =
                    pnlEncabezadoReportes.ClientSize.Width -
                    btnCerrarReportes.Width -
                    20;

                if (btnActualizar.Parent ==
                    pnlEncabezadoReportes)
                {
                    btnActualizar.Left =
                        btnCerrarReportes.Left -
                        btnActualizar.Width -
                        20;
                }
            };
        }

        private void AjustarPosicionHerramientasReporte()
        {
            if (pnlHerramientasReporte == null ||
                pnlCabeceraReporte == null)
            {
                return;
            }

            int izquierdaDeseada = 430;

            int maximoPermitido =
                pnlCabeceraReporte.ClientSize.Width -
                pnlHerramientasReporte.Width -
                18;

            pnlHerramientasReporte.Left =
                Math.Max(
                    350,
                    Math.Min(
                        izquierdaDeseada,
                        maximoPermitido
                    )
                );

            pnlHerramientasReporte.Top = 15;
            pnlHerramientasReporte.BringToFront();
        }

        private void ActualizarFechaReporte()
        {
            if (lblFechaGeneracionReporte == null)
                return;

            lblFechaGeneracionReporte.Text =
                "Actualizado: " +
                DateTime.Now.ToString(
                    "dd/MM/yyyy hh:mm:ss tt"
                );
        }

        // =========================================================
        // DIMENSIONES
        // =========================================================

        private void AjustarDisenoReportes()
        {
            if (!disenoReportesInicializado ||
                pnlCuerpoReportes == null ||
                pnlReportePrincipal == null)
            {
                return;
            }

            const int margen = 18;
            const int anchoMinimo = 820;
            const int altoMinimo = 560;

            int anchoDisponible =
                pnlCuerpoReportes.ClientSize.Width -
                (margen * 2);

            int altoDisponible =
                pnlCuerpoReportes.ClientSize.Height -
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

            pnlReportePrincipal.Location =
                new Point(margen, margen);

            pnlReportePrincipal.Size =
                new Size(
                    anchoPanel,
                    altoPanel
                );

            pnlCuerpoReportes.AutoScrollMinSize =
                new Size(
                    anchoPanel + (margen * 2),
                    altoPanel + (margen * 2)
                );

            AjustarPosicionHerramientasReporte();

            pnlContenedorTexto.Size =
                new Size(
                    pnlReportePrincipal.ClientSize.Width - 36,
                    pnlReportePrincipal.ClientSize.Height - 106
                );

            btnCerrarReportes.Left =
                pnlEncabezadoReportes.ClientSize.Width -
                btnCerrarReportes.Width -
                20;

            btnActualizar.Left =
                btnCerrarReportes.Left -
                btnActualizar.Width -
                20;

            RedondearControlReportes(
                pnlReportePrincipal,
                18
            );

            RedondearControlReportes(
                pnlContenedorTexto,
                12
            );
        }

        // =========================================================
        // NAVEGACIÓN
        // =========================================================

        private void VolverDashboardReportes()
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

        private void AbrirFormularioReportes(
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
                    "Navegación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                formulario.Dispose();
            }
        }

        // =========================================================
        // COLORES
        // =========================================================

        private static Color AclararColorReportes(
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

        // =========================================================
        // BORDES REDONDEADOS
        // =========================================================

        private static void RedondearControlReportes(
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

                if (pnlMenuReportes != null)
                {
                    pnlMenuReportes.Visible = false;
                    pnlMenuReportes.Dock = DockStyle.None;
                    pnlMenuReportes.Width = 0;
                }

                if (pnlEncabezadoReportes != null)
                {
                    pnlEncabezadoReportes.Visible = false;
                    pnlEncabezadoReportes.Dock = DockStyle.None;
                    pnlEncabezadoReportes.Height = 0;
                }

                if (pnlContenidoReportes != null)
                {
                    pnlContenidoReportes.Visible = true;
                    pnlContenidoReportes.Dock = DockStyle.Fill;
                    pnlContenidoReportes.Location = Point.Empty;
                    pnlContenidoReportes.Margin = new Padding(0);
                    pnlContenidoReportes.Padding = new Padding(0);
                    pnlContenidoReportes.BringToFront();
                }

                if (pnlCuerpoReportes != null)
                {
                    pnlCuerpoReportes.Visible = true;
                    pnlCuerpoReportes.Dock = DockStyle.Fill;
                    pnlCuerpoReportes.Location = Point.Empty;
                    pnlCuerpoReportes.Margin = new Padding(0);
                }

                PerformLayout();

                if (pnlCuerpoReportes != null)
                {
                    AjustarDisenoReportes();
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