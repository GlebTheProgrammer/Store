using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Store.Messages
{
    public class DebugNotificationService : INotificationService
    {
        public void SendConfirmationCode(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);
        }

        public void StartProcess(Order order)
        {
            using (var client = new SmtpClient())
            {
                var message = new MailMessage("from@at.my.domain", "to@at.my.domain");
                message.Subject = "Заказ #" + order.Id;

                var builder = new StringBuilder();
                foreach (var item in order.Items)
                {
                    builder.Append("{0}, {1}", item.BookId, item.Count);
                    builder.AppendLine();
                }

                message.Body = builder.ToString();
                client.Send(message);
            }
        }

        //public void StartProcess(Order order)
        //{
        //    // отправитель - устанавливаем адрес и отображаемое в письме имя
        //    MailAddress from = new MailAddress("frommail@gmail.com", "Gleb");
        //    // кому отправляем
        //    MailAddress to = new MailAddress("tomail@gmail.com");
        //    // создаем объект сообщения
        //    MailMessage m = new MailMessage(from, to);
        //    // тема письма
        //    m.Subject = "Заказ #" + order.Id;

        //    var builder = new StringBuilder();
        //    foreach (var item in order.Items)
        //    {
        //        builder.Append("{0}, {1}", item.BookId, item.Count);
        //        builder.AppendLine();
        //    }

        //    // текст письма
        //    m.Body = builder.ToString();
        //    // письмо представляет код html
        //    m.IsBodyHtml = true;

        //    const string fromPassword = "fromPassword";

        //    // адрес smtp-сервера и порт, с которого будем отправлять письмо
        //    SmtpClient smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 25,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("from@gmail.com", fromPassword)
        //    };
        //    // логин и пароль
        //    smtp.Credentials = new NetworkCredential("from@gmail.com", "Master95%");
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //}
    }
}
