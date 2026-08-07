using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    // TODO: Arquitectura en Capas - Clase de negocio perteneciente a CAPA_NEGOCIOS, encapsula el envío de correos electrónicos del sistema (registro, cancelación, pagos)
    // TODO: Clases creadas según su uso, sin código ajeno - Clase dedicada únicamente al envío de correos, sin mezclar lógica de otras entidades
    public sealed class ServicioCorreo
    {
        // TODO: Encapsulacion - Constantes privadas con los datos del servidor SMTP usado para el envío de correos
        private const string ServidorSmtp =
            "smtp.gmail.com";

        private const int PuertoSmtp = 587;

        // TODO: Interfaces Y Asincrónicos - Método base asíncrono que arma y envía el correo vía SMTP; es el que reutilizan EnviarRegistroExitosoAsync, EnviarCancelacionMatriculaAsync y EnviarConfirmacionPagoAsync
        // TODO: Llamadas asíncronas - Usa await con SendMailAsync para enviar el correo sin bloquear el hilo de la interfaz gráfica
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

        // TODO: Interfaces Y Asincrónicos - Método asíncrono que cumple la firma definida en IServicioCorreo, notifica al alumno que su registro fue exitoso
        // TODO: Llamadas asíncronas - Construye el correo HTML y delega el envío a EnviarCorreoAsync mediante await
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

        // TODO: Interfaces Y Asincrónicos - Método asíncrono que cumple la firma definida en IServicioCorreo, notifica al alumno que su matrícula fue cancelada
        // TODO: Llamadas asíncronas - Construye el correo HTML y delega el envío a EnviarCorreoAsync mediante await
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

        // TODO: Llamadas asíncronas - Método asíncrono adicional (no exigido por IServicioCorreo) que envía el recibo de pago con el detalle de la transacción
        // TODO: Interfaces Y Asincrónicos - Construye el correo HTML y delega el envío a EnviarCorreoAsync mediante await; se usa desde frmPagos tras registrar un pago
        public async Task EnviarConfirmacionPagoAsync(
            string destinatario,
            string nombreAlumno,
            string codigoMatricula,
            string nombreNivel,
            DateTime fechaPago,
            decimal monto,
            string metodoPago)
        {
            string nombreSeguro =
                string.IsNullOrWhiteSpace(nombreAlumno)
                    ? "Estudiante"
                    : nombreAlumno.Trim();

            string matriculaSegura =
                string.IsNullOrWhiteSpace(codigoMatricula)
                    ? "No disponible"
                    : codigoMatricula.Trim();

            string nivelSeguro =
                string.IsNullOrWhiteSpace(nombreNivel)
                    ? "No especificado"
                    : nombreNivel.Trim();

            string metodoSeguro =
                string.IsNullOrWhiteSpace(metodoPago)
                    ? "No especificado"
                    : metodoPago.Trim();

            string asunto =
                "Recibo de pago - Lexbridge";

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
                            <div style='font-size:13px; color:#B9C8E8; margin-top:6px;'>RECIBO DE PAGO</div>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding:32px 34px 18px 34px;'>
                            <div style='font-size:23px; font-weight:700; color:#FFBE2E; margin-bottom:16px;'>Pago confirmado</div>
                            <p style='font-size:16px; line-height:1.7; color:#E7EEF9; margin:0 0 18px 0;'>
                                Hola <strong>{WebUtility.HtmlEncode(nombreSeguro)}</strong>,
                            </p>
                            <p style='font-size:15px; line-height:1.7; color:#D7E2F5; margin:0 0 20px 0;'>
                                Hemos registrado correctamente tu pago en el sistema de Lexbridge.
                            </p>
                            <table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color:#102A5C; border-radius:12px; overflow:hidden;'>
                                <tr>
                                    <td style='padding:14px 18px; color:#AFC0DF; border-bottom:1px solid #294779;'>Matricula</td>
                                    <td align='right' style='padding:14px 18px; color:#FFFFFF; font-weight:600; border-bottom:1px solid #294779;'>{WebUtility.HtmlEncode(matriculaSegura)}</td>
                                </tr>
                                <tr>
                                    <td style='padding:14px 18px; color:#AFC0DF; border-bottom:1px solid #294779;'>Nivel</td>
                                    <td align='right' style='padding:14px 18px; color:#FFFFFF; font-weight:600; border-bottom:1px solid #294779;'>{WebUtility.HtmlEncode(nivelSeguro)}</td>
                                </tr>
                                <tr>
                                    <td style='padding:14px 18px; color:#AFC0DF; border-bottom:1px solid #294779;'>Fecha</td>
                                    <td align='right' style='padding:14px 18px; color:#FFFFFF; font-weight:600; border-bottom:1px solid #294779;'>{fechaPago:dd/MM/yyyy}</td>
                                </tr>
                                <tr>
                                    <td style='padding:14px 18px; color:#AFC0DF; border-bottom:1px solid #294779;'>Metodo</td>
                                    <td align='right' style='padding:14px 18px; color:#FFFFFF; font-weight:600; border-bottom:1px solid #294779;'>{WebUtility.HtmlEncode(metodoSeguro)}</td>
                                </tr>
                                <tr>
                                    <td style='padding:16px 18px; color:#AFC0DF;'>Monto pagado</td>
                                    <td align='right' style='padding:16px 18px; color:#31D3C2; font-size:20px; font-weight:700;'>RD${monto:N2}</td>
                                </tr>
                            </table>
                            <div style='background-color:#123B45; border-left:4px solid #19C7B5; padding:15px 17px; border-radius:10px; margin-top:22px; color:#D9F8F4; font-size:14px;'>
                                Conserva este mensaje como comprobante de la operacion.
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding:18px 34px 34px 34px;'>
                            <div style='border-top:1px solid #294779; padding-top:20px; color:#9FB2D6; font-size:13px;'>
                                Atentamente,<br>
                                <strong style='color:#FFFFFF;'>Academia de Ingles Lexbridge</strong>
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
