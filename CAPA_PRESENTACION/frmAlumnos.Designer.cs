namespace CAPA_PRESENTACION
{
    partial class frmAlumnos
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
            lblNombre = new Label();
            lblApellido = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            lblFechaNacimiento = new Label();
            chkIntensivo = new CheckBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnVerNivel = new Button();
            dgvAlumnos = new DataGridView();
            btnPromover = new Button();
            btnActualizar = new Button();
            lblMensaje = new Label();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            btnInfoAlumno = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(475, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(300, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Alumnos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(159, 91);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(118, 31);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre *";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(156, 211);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(121, 31);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido *";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(154, 334);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(123, 31);
            lblTelefono.TabIndex = 3;
            lblTelefono.Text = "Teléfono *";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCorreo.ForeColor = Color.White;
            lblCorreo.Location = new Point(159, 459);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(102, 31);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo *";
            // 
            // txtNombre
            // 
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Location = new Point(84, 140);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(257, 27);
            txtNombre.TabIndex = 5;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // txtApellido
            // 
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.Location = new Point(84, 271);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(257, 27);
            txtApellido.TabIndex = 6;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // txtTelefono
            // 
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Location = new Point(84, 390);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(257, 27);
            txtTelefono.TabIndex = 7;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // txtCorreo
            // 
            txtCorreo.BorderStyle = BorderStyle.FixedSingle;
            txtCorreo.Location = new Point(84, 511);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(257, 27);
            txtCorreo.TabIndex = 8;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(134, 632);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(127, 27);
            dtpFechaNacimiento.TabIndex = 9;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.AutoSize = true;
            lblFechaNacimiento.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaNacimiento.ForeColor = Color.White;
            lblFechaNacimiento.Location = new Point(84, 578);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(254, 31);
            lblFechaNacimiento.TabIndex = 10;
            lblFechaNacimiento.Text = "Fecha de Nacimiento *";
            // 
            // chkIntensivo
            // 
            chkIntensivo.AutoSize = true;
            chkIntensivo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIntensivo.ForeColor = Color.White;
            chkIntensivo.Location = new Point(403, 212);
            chkIntensivo.Name = "chkIntensivo";
            chkIntensivo.Size = new Size(236, 32);
            chkIntensivo.TabIndex = 11;
            chkIntensivo.Text = "Modalidad Intensiva?";
            chkIntensivo.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(1009, 271);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(112, 54);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(703, 271);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 54);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.CadetBlue;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(859, 271);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(125, 54);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVerNivel
            // 
            btnVerNivel.BackColor = Color.CadetBlue;
            btnVerNivel.Cursor = Cursors.Hand;
            btnVerNivel.FlatStyle = FlatStyle.Flat;
            btnVerNivel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVerNivel.ForeColor = Color.White;
            btnVerNivel.Location = new Point(1009, 180);
            btnVerNivel.Name = "btnVerNivel";
            btnVerNivel.Size = new Size(112, 58);
            btnVerNivel.TabIndex = 15;
            btnVerNivel.Text = "Ver Nivel";
            btnVerNivel.UseVisualStyleBackColor = false;
            btnVerNivel.Click += btnVerNivel_Click;
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlumnos.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvAlumnos.BorderStyle = BorderStyle.None;
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.Location = new Point(403, 364);
            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.RowHeadersWidth = 30;
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.Size = new Size(1151, 368);
            dgvAlumnos.TabIndex = 16;
            dgvAlumnos.SelectionChanged += dgvAlumnos_SelectionChanged;
            // 
            // btnPromover
            // 
            btnPromover.BackColor = Color.CadetBlue;
            btnPromover.Cursor = Cursors.Hand;
            btnPromover.FlatStyle = FlatStyle.Flat;
            btnPromover.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPromover.ForeColor = Color.White;
            btnPromover.Location = new Point(859, 180);
            btnPromover.Name = "btnPromover";
            btnPromover.Size = new Size(125, 58);
            btnPromover.TabIndex = 17;
            btnPromover.Text = "Promover";
            btnPromover.UseVisualStyleBackColor = false;
            btnPromover.Click += btnPromover_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.CadetBlue;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(703, 180);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(125, 62);
            btnActualizar.TabIndex = 18;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(113, 687);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 19;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnInfoAlumno
            // 
            btnInfoAlumno.BackColor = Color.CadetBlue;
            btnInfoAlumno.Cursor = Cursors.Hand;
            btnInfoAlumno.FlatStyle = FlatStyle.Flat;
            btnInfoAlumno.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInfoAlumno.ForeColor = Color.White;
            btnInfoAlumno.Location = new Point(807, 84);
            btnInfoAlumno.Name = "btnInfoAlumno";
            btnInfoAlumno.Size = new Size(219, 53);
            btnInfoAlumno.TabIndex = 20;
            btnInfoAlumno.Text = "Ver Información";
            btnInfoAlumno.UseVisualStyleBackColor = false;
            btnInfoAlumno.Click += btnInfoAlumno_Click;
            // 
            // frmAlumnos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1582, 753);
            Controls.Add(btnInfoAlumno);
            Controls.Add(lblMensaje);
            Controls.Add(btnActualizar);
            Controls.Add(btnPromover);
            Controls.Add(dgvAlumnos);
            Controls.Add(btnVerNivel);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(chkIntensivo);
            Controls.Add(lblFechaNacimiento);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtCorreo);
            Controls.Add(txtTelefono);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblCorreo);
            Controls.Add(lblTelefono);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmAlumnos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Alumnos";
            Load += frmAlumnos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private DateTimePicker dtpFechaNacimiento;
        private Label lblFechaNacimiento;
        private CheckBox chkIntensivo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Button btnVerNivel;
        private DataGridView dgvAlumnos;
        private Button btnPromover;
        private Button btnActualizar;
        private Label lblMensaje;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Button btnInfoAlumno;
    }
    // dariel11
}