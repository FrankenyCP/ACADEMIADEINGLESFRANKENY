namespace CAPA_PRESENTACION
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TODO Integrante 5: configuración obligatoria de QuestPDF (licencia gratuita para uso académico/no comercial).
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}