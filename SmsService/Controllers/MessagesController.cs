using System;
using System.Web.Http;
using Sms.Data;
using SmsService.Models;

namespace SmsService.Controllers
{
    public class MessagesController : BaseController<MessageContract, IMessageRepository>
    {
        private IMessageMapper _messageMapper;

        public MessagesController(IMessageRepository repository): base(repository)
        {
        }

        protected IMessageMapper MessageMapper
        {
            get { return _messageMapper ?? (_messageMapper = new MessageMapper(Repository)); }
        }

        [HttpPost]
        public virtual IHttpActionResult Post([FromBody] MessageContract message)
        {
            if ((message.MessageId == null) || (message.ReceiverPhoneNumber == null))
                return BadRequest("Message Id or Receiver phone number cannot be null");


            var insertedEntity = MessageMapper.Insert(message);
            var messageModel = MessageMapper.Map(insertedEntity);
            return CreatedAtRoute("Messages", new {id = messageModel.MessageId}, messageModel);
        }

        [HttpGet]
        public IHttpActionResult GetMessage(Guid id)
        {
            var result = Repository.GetMessageByMessageId(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
