using System.IO;
using Sms.Data;

namespace SmsService.Models
{
    public interface IMessageMapper
    {
        Message Parse(MessageRequest model);
        MessageResponse Map(Message entity);
    }

    public class MessageMapper : IMessageMapper
    {
        public Message Parse(MessageRequest model)
        {
            if(!model.MessageId.HasValue)
                throw new InvalidDataException("Message Id cannot be null");

            var message = new Message
            {
                MessageId = model.MessageId.Value,
                ReceiverPhone = model.ReceiverPhoneNumber,
                Status = "Created"
            };
            return message;
        }

        public MessageResponse Map(Message entity)
        {
            var message = new MessageResponse
            {
                MessageId = entity.MessageId,
                ReceiverPhoneNumber = entity.ReceiverPhone,
                Sms = entity.Sms,
                Status = entity.Status,
            };
            return message;
        }
    }
}