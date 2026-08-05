namespace CAPA_PRESENTACION
{
    partial class frmConsultaMatriculas
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
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnLimpiarBusqueda = new Button();
            dgvConsulta = new DataGridView();
            lblDetalle = new Label();
            pnlDetalle = new Panel();
            lblNombreAlumno = new Label();
            lblValorNombreAlumno = new Label();
            lblNivel = new Label();
            lblValorNivel = new Label();
            lblCosto = new Label();
            lblValorCosto = new Label();
            lblPagado = new Label();
            lblValorPagado = new Label();
            lblPendiente = new Label();
            lblValorPendiente = new Label();
            lblEstado = new Label();
            lblValorEstado = new Label();
            picEstado = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvConsulta).BeginInit();
            pnlDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEstado).BeginInit();
            SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────
            lblTitulo.AutoSize = false;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(12, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(660, 40);
            lblTitulo.Text = "Consulta de Matrículas y Estado de Pagos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblBuscar ─────────────────────────────────────
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(12, 65);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Text = "Buscar por nombre de alumno:";

            // ── txtBuscar ─────────────────────────────────────
            txtBuscar.BackColor = Color.FromArgb(44, 62, 90);
            txtBuscar.ForeColor = Color.White;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(12, 85);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 25);
            txtBuscar.TabIndex = 0;

            // ── btnBuscar ─────────────────────────────────────
            btnBuscar.BackColor = Color.FromArgb(46, 134, 222);
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(320, 83);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.Text = "🔍 Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.Click += btnBuscar_Click;

            // ── btnLimpiarBusqueda ────────────────────────────
            btnLimpiarBusqueda.BackColor = Color.FromArgb(127, 140, 141);
            btnLimpiarBusqueda.FlatAppearance.BorderSize = 0;
            btnLimpiarBusqueda.FlatStyle = FlatStyle.Flat;
            btnLimpiarBusqueda.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpiarBusqueda.ForeColor = Color.White;
            btnLimpiarBusqueda.Location = new Point(430, 83);
            btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            btnLimpiarBusqueda.Size = new Size(100, 30);
            btnLimpiarBusqueda.Text = "✖ Limpiar";
            btnLimpiarBusqueda.UseVisualStyleBackColor = false;
            btnLimpiarBusqueda.Cursor = Cursors.Hand;
            btnLimpiarBusqueda.Click += btnLimpiarBusqueda_Click;

            // ── dgvConsulta ───────────────────────────────────
            dgvConsulta.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvConsulta.BorderStyle = BorderStyle.None;
            dgvConsulta.RowHeadersVisible = true;
            dgvConsulta.RowHeadersWidth = 30;
            dgvConsulta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsulta.MultiSelect = false;
            dgvConsulta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConsulta.Location = new Point(12, 125);
            dgvConsulta.Name = "dgvConsulta";
            dgvConsulta.Size = new Size(660, 200);
            dgvConsulta.TabIndex = 1;
            dgvConsulta.SelectionChanged += dgvConsulta_SelectionChanged;

            // ── lblDetalle ────────────────────────────────────
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.White;
            lblDetalle.Location = new Point(12, 340);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Text = "Detalle del alumno seleccionado:";

            // ── pnlDetalle ────────────────────────────────────
            pnlDetalle.BackColor = Color.FromArgb(13, 27, 42);
            pnlDetalle.BorderStyle = BorderStyle.FixedSingle;
            pnlDetalle.Location = new Point(12, 365);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Size = new Size(660, 180);

            // ── picEstado ─────────────────────────────────────
            picEstado.Location = new Point(580, 60);
            picEstado.Name = "picEstado";
            picEstado.Size = new Size(60, 60);
            picEstado.SizeMode = PictureBoxSizeMode.Zoom;

            // ── lblNombreAlumno ───────────────────────────────
            lblNombreAlumno.AutoSize = true;
            lblNombreAlumno.Font = new Font("Segoe UI", 9F);
            lblNombreAlumno.ForeColor = Color.FromArgb(160, 180, 200);
            lblNombreAlumno.Location = new Point(15, 15);
            lblNombreAlumno.Text = "Alumno:";

            lblValorNombreAlumno.AutoSize = true;
            lblValorNombreAlumno.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValorNombreAlumno.ForeColor = Color.White;
            lblValorNombreAlumno.Location = new Point(15, 33);
            lblValorNombreAlumno.Name = "lblValorNombreAlumno";
            lblValorNombreAlumno.Text = "-";

            // ── lblNivel ──────────────────────────────────────
            lblNivel.AutoSize = true;
            lblNivel.Font = new Font("Segoe UI", 9F);
            lblNivel.ForeColor = Color.FromArgb(160, 180, 200);
            lblNivel.Location = new Point(200, 15);
            lblNivel.Text = "Nivel:";

            lblValorNivel.AutoSize = true;
            lblValorNivel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblValorNivel.ForeColor = Color.White;
            lblValorNivel.Location = new Point(200, 33);
            lblValorNivel.Name = "lblValorNivel";
            lblValorNivel.Text = "-";

            // ── lblCosto ──────────────────────────────────────
            lblCosto.AutoSize = true;
            lblCosto.Font = new Font("Segoe UI", 9F);
            lblCosto.ForeColor = Color.FromArgb(160, 180, 200);
            lblCosto.Location = new Point(15, 75);
            lblCosto.Text = "Costo del nivel:";

            lblValorCosto.AutoSize = true;
            lblValorCosto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblValorCosto.ForeColor = Color.White;
            lblValorCosto.Location = new Point(15, 93);
            lblValorCosto.Name = "lblValorCosto";
            lblValorCosto.Text = "RD$0.00";

            // ── lblPagado ─────────────────────────────────────
            lblPagado.AutoSize = true;
            lblPagado.Font = new Font("Segoe UI", 9F);
            lblPagado.ForeColor = Color.FromArgb(160, 180, 200);
            lblPagado.Location = new Point(200, 75);
            lblPagado.Text = "Total pagado:";

            lblValorPagado.AutoSize = true;
            lblValorPagado.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblValorPagado.ForeColor = Color.FromArgb(46, 213, 115);
            lblValorPagado.Location = new Point(200, 93);
            lblValorPagado.Name = "lblValorPagado";
            lblValorPagado.Text = "RD$0.00";

            // ── lblPendiente ──────────────────────────────────
            lblPendiente.AutoSize = true;
            lblPendiente.Font = new Font("Segoe UI", 9F);
            lblPendiente.ForeColor = Color.FromArgb(160, 180, 200);
            lblPendiente.Location = new Point(390, 75);
            lblPendiente.Text = "Saldo pendiente:";

            lblValorPendiente.AutoSize = true;
            lblValorPendiente.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblValorPendiente.ForeColor = Color.FromArgb(255, 165, 0);
            lblValorPendiente.Location = new Point(390, 93);
            lblValorPendiente.Name = "lblValorPendiente";
            lblValorPendiente.Text = "RD$0.00";

            // ── lblEstado ─────────────────────────────────────
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(160, 180, 200);
            lblEstado.Location = new Point(15, 140);
            lblEstado.Text = "Estado:";

            lblValorEstado.AutoSize = true;
            lblValorEstado.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblValorEstado.ForeColor = Color.White;
            lblValorEstado.Location = new Point(15, 155);
            lblValorEstado.Name = "lblValorEstado";
            lblValorEstado.Text = "-";

            // ── Agregar controles al panel ────────────────────
            pnlDetalle.Controls.Add(lblNombreAlumno);
            pnlDetalle.Controls.Add(lblValorNombreAlumno);
            pnlDetalle.Controls.Add(lblNivel);
            pnlDetalle.Controls.Add(lblValorNivel);
            pnlDetalle.Controls.Add(lblCosto);
            pnlDetalle.Controls.Add(lblValorCosto);
            pnlDetalle.Controls.Add(lblPagado);
            pnlDetalle.Controls.Add(lblValorPagado);
            pnlDetalle.Controls.Add(lblPendiente);
            pnlDetalle.Controls.Add(lblValorPendiente);
            pnlDetalle.Controls.Add(lblEstado);
            pnlDetalle.Controls.Add(lblValorEstado);
            pnlDetalle.Controls.Add(picEstado);

            // ── frmConsultaMatriculas ─────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(690, 560);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnLimpiarBusqueda);
            Controls.Add(dgvConsulta);
            Controls.Add(lblDetalle);
            Controls.Add(pnlDetalle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmConsultaMatriculas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Consulta de Matrículas";
            Load += frmConsultaMatriculas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvConsulta).EndInit();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEstado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // TODO: Declaración de controles
        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnLimpiarBusqueda;
        private DataGridView dgvConsulta;
        private Label lblDetalle;
        private Panel pnlDetalle;
        private Label lblNombreAlumno;
        private Label lblValorNombreAlumno;
        private Label lblNivel;
        private Label lblValorNivel;
        private Label lblCosto;
        private Label lblValorCosto;
        private Label lblPagado;
        private Label lblValorPagado;
        private Label lblPendiente;
        private Label lblValorPendiente;
        private Label lblEstado;
        private Label lblValorEstado;
        private PictureBox picEstado;
    }
}