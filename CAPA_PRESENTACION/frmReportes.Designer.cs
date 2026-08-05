namespace CAPA_PRESENTACION
{
    partial class frmReportes
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
            txtReporte = new TextBox();
            btnActualizar = new Button();
            btn_ExportarReportePDF = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(417, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(289, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reportes Generales";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtReporte
            // 
            txtReporte.BackColor = Color.FromArgb(44, 62, 90);
            txtReporte.Font = new Font("Microsoft Sans Serif", 8.25F);
            txtReporte.ForeColor = Color.White;
            txtReporte.Location = new Point(29, 96);
            txtReporte.Multiline = true;
            txtReporte.Name = "txtReporte";
            txtReporte.ScrollBars = ScrollBars.Vertical;
            txtReporte.Size = new Size(1113, 429);
            txtReporte.TabIndex = 1;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.CadetBlue;
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(262, 579);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(220, 76);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar Reporte";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btn_ExportarReportePDF
            // 
            btn_ExportarReportePDF.BackColor = Color.CadetBlue;
            btn_ExportarReportePDF.Cursor = Cursors.Hand;
            btn_ExportarReportePDF.FlatStyle = FlatStyle.Flat;
            btn_ExportarReportePDF.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ExportarReportePDF.ForeColor = Color.White;
            btn_ExportarReportePDF.Location = new Point(693, 579);
            btn_ExportarReportePDF.Name = "btn_ExportarReportePDF";
            btn_ExportarReportePDF.Size = new Size(220, 76);
            btn_ExportarReportePDF.TabIndex = 3;
            btn_ExportarReportePDF.Text = "Exportar Reporte";
            btn_ExportarReportePDF.UseVisualStyleBackColor = false;
            btn_ExportarReportePDF.Click += btn_ExportarReportePDF_Click;
            // 
            // frmReportes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1182, 753);
            Controls.Add(btn_ExportarReportePDF);
            Controls.Add(btnActualizar);
            Controls.Add(txtReporte);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes Generales";
            Load += frmReportes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtReporte;
        private Button btnActualizar;
        private Button btn_ExportarReportePDF;
    }
}