namespace CAPA_PRESENTACION
{
    partial class frmMatriculas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblAlumno = new Label();
            lblNivel = new Label();
            lblInstructor = new Label();
            lblFechamatricula = new Label();
            cmbAlumno = new ComboBox();
            cmbNivel = new ComboBox();
            cmbInstructor = new ComboBox();
            dtpFechaMatricula = new DateTimePicker();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            dgvMatriculas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMatriculas).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(420, 31);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(324, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Matrículas";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAlumno
            // 
            lblAlumno.AutoSize = true;
            lblAlumno.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAlumno.ForeColor = Color.White;
            lblAlumno.Location = new Point(126, 113);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Size = new Size(100, 31);
            lblAlumno.TabIndex = 1;
            lblAlumno.Text = "Alumno";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNivel.ForeColor = Color.White;
            lblNivel.Location = new Point(138, 241);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(70, 31);
            lblNivel.TabIndex = 2;
            lblNivel.Text = "Nivel";
            // 
            // lblInstructor
            // 
            lblInstructor.AutoSize = true;
            lblInstructor.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInstructor.ForeColor = Color.White;
            lblInstructor.Location = new Point(106, 376);
            lblInstructor.Name = "lblInstructor";
            lblInstructor.Size = new Size(120, 31);
            lblInstructor.TabIndex = 3;
            lblInstructor.Text = "Instructor";
            // 
            // lblFechamatricula
            // 
            lblFechamatricula.AutoSize = true;
            lblFechamatricula.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechamatricula.ForeColor = Color.White;
            lblFechamatricula.Location = new Point(69, 510);
            lblFechamatricula.Name = "lblFechamatricula";
            lblFechamatricula.Size = new Size(216, 31);
            lblFechamatricula.TabIndex = 4;
            lblFechamatricula.Text = "Fecha de Matrícula";
            // 
            // cmbAlumno
            // 
            cmbAlumno.BackColor = Color.FromArgb(44, 62, 90);
            cmbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumno.ForeColor = Color.White;
            cmbAlumno.FormattingEnabled = true;
            cmbAlumno.Location = new Point(62, 161);
            cmbAlumno.Name = "cmbAlumno";
            cmbAlumno.Size = new Size(223, 28);
            cmbAlumno.TabIndex = 5;
            // 
            // cmbNivel
            // 
            cmbNivel.BackColor = Color.FromArgb(44, 62, 90);
            cmbNivel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNivel.ForeColor = Color.White;
            cmbNivel.FormattingEnabled = true;
            cmbNivel.Location = new Point(62, 284);
            cmbNivel.Name = "cmbNivel";
            cmbNivel.Size = new Size(223, 28);
            cmbNivel.TabIndex = 6;
            // 
            // cmbInstructor
            // 
            cmbInstructor.BackColor = Color.FromArgb(44, 62, 90);
            cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstructor.ForeColor = Color.White;
            cmbInstructor.FormattingEnabled = true;
            cmbInstructor.Location = new Point(62, 427);
            cmbInstructor.Name = "cmbInstructor";
            cmbInstructor.Size = new Size(223, 28);
            cmbInstructor.TabIndex = 7;
            // 
            // dtpFechaMatricula
            // 
            dtpFechaMatricula.Format = DateTimePickerFormat.Short;
            dtpFechaMatricula.Location = new Point(97, 565);
            dtpFechaMatricula.Name = "dtpFechaMatricula";
            dtpFechaMatricula.Size = new Size(129, 27);
            dtpFechaMatricula.TabIndex = 8;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(116, 645);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 9;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(946, 128);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(154, 61);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.CadetBlue;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(749, 128);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(154, 61);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(545, 128);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(154, 61);
            btnEliminar.TabIndex = 12;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvMatriculas
            // 
            dgvMatriculas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatriculas.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvMatriculas.BorderStyle = BorderStyle.None;
            dgvMatriculas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatriculas.Location = new Point(430, 243);
            dgvMatriculas.MultiSelect = false;
            dgvMatriculas.Name = "dgvMatriculas";
            dgvMatriculas.RowHeadersWidth = 30;
            dgvMatriculas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatriculas.Size = new Size(1122, 431);
            dgvMatriculas.TabIndex = 13;
            dgvMatriculas.CellClick += dgvMatriculas_CellClick;
            // 
            // frmMatriculas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1582, 753);
            Controls.Add(dgvMatriculas);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(lblMensaje);
            Controls.Add(dtpFechaMatricula);
            Controls.Add(cmbInstructor);
            Controls.Add(cmbNivel);
            Controls.Add(cmbAlumno);
            Controls.Add(lblFechamatricula);
            Controls.Add(lblInstructor);
            Controls.Add(lblNivel);
            Controls.Add(lblAlumno);
            Controls.Add(lblTitulo);
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

        #endregion

        private Label lblTitulo;
        private Label lblAlumno;
        private Label lblNivel;
        private Label lblInstructor;
        private Label lblFechamatricula;
        private ComboBox cmbAlumno;
        private ComboBox cmbNivel;
        private ComboBox cmbInstructor;
        private DateTimePicker dtpFechaMatricula;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Button btnEliminar;
        private DataGridView dgvMatriculas;
    }
}