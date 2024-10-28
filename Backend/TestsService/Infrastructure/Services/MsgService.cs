using System.Text;
using System.Text.Json;
using Domain.Services;
using NATS.Client.Core;


namespace Infrastructure.Services
{
    public class MsgService: IMsgService
    {
        private readonly NatsConnection _connection;
        private readonly ILogger<MsgService> _logger;

        public MsgService(NatsConnection connection, ILogger<MsgService> logger)
        {
            _logger = logger;
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public NatsConnection GetConnection()
        {
            return _connection;
        }

        public void Publish(string subject, string message)
        {
            _connection.PublishAsync(subject, Encoding.UTF8.GetBytes(message));
        }

        public async Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler)
        {
            await using var sub = await _connection.SubscribeCoreAsync<string>(subject);
            _logger.LogInformation($"Subscribed to topic {subject}");

            await foreach (var msg in sub.Msgs.ReadAllAsync())
            {
                _logger.LogInformation($"Received message on {msg.Subject}");

                if (string.IsNullOrEmpty(msg.ReplyTo))
                {
                    _logger.LogWarning("ReplyTo is empty. Cannot send a reply.");
                    continue;
                }

                TRequest request;
                try
                {
                    request = JsonSerializer.Deserialize<TRequest>(msg.Data);
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Failed to deserialize message data.");
                    continue;
                }

                TResponse result;
                try
                {
                    result = await messageHandler(request);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Message handler threw an exception.");
                    continue;
                }

                var response = JsonSerializer.Serialize(result);
                _logger.LogInformation("Sending response to {ReplyTo}", msg.ReplyTo);

                await msg.ReplyAsync(response);
                _logger.LogInformation($"Response {subject} sent successfully");
            }
        }
        public async Task SubscribeAsync<TResponse>(string subject, Func<Task<TResponse>> messageHandler)
        {
            await using var sub = await _connection.SubscribeCoreAsync<string>(subject);
            _logger.LogInformation($"Subscribed to topic {subject}");

            await foreach (var msg in sub.Msgs.ReadAllAsync())
            {
                _logger.LogInformation($"Received message on {msg.Subject}");

                if (string.IsNullOrEmpty(msg.ReplyTo))
                {
                    _logger.LogWarning("ReplyTo is empty. Cannot send a reply.");
                    continue;
                }

              

                TResponse result;
                try
                {
                    result = await messageHandler();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Message handler threw an exception.");
                    continue;
                }

                var response = JsonSerializer.Serialize(result);
                _logger.LogInformation("Sending response to {ReplyTo}", msg.ReplyTo);

                await msg.ReplyAsync(response);
                _logger.LogInformation($"Response {subject} sent successfully");
            }
        }
    }
}