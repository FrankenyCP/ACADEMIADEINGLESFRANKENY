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
            lblTitulo.Location = new Point(594, 23);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(360, 48);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Alumnos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(198, 114);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(146, 38);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre *";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(195, 264);
            lblApellido.Margin = new Padding(4, 0, 4, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(148, 38);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido *";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(192, 417);
            lblTelefono.Margin = new Padding(4, 0, 4, 0);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(151, 38);
            lblTelefono.TabIndex = 3;
            lblTelefono.Text = "Teléfono *";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCorreo.ForeColor = Color.White;
            lblCorreo.Location = new Point(198, 573);
            lblCorreo.Margin = new Padding(4, 0, 4, 0);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(126, 38);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo *";
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.AutoSize = true;
            lblFechaNacimiento.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaNacimiento.ForeColor = Color.White;
            lblFechaNacimiento.Location = new Point(105, 723);
            lblFechaNacimiento.Margin = new Padding(4, 0, 4, 0);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(313, 38);
            lblFechaNacimiento.TabIndex = 10;
            lblFechaNacimiento.Text = "Fecha de Nacimiento *";
            // 
            // chkIntensivo
            // 
            chkIntensivo.AutoSize = true;
            chkIntensivo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIntensivo.ForeColor = Color.White;
            chkIntensivo.Location = new Point(504, 265);
            chkIntensivo.Margin = new Padding(4);
            chkIntensivo.Name = "chkIntensivo";
            chkIntensivo.Size = new Size(285, 36);
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
            btnGuardar.Location = new Point(1262, 339);
            btnGuardar.Margin = new Padding(4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 67);
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
            btnEliminar.Location = new Point(878, 339);
            btnEliminar.Margin = new Padding(4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(156, 67);
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
            btnLimpiar.Location = new Point(1074, 339);
            btnLimpiar.Margin = new Padding(4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(156, 67);
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
            btnVerNivel.Location = new Point(1262, 225);
            btnVerNivel.Margin = new Padding(4);
            btnVerNivel.Name = "btnVerNivel";
            btnVerNivel.Size = new Size(140, 73);
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
            dgvAlumnos.Location = new Point(480, 442);
            dgvAlumnos.Margin = new Padding(4);
            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.RowHeadersWidth = 30;
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.Size = new Size(1438, 460);
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
            btnPromover.Location = new Point(1074, 225);
            btnPromover.Margin = new Padding(4);
            btnPromover.Name = "btnPromover";
            btnPromover.Size = new Size(156, 73);
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
            btnActualizar.Location = new Point(878, 225);
            btnActualizar.Margin = new Padding(4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(156, 77);
            btnActualizar.TabIndex = 18;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(142, 859);
            lblMensaje.Margin = new Padding(4, 0, 4, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 25);
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
            btnInfoAlumno.Location = new Point(878, 114);
            btnInfoAlumno.Margin = new Padding(4);
            btnInfoAlumno.Name = "btnInfoAlumno";
            btnInfoAlumno.Size = new Size(274, 66);
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
            btnNuevo.Location = new Point(1214, 114);
            btnNuevo.Margin = new Padding(4);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(188, 66);
            btnNuevo.TabIndex = 21;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Location = new Point(1498, 166);
            txtBuscar.Margin = new Padding(4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Nombre o apellido...";
            txtBuscar.Size = new Size(420, 31);
            txtBuscar.TabIndex = 22;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(1498, 120);
            lblBuscar.Margin = new Padding(4, 0, 4, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(89, 30);
            lblBuscar.TabIndex = 23;
            lblBuscar.Text = "Buscar:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(105, 182);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(320, 31);
            txtNombre.TabIndex = 24;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(105, 327);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(320, 31);
            txtApellido.TabIndex = 25;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(105, 484);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(320, 31);
            txtTelefono.TabIndex = 26;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(105, 647);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(320, 31);
            txtCorreo.TabIndex = 27;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(105, 787);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(300, 31);
            dtpFechaNacimiento.TabIndex = 28;
            // 
            // frmAlumnos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1924, 910);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
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
}