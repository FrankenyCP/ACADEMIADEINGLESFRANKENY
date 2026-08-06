namespace CAPA_PRESENTACION
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            lblContrasena = new Label();
            lblMensaje = new Label();
            btnIngresar = new Button();
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(379, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(365, 50);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Academia De Inglés";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 12F);
            lblSubtitulo.ForeColor = Color.White;
            lblSubtitulo.Location = new Point(453, 99);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(222, 28);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Bienvenido, inicia sesión";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Location = new Point(517, 160);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(89, 28);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContrasena.ForeColor = Color.White;
            lblContrasena.Location = new Point(496, 279);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(123, 28);
            lblContrasena.TabIndex = 3;
            lblContrasena.Text = "Contraseña:";
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Location = new Point(235, 490);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 4;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.CadetBlue;
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(494, 391);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(120, 55);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.White;
            txtUsuario.Location = new Point(496, 201);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(125, 27);
            txtUsuario.TabIndex = 6;
            // 
            // txtContrasena
            // 
            txtContrasena.BackColor = Color.White;
            txtContrasena.Location = new Point(494, 330);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(125, 27);
            txtContrasena.TabIndex = 7;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1109, 553);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(btnIngresar);
            Controls.Add(lblMensaje);
            Controls.Add(lblContrasena);
            Controls.Add(lblUsuario);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Academia de Inglés — Login";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblUsuario;
        private Label lblContrasena;
        private Label lblMensaje;
        private Button btnIngresar;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
    }
}
