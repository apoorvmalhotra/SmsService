using System;
using Sms.Data;
using SmsService.Models;

namespace SmsService
{
    public interface IMessageRepository
    {
        MessageResponse Insert(MessageRequest model);
        MessageResponse GetMessageByMessageId(Guid messageId);

        SmsStatus VerifySms(Guid messageId, string sms);
    }
}