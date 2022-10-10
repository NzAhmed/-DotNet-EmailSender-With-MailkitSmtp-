/************************************************************************
Important
We don't recommend that you use the SmtpClient class for new development 
because SmtpClient doesn't support many modern protocols. 
Use MailKit or other libraries instead.

MailKit is an Open Source cross-platform . NET mail-client library -- for IMAP, POP3, and SMTP --
Mailkit is based on MimeKit and optimized for mobile devices.
    // Install-Package MailKit
    /*
        ------------ Gmail SMTP ------------------
        myaccount.google.com 
        -> security 
        -> 2-Step Verification 
        -> Get Started 

        -> To continue, first verify it's you
        -> setup your phone
        -> 
        2-step verification turned on

        -------------------
        myaccount.google.com
        security -> Signing in to Google
                    -> App passwords
        generate passwords ,,,
    *******************************************************************/
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


void SendMail(string to, string subject, string messageContent)
{
    /*
    string fromAddress = _mailServerConfig.FromAddress;
    string serverAddress = _mailServerConfig.ServerAddress;
    string username = _mailServerConfig.Username;
    string password = _mailServerConfig.Password;
    int port = _mailServerConfig.Port;
    bool isSsl = _mailServerConfig.IsUseSsl;
    */ 

    var message = new MimeMessage ();
    message.From.Add (new MailboxAddress ("Nizo Code", "nizocode@gmail.com"));
    message.To.Add (new MailboxAddress ("Internet Issue", "internetissue@gmail.com"));
    message.Subject = "How you doin'?";

    var bodyBuilder = new BodyBuilder ();

    // 1- Set the plain-text version of the message text
    // ----------------------------------------------
    bodyBuilder.TextBody = @"Hey Friend,
        I just wanted to let you know that brother and I 
        were going to go play some paintball, you in? 
        Regards,,,
        NZ";
   

    // In order to reference test.png from the html text, we'll need to add it
    // to builder.LinkedResources and then use its Content-Id value in the img src.
    var image = bodyBuilder.LinkedResources.Add (@"C:\Users\NZ\Documents\test.png");
    image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId ();

    // 2- Set the html version of the message text
    // ----------------------------------------
    bodyBuilder.HtmlBody =  string.Format (@"<p>Hey Friend,<br>
        <img src='http://vignette3.wikia.nocookie.net/okami/images/2/24/Google_chrome_logo.png/revision/latest?cb=20130220183624' alt='Smiley face' height='42' width='42'>
        <p>I just wanted to let you know that brother and I
        were going to go play some paintball, you in?  </p>
        <br> Regards,,,
        <br> NZ 
        <center><img src='cid:{0}'></center>", image.ContentId);
    

    // We may also want to attach a calendar event for Monica's party...
    bodyBuilder.Attachments.Add (@"C:\Users\NZ\Documents\test.png");
    bodyBuilder.Attachments.Add (@"C:\Users\NZ\Documents\nz.txt");


    // Now we just need to set the message body and we're done
    message.Body = bodyBuilder.ToMessageBody ();




    try
    {
        using (var client = new SmtpClient ()) {
            //client.Connect("HostName", "Port", MailKit.Security.SecureSocketOptions.None);
            client.Connect ("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            // Note: only needed if the SMTP server requires authentication
            client.Authenticate ("nizocode@gmail.com", "iybtzalsooiblkob");

            client.Send (message);
            Console.WriteLine("Success");
            client.Disconnect (true);
        }
    }
    catch (SmtpCommandException ex)
    {
        Console.WriteLine(ex.ToString());              
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());                
    }
}


SendMail("x","x","x");


