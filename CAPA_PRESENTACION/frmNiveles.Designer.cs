namespace CAPA_PRESENTACION
{
    partial class frmNiveles
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
            //TODO: Controles ORIGINALES del formulario (ya existían, no se tocaron).
            lblTitulo = new Label();
            lblNombreNivel = new Label();
            lblDuracion = new Label();
            lblCosto = new Label();
            txtNombreNivel = new TextBox();
            txtDuracion = new TextBox();
            txtCosto = new TextBox();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            dgvNiveles = new DataGridView();
            //TODO: Controles NUEVOS agregados para cumplir Requisito 1 (Buscar)
            //TODO: y para mostrar el total de registros cargados.
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvNiveles).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo  //TODO: Título del formulario "Gestión de Niveles" (original).
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(443, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(279, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Niveles";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombreNivel  //TODO: Etiqueta "Nombre del Nivel *" (original).
            // 
            lblNombreNivel.AutoSize = true;
            lblNombreNivel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreNivel.ForeColor = Color.White;
            lblNombreNivel.Location = new Point(52, 106);
            lblNombreNivel.Name = "lblNombreNivel";
            lblNombreNivel.Size = new Size(219, 31);
            lblNombreNivel.TabIndex = 1;
            lblNombreNivel.Text = "Nombre del Nivel *";
            // 
            // lblDuracion  //TODO: Etiqueta "Duración (Meses) *" (original).
            // 
            lblDuracion.AutoSize = true;
            lblDuracion.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDuracion.ForeColor = Color.White;
            lblDuracion.Location = new Point(43, 243);
            lblDuracion.Name = "lblDuracion";
            lblDuracion.Size = new Size(216, 31);
            lblDuracion.TabIndex = 2;
            lblDuracion.Text = "Duración (Meses) *";
            // 
            // lblCosto  //TODO: Etiqueta "Costo *" (original).
            // 
            lblCosto.AutoSize = true;
            lblCosto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCosto.ForeColor = Color.White;
            lblCosto.Location = new Point(109, 365);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(91, 31);
            lblCosto.TabIndex = 3;
            lblCosto.Text = "Costo *";
            // 
            // txtNombreNivel  //TODO: Cuadro de texto donde se escribe el nombre del nivel (original).
            // 
            txtNombreNivel.Location = new Point(52, 168);
            txtNombreNivel.Name = "txtNombreNivel";
            txtNombreNivel.Size = new Size(207, 27);
            txtNombreNivel.TabIndex = 4;
            // 
            // txtDuracion  //TODO: Cuadro de texto donde se escribe la duración en meses (original).
            // 
            txtDuracion.Location = new Point(52, 300);
            txtDuracion.Name = "txtDuracion";
            txtDuracion.Size = new Size(207, 27);
            txtDuracion.TabIndex = 5;
            txtDuracion.KeyPress += txtDuracion_KeyPress;
            // 
            // txtCosto  //TODO: Cuadro de texto donde se escribe el costo del nivel (original).
            // 
            txtCosto.Location = new Point(52, 419);
            txtCosto.Name = "txtCosto";
            txtCosto.Size = new Size(207, 27);
            txtCosto.TabIndex = 6;
            txtCosto.KeyPress += txtCosto_KeyPress;
            // 
            // lblMensaje  //TODO: Etiqueta roja donde se muestran errores de validación (original).
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(62, 610);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 28);
            lblMensaje.TabIndex = 7;
            // 
            // btnGuardar  //TODO: Botón que INSERTA un nuevo nivel (original).
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(915, 181);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 54);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar  //TODO: Botón que EDITA el nivel seleccionado (original).
            // 
            btnActualizar.BackColor = Color.CadetBlue;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(695, 181);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(125, 58);
            btnActualizar.TabIndex = 9;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnLimpiar  //TODO: Botón que limpia los campos del formulario (original).
            // 
            btnLimpiar.BackColor = Color.CadetBlue;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(695, 95);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(125, 54);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar  //TODO: Botón que ELIMINA el nivel seleccionado (original).
            // 
            btnEliminar.BackColor = Color.Red;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(915, 95);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 54);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvNiveles  //TODO: Grilla (tabla) que muestra la lista de niveles (original).
            // 
            dgvNiveles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNiveles.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvNiveles.BorderStyle = BorderStyle.None;
            dgvNiveles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNiveles.Location = new Point(443, 300);
            dgvNiveles.MultiSelect = false;
            dgvNiveles.Name = "dgvNiveles";
            dgvNiveles.RowHeadersWidth = 30;
            dgvNiveles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNiveles.Size = new Size(684, 424);
            dgvNiveles.TabIndex = 12;
            dgvNiveles.SelectionChanged += dgvNiveles_SelectionChanged;
            // 
            // lblBuscar  //TODO: Etiqueta del cuadro de búsqueda (NUEVO).
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(52, 460);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(73, 28);
            lblBuscar.TabIndex = 13;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar  //TODO: Cuadro donde el usuario escribe el nombre del nivel a buscar (NUEVO).
            // 
            txtBuscar.Location = new Point(52, 495);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Nombre del nivel";
            txtBuscar.Size = new Size(207, 27);
            txtBuscar.TabIndex = 14;
            // 
            // btnBuscar  //TODO: Botón que ejecuta la búsqueda contra la base de datos (NUEVO).
            // 
            btnBuscar.BackColor = Color.CadetBlue;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(52, 530);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(207, 37);
            btnBuscar.TabIndex = 15;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lblTotal  //TODO: Muestra "Total de niveles: N" o "Resultados encontrados: N" (NUEVO).
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.Gainsboro;
            lblTotal.Location = new Point(443, 272);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 23);
            lblTotal.TabIndex = 16;
            // 
            // frmNiveles
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1182, 753);
            //TODO: Se agregan los 4 controles nuevos a la colección Controls del formulario.
            Controls.Add(lblTotal);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            //TODO: Controles originales (sin cambios de orden ni de nombre).
            Controls.Add(dgvNiveles);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnActualizar);
            Controls.Add(btnGuardar);
            Controls.Add(lblMensaje);
            Controls.Add(txtCosto);
            Controls.Add(txtDuracion);
            Controls.Add(txtNombreNivel);
            Controls.Add(lblCosto);
            Controls.Add(lblDuracion);
            Controls.Add(lblNombreNivel);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmNiveles";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Niveles";
            Load += frmNiveles_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNiveles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        //TODO: Campos originales (declaración de los controles visuales del formulario).
        private Label lblTitulo;
        private Label lblNombreNivel;
        private Label lblDuracion;
        private Label lblCosto;
        private TextBox txtNombreNivel;
        private TextBox txtDuracion;
        private TextBox txtCosto;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnLimpiar;
        private Button btnEliminar;
        private DataGridView dgvNiveles;
        //TODO: Campos NUEVOS: buscador y etiqueta de total.
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Label lblTotal;
    }
}
