using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace App.API
{
    public class Emails
    {
        public void prepareMessage()
        {
        MimeMessage message = new MimeMessage();
        MailboxAddress from = new MailboxAddress("Admin", "karol.testm@gmail.com");
        message.From.Add(from);
        MailboxAddress to = new MailboxAddress("User", "kaajooj@gmail.com");
        message.To.Add(to);
        message.Subject = "CryptoWebApp alert";

        BodyBuilder bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = "<h1>CryptoWebApp - alert</h1><p>Price below alert(-10) - buy ETH</p>";
        bodyBuilder.TextBody = "Test World!";
        
        message.Body = bodyBuilder.ToMessageBody();

        connectionMessage(message);
        }
       
        private static void connectionMessage(MimeMessage message)
        {
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("karol.testm@gmail.com", "ZJQVkPD6Ttest");
            sendMessage(message, client);
        }

        
        private static void sendMessage(MimeMessage message, SmtpClient client)
        {
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
        

    }

}