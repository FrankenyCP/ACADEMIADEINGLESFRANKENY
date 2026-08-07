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
            btnInfoAlumno = new Button();
            btnNuevo = new Button();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
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
            lblNombre.Location = new Point(158, 91);
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
            lblCorreo.Location = new Point(158, 458);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(102, 31);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo *";
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
            btnGuardar.Location = new Point(1010, 271);
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
            btnEliminar.Location = new Point(702, 271);
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
            btnVerNivel.Location = new Point(1010, 180);
            btnVerNivel.Name = "btnVerNivel";
            btnVerNivel.Size = new Size(112, 58);
            btnVerNivel.TabIndex = 15;
            btnVerNivel.Text = "Ver Nivel";
            btnVerNivel.UseVisualStyleBackColor = false;
            btnVerNivel.Click += btnVerNivel_Click;
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlumnos.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvAlumnos.BorderStyle = BorderStyle.None;
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.Location = new Point(384, 354);
            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.RowHeadersWidth = 30;
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.Size = new Size(1150, 368);
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
            btnActualizar.Location = new Point(702, 180);
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
            lblMensaje.Location = new Point(114, 687);
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
            btnInfoAlumno.Location = new Point(702, 91);
            btnInfoAlumno.Name = "btnInfoAlumno";
            btnInfoAlumno.Size = new Size(219, 53);
            btnInfoAlumno.TabIndex = 20;
            btnInfoAlumno.Text = "Ver Información";
            btnInfoAlumno.UseVisualStyleBackColor = false;
            btnInfoAlumno.Click += btnInfoAlumno_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.CadetBlue;
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(971, 91);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(150, 53);
            btnNuevo.TabIndex = 21;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Location = new Point(1198, 133);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Nombre o apellido...";
            txtBuscar.Size = new Size(336, 27);
            txtBuscar.TabIndex = 22;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(1198, 96);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(77, 25);
            lblBuscar.TabIndex = 23;
            lblBuscar.Text = "Buscar:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(84, 146);
            txtNombre.Margin = new Padding(2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(257, 27);
            txtNombre.TabIndex = 24;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(84, 262);
            txtApellido.Margin = new Padding(2);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(257, 27);
            txtApellido.TabIndex = 25;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(84, 387);
            txtTelefono.Margin = new Padding(2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(257, 27);
            txtTelefono.TabIndex = 26;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(84, 518);
            txtCorreo.Margin = new Padding(2);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(257, 27);
            txtCorreo.TabIndex = 27;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(84, 630);
            dtpFechaNacimiento.Margin = new Padding(2);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(241, 27);
            dtpFechaNacimiento.TabIndex = 28;
            // 
            // frmAlumnos
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1400, 800);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtCorreo);
            Controls.Add(txtTelefono);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnNuevo);
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
            Controls.Add(lblCorreo);
            Controls.Add(lblTelefono);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            DoubleBuffered = true;
            MaximizeBox = false;
            MinimumSize = new Size(1000, 650);
            Name = "frmAlumnos";
            StartPosition = FormStartPosition.CenterScreen;
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
        private Button btnInfoAlumno;
        private Button btnNuevo;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private DateTimePicker dtpFechaNacimiento;
    }
    // dariel11
}