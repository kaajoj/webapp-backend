using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;

namespace App.API
{
    public class Emails
    {
        public void prepareMessage(String alertStr)
        {
        MimeMessage message = new MimeMessage();
        MailboxAddress from = new MailboxAddress("CryptoWebApp", "karol.testm@gmail.com");
        message.From.Add(from);
        MailboxAddress to = new MailboxAddress("kaajooj@gmail.com", "kaajooj@gmail.com");
        message.To.Add(to);
        message.Subject = "Price alert notification";

        BodyBuilder bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = "<h1>Automatic alert</h1><p><h4>"+ alertStr +"</h4></p>";
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