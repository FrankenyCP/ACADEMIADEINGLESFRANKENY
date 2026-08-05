namespace CAPA_PRESENTACION
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            pnlDashboard = new Panel();
            lblTituloAlumnos = new Label();
            lblTotalAlumnos = new Label();
            lblTituloMatriculas = new Label();
            lblTotalMatriculas = new Label();
            lblTituloInstructores = new Label();
            lblTotalInstructores = new Label();
            lblTituloIngresos = new Label();
            lblTotalIngresos = new Label();
            lblTituloPendiente = new Label();
            lblTotalPendiente = new Label();
            lblTituloNivel = new Label();
            lblNivelPopular = new Label();
            lblEstadoDashboard = new Label();
            btnRefrescarDashboard = new Button();
            btnAlumnos = new Button();
            btnNiveles = new Button();
            btnInstructores = new Button();
            btnMatriculas = new Button();
            btnPagos = new Button();
            btnReportes = new Button();
            btnConsultaMatriculas = new Button();
            btnSalir = new Button();
            picLogo = new PictureBox();
            pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────
            lblTitulo.AutoSize = false;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(12, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(460, 45);
            lblTitulo.Text = "Academia de Inglés";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // ── picLogo ───────────────────────────────────────
            picLogo.Location = new Point(490, 10);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(90, 65);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabStop = false;

            // ── pnlDashboard ──────────────────────────────────
            pnlDashboard.BackColor = Color.FromArgb(13, 27, 42);
            pnlDashboard.BorderStyle = BorderStyle.FixedSingle;
            pnlDashboard.Location = new Point(12, 75);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(568, 200);
            pnlDashboard.Controls.Add(lblTituloAlumnos);
            pnlDashboard.Controls.Add(lblTotalAlumnos);
            pnlDashboard.Controls.Add(lblTituloMatriculas);
            pnlDashboard.Controls.Add(lblTotalMatriculas);
            pnlDashboard.Controls.Add(lblTituloInstructores);
            pnlDashboard.Controls.Add(lblTotalInstructores);
            pnlDashboard.Controls.Add(lblTituloIngresos);
            pnlDashboard.Controls.Add(lblTotalIngresos);
            pnlDashboard.Controls.Add(lblTituloPendiente);
            pnlDashboard.Controls.Add(lblTotalPendiente);
            pnlDashboard.Controls.Add(lblTituloNivel);
            pnlDashboard.Controls.Add(lblNivelPopular);
            pnlDashboard.Controls.Add(lblEstadoDashboard);
            pnlDashboard.Controls.Add(btnRefrescarDashboard);

            // ── lblTituloAlumnos ──────────────────────────────
            lblTituloAlumnos.AutoSize = true;
            lblTituloAlumnos.Font = new Font("Segoe UI", 9F);
            lblTituloAlumnos.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloAlumnos.Location = new Point(20, 15);
            lblTituloAlumnos.Name = "lblTituloAlumnos";
            lblTituloAlumnos.Text = "👥 Alumnos";

            // ── lblTotalAlumnos ───────────────────────────────
            lblTotalAlumnos.AutoSize = true;
            lblTotalAlumnos.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalAlumnos.ForeColor = Color.White;
            lblTotalAlumnos.Location = new Point(20, 35);
            lblTotalAlumnos.Name = "lblTotalAlumnos";
            lblTotalAlumnos.Text = "0";

            // ── lblTituloMatriculas ───────────────────────────
            lblTituloMatriculas.AutoSize = true;
            lblTituloMatriculas.Font = new Font("Segoe UI", 9F);
            lblTituloMatriculas.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloMatriculas.Location = new Point(200, 15);
            lblTituloMatriculas.Name = "lblTituloMatriculas";
            lblTituloMatriculas.Text = "📋 Matrículas";

            // ── lblTotalMatriculas ────────────────────────────
            lblTotalMatriculas.AutoSize = true;
            lblTotalMatriculas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalMatriculas.ForeColor = Color.White;
            lblTotalMatriculas.Location = new Point(200, 35);
            lblTotalMatriculas.Name = "lblTotalMatriculas";
            lblTotalMatriculas.Text = "0";

            // ── lblTituloInstructores ─────────────────────────
            lblTituloInstructores.AutoSize = true;
            lblTituloInstructores.Font = new Font("Segoe UI", 9F);
            lblTituloInstructores.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloInstructores.Location = new Point(390, 15);
            lblTituloInstructores.Name = "lblTituloInstructores";
            lblTituloInstructores.Text = "👨‍🏫 Instructores";

            // ── lblTotalInstructores ──────────────────────────
            lblTotalInstructores.AutoSize = true;
            lblTotalInstructores.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalInstructores.ForeColor = Color.White;
            lblTotalInstructores.Location = new Point(390, 35);
            lblTotalInstructores.Name = "lblTotalInstructores";
            lblTotalInstructores.Text = "0";

            // ── lblTituloIngresos ─────────────────────────────
            lblTituloIngresos.AutoSize = true;
            lblTituloIngresos.Font = new Font("Segoe UI", 9F);
            lblTituloIngresos.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloIngresos.Location = new Point(20, 100);
            lblTituloIngresos.Name = "lblTituloIngresos";
            lblTituloIngresos.Text = "💰 Ingresos";

            // ── lblTotalIngresos ──────────────────────────────
            lblTotalIngresos.AutoSize = true;
            lblTotalIngresos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalIngresos.ForeColor = Color.FromArgb(46, 213, 115);
            lblTotalIngresos.Location = new Point(20, 118);
            lblTotalIngresos.Name = "lblTotalIngresos";
            lblTotalIngresos.Text = "RD$0.00";

            // ── lblTituloPendiente ────────────────────────────
            lblTituloPendiente.AutoSize = true;
            lblTituloPendiente.Font = new Font("Segoe UI", 9F);
            lblTituloPendiente.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloPendiente.Location = new Point(200, 100);
            lblTituloPendiente.Name = "lblTituloPendiente";
            lblTituloPendiente.Text = "⏳ Pendiente";

            // ── lblTotalPendiente ─────────────────────────────
            lblTotalPendiente.AutoSize = true;
            lblTotalPendiente.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalPendiente.ForeColor = Color.FromArgb(255, 165, 0);
            lblTotalPendiente.Location = new Point(200, 118);
            lblTotalPendiente.Name = "lblTotalPendiente";
            lblTotalPendiente.Text = "RD$0.00";

            // ── lblTituloNivel ────────────────────────────────
            lblTituloNivel.AutoSize = true;
            lblTituloNivel.Font = new Font("Segoe UI", 9F);
            lblTituloNivel.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloNivel.Location = new Point(390, 100);
            lblTituloNivel.Name = "lblTituloNivel";
            lblTituloNivel.Text = "🏆 Nivel Popular";

            // ── lblNivelPopular ───────────────────────────────
            lblNivelPopular.AutoSize = true;
            lblNivelPopular.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNivelPopular.ForeColor = Color.FromArgb(79, 214, 255);
            lblNivelPopular.Location = new Point(390, 118);
            lblNivelPopular.Name = "lblNivelPopular";
            lblNivelPopular.Text = "-";

            // ── lblEstadoDashboard ────────────────────────────
            lblEstadoDashboard.AutoSize = true;
            lblEstadoDashboard.Font = new Font("Segoe UI", 8F);
            lblEstadoDashboard.ForeColor = Color.LimeGreen;
            lblEstadoDashboard.Location = new Point(20, 170);
            lblEstadoDashboard.Name = "lblEstadoDashboard";
            lblEstadoDashboard.Text = "";

            // ── btnRefrescarDashboard ─────────────────────────
            btnRefrescarDashboard.BackColor = Color.FromArgb(46, 134, 222);
            btnRefrescarDashboard.FlatAppearance.BorderSize = 0;
            btnRefrescarDashboard.FlatStyle = FlatStyle.Flat;
            btnRefrescarDashboard.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefrescarDashboard.ForeColor = Color.White;
            btnRefrescarDashboard.Location = new Point(420, 162);
            btnRefrescarDashboard.Name = "btnRefrescarDashboard";
            btnRefrescarDashboard.Size = new Size(130, 30);
            btnRefrescarDashboard.Text = "↻ Actualizar";
            btnRefrescarDashboard.UseVisualStyleBackColor = false;
            btnRefrescarDashboard.Cursor = Cursors.Hand;
            btnRefrescarDashboard.Click += btnRefrescarDashboard_Click;

            // ── btnAlumnos ────────────────────────────────────
            btnAlumnos.BackColor = Color.FromArgb(52, 152, 219);
            btnAlumnos.FlatAppearance.BorderSize = 0;
            btnAlumnos.FlatStyle = FlatStyle.Flat;
            btnAlumnos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAlumnos.ForeColor = Color.White;
            btnAlumnos.Location = new Point(12, 295);
            btnAlumnos.Name = "btnAlumnos";
            btnAlumnos.Size = new Size(175, 50);
            btnAlumnos.Text = "Alumnos";
            btnAlumnos.UseVisualStyleBackColor = false;
            btnAlumnos.Cursor = Cursors.Hand;
            btnAlumnos.Click += btnAlumnos_Click;

            // ── btnNiveles ────────────────────────────────────
            btnNiveles.BackColor = Color.FromArgb(52, 152, 219);
            btnNiveles.FlatAppearance.BorderSize = 0;
            btnNiveles.FlatStyle = FlatStyle.Flat;
            btnNiveles.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNiveles.ForeColor = Color.White;
            btnNiveles.Location = new Point(200, 295);
            btnNiveles.Name = "btnNiveles";
            btnNiveles.Size = new Size(175, 50);
            btnNiveles.Text = "Niveles";
            btnNiveles.UseVisualStyleBackColor = false;
            btnNiveles.Cursor = Cursors.Hand;
            btnNiveles.Click += btnNiveles_Click;

            // ── btnInstructores ───────────────────────────────
            btnInstructores.BackColor = Color.FromArgb(52, 152, 219);
            btnInstructores.FlatAppearance.BorderSize = 0;
            btnInstructores.FlatStyle = FlatStyle.Flat;
            btnInstructores.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnInstructores.ForeColor = Color.White;
            btnInstructores.Location = new Point(390, 295);
            btnInstructores.Name = "btnInstructores";
            btnInstructores.Size = new Size(175, 50);
            btnInstructores.Text = "Instructores";
            btnInstructores.UseVisualStyleBackColor = false;
            btnInstructores.Cursor = Cursors.Hand;
            btnInstructores.Click += btnInstructores_Click;

            // ── btnMatriculas ─────────────────────────────────
            btnMatriculas.BackColor = Color.FromArgb(52, 152, 219);
            btnMatriculas.FlatAppearance.BorderSize = 0;
            btnMatriculas.FlatStyle = FlatStyle.Flat;
            btnMatriculas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnMatriculas.ForeColor = Color.White;
            btnMatriculas.Location = new Point(12, 360);
            btnMatriculas.Name = "btnMatriculas";
            btnMatriculas.Size = new Size(175, 50);
            btnMatriculas.Text = "Matrículas";
            btnMatriculas.UseVisualStyleBackColor = false;
            btnMatriculas.Cursor = Cursors.Hand;
            btnMatriculas.Click += btnMatriculas_Click;

            // ── btnPagos ──────────────────────────────────────
            btnPagos.BackColor = Color.FromArgb(52, 152, 219);
            btnPagos.FlatAppearance.BorderSize = 0;
            btnPagos.FlatStyle = FlatStyle.Flat;
            btnPagos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPagos.ForeColor = Color.White;
            btnPagos.Location = new Point(200, 360);
            btnPagos.Name = "btnPagos";
            btnPagos.Size = new Size(175, 50);
            btnPagos.Text = "Pagos";
            btnPagos.UseVisualStyleBackColor = false;
            btnPagos.Cursor = Cursors.Hand;
            btnPagos.Click += btnPagos_Click;

            // ── btnReportes ───────────────────────────────────
            btnReportes.BackColor = Color.FromArgb(52, 152, 219);
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(390, 360);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(175, 50);
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.Click += btnReportes_Click;

            // ── btnConsultaMatriculas ─────────────────────────
            btnConsultaMatriculas.BackColor = Color.FromArgb(142, 68, 173);
            btnConsultaMatriculas.FlatAppearance.BorderSize = 0;
            btnConsultaMatriculas.FlatStyle = FlatStyle.Flat;
            btnConsultaMatriculas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConsultaMatriculas.ForeColor = Color.White;
            btnConsultaMatriculas.Location = new Point(12, 430);
            btnConsultaMatriculas.Name = "btnConsultaMatriculas";
            btnConsultaMatriculas.Size = new Size(175, 50);
            btnConsultaMatriculas.Text = "📋 Consulta";
            btnConsultaMatriculas.UseVisualStyleBackColor = false;
            btnConsultaMatriculas.Cursor = Cursors.Hand;
            btnConsultaMatriculas.Click += btnConsultaMatriculas_Click;

            // ── btnSalir ──────────────────────────────────────
            btnSalir.BackColor = Color.FromArgb(192, 57, 43);
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(200, 430);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(175, 50);
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Click += btnSalir_Click;

            // ── frmPrincipal ──────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(594, 510);
            Controls.Add(picLogo);
            Controls.Add(lblTitulo);
            Controls.Add(pnlDashboard);
            Controls.Add(btnAlumnos);
            Controls.Add(btnNiveles);
            Controls.Add(btnInstructores);
            Controls.Add(btnMatriculas);
            Controls.Add(btnPagos);
            Controls.Add(btnReportes);
            Controls.Add(btnConsultaMatriculas);
            Controls.Add(btnSalir);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Academia de Inglés — Panel Principal";
            Load += frmPrincipal_Load;
            pnlDashboard.ResumeLayout(false);
            pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        // TODO: Declaración de controles
        private Label lblTitulo;
        private Label lblTituloAlumnos;
        private Label lblTotalAlumnos;
        private Label lblTituloMatriculas;
        private Label lblTotalMatriculas;
        private Label lblTituloInstructores;
        private Label lblTotalInstructores;
        private Label lblTituloIngresos;
        private Label lblTotalIngresos;
        private Label lblTituloPendiente;
        private Label lblTotalPendiente;
        private Label lblTituloNivel;
        private Label lblNivelPopular;
        private Label lblEstadoDashboard;
        private Button btnRefrescarDashboard;
        private Panel pnlDashboard;
        private Button btnAlumnos;
        private Button btnNiveles;
        private Button btnInstructores;
        private Button btnMatriculas;
        private Button btnPagos;
        private Button btnReportes;
        private Button btnConsultaMatriculas;
        private Button btnSalir;
        private PictureBox picLogo;
    }
}