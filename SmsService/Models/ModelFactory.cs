using System.Net.Http;
using System.Web.Http.Routing;
using Sms.Data;

namespace SmsService.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private IMessageRepository _messageRepository;
        public ModelFactory(HttpRequestMessage request, IMessageRepository messageRepository)
        {
            _urlHelper = new UrlHelper(request);
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
//                Url = _urlHelper.Link("Message", new { controller = "Messages", id = entity.Id } )
            };
            return message;
        }

        public Message Insert(MessageContract message)
        {
            var entity = Parse(message);
            _messageRepository.Insert(entity);
            return entity;
        }
    }
}