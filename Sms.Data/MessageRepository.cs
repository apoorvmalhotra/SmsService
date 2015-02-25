using System;

namespace Sms.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageHandlerEntities _context;

        public MessageRepository(IMessageHandlerEntities context)
        {
            _context = context;
        }

        public bool Insert(Message entity)
        {
            _context.Messages.Add(entity);
            _context.SaveChanges();
            return true;
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
            throw new NotImplementedException();
        }
    }
}