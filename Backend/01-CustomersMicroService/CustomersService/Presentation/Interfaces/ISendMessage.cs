using Domain.Services;

namespace Presentation.Interfaces
{
    public abstract class ISendMessage<T> 
    {

        protected readonly T  _useCase;
        protected readonly IMsgService _msgService;
        protected readonly ILogger<ISendMessage<T> > _logger;
        

        public ISendMessage( T  useCase, IMsgService msgService,ILogger<ISendMessage<T> >   logger)
        {
            _useCase = useCase;      
            _logger = logger;
            _msgService = msgService;
            Execute().ConfigureAwait(false);      
        }



        public virtual async Task Execute(){
           
        }      
    }
}