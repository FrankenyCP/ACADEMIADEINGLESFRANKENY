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
            pnlDatosBancarios = new Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnCerrarPanel = new Button();
            lblDocumento = new Label();
            lblBeneficiario = new Label();
            lblCuenta = new Label();
            lblBanco = new Label();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            btn_ExportarPDF = new Button();
            btn_ExportarExcelfrmPagos = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).BeginInit();
            pnlDatosBancarios.SuspendLayout();
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
            cmbMetodoPago.SelectedIndexChanged += cmbMetodoPago_SelectedIndexChanged;
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
            txtMonto.KeyPress += txtMonto_KeyPress;
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.ForeColor = Color.FromArgb(0, 192, 0);
            lblSaldo.Location = new Point(104, 652);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(0, 20);
            lblSaldo.TabIndex = 9;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(104, 613);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 10;
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
            dgvPagos.Location = new Point(465, 244);
            dgvPagos.MultiSelect = false;
            dgvPagos.Name = "dgvPagos";
            dgvPagos.RowHeadersWidth = 30;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagos.Size = new Size(905, 428);
            dgvPagos.TabIndex = 14;
            dgvPagos.SelectionChanged += dgvPagos_SelectionChanged;
            // 
            // pnlDatosBancarios
            // 
            pnlDatosBancarios.Controls.Add(label4);
            pnlDatosBancarios.Controls.Add(label3);
            pnlDatosBancarios.Controls.Add(label2);
            pnlDatosBancarios.Controls.Add(label1);
            pnlDatosBancarios.Controls.Add(btnCerrarPanel);
            pnlDatosBancarios.Controls.Add(lblDocumento);
            pnlDatosBancarios.Controls.Add(lblBeneficiario);
            pnlDatosBancarios.Controls.Add(lblCuenta);
            pnlDatosBancarios.Controls.Add(lblBanco);
            pnlDatosBancarios.Location = new Point(342, 188);
            pnlDatosBancarios.Name = "pnlDatosBancarios";
            pnlDatosBancarios.Size = new Size(1228, 675);
            pnlDatosBancarios.TabIndex = 15;
            pnlDatosBancarios.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(505, 480);
            label4.Name = "label4";
            label4.Size = new Size(175, 31);
            label4.TabIndex = 8;
            label4.Text = "000-0000000-0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(426, 367);
            label3.Name = "label3";
            label3.Size = new Size(396, 31);
            label3.TabIndex = 7;
            label3.Text = "Frankeny Antonio Castillo Paniagua";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(485, 266);
            label2.Name = "label2";
            label2.Size = new Size(131, 31);
            label2.TabIndex = 6;
            label2.Text = "835387382";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(339, 41);
            label1.Name = "label1";
            label1.Size = new Size(326, 54);
            label1.TabIndex = 5;
            label1.Text = "Datos Bancarios";
            // 
            // btnCerrarPanel
            // 
            btnCerrarPanel.BackColor = Color.Red;
            btnCerrarPanel.FlatStyle = FlatStyle.Flat;
            btnCerrarPanel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrarPanel.ForeColor = Color.White;
            btnCerrarPanel.Location = new Point(1188, 0);
            btnCerrarPanel.Name = "btnCerrarPanel";
            btnCerrarPanel.Size = new Size(40, 40);
            btnCerrarPanel.TabIndex = 4;
            btnCerrarPanel.Text = "X";
            btnCerrarPanel.UseVisualStyleBackColor = false;
            btnCerrarPanel.Click += btnCerrarPanel_Click;
            // 
            // lblDocumento
            // 
            lblDocumento.AutoSize = true;
            lblDocumento.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDocumento.ForeColor = Color.Gold;
            lblDocumento.Location = new Point(200, 480);
            lblDocumento.Name = "lblDocumento";
            lblDocumento.Size = new Size(293, 31);
            lblDocumento.TabIndex = 3;
            lblDocumento.Text = "Documento de Identidad: ";
            // 
            // lblBeneficiario
            // 
            lblBeneficiario.AutoSize = true;
            lblBeneficiario.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBeneficiario.ForeColor = Color.Gold;
            lblBeneficiario.Location = new Point(265, 367);
            lblBeneficiario.Name = "lblBeneficiario";
            lblBeneficiario.Size = new Size(155, 31);
            lblBeneficiario.TabIndex = 2;
            lblBeneficiario.Text = "Beneficiario: ";
            // 
            // lblCuenta
            // 
            lblCuenta.AutoSize = true;
            lblCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCuenta.ForeColor = Color.Gold;
            lblCuenta.Location = new Point(231, 266);
            lblCuenta.Name = "lblCuenta";
            lblCuenta.Size = new Size(227, 31);
            lblCuenta.TabIndex = 1;
            lblCuenta.Text = "Número de Cuenta: ";
            // 
            // lblBanco
            // 
            lblBanco.AutoSize = true;
            lblBanco.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBanco.ForeColor = Color.White;
            lblBanco.Location = new Point(303, 139);
            lblBanco.Name = "lblBanco";
            lblBanco.Size = new Size(405, 41);
            lblBanco.TabIndex = 0;
            lblBanco.Text = " Banco Popular Dominicano";
            // 
            // btn_ExportarPDF
            // 
            btn_ExportarPDF.BackColor = Color.CadetBlue;
            btn_ExportarPDF.Cursor = Cursors.Hand;
            btn_ExportarPDF.FlatStyle = FlatStyle.Flat;
            btn_ExportarPDF.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ExportarPDF.ForeColor = Color.White;
            btn_ExportarPDF.Location = new Point(1097, 112);
            btn_ExportarPDF.Name = "btn_ExportarPDF";
            btn_ExportarPDF.Size = new Size(150, 57);
            btn_ExportarPDF.TabIndex = 16;
            btn_ExportarPDF.Text = "Exportar PDF";
            btn_ExportarPDF.UseVisualStyleBackColor = false;
            btn_ExportarPDF.Click += btnExportarPDF_Click;
            // 
            // btn_ExportarExcelfrmPagos
            // 
            btn_ExportarExcelfrmPagos.BackColor = Color.CadetBlue;
            btn_ExportarExcelfrmPagos.Cursor = Cursors.Hand;
            btn_ExportarExcelfrmPagos.FlatStyle = FlatStyle.Flat;
            btn_ExportarExcelfrmPagos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ExportarExcelfrmPagos.ForeColor = Color.White;
            btn_ExportarExcelfrmPagos.Location = new Point(1277, 112);
            btn_ExportarExcelfrmPagos.Name = "btn_ExportarExcelfrmPagos";
            btn_ExportarExcelfrmPagos.Size = new Size(169, 57);
            btn_ExportarExcelfrmPagos.TabIndex = 17;
            btn_ExportarExcelfrmPagos.Text = "Exportar Excel";
            btn_ExportarExcelfrmPagos.UseVisualStyleBackColor = false;
            btn_ExportarExcelfrmPagos.Click += btn_ExportarExcelfrmPagos_Click;
            // 
            // frmPagos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1400, 800);
            Controls.Add(btn_ExportarExcelfrmPagos);
            Controls.Add(pnlDatosBancarios);
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
            Controls.Add(btn_ExportarPDF);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmPagos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pagos";
            Load += frmPagos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPagos).EndInit();
            pnlDatosBancarios.ResumeLayout(false);
            pnlDatosBancarios.PerformLayout();
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
        private Panel pnlDatosBancarios;
        private Label lblBeneficiario;
        private Label lblCuenta;
        private Label lblBanco;
        private Label lblDocumento;
        private Label label1;
        private Button btnCerrarPanel;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button btn_ExportarPDF;
        private Button btn_ExportarExcelfrmPagos;
    }
}