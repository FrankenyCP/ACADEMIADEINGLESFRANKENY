using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public sealed class ServicioCorreo
    {
        private const string ServidorSmtp =
            "smtp.gmail.com";

        private const int PuertoSmtp = 587;

        public async Task EnviarCorreoAsync(
            string destinatario,
            string asunto,
            string contenidoHtml)
        {
            if (string.IsNullOrWhiteSpace(destinatario))
            {
                throw new ArgumentException(
                    "El destinatario no tiene un correo válido.",
                    nameof(destinatario)
                );
            }

            string correoEmisor =
                Environment.GetEnvironmentVariable(
                    "LEXBRIDGE_CORREO",
                    EnvironmentVariableTarget.User
                ) ?? string.Empty;

            string claveAplicacion =
                Environment.GetEnvironmentVariable(
                    "LEXBRIDGE_CLAVE_CORREO",
                    EnvironmentVariableTarget.User
                ) ?? string.Empty;

            if (string.IsNullOrWhiteSpace(correoEmisor) ||
                string.IsNullOrWhiteSpace(claveAplicacion))
            {
                throw new InvalidOperationException(
                    "Las variables LEXBRIDGE_CORREO y " +
                    "LEXBRIDGE_CLAVE_CORREO no están configuradas."
                );
            }

            using MailMessage mensaje =
                new MailMessage();

            mensaje.From = new MailAddress(
                correoEmisor,
                "Lexbridge"
            );

            mensaje.To.Add(destinatario.Trim());
            mensaje.Subject = asunto;
            mensaje.Body = contenidoHtml;
            mensaje.IsBodyHtml = true;
            mensaje.BodyEncoding = Encoding.UTF8;
            mensaje.SubjectEncoding = Encoding.UTF8;

            using SmtpClient cliente =
                new SmtpClient(
                    ServidorSmtp,
                    PuertoSmtp
                );

            cliente.EnableSsl = true;
            cliente.UseDefaultCredentials = false;

            cliente.Credentials =
                new NetworkCredential(
                    correoEmisor,
                    claveAplicacion
                );

            await cliente.SendMailAsync(mensaje);
        }

        public async Task EnviarRegistroExitosoAsync(
            string destinatario,
            string nombreCompleto)
        {
            string nombreSeguro =
                string.IsNullOrWhiteSpace(nombreCompleto)
                    ? "Estudiante"
                    : nombreCompleto.Trim();

            string asunto =
                "Registro exitoso - Lexbridge";

            string mensajeHtml = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin:0; padding:0; background-color:#071739; font-family:Segoe UI, Arial, sans-serif;'>
    <table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color:#071739; padding:30px 12px;'>
        <tr>
            <td align='center'>
                <table role='presentation' width='600' cellspacing='0' cellpadding='0' border='0' style='max-width:600px; width:100%; background-color:#0B1F4A; border-radius:16px; overflow:hidden; border:1px solid #213E75;'>
                    <tr>
                        <td style='background:linear-gradient(90deg,#0B1F4A,#3D176E); padding:28px 30px; text-align:center;'>
                            <div style='font-size:28px; font-weight:700; color:#FFFFFF; letter-spacing:1px;'>LEXBRIDGE</div>
                            <div style='font-size:13px; color:#B9C8E8; margin-top:6px;'>ACADEMIA DE INGLÉS</div>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:34px 34px 18px 34px;'>
                            <div style='font-size:24px; font-weight:700; color:#FFBE2E; margin-bottom:18px;'>
                                ¡Registro completado!
                            </div>

                            <p style='font-size:16px; line-height:1.7; color:#E7EEF9; margin:0 0 16px 0;'>
                                Hola <strong>{WebUtility.HtmlEncode(nombreSeguro)}</strong>,
                            </p>

                            <p style='font-size:16px; line-height:1.7; color:#D7E2F5; margin:0 0 16px 0;'>
                                Tu registro en Lexbridge fue completado correctamente.
                            </p>

                            <div style='background-color:#102A5C; border-left:4px solid #19C7B5; padding:16px 18px; border-radius:10px; margin:22px 0;'>
                                <div style='font-size:15px; color:#FFFFFF; font-weight:600;'>
                                    Bienvenido a la Academia de Inglés Lexbridge.
                                </div>
                                <div style='font-size:14px; color:#BFCDE8; margin-top:6px;'>
                                    Tu información ya se encuentra registrada en nuestro sistema.
                                </div>
                            </div>

                            <p style='font-size:15px; line-height:1.7; color:#BFCDE8; margin:0;'>
                                Si necesitas asistencia, comunícate con la administración de la academia.
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:18px 34px 34px 34px;'>
                            <div style='border-top:1px solid #294779; padding-top:20px; color:#9FB2D6; font-size:13px; line-height:1.6;'>
                                Atentamente,<br>
                                <strong style='color:#FFFFFF;'>Academia de Inglés Lexbridge</strong>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";

            await EnviarCorreoAsync(
                destinatario,
                asunto,
                mensajeHtml
            );
        }

        public async Task EnviarCancelacionMatriculaAsync(
            string destinatario,
            string nombreAlumno,
            string nombreNivel)
        {
            string nombreSeguro =
                string.IsNullOrWhiteSpace(nombreAlumno)
                    ? "Estudiante"
                    : nombreAlumno.Trim();

            string nivelSeguro =
                string.IsNullOrWhiteSpace(nombreNivel)
                    ? "el nivel registrado"
                    : nombreNivel.Trim();

            string asunto =
                "Cancelación de matrícula - Lexbridge";

            string mensajeHtml = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin:0; padding:0; background-color:#071739; font-family:Segoe UI, Arial, sans-serif;'>
    <table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color:#071739; padding:30px 12px;'>
        <tr>
            <td align='center'>
                <table role='presentation' width='600' cellspacing='0' cellpadding='0' border='0' style='max-width:600px; width:100%; background-color:#0B1F4A; border-radius:16px; overflow:hidden; border:1px solid #213E75;'>
                    <tr>
                        <td style='background:linear-gradient(90deg,#0B1F4A,#3D176E); padding:28px 30px; text-align:center;'>
                            <div style='font-size:28px; font-weight:700; color:#FFFFFF; letter-spacing:1px;'>LEXBRIDGE</div>
                            <div style='font-size:13px; color:#B9C8E8; margin-top:6px;'>ACADEMIA DE INGLÉS</div>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:34px 34px 18px 34px;'>
                            <div style='font-size:24px; font-weight:700; color:#FFBE2E; margin-bottom:18px;'>
                                Matrícula cancelada
                            </div>

                            <p style='font-size:16px; line-height:1.7; color:#E7EEF9; margin:0 0 16px 0;'>
                                Hola <strong>{WebUtility.HtmlEncode(nombreSeguro)}</strong>,
                            </p>

                            <p style='font-size:16px; line-height:1.7; color:#D7E2F5; margin:0 0 16px 0;'>
                                Te informamos que tu matrícula en
                                <strong style='color:#FFFFFF;'>{WebUtility.HtmlEncode(nivelSeguro)}</strong>
                                ha sido cancelada en el sistema de Lexbridge.
                            </p>

                            <div style='background-color:#32152C; border-left:4px solid #E73C72; padding:16px 18px; border-radius:10px; margin:22px 0;'>
                                <div style='font-size:15px; color:#FFFFFF; font-weight:600;'>
                                    Esta matrícula ya no aparece como activa.
                                </div>
                                <div style='font-size:14px; color:#E9BED0; margin-top:6px;'>
                                    El resto de tu información permanece registrada según corresponda.
                                </div>
                            </div>

                            <p style='font-size:15px; line-height:1.7; color:#BFCDE8; margin:0;'>
                                Si consideras que se trata de un error, comunícate con la administración de la academia.
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:18px 34px 34px 34px;'>
                            <div style='border-top:1px solid #294779; padding-top:20px; color:#9FB2D6; font-size:13px; line-height:1.6;'>
                                Atentamente,<br>
                                <strong style='color:#FFFFFF;'>Academia de Inglés Lexbridge</strong>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";

            await EnviarCorreoAsync(
                destinatario,
                asunto,
                mensajeHtml
            );
        }
    }
}