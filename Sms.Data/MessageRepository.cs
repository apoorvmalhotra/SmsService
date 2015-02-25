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

        public bool Update(Message originalEntity, Message updatedEntity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageByMessageId(Guid messageId)
        {
            return _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
        }
    }
}