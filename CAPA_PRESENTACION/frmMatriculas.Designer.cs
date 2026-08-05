namespace CAPA_PRESENTACION
{
    partial class frmMatriculas
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
            cmbAlumno = new ComboBox();
            cmbNivel = new ComboBox();
            cmbInstructor = new ComboBox();
            dtpFechaMatricula = new DateTimePicker();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnNuevo = new Button();
            lblMensaje = new Label();
            lblEstado = new Label();
            lblTitulo = new Label();
            lblAlumno = new Label();
            lblNivel = new Label();
            lblInstructor = new Label();
            lblFecha = new Label();
            dgvMatriculas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMatriculas).BeginInit();
            SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────
            lblTitulo.AutoSize = false;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(12, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(560, 40);
            lblTitulo.Text = "Gestión de Matrículas";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblAlumno ─────────────────────────────────────
            lblAlumno.AutoSize = true;
            lblAlumno.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAlumno.ForeColor = Color.White;
            lblAlumno.Location = new Point(12, 70);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Text = "Alumno *";

            // ── cmbAlumno ─────────────────────────────────────
            cmbAlumno.BackColor = Color.FromArgb(44, 62, 90);
            cmbAlumno.ForeColor = Color.White;
            cmbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumno.Font = new Font("Segoe UI", 10F);
            cmbAlumno.Location = new Point(12, 90);
            cmbAlumno.Name = "cmbAlumno";
            cmbAlumno.Size = new Size(220, 25);
            cmbAlumno.TabIndex = 0;

            // ── lblNivel ──────────────────────────────────────
            lblNivel.AutoSize = true;
            lblNivel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNivel.ForeColor = Color.White;
            lblNivel.Location = new Point(12, 125);
            lblNivel.Name = "lblNivel";
            lblNivel.Text = "Nivel *";

            // ── cmbNivel ──────────────────────────────────────
            cmbNivel.BackColor = Color.FromArgb(44, 62, 90);
            cmbNivel.ForeColor = Color.White;
            cmbNivel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivel.Font = new Font("Segoe UI", 10F);
            cmbNivel.Location = new Point(12, 145);
            cmbNivel.Name = "cmbNivel";
            cmbNivel.Size = new Size(220, 25);
            cmbNivel.TabIndex = 1;

            // ── lblInstructor ─────────────────────────────────
            lblInstructor.AutoSize = true;
            lblInstructor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInstructor.ForeColor = Color.White;
            lblInstructor.Location = new Point(12, 180);
            lblInstructor.Name = "lblInstructor";
            lblInstructor.Text = "Instructor *";

            // ── cmbInstructor ─────────────────────────────────
            cmbInstructor.BackColor = Color.FromArgb(44, 62, 90);
            cmbInstructor.ForeColor = Color.White;
            cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstructor.Font = new Font("Segoe UI", 10F);
            cmbInstructor.Location = new Point(12, 200);
            cmbInstructor.Name = "cmbInstructor";
            cmbInstructor.Size = new Size(220, 25);
            cmbInstructor.TabIndex = 2;

            // ── lblFecha ──────────────────────────────────────
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFecha.ForeColor = Color.White;
            lblFecha.Location = new Point(12, 235);
            lblFecha.Name = "lblFecha";
            lblFecha.Text = "Fecha de Matrícula *";

            // ── dtpFechaMatricula ─────────────────────────────
            dtpFechaMatricula.Format = DateTimePickerFormat.Short;
            dtpFechaMatricula.Location = new Point(12, 255);
            dtpFechaMatricula.Name = "dtpFechaMatricula";
            dtpFechaMatricula.Size = new Size(220, 23);
            dtpFechaMatricula.TabIndex = 3;

            // ── btnNuevo ──────────────────────────────────────
            btnNuevo.BackColor = Color.FromArgb(39, 174, 96);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(260, 90);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(110, 38);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.Click += btnNuevo_Click;

            // ── btnGuardar ────────────────────────────────────
            btnGuardar.BackColor = Color.FromArgb(46, 134, 222);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(380, 90);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(110, 38);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Click += btnGuardar_Click;

            // ── btnEliminar ───────────────────────────────────
            btnEliminar.BackColor = Color.FromArgb(192, 57, 43);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(260, 138);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 38);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Click += btnEliminar_Click;

            // ── btnLimpiar ────────────────────────────────────
            btnLimpiar.BackColor = Color.FromArgb(127, 140, 141);
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(380, 138);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(110, 38);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.Click += btnLimpiar_Click;

            // ── lblMensaje ────────────────────────────────────
            lblMensaje.AutoSize = false;
            lblMensaje.Font = new Font("Segoe UI", 9F);
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(12, 295);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(560, 20);
            lblMensaje.Text = "";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblEstado ─────────────────────────────────────
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.LimeGreen;
            lblEstado.Location = new Point(12, 320);
            lblEstado.Name = "lblEstado";
            lblEstado.Text = "";

            // ── dgvMatriculas ─────────────────────────────────
            dgvMatriculas.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvMatriculas.BorderStyle = BorderStyle.None;
            dgvMatriculas.RowHeadersVisible = true;
            dgvMatriculas.RowHeadersWidth = 30;
            dgvMatriculas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatriculas.MultiSelect = false;
            dgvMatriculas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatriculas.Location = new Point(12, 345);
            dgvMatriculas.Name = "dgvMatriculas";
            dgvMatriculas.Size = new Size(560, 200);
            dgvMatriculas.TabIndex = 8;
            dgvMatriculas.SelectionChanged += dgvMatriculas_SelectionChanged;

            // ── frmMatriculas ─────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(594, 560);
            Controls.Add(lblTitulo);
            Controls.Add(lblAlumno);
            Controls.Add(cmbAlumno);
            Controls.Add(lblNivel);
            Controls.Add(cmbNivel);
            Controls.Add(lblInstructor);
            Controls.Add(cmbInstructor);
            Controls.Add(lblFecha);
            Controls.Add(dtpFechaMatricula);
            Controls.Add(btnNuevo);
            Controls.Add(btnGuardar);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(lblMensaje);
            Controls.Add(lblEstado);
            Controls.Add(dgvMatriculas);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmMatriculas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Matrículas";
            Load += frmMatriculas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMatriculas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // TODO: Declaración de controles
        private ComboBox cmbAlumno;
        private ComboBox cmbNivel;
        private ComboBox cmbInstructor;
        private DateTimePicker dtpFechaMatricula;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnNuevo;
        private Label lblMensaje;
        private Label lblEstado;
        private Label lblTitulo;
        private Label lblAlumno;
        private Label lblNivel;
        private Label lblInstructor;
        private Label lblFecha;
        private DataGridView dgvMatriculas;
    }
}