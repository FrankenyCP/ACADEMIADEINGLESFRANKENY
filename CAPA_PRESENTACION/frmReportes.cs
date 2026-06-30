using CAPA_DATOS;
using CAPA_NEGOCIOS;

namespace CAPA_PRESENTACION
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

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
                "======== REPORTE GENERAL ========\r\n\r\n" +
                "Total de Alumnos:       " + totalAlumnos + "\r\n" +
                "Total de Niveles:       " + totalNiveles + "\r\n" +
                "Total de Instructores:  " + totalInstructores + "\r\n" +
                "Total de Matrículas:    " + totalMatriculas + "\r\n" +
                "Total Recaudado:        " + totalRecaudado.ToString("C") + "\r\n\r\n" +
                ciclo.ObtenerResumen() + "\r\n" +
                "Ciclo activo: " + (ciclo.EstaActivo() ? "Sí" : "No");
        }

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
    }
}