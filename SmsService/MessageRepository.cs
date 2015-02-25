using System;
using System.Linq;
using Sms.Data;
using SmsService.Models;

namespace SmsService
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageHandlerEntities _context;
        private readonly IMessageMapper _messageMapper;

        public MessageRepository(IMessageHandlerEntities context, IMessageMapper mapper)
        {
            _context = context;
            _messageMapper = mapper;
        }

        private void Insert(Message entity)
        {
            _context.Messages.Add(entity);
            _context.SaveChanges();
        }

        public MessageResponse Insert(MessageRequest request)
        {
            var entityToSave = _messageMapper.Parse(request);
            Insert(entityToSave);
            return _messageMapper.Map(entityToSave);
        }

        public MessageResponse GetMessageByMessageId(Guid messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
            return (message == null) ? null : _messageMapper.Map(message);
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