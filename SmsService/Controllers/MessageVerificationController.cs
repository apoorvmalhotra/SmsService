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
        public MessageVerificationController(IMessageRepository repo) : base(repo)
        {
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

            var messageModel = Repository.GetMessageByMessageId(messageVerification.MessageId);
            return CreatedAtRoute("Messages", new { id = messageModel.MessageId }, messageModel);
        }
    }
}