using System;

namespace Emails.Data
{
    public interface IMessageRepository
    {
        Message Insert(Message model);
        bool Update(Message originalEntity, Message updatedEntity);
        bool Delete(int id);

        Message GetMessageByMessageId(Guid messageId);
    }
}