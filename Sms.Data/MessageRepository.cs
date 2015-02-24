using System;

namespace Emails.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageHandlerEntities _context;

        public MessageRepository(MessageHandlerEntities context)
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
            throw new NotImplementedException();
        }
    }
}