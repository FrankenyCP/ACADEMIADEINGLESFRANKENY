namespace CAPA_PRESENTACION
{
    partial class frmPagos
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
            lblMatricula = new Label();
            lblFechaPago = new Label();
            lblMonto = new Label();
            lblMetodoPago = new Label();
            cmbMatricula = new ComboBox();
            cmbMetodoPago = new ComboBox();
            dtpFechaPago = new DateTimePicker();
            txtMonto = new TextBox();
            lblSaldo = new Label();
            lblMensaje = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            dgvPagos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(510, 19);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(259, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Pagos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMatricula
            // 
            lblMatricula.AutoSize = true;
            lblMatricula.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMatricula.ForeColor = Color.White;
            lblMatricula.Location = new Point(104, 89);
            lblMatricula.Name = "lblMatricula";
            lblMatricula.Size = new Size(118, 28);
            lblMatricula.TabIndex = 1;
            lblMatricula.Text = "Matrícula *";
            // 
            // lblFechaPago
            // 
            lblFechaPago.AutoSize = true;
            lblFechaPago.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaPago.ForeColor = Color.White;
            lblFechaPago.Location = new Point(72, 215);
            lblFechaPago.Name = "lblFechaPago";
            lblFechaPago.Size = new Size(163, 28);
            lblFechaPago.TabIndex = 2;
            lblFechaPago.Text = "Fecha de Pago *";
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMonto.ForeColor = Color.White;
            lblMonto.Location = new Point(113, 349);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(90, 28);
            lblMonto.TabIndex = 3;
            lblMonto.Text = "Monto *";
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.AutoSize = true;
            lblMetodoPago.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMetodoPago.ForeColor = Color.White;
            lblMetodoPago.Location = new Point(81, 475);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(183, 28);
            lblMetodoPago.TabIndex = 4;
            lblMetodoPago.Text = "Método de Pago *";
            // 
            // cmbMatricula
            // 
            cmbMatricula.BackColor = Color.FromArgb(44, 62, 90);
            cmbMatricula.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMatricula.ForeColor = Color.White;
            cmbMatricula.FormattingEnabled = true;
            cmbMatricula.Location = new Point(41, 138);
            cmbMatricula.Name = "cmbMatricula";
            cmbMatricula.Size = new Size(236, 28);
            cmbMatricula.TabIndex = 5;
            cmbMatricula.SelectedIndexChanged += cmbMatricula_SelectedIndexChanged;
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.BackColor = Color.FromArgb(44, 62, 90);
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.ForeColor = Color.White;
            cmbMetodoPago.FormattingEnabled = true;
            cmbMetodoPago.Location = new Point(41, 526);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(236, 28);
            cmbMetodoPago.TabIndex = 6;
            // 
            // dtpFechaPago
            // 
            dtpFechaPago.Format = DateTimePickerFormat.Short;
            dtpFechaPago.Location = new Point(97, 267);
            dtpFechaPago.Name = "dtpFechaPago";
            dtpFechaPago.Size = new Size(125, 27);
            dtpFechaPago.TabIndex = 7;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(44, 62, 90);
            txtMonto.ForeColor = Color.White;
            txtMonto.Location = new Point(41, 392);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(236, 27);
            txtMonto.TabIndex = 8;
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.ForeColor = Color.FromArgb(0, 192, 0);
            lblSaldo.Location = new Point(127, 652);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(21, 20);
            lblSaldo.TabIndex = 9;
            lblSaldo.Text = "\"\"";
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(127, 613);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(21, 20);
            lblMensaje.TabIndex = 10;
            lblMensaje.Text = "\"\"";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.CadetBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(905, 112);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 57);
            btnGuardar.TabIndex = 11;
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
            btnLimpiar.Location = new Point(712, 112);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(150, 57);
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
            btnEliminar.Location = new Point(525, 112);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(150, 57);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvPagos
            // 
            dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPagos.BackgroundColor = Color.FromArgb(27, 42, 74);
            dgvPagos.BorderStyle = BorderStyle.None;
            dgvPagos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPagos.Location = new Point(407, 230);
            dgvPagos.Name = "dgvPagos";
            dgvPagos.RowHeadersVisible = false;
            dgvPagos.RowHeadersWidth = 51;
            dgvPagos.Size = new Size(728, 428);
            dgvPagos.TabIndex = 14;
            // 
            // frmPagos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1182, 753);
            Controls.Add(dgvPagos);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(lblMensaje);
            Controls.Add(lblSaldo);
            Controls.Add(txtMonto);
            Controls.Add(dtpFechaPago);
            Controls.Add(cmbMetodoPago);
            Controls.Add(cmbMatricula);
            Controls.Add(lblMetodoPago);
            Controls.Add(lblMonto);
            Controls.Add(lblFechaPago);
            Controls.Add(lblMatricula);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmPagos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pagos";
            ((System.ComponentModel.ISupportInitialize)dgvPagos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblMatricula;
        private Label lblFechaPago;
        private Label lblMonto;
        private Label lblMetodoPago;
        private ComboBox cmbMatricula;
        private ComboBox cmbMetodoPago;
        private DateTimePicker dtpFechaPago;
        private TextBox txtMonto;
        private Label lblSaldo;
        private Label lblMensaje;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Button btnEliminar;
        private DataGridView dgvPagos;
    }
}