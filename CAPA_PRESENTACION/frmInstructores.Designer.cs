namespace CAPA_PRESENTACION
{
    partial class frmInstructores
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
            lblEspecialidad = new Label();
            lblTelefono = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtEspecialidad = new TextBox();
            txtTelefono = new TextBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            dgvInstructores = new DataGridView();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvInstructores).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(381, 21);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(281, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Instructores";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(72, 69);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 25);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre *";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApellido.ForeColor = Color.White;
            lblApellido.Location = new Point(72, 163);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(100, 25);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido *";
            // 
            // lblEspecialidad
            // 
            lblEspecialidad.AutoSize = true;
            lblEspecialidad.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEspecialidad.ForeColor = Color.White;
            lblEspecialidad.Location = new Point(51, 266);
            lblEspecialidad.Name = "lblEspecialidad";
            lblEspecialidad.Size = new Size(139, 25);
            lblEspecialidad.TabIndex = 3;
            lblEspecialidad.Text = "Especialidad * ";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(67, 374);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(103, 25);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono *";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(41, 111);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(157, 23);
            txtNombre.TabIndex = 5;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(41, 203);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(157, 23);
            txtApellido.TabIndex = 6;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // txtEspecialidad
            // 
            txtEspecialidad.Location = new Point(41, 311);
            txtEspecialidad.Margin = new Padding(3, 2, 3, 2);
            txtEspecialidad.Name = "txtEspecialidad";
            txtEspecialidad.Size = new Size(157, 23);
            txtEspecialidad.TabIndex = 7;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(41, 430);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(157, 23);
            txtTelefono.TabIndex = 8;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(72, 492);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 15);
            lblMensaje.TabIndex = 9;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(752, 185);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(129, 38);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.CadetBlue;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(572, 111);
            btnActualizar.Margin = new Padding(3, 2, 3, 2);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(119, 46);
            btnActualizar.TabIndex = 11;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.CadetBlue;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(572, 185);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(119, 38);
            btnLimpiar.TabIndex = 12;
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
            btnEliminar.Location = new Point(752, 111);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(129, 46);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvInstructores
            // 
            dgvInstructores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInstructores.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvInstructores.BorderStyle = BorderStyle.None;
            dgvInstructores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInstructores.Location = new Point(346, 258);
            dgvInstructores.Margin = new Padding(3, 2, 3, 2);
            dgvInstructores.MultiSelect = false;
            dgvInstructores.Name = "dgvInstructores";
            dgvInstructores.RowHeadersWidth = 30;
            dgvInstructores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInstructores.Size = new Size(941, 270);
            dgvInstructores.TabIndex = 14;
            dgvInstructores.CellClick += dgvInstructores_CellClick;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(572, 30);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(60, 21);
            lblBuscar.TabIndex = 15;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(572, 57);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Nombre, apellido o especialidad";
            txtBuscar.Size = new Size(176, 23);
            txtBuscar.TabIndex = 16;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.CadetBlue;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(761, 52);
            btnBuscar.Margin = new Padding(3, 2, 3, 2);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(119, 28);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.Gainsboro;
            lblTotal.Location = new Point(346, 238);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 19);
            lblTotal.TabIndex = 18;
            // 
            // frmInstructores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1297, 565);
            Controls.Add(lblTotal);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(dgvInstructores);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnActualizar);
            Controls.Add(btnGuardar);
            Controls.Add(lblMensaje);
            Controls.Add(txtTelefono);
            Controls.Add(txtEspecialidad);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblTelefono);
            Controls.Add(lblEspecialidad);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "frmInstructores";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Instructores";
            Load += frmInstructores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInstructores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        //TODO: Campos originales (declaración de los controles visuales del formulario).
        private Label lblTitulo;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblEspecialidad;
        private Label lblTelefono;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtEspecialidad;
        private TextBox txtTelefono;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnLimpiar;
        private Button btnEliminar;
        private DataGridView dgvInstructores;
        //TODO: Campos NUEVOS: buscador y etiqueta de total.
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label lblTotal;
    }
}
