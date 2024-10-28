using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailNotificationService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var emailSender = services.GetRequiredService<IEmailSender>();

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "email_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailMessage = JsonSerializer.Deserialize<EmailMessage>(message);

                if (emailMessage != null)
                {
                    await emailSender.SendEmailAsync(emailMessage);
                }
            };

            channel.BasicConsume(queue: "email_queue",
                                 autoAck: true,
                                 consumer: consumer);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IEmailSender, EmailSender>();
                });
    }

    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }

    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailMessage.From));
            email.To.Add(MailboxAddress.Parse(emailMessage.To));
            email.Subject = emailMessage.Subject;
            email.Body = new TextPart("plain") { Text = emailMessage.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.your-email-provider.com", 587, false);
            await smtp.AuthenticateAsync("your-email@example.com", "your-email-password");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
