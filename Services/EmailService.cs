using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using VSApi.Interfaces;

namespace VSApi.Services
{
    public class EmailService : IEmailService
    {
        public MimeMessage PrepareMessage(string alertStr, string price, string oldPrice, string change)
        {
            var message = new MimeMessage();
            var from = new MailboxAddress("CryptoWebApp", "karol.testm@gmail.com");
            message.From.Add(from);
            var to = new MailboxAddress("kaajooj@gmail.com", "kaajooj@gmail.com");
            message.To.Add(to);
            message.Subject = "Price alert notification";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = "<h1>Automatic alert</h1><p><font color='green'><h4>" + alertStr + "</h4></font></p>" +
                           "<p>Current Price: " + "<b><font color='green'>" + price + "</font></b>" + "<br>" +
                           "Old Price: " + "<b><font color='green'>" + oldPrice + "</font></b>" + "<br>" +
                           "Price change: " + "<b><font color='green'>" + change + "%</font></b>" + "</p>" +
                           "<p><a href='http://localhost:4200/wallet' style='color:green'>Open your wallet</a></p>"
            };
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        public void SendMessage(MimeMessage message)
        {
            var client = MessageConnectionData();
            client.Send(message);               // using
            client.Disconnect(true);
            client.Dispose();
        }

        private static IMailTransport MessageConnectionData()
        {
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("karol.testm@gmail.com", "ZJQVkPD6Ttest");
            return client;
        }
    }
}
