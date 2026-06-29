namespace CAPA_PRESENTACION
{
    partial class frmPrincipal
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
            btnAlumnos = new Button();
            btnNiveles = new Button();
            btnInstructores = new Button();
            btnMatriculas = new Button();
            btnPagos = new Button();
            btnReportes = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(213, 21);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(272, 38);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Academia de Inglés";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAlumnos
            // 
            btnAlumnos.BackColor = Color.CadetBlue;
            btnAlumnos.Cursor = Cursors.Hand;
            btnAlumnos.FlatStyle = FlatStyle.Flat;
            btnAlumnos.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAlumnos.ForeColor = Color.Black;
            btnAlumnos.Location = new Point(266, 102);
            btnAlumnos.Name = "btnAlumnos";
            btnAlumnos.Size = new Size(179, 60);
            btnAlumnos.TabIndex = 1;
            btnAlumnos.Text = "Alumnos";
            btnAlumnos.UseVisualStyleBackColor = false;
            btnAlumnos.Click += btnAlumnos_Click;
            // 
            // btnNiveles
            // 
            btnNiveles.BackColor = Color.CadetBlue;
            btnNiveles.Cursor = Cursors.Hand;
            btnNiveles.FlatStyle = FlatStyle.Flat;
            btnNiveles.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNiveles.Location = new Point(266, 193);
            btnNiveles.Name = "btnNiveles";
            btnNiveles.Size = new Size(179, 57);
            btnNiveles.TabIndex = 2;
            btnNiveles.Text = "Niveles";
            btnNiveles.UseVisualStyleBackColor = false;
            btnNiveles.Click += btnNiveles_Click;
            // 
            // btnInstructores
            // 
            btnInstructores.BackColor = Color.CadetBlue;
            btnInstructores.Cursor = Cursors.Hand;
            btnInstructores.FlatStyle = FlatStyle.Flat;
            btnInstructores.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInstructores.Location = new Point(266, 285);
            btnInstructores.Name = "btnInstructores";
            btnInstructores.Size = new Size(179, 54);
            btnInstructores.TabIndex = 3;
            btnInstructores.Text = "Instructores";
            btnInstructores.UseVisualStyleBackColor = false;
            btnInstructores.Click += btnInstructores_Click;
            // 
            // btnMatriculas
            // 
            btnMatriculas.BackColor = Color.CadetBlue;
            btnMatriculas.Cursor = Cursors.Hand;
            btnMatriculas.FlatStyle = FlatStyle.Flat;
            btnMatriculas.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMatriculas.Location = new Point(266, 366);
            btnMatriculas.Name = "btnMatriculas";
            btnMatriculas.Size = new Size(179, 52);
            btnMatriculas.TabIndex = 4;
            btnMatriculas.Text = "Matrículas";
            btnMatriculas.UseVisualStyleBackColor = false;
            btnMatriculas.Click += btnMatriculas_Click;
            // 
            // btnPagos
            // 
            btnPagos.BackColor = Color.CadetBlue;
            btnPagos.Cursor = Cursors.Hand;
            btnPagos.FlatStyle = FlatStyle.Flat;
            btnPagos.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPagos.Location = new Point(266, 457);
            btnPagos.Name = "btnPagos";
            btnPagos.Size = new Size(179, 51);
            btnPagos.TabIndex = 5;
            btnPagos.Text = "Pagos";
            btnPagos.UseVisualStyleBackColor = false;
            btnPagos.Click += btnPagos_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.CadetBlue;
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.Location = new Point(266, 544);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(179, 54);
            btnReportes.TabIndex = 6;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.Red;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(266, 635);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(179, 48);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(682, 753);
            Controls.Add(btnSalir);
            Controls.Add(btnReportes);
            Controls.Add(btnPagos);
            Controls.Add(btnMatriculas);
            Controls.Add(btnInstructores);
            Controls.Add(btnNiveles);
            Controls.Add(btnAlumnos);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Academia de Inglés";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnAlumnos;
        private Button btnNiveles;
        private Button btnInstructores;
        private Button btnMatriculas;
        private Button btnPagos;
        private Button btnReportes;
        private Button btnSalir;
    }
}