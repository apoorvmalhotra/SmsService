using System.Net.Http;
using System.Web.Http.Routing;
using Emails.Data;

namespace EmailService.Models
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

        public Message Parse(MessageModel model)
        {
            var message = new Message
            {
                MessageId = model.MessageId,
                ReceiverPhone = model.ReceiverPhoneNumber,
                Sms = model.Sms,
                Status = model.Status
            };
            return message;
        }

        public MessageModel Map(Message entity)
        {
            var message = new MessageModel
            {
                MessageId = entity.MessageId,
                ReceiverPhoneNumber = entity.ReceiverPhone,
                Sms = entity.Sms,
                Status = entity.Status,
                Url = _urlHelper.Link("Message", new { controller = "Messages", id = entity.Id } )
            };
            return message;
        }

    }
}