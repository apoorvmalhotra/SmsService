using System.Web.Http;
using Sms.Data;
using SmsService.Models;

namespace SmsService.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly IMessageRepository _repository;
        private ModelFactory _modelFactory;

        public MessagesController(IMessageRepository repository)
        {
            _repository = repository;
        }

        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, _repository);
                }
                return _modelFactory;
            }
        }

        [HttpPost]
        public virtual IHttpActionResult Post([FromBody] MessageContract message)
        {
            if (ModelState.IsValid)
            {
                var insertedEntity = ModelFactory.Insert(message);
                var messageModel = ModelFactory.Map(insertedEntity);
                var t = CreatedAtRoute("Messages", new {id = messageModel.MessageId}, messageModel);
                return t;
            }
            return null;
        }
        
    }
}
