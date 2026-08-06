using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CAPA_NEGOCIOS
{
    public class ServicioCorreo : IServicioCorreo
    {
        private readonly string correoRemitente;
        private readonly string claveAplicacion;

       public ServicioCorreo()
{
    correoRemitente =
        Environment.GetEnvironmentVariable(
            "LEXBRIDGE_CORREO",
            EnvironmentVariableTarget.User
        )
        ??
        Environment.GetEnvironmentVariable(
            "LEXBRIDGE_CORREO",
            EnvironmentVariableTarget.Machine
        )
        ??
        string.Empty;

    claveAplicacion =
        Environment.GetEnvironmentVariable(
            "LEXBRIDGE_CLAVE_CORREO",
            EnvironmentVariableTarget.User
        )
        ??
        Environment.GetEnvironmentVariable(
            "LEXBRIDGE_CLAVE_CORREO",
            EnvironmentVariableTarget.Machine
        )
        ??
        string.Empty;
}
        public async Task EnviarRegistroExitosoAsync(
            string correoDestino,
            string nombreAlumno)
        {
            ValidarConfiguracion();

            if (string.IsNullOrWhiteSpace(correoDestino))
            {
                throw new ArgumentException(
                    "El alumno no tiene un correo registrado."
                );
            }

            MimeMessage mensaje = new MimeMessage();

            mensaje.From.Add(
                new MailboxAddress(
                    "Lexbridge No Reply",
                    correoRemitente
                )
            );

            mensaje.To.Add(
                MailboxAddress.Parse(correoDestino)
            );

            mensaje.Subject =
                "Registro exitoso en Lexbridge";

            BodyBuilder contenido = new BodyBuilder
            {
                HtmlBody = ConstruirCorreoRegistro(
                    nombreAlumno
                ),

                TextBody =
                    "Hola " + nombreAlumno + ".\r\n\r\n" +
                    "Su registro en Lexbridge se completó correctamente.\r\n" +
                    "Ya forma parte de nuestra academia de inglés.\r\n\r\n" +
                    "Este es un mensaje automático. No responda a este correo. Gracias, " +
                    "Lexbridge Academy" +
                    ""
            };

            mensaje.Body = contenido.ToMessageBody();

            await EnviarAsync(mensaje);
        }

        public async Task EnviarConfirmacionPagoAsync(
            string correoDestino,
            string nombreAlumno,
            decimal monto,
            DateTime fechaPago,
            string metodoPago)
        {
            ValidarConfiguracion();

            if (string.IsNullOrWhiteSpace(correoDestino))
            {
                throw new ArgumentException(
                    "El alumno no tiene un correo registrado."
                );
            }

            MimeMessage mensaje = new MimeMessage();

            mensaje.From.Add(
                new MailboxAddress(
                    "Lexbridge No Reply",
                    correoRemitente
                )
            );

            mensaje.To.Add(
                MailboxAddress.Parse(correoDestino)
            );

            mensaje.Subject =
                "Confirmación de pago - Lexbridge";

            BodyBuilder contenido = new BodyBuilder
            {
                HtmlBody = ConstruirCorreoPago(
                    nombreAlumno,
                    monto,
                    fechaPago,
                    metodoPago
                ),

                TextBody =
                    "Hola " + nombreAlumno + ".\r\n\r\n" +
                    "Tu pago fue registrado correctamente.\r\n" +
                    "Monto: RD$" + monto.ToString("N2") + "\r\n" +
                    "Fecha: " + fechaPago.ToString("dd/MM/yyyy") + "\r\n" +
                    "Método: " + metodoPago + "\r\n\r\n" +
                    "Este es un mensaje automático."
            };

            mensaje.Body = contenido.ToMessageBody();

            await EnviarAsync(mensaje);
        }

        private async Task EnviarAsync(
            MimeMessage mensaje)
        {
            using SmtpClient cliente = new SmtpClient();

            try
            {
                await cliente.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    SecureSocketOptions.StartTls
                );

                await cliente.AuthenticateAsync(
                    correoRemitente,
                    claveAplicacion
                );

                await cliente.SendAsync(mensaje);

                await cliente.DisconnectAsync(true);
            }
            catch
            {
                if (cliente.IsConnected)
                {
                    await cliente.DisconnectAsync(true);
                }

                throw;
            }
        }

        private void ValidarConfiguracion()
        {
            if (string.IsNullOrWhiteSpace(correoRemitente))
            {
                throw new InvalidOperationException(
                    "No se configuró la variable LEXBRIDGE_CORREO."
                );
            }

            if (string.IsNullOrWhiteSpace(claveAplicacion))
            {
                throw new InvalidOperationException(
                    "No se configuró la variable LEXBRIDGE_CLAVE_CORREO."
                );
            }
        }

        private static string ConstruirCorreoRegistro(
            string nombreAlumno)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>

<body style='
    margin:0;
    padding:0;
    background-color:#071739;
    font-family:Segoe UI, Arial, sans-serif;
'>

    <div style='
        max-width:620px;
        margin:30px auto;
        background-color:#0D2249;
        border-radius:16px;
        overflow:hidden;
        border:1px solid #253F76;
    '>

        <div style='
            padding:28px;
            text-align:center;
            background:linear-gradient(
                90deg,
                #071B46,
                #54208F
            );
        '>

            <h1 style='
                margin:0;
                color:#FFFFFF;
                font-size:28px;
            '>
                LEXBRIDGE
            </h1>

            <p style='
                margin:7px 0 0;
                color:#C0CAE0;
            '>
                Academia de Inglés
            </p>

        </div>

        <div style='padding:34px;'>

            <h2 style='
                margin-top:0;
                color:#FFBC24;
            '>
                Registro completado
            </h2>

            <p style='
                color:#E4EAF5;
                font-size:16px;
            '>
                Hola, <strong>{nombreAlumno}</strong>.
            </p>

            <p style='
                color:#C7D2E6;
                font-size:15px;
                line-height:1.7;
            '>
                Tu registro en Lexbridge se completó
                correctamente. Ya formas parte de nuestra
                academia de inglés.
            </p>

            <div style='
                margin:25px 0;
                padding:18px;
                background-color:#071739;
                border-left:4px solid #20C9B5;
                border-radius:8px;
            '>

                <p style='
                    margin:0;
                    color:#FFFFFF;
                    font-size:15px;
                '>
                    Próximo paso: seleccionar tu nivel,
                    instructor y completar la matrícula.
                </p>

            </div>

            <p style='
                color:#AAB8D2;
                font-size:14px;
                line-height:1.6;
            '>
                Gracias por elegir Lexbridge para continuar
                tu aprendizaje.
            </p>

        </div>

        <div style='
            padding:19px;
            text-align:center;
            background-color:#071739;
            color:#8798B8;
            font-size:12px;
        '>
            Este es un mensaje automático.
            Por favor, no respondas a este correo.
        </div>

    </div>

</body>
</html>";
        }

        private static string ConstruirCorreoPago(
            string nombreAlumno,
            decimal monto,
            DateTime fechaPago,
            string metodoPago)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>

<body style='
    margin:0;
    padding:0;
    background-color:#071739;
    font-family:Segoe UI, Arial, sans-serif;
'>

    <div style='
        max-width:620px;
        margin:30px auto;
        background-color:#0D2249;
        border-radius:16px;
        overflow:hidden;
        border:1px solid #253F76;
    '>

        <div style='
            padding:28px;
            text-align:center;
            background:linear-gradient(
                90deg,
                #071B46,
                #54208F
            );
        '>

            <h1 style='
                margin:0;
                color:#FFFFFF;
                font-size:28px;
            '>
                LEXBRIDGE
            </h1>

            <p style='
                margin:7px 0 0;
                color:#C0CAE0;
            '>
                Confirmación de pago
            </p>

        </div>

        <div style='padding:34px;'>

            <h2 style='
                margin-top:0;
                color:#20C9B5;
            '>
                Pago registrado correctamente
            </h2>

            <p style='color:#E4EAF5;'>
                Hola, <strong>{nombreAlumno}</strong>.
            </p>

            <p style='
                color:#C7D2E6;
                line-height:1.7;
            '>
                Hemos registrado tu pago con la siguiente
                información:
            </p>

            <table style='
                width:100%;
                border-collapse:collapse;
                margin-top:20px;
            '>

                <tr>
                    <td style='
                        padding:13px;
                        color:#A9B7D1;
                        border-bottom:1px solid #2A4479;
                    '>
                        Monto
                    </td>

                    <td style='
                        padding:13px;
                        text-align:right;
                        color:#48D89F;
                        font-weight:bold;
                        border-bottom:1px solid #2A4479;
                    '>
                        RD${monto:N2}
                    </td>
                </tr>

                <tr>
                    <td style='
                        padding:13px;
                        color:#A9B7D1;
                        border-bottom:1px solid #2A4479;
                    '>
                        Fecha
                    </td>

                    <td style='
                        padding:13px;
                        text-align:right;
                        color:#FFFFFF;
                        border-bottom:1px solid #2A4479;
                    '>
                        {fechaPago:dd/MM/yyyy}
                    </td>
                </tr>

                <tr>
                    <td style='
                        padding:13px;
                        color:#A9B7D1;
                    '>
                        Método
                    </td>

                    <td style='
                        padding:13px;
                        text-align:right;
                        color:#FFFFFF;
                    '>
                        {metodoPago}
                    </td>
                </tr>

            </table>

        </div>

        <div style='
            padding:19px;
            text-align:center;
            background-color:#071739;
            color:#8798B8;
            font-size:12px;
        '>
            Este es un mensaje automático.
            Por favor, no respondas a este correo.
        </div>

    </div>

</body>
</html>";
        }
    }
}