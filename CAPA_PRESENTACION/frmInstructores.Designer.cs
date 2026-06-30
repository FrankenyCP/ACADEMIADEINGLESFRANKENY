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
            ((System.ComponentModel.ISupportInitialize)dgvInstructores).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(435, 28);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(344, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Instructores";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(82, 92);
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
            lblApellido.Location = new Point(82, 217);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(121, 31);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido *";
            // 
            // lblEspecialidad
            // 
            lblEspecialidad.AutoSize = true;
            lblEspecialidad.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEspecialidad.ForeColor = Color.White;
            lblEspecialidad.Location = new Point(58, 355);
            lblEspecialidad.Name = "lblEspecialidad";
            lblEspecialidad.Size = new Size(168, 31);
            lblEspecialidad.TabIndex = 3;
            lblEspecialidad.Text = "Especialidad * ";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTelefono.ForeColor = Color.White;
            lblTelefono.Location = new Point(77, 499);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(123, 31);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono *";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(47, 148);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(179, 27);
            txtNombre.TabIndex = 5;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(47, 271);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(179, 27);
            txtApellido.TabIndex = 6;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // txtEspecialidad
            // 
            txtEspecialidad.Location = new Point(47, 415);
            txtEspecialidad.Name = "txtEspecialidad";
            txtEspecialidad.Size = new Size(179, 27);
            txtEspecialidad.TabIndex = 7;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(47, 573);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(179, 27);
            txtTelefono.TabIndex = 8;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(82, 656);
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
            btnGuardar.Location = new Point(859, 247);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(147, 51);
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
            btnActualizar.Location = new Point(654, 148);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(136, 61);
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
            btnLimpiar.Location = new Point(654, 247);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(136, 51);
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
            btnEliminar.Location = new Point(859, 148);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(147, 61);
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
            dgvInstructores.Location = new Point(395, 344);
            dgvInstructores.MultiSelect = false;
            dgvInstructores.Name = "dgvInstructores";
            dgvInstructores.RowHeadersWidth = 30;
            dgvInstructores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInstructores.Size = new Size(1075, 360);
            dgvInstructores.TabIndex = 14;
            dgvInstructores.CellClick += dgvInstructores_CellClick;
            // 
            // frmInstructores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1482, 753);
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
    }
}