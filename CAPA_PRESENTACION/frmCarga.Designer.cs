namespace CAPA_PRESENTACION
{
    partial class frmCarga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarga));
            lblEstado = new Label();
            lblPorcentaje = new Label();
            lblCopyright = new Label();
            pnlBarraFondo = new Panel();
            pnlBarra = new Panel();
            pnlProgreso = new Panel();
            panel2 = new Panel();
            picLogo = new PictureBox();
            lblVersion = new Label();
            lblPunto = new Label();
            pnlBarraFondo.SuspendLayout();
            pnlProgreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.BackColor = Color.Transparent;
            lblEstado.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEstado.ForeColor = Color.White;
            lblEstado.Location = new Point(450, 465);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(186, 25);
            lblEstado.TabIndex = 0;
            lblEstado.Text = "Inicializando sistema...";
            // 
            // lblPorcentaje
            // 
            lblPorcentaje.AutoSize = true;
            lblPorcentaje.BackColor = Color.Transparent;
            lblPorcentaje.ForeColor = Color.FromArgb(242, 183, 5);
            lblPorcentaje.Location = new Point(905, 507);
            lblPorcentaje.Name = "lblPorcentaje";
            lblPorcentaje.Size = new Size(29, 20);
            lblPorcentaje.TabIndex = 1;
            lblPorcentaje.Text = "0%";
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.BackColor = Color.Transparent;
            lblCopyright.ForeColor = Color.Gainsboro;
            lblCopyright.Location = new Point(28, 710);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(132, 20);
            lblCopyright.TabIndex = 2;
            lblCopyright.Text = "© 2026 Lexbridge ";
            // 
            // pnlBarraFondo
            // 
            pnlBarraFondo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlBarraFondo.BackColor = Color.FromArgb(70, 82, 110);
            pnlBarraFondo.Controls.Add(pnlBarra);
            pnlBarraFondo.Location = new Point(285, 515);
            pnlBarraFondo.Name = "pnlBarraFondo";
            pnlBarraFondo.Size = new Size(600, 14);
            pnlBarraFondo.TabIndex = 3;
            // 
            // pnlBarra
            // 
            pnlBarra.BackColor = Color.FromArgb(242, 183, 5);
            pnlBarra.Location = new Point(3, 0);
            pnlBarra.Name = "pnlBarra";
            pnlBarra.Size = new Size(0, 8);
            pnlBarra.TabIndex = 4;
            // 
            // pnlProgreso
            // 
            pnlProgreso.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlProgreso.BackColor = Color.FromArgb(70, 82, 110);
            pnlProgreso.Controls.Add(panel2);
            pnlProgreso.Location = new Point(285, 515);
            pnlProgreso.Name = "pnlProgreso";
            pnlProgreso.Size = new Size(0, 14);
            pnlProgreso.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(242, 183, 5);
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(0, 8);
            panel2.TabIndex = 4;
            // 
            // picLogo
            // 
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.Location = new Point(300, 24);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(585, 410);
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.TabIndex = 6;
            picLogo.TabStop = false;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.ForeColor = Color.Gainsboro;
            lblVersion.Location = new Point(175, 710);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(84, 20);
            lblVersion.TabIndex = 7;
            lblVersion.Text = " Version 1.0";
            // 
            // lblPunto
            // 
            lblPunto.AutoSize = true;
            lblPunto.BackColor = Color.Transparent;
            lblPunto.ForeColor = Color.Gainsboro;
            lblPunto.Location = new Point(160, 170);
            lblPunto.Name = "lblPunto";
            lblPunto.Size = new Size(19, 20);
            lblPunto.TabIndex = 8;
            lblPunto.Text = "• ";
            // 
            // frmCarga
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(10, 27, 57);
            ClientSize = new Size(1182, 703);
            Controls.Add(lblPunto);
            Controls.Add(lblVersion);
            Controls.Add(pnlProgreso);
            Controls.Add(pnlBarraFondo);
            Controls.Add(lblCopyright);
            Controls.Add(lblPorcentaje);
            Controls.Add(lblEstado);
            Controls.Add(picLogo);
            Name = "frmCarga";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCarga";
   
            pnlBarraFondo.ResumeLayout(false);
            pnlProgreso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEstado;
        private Label lblPorcentaje;
        private Label lblCopyright;
        private Panel pnlBarraFondo;
        private Panel pnlBarra;
        private Panel pnlProgreso;
        private Panel panel2;
        private PictureBox picLogo;
        private Label lblVersion;
        private Label lblPunto;
    }
}