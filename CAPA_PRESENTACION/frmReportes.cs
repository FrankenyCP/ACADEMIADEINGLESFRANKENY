using CAPA_DATOS;
using CAPA_NEGOCIOS;
using System.Linq;

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
                " REPORTE GENERAL \r\n\r\n" +
                "Total de Alumnos:       " + totalAlumnos + "\r\n" +
                "Total de Niveles:       " + totalNiveles + "\r\n" +
                "Total de Instructores:  " + totalInstructores + "\r\n" +
                "Total de Matrículas:    " + totalMatriculas + "\r\n" +
                "Total Recaudado:        RD$" + totalRecaudado.ToString("N2") + "\r\n\r\n" +
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

        private async void btn_ExportarReportePDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReporte.Text))
                {
                    MessageBox.Show("No hay información de reporte para exportar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog dialogo = new SaveFileDialog())
                {
                    dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    dialogo.FileName = "Reporte_General_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

                    if (dialogo.ShowDialog() != DialogResult.OK)
                        return;

                    string titulo = "Reporte General";
                    List<string> contenido = txtReporte.Text
                        .Split(new[] { "\r\n" }, StringSplitOptions.None)
                        .ToList();

                    iExportador exportador = new ExportadorPdf();
                    bool exito = await exportador.ExportarAsync(titulo, contenido, dialogo.FileName);

                    if (exito)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dialogo.FileName)
                        {
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}