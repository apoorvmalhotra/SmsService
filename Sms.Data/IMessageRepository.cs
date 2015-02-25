using System;
using SmsService.Models;

namespace Sms.Data
{
    public interface IMessageRepository
    {
        bool Insert(Message model);
        bool Update(Message originalEntity, Message updatedEntity);
        bool Delete(int id);

        Message GetMessageByMessageId(Guid messageId);
    }
}