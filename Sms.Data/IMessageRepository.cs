using System;

namespace Sms.Data
{
    public interface IMessageRepository
    {
        Message Insert(Message model);
        Message GetMessageByMessageId(Guid messageId);

        SmsStatus VerifySms(Guid messageId, string sms);
    }
}