

namespace Domain.Services
{
    public interface IMsgService
    {
        void Publish(string subject, string message);
        
        void Subscribe(string subject, Action<string> handler);
        
       Task<string> RequestAsync<TRequest>(string subject, TRequest message);
       Task<string> RequestAsync(string subject);
    }
}