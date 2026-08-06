namespace CAPA_PRESENTACION
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            pnlDashboard = new Panel();
            lblTituloAlumnos = new Label();
            lblTotalAlumnos = new Label();
            lblTituloMatriculas = new Label();
            lblTotalMatriculas = new Label();
            lblTituloInstructores = new Label();
            lblTotalInstructores = new Label();
            lblTituloIngresos = new Label();
            lblTotalIngresos = new Label();
            lblTituloPendiente = new Label();
            lblTotalPendiente = new Label();
            lblTituloNivel = new Label();
            lblNivelPopular = new Label();
            lblEstadoDashboard = new Label();
            btnRefrescarDashboard = new Button();
            btnAlumnos = new Button();
            btnNiveles = new Button();
            btnInstructores = new Button();
            btnMatriculas = new Button();
            btnPagos = new Button();
            btnReportes = new Button();
            btnConsultaMatriculas = new Button();
            btnSalir = new Button();
            picLogo = new PictureBox();
            pnlNotificaciones = new Panel();
            lblTituloNotificaciones = new Label();
            lblCantidadNotificaciones = new Label();
            lstNotificaciones = new ListBox();
            pnlEnglishWorld = new Panel();
            label1 = new Label();
            lblTituloEnglishWorld = new Label();
            lblTipoContenido = new Label();
            lblTituloContenido = new Label();
            lblDetalleContenido = new Label();
            btnSiguienteContenido = new Button();
            lblFechaHora = new Label();
            pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlNotificaciones.SuspendLayout();
            pnlEnglishWorld.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(14, 27);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(526, 60);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Academia de Inglés";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDashboard
            // 
            pnlDashboard.BackColor = Color.FromArgb(13, 27, 42);
            pnlDashboard.BorderStyle = BorderStyle.FixedSingle;
            pnlDashboard.Controls.Add(lblTituloAlumnos);
            pnlDashboard.Controls.Add(lblTotalAlumnos);
            pnlDashboard.Controls.Add(lblTituloMatriculas);
            pnlDashboard.Controls.Add(lblTotalMatriculas);
            pnlDashboard.Controls.Add(lblTituloInstructores);
            pnlDashboard.Controls.Add(lblTotalInstructores);
            pnlDashboard.Controls.Add(lblTituloIngresos);
            pnlDashboard.Controls.Add(lblTotalIngresos);
            pnlDashboard.Controls.Add(lblTituloPendiente);
            pnlDashboard.Controls.Add(lblTotalPendiente);
            pnlDashboard.Controls.Add(lblTituloNivel);
            pnlDashboard.Controls.Add(lblNivelPopular);
            pnlDashboard.Controls.Add(lblEstadoDashboard);
            pnlDashboard.Controls.Add(btnRefrescarDashboard);
            pnlDashboard.Location = new Point(14, 100);
            pnlDashboard.Margin = new Padding(3, 4, 3, 4);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new Size(649, 266);
            pnlDashboard.TabIndex = 2;
            // 
            // lblTituloAlumnos
            // 
            lblTituloAlumnos.AutoSize = true;
            lblTituloAlumnos.Font = new Font("Segoe UI", 9F);
            lblTituloAlumnos.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloAlumnos.Location = new Point(23, 20);
            lblTituloAlumnos.Name = "lblTituloAlumnos";
            lblTituloAlumnos.Size = new Size(92, 20);
            lblTituloAlumnos.TabIndex = 0;
            lblTituloAlumnos.Text = "👥 Alumnos";
            // 
            // lblTotalAlumnos
            // 
            lblTotalAlumnos.AutoSize = true;
            lblTotalAlumnos.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalAlumnos.ForeColor = Color.White;
            lblTotalAlumnos.Location = new Point(23, 47);
            lblTotalAlumnos.Name = "lblTotalAlumnos";
            lblTotalAlumnos.Size = new Size(40, 46);
            lblTotalAlumnos.TabIndex = 1;
            lblTotalAlumnos.Text = "0";
            // 
            // lblTituloMatriculas
            // 
            lblTituloMatriculas.AutoSize = true;
            lblTituloMatriculas.Font = new Font("Segoe UI", 9F);
            lblTituloMatriculas.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloMatriculas.Location = new Point(229, 20);
            lblTituloMatriculas.Name = "lblTituloMatriculas";
            lblTituloMatriculas.Size = new Size(102, 20);
            lblTituloMatriculas.TabIndex = 2;
            lblTituloMatriculas.Text = "📋 Matrículas";
            // 
            // lblTotalMatriculas
            // 
            lblTotalMatriculas.AutoSize = true;
            lblTotalMatriculas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalMatriculas.ForeColor = Color.White;
            lblTotalMatriculas.Location = new Point(229, 47);
            lblTotalMatriculas.Name = "lblTotalMatriculas";
            lblTotalMatriculas.Size = new Size(40, 46);
            lblTotalMatriculas.TabIndex = 3;
            lblTotalMatriculas.Text = "0";
            // 
            // lblTituloInstructores
            // 
            lblTituloInstructores.AutoSize = true;
            lblTituloInstructores.Font = new Font("Segoe UI", 9F);
            lblTituloInstructores.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloInstructores.Location = new Point(446, 20);
            lblTituloInstructores.Name = "lblTituloInstructores";
            lblTituloInstructores.Size = new Size(110, 20);
            lblTituloInstructores.TabIndex = 4;
            lblTituloInstructores.Text = "👨‍🏫 Instructores";
            // 
            // lblTotalInstructores
            // 
            lblTotalInstructores.AutoSize = true;
            lblTotalInstructores.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalInstructores.ForeColor = Color.White;
            lblTotalInstructores.Location = new Point(446, 47);
            lblTotalInstructores.Name = "lblTotalInstructores";
            lblTotalInstructores.Size = new Size(40, 46);
            lblTotalInstructores.TabIndex = 5;
            lblTotalInstructores.Text = "0";
            // 
            // lblTituloIngresos
            // 
            lblTituloIngresos.AutoSize = true;
            lblTituloIngresos.Font = new Font("Segoe UI", 9F);
            lblTituloIngresos.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloIngresos.Location = new Point(23, 133);
            lblTituloIngresos.Name = "lblTituloIngresos";
            lblTituloIngresos.Size = new Size(89, 20);
            lblTituloIngresos.TabIndex = 6;
            lblTituloIngresos.Text = "💰 Ingresos";
            // 
            // lblTotalIngresos
            // 
            lblTotalIngresos.AutoSize = true;
            lblTotalIngresos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalIngresos.ForeColor = Color.FromArgb(46, 213, 115);
            lblTotalIngresos.Location = new Point(23, 157);
            lblTotalIngresos.Name = "lblTotalIngresos";
            lblTotalIngresos.Size = new Size(111, 32);
            lblTotalIngresos.TabIndex = 7;
            lblTotalIngresos.Text = "RD$0.00";
            // 
            // lblTituloPendiente
            // 
            lblTituloPendiente.AutoSize = true;
            lblTituloPendiente.Font = new Font("Segoe UI", 9F);
            lblTituloPendiente.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloPendiente.Location = new Point(229, 133);
            lblTituloPendiente.Name = "lblTituloPendiente";
            lblTituloPendiente.Size = new Size(99, 20);
            lblTituloPendiente.TabIndex = 8;
            lblTituloPendiente.Text = "⏳ Pendiente";
            // 
            // lblTotalPendiente
            // 
            lblTotalPendiente.AutoSize = true;
            lblTotalPendiente.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalPendiente.ForeColor = Color.FromArgb(255, 165, 0);
            lblTotalPendiente.Location = new Point(229, 157);
            lblTotalPendiente.Name = "lblTotalPendiente";
            lblTotalPendiente.Size = new Size(111, 32);
            lblTotalPendiente.TabIndex = 9;
            lblTotalPendiente.Text = "RD$0.00";
            // 
            // lblTituloNivel
            // 
            lblTituloNivel.AutoSize = true;
            lblTituloNivel.Font = new Font("Segoe UI", 9F);
            lblTituloNivel.ForeColor = Color.FromArgb(160, 180, 200);
            lblTituloNivel.Location = new Point(446, 133);
            lblTituloNivel.Name = "lblTituloNivel";
            lblTituloNivel.Size = new Size(122, 20);
            lblTituloNivel.TabIndex = 10;
            lblTituloNivel.Text = "🏆 Nivel Popular";
            // 
            // lblNivelPopular
            // 
            lblNivelPopular.AutoSize = true;
            lblNivelPopular.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblNivelPopular.ForeColor = Color.FromArgb(79, 214, 255);
            lblNivelPopular.Location = new Point(446, 157);
            lblNivelPopular.Name = "lblNivelPopular";
            lblNivelPopular.Size = new Size(24, 32);
            lblNivelPopular.TabIndex = 11;
            lblNivelPopular.Text = "-";
            // 
            // lblEstadoDashboard
            // 
            lblEstadoDashboard.AutoSize = true;
            lblEstadoDashboard.Font = new Font("Segoe UI", 8F);
            lblEstadoDashboard.ForeColor = Color.LimeGreen;
            lblEstadoDashboard.Location = new Point(23, 227);
            lblEstadoDashboard.Name = "lblEstadoDashboard";
            lblEstadoDashboard.Size = new Size(0, 19);
            lblEstadoDashboard.TabIndex = 12;
            // 
            // btnRefrescarDashboard
            // 
            btnRefrescarDashboard.BackColor = Color.FromArgb(46, 134, 222);
            btnRefrescarDashboard.Cursor = Cursors.Hand;
            btnRefrescarDashboard.FlatAppearance.BorderSize = 0;
            btnRefrescarDashboard.FlatStyle = FlatStyle.Flat;
            btnRefrescarDashboard.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefrescarDashboard.ForeColor = Color.White;
            btnRefrescarDashboard.Location = new Point(480, 216);
            btnRefrescarDashboard.Margin = new Padding(3, 4, 3, 4);
            btnRefrescarDashboard.Name = "btnRefrescarDashboard";
            btnRefrescarDashboard.Size = new Size(149, 40);
            btnRefrescarDashboard.TabIndex = 13;
            btnRefrescarDashboard.Text = "↻ Actualizar";
            btnRefrescarDashboard.UseVisualStyleBackColor = false;
            btnRefrescarDashboard.Click += btnRefrescarDashboard_Click;
            // 
            // btnAlumnos
            // 
            btnAlumnos.BackColor = Color.FromArgb(52, 152, 219);
            btnAlumnos.Cursor = Cursors.Hand;
            btnAlumnos.FlatAppearance.BorderSize = 0;
            btnAlumnos.FlatStyle = FlatStyle.Flat;
            btnAlumnos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAlumnos.ForeColor = Color.White;
            btnAlumnos.Location = new Point(14, 393);
            btnAlumnos.Margin = new Padding(3, 4, 3, 4);
            btnAlumnos.Name = "btnAlumnos";
            btnAlumnos.Size = new Size(200, 67);
            btnAlumnos.TabIndex = 3;
            btnAlumnos.Text = "Alumnos";
            btnAlumnos.UseVisualStyleBackColor = false;
            btnAlumnos.Click += btnAlumnos_Click;
            // 
            // btnNiveles
            // 
            btnNiveles.BackColor = Color.FromArgb(52, 152, 219);
            btnNiveles.Cursor = Cursors.Hand;
            btnNiveles.FlatAppearance.BorderSize = 0;
            btnNiveles.FlatStyle = FlatStyle.Flat;
            btnNiveles.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNiveles.ForeColor = Color.White;
            btnNiveles.Location = new Point(229, 393);
            btnNiveles.Margin = new Padding(3, 4, 3, 4);
            btnNiveles.Name = "btnNiveles";
            btnNiveles.Size = new Size(200, 67);
            btnNiveles.TabIndex = 4;
            btnNiveles.Text = "Niveles";
            btnNiveles.UseVisualStyleBackColor = false;
            btnNiveles.Click += btnNiveles_Click;
            // 
            // btnInstructores
            // 
            btnInstructores.BackColor = Color.FromArgb(52, 152, 219);
            btnInstructores.Cursor = Cursors.Hand;
            btnInstructores.FlatAppearance.BorderSize = 0;
            btnInstructores.FlatStyle = FlatStyle.Flat;
            btnInstructores.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnInstructores.ForeColor = Color.White;
            btnInstructores.Location = new Point(446, 393);
            btnInstructores.Margin = new Padding(3, 4, 3, 4);
            btnInstructores.Name = "btnInstructores";
            btnInstructores.Size = new Size(200, 67);
            btnInstructores.TabIndex = 5;
            btnInstructores.Text = "Instructores";
            btnInstructores.UseVisualStyleBackColor = false;
            btnInstructores.Click += btnInstructores_Click;
            // 
            // btnMatriculas
            // 
            btnMatriculas.BackColor = Color.FromArgb(52, 152, 219);
            btnMatriculas.Cursor = Cursors.Hand;
            btnMatriculas.FlatAppearance.BorderSize = 0;
            btnMatriculas.FlatStyle = FlatStyle.Flat;
            btnMatriculas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnMatriculas.ForeColor = Color.White;
            btnMatriculas.Location = new Point(14, 480);
            btnMatriculas.Margin = new Padding(3, 4, 3, 4);
            btnMatriculas.Name = "btnMatriculas";
            btnMatriculas.Size = new Size(200, 67);
            btnMatriculas.TabIndex = 6;
            btnMatriculas.Text = "Matrículas";
            btnMatriculas.UseVisualStyleBackColor = false;
            btnMatriculas.Click += btnMatriculas_Click;
            // 
            // btnPagos
            // 
            btnPagos.BackColor = Color.FromArgb(52, 152, 219);
            btnPagos.Cursor = Cursors.Hand;
            btnPagos.FlatAppearance.BorderSize = 0;
            btnPagos.FlatStyle = FlatStyle.Flat;
            btnPagos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPagos.ForeColor = Color.White;
            btnPagos.Location = new Point(229, 480);
            btnPagos.Margin = new Padding(3, 4, 3, 4);
            btnPagos.Name = "btnPagos";
            btnPagos.Size = new Size(200, 67);
            btnPagos.TabIndex = 7;
            btnPagos.Text = "Pagos";
            btnPagos.UseVisualStyleBackColor = false;
            btnPagos.Click += btnPagos_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(52, 152, 219);
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(446, 480);
            btnReportes.Margin = new Padding(3, 4, 3, 4);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(200, 67);
            btnReportes.TabIndex = 8;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnConsultaMatriculas
            // 
            btnConsultaMatriculas.BackColor = Color.FromArgb(142, 68, 173);
            btnConsultaMatriculas.Cursor = Cursors.Hand;
            btnConsultaMatriculas.FlatAppearance.BorderSize = 0;
            btnConsultaMatriculas.FlatStyle = FlatStyle.Flat;
            btnConsultaMatriculas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConsultaMatriculas.ForeColor = Color.White;
            btnConsultaMatriculas.Location = new Point(14, 573);
            btnConsultaMatriculas.Margin = new Padding(3, 4, 3, 4);
            btnConsultaMatriculas.Name = "btnConsultaMatriculas";
            btnConsultaMatriculas.Size = new Size(200, 67);
            btnConsultaMatriculas.TabIndex = 9;
            btnConsultaMatriculas.Text = "📋 Consulta";
            btnConsultaMatriculas.UseVisualStyleBackColor = false;
            btnConsultaMatriculas.Click += btnConsultaMatriculas_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(192, 57, 43);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(229, 573);
            btnSalir.Margin = new Padding(3, 4, 3, 4);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(200, 67);
            btnSalir.TabIndex = 10;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // picLogo
            // 
            picLogo.Location = new Point(560, 13);
            picLogo.Margin = new Padding(3, 4, 3, 4);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(103, 87);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // pnlNotificaciones
            // 
            pnlNotificaciones.BackColor = Color.FromArgb(10, 29, 50);
            pnlNotificaciones.Controls.Add(lstNotificaciones);
            pnlNotificaciones.Controls.Add(lblCantidadNotificaciones);
            pnlNotificaciones.Controls.Add(lblTituloNotificaciones);
            pnlNotificaciones.Location = new Point(720, 75);
            pnlNotificaciones.Name = "pnlNotificaciones";
            pnlNotificaciones.Size = new Size(395, 270);
            pnlNotificaciones.TabIndex = 11;
            // 
            // lblTituloNotificaciones
            // 
            lblTituloNotificaciones.AutoSize = true;
            lblTituloNotificaciones.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloNotificaciones.ForeColor = Color.White;
            lblTituloNotificaciones.Location = new Point(20, 18);
            lblTituloNotificaciones.Name = "lblTituloNotificaciones";
            lblTituloNotificaciones.Size = new Size(204, 38);
            lblTituloNotificaciones.TabIndex = 0;
            lblTituloNotificaciones.Text = "Notificaciones";
            // 
            // lblCantidadNotificaciones
            // 
            lblCantidadNotificaciones.AutoSize = true;
            lblCantidadNotificaciones.BackColor = Color.Crimson;
            lblCantidadNotificaciones.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCantidadNotificaciones.ForeColor = Color.White;
            lblCantidadNotificaciones.Location = new Point(345, 18);
            lblCantidadNotificaciones.Name = "lblCantidadNotificaciones";
            lblCantidadNotificaciones.Size = new Size(20, 23);
            lblCantidadNotificaciones.TabIndex = 1;
            lblCantidadNotificaciones.Text = "0";
            lblCantidadNotificaciones.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstNotificaciones
            // 
            lstNotificaciones.BackColor = Color.FromArgb(17, 42, 70);
            lstNotificaciones.BorderStyle = BorderStyle.None;
            lstNotificaciones.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstNotificaciones.ForeColor = Color.White;
            lstNotificaciones.FormattingEnabled = true;
            lstNotificaciones.Location = new Point(20, 65);
            lstNotificaciones.Name = "lstNotificaciones";
            lstNotificaciones.Size = new Size(355, 161);
            lstNotificaciones.TabIndex = 2;
            // 
            // pnlEnglishWorld
            // 
            pnlEnglishWorld.BackColor = Color.FromArgb(10, 29, 50);
            pnlEnglishWorld.Controls.Add(btnSiguienteContenido);
            pnlEnglishWorld.Controls.Add(lblDetalleContenido);
            pnlEnglishWorld.Controls.Add(lblTituloContenido);
            pnlEnglishWorld.Controls.Add(lblTipoContenido);
            pnlEnglishWorld.Controls.Add(label1);
            pnlEnglishWorld.Controls.Add(lblTituloEnglishWorld);
            pnlEnglishWorld.Location = new Point(720, 365);
            pnlEnglishWorld.Name = "pnlEnglishWorld";
            pnlEnglishWorld.Size = new Size(395, 290);
            pnlEnglishWorld.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Crimson;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(345, 18);
            label1.Name = "label1";
            label1.Size = new Size(20, 23);
            label1.TabIndex = 1;
            label1.Text = "0";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTituloEnglishWorld
            // 
            lblTituloEnglishWorld.AutoSize = true;
            lblTituloEnglishWorld.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloEnglishWorld.ForeColor = Color.White;
            lblTituloEnglishWorld.Location = new Point(20, 18);
            lblTituloEnglishWorld.Name = "lblTituloEnglishWorld";
            lblTituloEnglishWorld.Size = new Size(199, 38);
            lblTituloEnglishWorld.TabIndex = 0;
            lblTituloEnglishWorld.Text = "English World";
            // 
            // lblTipoContenido
            // 
            lblTipoContenido.AutoSize = true;
            lblTipoContenido.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTipoContenido.ForeColor = Color.Gold;
            lblTipoContenido.Location = new Point(20, 65);
            lblTipoContenido.Name = "lblTipoContenido";
            lblTipoContenido.Size = new Size(140, 20);
            lblTipoContenido.TabIndex = 2;
            lblTipoContenido.Text = "PALABRA DEL DIA";
            // 
            // lblTituloContenido
            // 
            lblTituloContenido.AutoSize = true;
            lblTituloContenido.Font = new Font("Segoe UI", 19.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloContenido.ForeColor = Color.White;
            lblTituloContenido.Location = new Point(20, 90);
            lblTituloContenido.Name = "lblTituloContenido";
            lblTituloContenido.Size = new Size(120, 45);
            lblTituloContenido.TabIndex = 3;
            lblTituloContenido.Text = "Bridge";
            // 
            // lblDetalleContenido
            // 
            lblDetalleContenido.AutoSize = true;
            lblDetalleContenido.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDetalleContenido.ForeColor = Color.FromArgb(210, 220, 235);
            lblDetalleContenido.Location = new Point(20, 135);
            lblDetalleContenido.Name = "lblDetalleContenido";
            lblDetalleContenido.Size = new Size(149, 25);
            lblDetalleContenido.TabIndex = 4;
            lblDetalleContenido.Text = "Significa 'Puente'.";
            // 
            // btnSiguienteContenido
            // 
            btnSiguienteContenido.BackColor = Color.FromArgb(52, 152, 219);
            btnSiguienteContenido.FlatStyle = FlatStyle.Flat;
            btnSiguienteContenido.ForeColor = Color.White;
            btnSiguienteContenido.Location = new Point(245, 238);
            btnSiguienteContenido.Name = "btnSiguienteContenido";
            btnSiguienteContenido.Size = new Size(130, 38);
            btnSiguienteContenido.TabIndex = 5;
            btnSiguienteContenido.Text = "Siguiente";
            btnSiguienteContenido.UseVisualStyleBackColor = false;
            // 
            // lblFechaHora
            // 
            lblFechaHora.AutoSize = true;
            lblFechaHora.BackColor = Color.Transparent;
            lblFechaHora.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFechaHora.ForeColor = Color.White;
            lblFechaHora.Location = new Point(790, 25);
            lblFechaHora.Name = "lblFechaHora";
            lblFechaHora.Size = new Size(114, 23);
            lblFechaHora.TabIndex = 6;
            lblFechaHora.Text = "English World";
            lblFechaHora.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 42, 74);
            ClientSize = new Size(1132, 680);
            Controls.Add(lblFechaHora);
            Controls.Add(pnlEnglishWorld);
            Controls.Add(pnlNotificaciones);
            Controls.Add(picLogo);
            Controls.Add(lblTitulo);
            Controls.Add(pnlDashboard);
            Controls.Add(btnAlumnos);
            Controls.Add(btnNiveles);
            Controls.Add(btnInstructores);
            Controls.Add(btnMatriculas);
            Controls.Add(btnPagos);
            Controls.Add(btnReportes);
            Controls.Add(btnConsultaMatriculas);
            Controls.Add(btnSalir);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "frmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Academia de Inglés — Panel Principal";
            Load += frmPrincipal_Load;
            pnlDashboard.ResumeLayout(false);
            pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlNotificaciones.ResumeLayout(false);
            pnlNotificaciones.PerformLayout();
            pnlEnglishWorld.ResumeLayout(false);
            pnlEnglishWorld.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        // TODO: Declaración de controles
        private Label lblTitulo;
        private Label lblTituloAlumnos;
        private Label lblTotalAlumnos;
        private Label lblTituloMatriculas;
        private Label lblTotalMatriculas;
        private Label lblTituloInstructores;
        private Label lblTotalInstructores;
        private Label lblTituloIngresos;
        private Label lblTotalIngresos;
        private Label lblTituloPendiente;
        private Label lblTotalPendiente;
        private Label lblTituloNivel;
        private Label lblNivelPopular;
        private Label lblEstadoDashboard;
        private Button btnRefrescarDashboard;
        private Panel pnlDashboard;
        private Button btnAlumnos;
        private Button btnNiveles;
        private Button btnInstructores;
        private Button btnMatriculas;
        private Button btnPagos;
        private Button btnReportes;
        private Button btnConsultaMatriculas;
        private Button btnSalir;
        private PictureBox picLogo;
        private Panel pnlNotificaciones;
        private Label lblTituloNotificaciones;
        private Label lblCantidadNotificaciones;
        private ListBox lstNotificaciones;
        private Panel pnlEnglishWorld;
        private Label label1;
        private Label lblTituloEnglishWorld;
        private Button btnSiguienteContenido;
        private Label lblDetalleContenido;
        private Label lblTituloContenido;
        private Label lblTipoContenido;
        private Label lblFechaHora;
    }
}