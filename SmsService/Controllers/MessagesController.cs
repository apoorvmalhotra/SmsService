using System;
using System.Web.Http;
using SmsService.Models;

namespace SmsService.Controllers
{
    public class MessagesController : BaseController<MessageRequest, IMessageRepository>
    {
        public MessagesController(IMessageRepository repository): base(repository)
        {
        }

        [HttpPost]
        public virtual IHttpActionResult Post([FromBody] MessageRequest message)
        {
            if ((message.MessageId == null) || (message.ReceiverPhoneNumber == null))
                return BadRequest("Message Id or Receiver phone number cannot be null");

            var savedModel = Repository.Insert(message);
            return CreatedAtRoute("Messages", new { id = savedModel.MessageId }, savedModel);
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
