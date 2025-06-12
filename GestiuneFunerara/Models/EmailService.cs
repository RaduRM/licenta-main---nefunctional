using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

public class EmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task TrimiteEmailResetareAsync(string destinatar, string cod)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
        message.To.Add(destinatar);
        message.Subject = "Memorial - Resetare parolă";
        message.Body = $@"
Bună ziua,

Ați solicitat o resetare a parolei pentru contul Memorial.

Codul dumneavoastră de resetare este: {cod}

Introduceți-l în pagina de resetare în următoarele 15 minute.
Dacă nu ați făcut această solicitare, ignorați acest email.

Cu respect,
Echipa Memorial";
        message.IsBodyHtml = false;

        using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }

    public async Task TrimiteEmailActivareContAsync(string destinatar, string cod)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
        message.To.Add(destinatar);
        message.Subject = "Memorial - Activare cont";
        message.Body = $@"
Bun venit în comunitatea Memorial!

Pentru a finaliza înregistrarea contului tău, introdu codul de activare de mai jos:

Cod activare: {cod}

După activare, vei avea acces complet la funcționalitățile platformei.
Dacă nu ai creat acest cont, poți ignora acest mesaj.

Cu respect,
Echipa Memorial";
        message.IsBodyHtml = false;

        using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }

    public async Task TrimiteEmailConfirmarePrecomandaAsync(string destinatar, string cod, int idPrecomanda)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
        message.To.Add(destinatar);
        message.Subject = $"Memorial - Confirmare precomandă #{idPrecomanda}";
        message.Body = $@"
<p>Bună ziua,</p>

<p>Ați trimis o precomandă pe platforma Memorial.</p>

<p><b>Codul dumneavoastră de confirmare:</b> <span style='color:#d9534f; font-size:1.2em;'><b>{cod}</b></span><br/>
<b>ID-ul precomenzii:</b> <span style='color:#5bc0de; font-size:1.2em;'><b>#{idPrecomanda}</b></span></p>

<p>Introduceți-l în pagina de confirmare în următoarele 15 minute.<br/>
Dacă nu ați făcut această solicitare, ignorați acest email.</p>

<p>Cu respect,<br/>
Echipa Memorial</p>";
        message.IsBodyHtml = true;

        using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }

    // 🟩 Metodă pentru trimiterea raportului PDF către admin
    public async Task TrimiteEmailAdminAsync(string emailDestinatar, string subiect, string mesaj, byte[] pdf)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
        message.To.Add(emailDestinatar);
        message.Subject = subiect;
        message.Body = mesaj;
        message.IsBodyHtml = false;

        // Adaugă raportul PDF ca atașament
        using var stream = new MemoryStream(pdf);
        var attachment = new Attachment(stream, "RaportPrecomanda.pdf", "application/pdf");
        message.Attachments.Add(attachment);

        using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}
