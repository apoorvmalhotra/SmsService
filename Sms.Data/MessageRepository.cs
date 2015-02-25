using System;
using System.Linq;

namespace Sms.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageHandlerEntities _context;

        public MessageRepository(IMessageHandlerEntities context)
        {
            _context = context;
        }

        public Message Insert(Message entity)
        {
            _context.Messages.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Message GetMessageByMessageId(Guid messageId)
        {
            return _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
        }

        public SmsStatus VerifySms(Guid messageId, string sms)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
            if (message == null) return SmsStatus.NotFound;

            if (message.Sms == sms)
            {
                message.VerificationTime = DateTime.Now;
                message.Status = "Verified";
                _context.Messages.Attach(message);
                _context.SaveChanges();
                return SmsStatus.Success;
            }

            return SmsStatus.InvalidSmsCode;
        }
    }
}