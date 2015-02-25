using Sms.Data;

namespace SmsService.Models
{
    public class ModelFactory
    {
        private readonly IMessageRepository _messageRepository;
        public ModelFactory(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Message Parse(MessageContract model)
        {
            var message = new Message
            {
                MessageId = model.MessageId.Value,
                ReceiverPhone = model.ReceiverPhoneNumber,
                Sms = model.Sms,
                Status = "Created"
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