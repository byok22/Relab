using NATS.Client.Core;

namespace Domain.Services
{
    public interface IMsgService
    {
        NatsConnection GetConnection();
        void Publish(string subject, string message);
        Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);
        Task SubscribeAsync<TResponse>(string subject, Func<Task<TResponse>> messageHandler);
    }
}