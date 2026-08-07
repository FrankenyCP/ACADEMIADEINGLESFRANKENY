using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    // TODO: Arquitectura en Capas - Formulario perteneciente a CAPA_PRESENTACION, es la opción de "consulta" que ofrece una vista general del sistema
    // TODO: Opción consulta - Permite dar un vistazo a los datos ya guardados en forma de resumen (totales de alumnos, niveles, instructores, matrículas e ingresos)
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        // TODO: Captura de error (try-catch) - Envuelve la carga inicial del reporte en try-catch para evitar el cierre forzado de la aplicación
        private void frmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                CargarReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: Arquitectura en Capas - Consulta varias clases de CAPA_DATOS (AlumnoCD, NivelCD, InstructorCD, MatriculaCD, PagoCD) para construir el resumen general del sistema
        // TODO: Clases y herencia - Instancia un objeto CicloAcademico (CAPA_NEGOCIOS) para mostrar el resumen del ciclo académico vigente y si está activo
        // TODO: Métodos, métodos abstractos y métodos virtuales - Método privado que arma el texto completo del reporte combinando datos de varias entidades
        private void CargarReporte()
        {
            AlumnoCD alumnoCD = new AlumnoCD();
            NivelCD nivelCD = new NivelCD();
            InstructorCD instructorCD = new InstructorCD();
            MatriculaCD matriculaCD = new MatriculaCD();
            PagoCD pagoCD = new PagoCD();

            int totalAlumnos = alumnoCD.ObtenerTodos().Count;
            int totalNiveles = nivelCD.ObtenerTodos().Count;
            int totalInstructores = instructorCD.ObtenerTodos().Count;
            int totalMatriculas = matriculaCD.ObtenerTodos().Count;
            List<_Pago> pagos = pagoCD.ObtenerTodos();

            decimal totalRecaudado = 0;
            for (int i = 0; i < pagos.Count; i++)
            {
                totalRecaudado = totalRecaudado + pagos[i].Monto;
            }

            CicloAcademico ciclo = new CicloAcademico(
                "Ciclo 2026-I", new DateTime(2026, 1, 1), new DateTime(2026, 12, 31));

            txtReporte.Text =
                " REPORTE GENERAL \r\n\r\n" +
                "Total de Alumnos:       " + totalAlumnos + "\r\n" +
                "Total de Niveles:       " + totalNiveles + "\r\n" +
                "Total de Instructores:  " + totalInstructores + "\r\n" +
                "Total de Matrículas:    " + totalMatriculas + "\r\n" +
                "Total Recaudado:        RD$" + totalRecaudado.ToString("N2") + "\r\n\r\n" +
                ciclo.ObtenerResumen() + "\r\n" +
                "Ciclo activo: " + (ciclo.EstaActivo() ? "Sí" : "No");
        }

        // TODO: Opción consulta - Botón que refresca el reporte con los datos más recientes de la base de datos, sin permitir modificarlos
        // TODO: Captura de error (try-catch) - Envuelve la actualización del reporte en try-catch para evitar el cierre forzado de la aplicación
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void coloruiControl1_Click(object sender, EventArgs e)
        {

        }

        private void colorPickerButton1_Click(object sender, EventArgs e)
        {

        }

        private void gridAwareTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}