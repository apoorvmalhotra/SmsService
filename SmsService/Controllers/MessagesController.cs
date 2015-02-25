using System;
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
            get { return _modelFactory ?? (_modelFactory = new ModelFactory(_repository)); }
        }

        [HttpPost]
        public virtual IHttpActionResult Post([FromBody] MessageContract message)
        {
            if ((message.MessageId == null) || (message.ReceiverPhoneNumber == null))
                return BadRequest("Message Id or Receiver phone number cannot be null");


            var insertedEntity = ModelFactory.Insert(message);
            var messageModel = ModelFactory.Map(insertedEntity);
            return CreatedAtRoute("Messages", new {id = messageModel.MessageId}, messageModel);
        }

        [HttpGet]
        public IHttpActionResult GetMessage(Guid id)
        {
            var result = _repository.GetMessageByMessageId(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
