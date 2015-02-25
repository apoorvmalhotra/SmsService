using System.IO;
using Sms.Data;

namespace SmsService.Models
{
    public interface IMessageMapper
    {
        Message Parse(MessageContract model);
        MessageContract Map(Message entity);
        Message Insert(MessageContract message);
    }

    public class MessageMapper : IMessageMapper
    {
        private readonly IMessageRepository _messageRepository;
        public MessageMapper(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Message Parse(MessageContract model)
        {
            if(!model.MessageId.HasValue)
                throw new InvalidDataException("Message Id cannot be null");

            var message = new Message
            {
                MessageId = model.MessageId.Value,
                ReceiverPhone = model.ReceiverPhoneNumber,
                Sms = model.Sms,
                Status = model.Status ?? "Created"
            };
            return message;
        }

        public MessageContract Map(Message entity)
        {
            var message = new MessageContract
            {
                MessageId = entity.MessageId,
                ReceiverPhoneNumber = entity.ReceiverPhone,
                Sms = entity.Sms,
                Status = entity.Status,
            };
            return message;
        }

        public Message Insert(MessageContract message)
        {
            var entity = Parse(message);
            return _messageRepository.Insert(entity);
        }
    }
}