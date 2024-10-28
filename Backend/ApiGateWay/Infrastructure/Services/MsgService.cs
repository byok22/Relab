using Domain.Services;
using NATS.Client.Core;

namespace Infrastructure.Services
{
    public class MsgService : IMsgService
    {
        private readonly NatsConnection _connection;

        public MsgService(NatsConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Publish(string subject, string message)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RequestAsync<TRequest>(string subject, TRequest message)
        {

              var replyOpts = new NatsSubOpts { Timeout = TimeSpan.FromSeconds(45) };
        

                var reply = await _connection.RequestAsync<TRequest,string>(subject,message, replyOpts: replyOpts);

                 return reply.Data??"";

                
         
        }

        public async Task<string> RequestAsync(string subject)
        {

              var replyOpts = new NatsSubOpts { Timeout = TimeSpan.FromSeconds(45) };
           
            var replyWOParameter = await _connection.RequestAsync<string>(subject, replyOpts: replyOpts);

             return replyWOParameter.Data??"";            
         
        }

        public void Subscribe(string subject, Action<string> handler)
        {
            throw new NotImplementedException();
        }
    }
}
