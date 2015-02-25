using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sms.Data;
using SmsService.Models;

namespace SmsService.Controllers
{
    public class MessageVerificationController : BaseController<Verification, IMessageRepository>
    {
        private IMessageMapper _messageMapper;

        public MessageVerificationController(IMessageRepository repo) : base(repo)
        {
        }

        protected IMessageMapper MessageMapper
        {
            get { return _messageMapper ?? (_messageMapper = new MessageMapper(Repository)); }
        }

        [HttpPost]
        public virtual IHttpActionResult Post([FromBody] Verification messageVerification)
        {
            if ((messageVerification.MessageId == Guid.Empty) || (messageVerification.SmsToVerify == null))
                return BadRequest("Message Id or Sms cannot be empty");

            var smsStatus = Repository.VerifySms(messageVerification.MessageId, messageVerification.SmsToVerify);
            if (smsStatus == SmsStatus.NotFound)
                return BadRequest("Invalid message id");

            if (smsStatus == SmsStatus.InvalidSmsCode)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Sms received"));

            var entity = Repository.GetMessageByMessageId(messageVerification.MessageId);
            var messageModel = MessageMapper.Map(entity);
            return CreatedAtRoute("Messages", new { id = messageModel.MessageId }, messageModel);
        }
    }
}