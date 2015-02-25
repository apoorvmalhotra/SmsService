using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using Sms.Data;
using SmsService.Controllers;
using SmsService.Models;

namespace SmsService.Tests.Controllers
{
    public class MessagesControllerTests
    {
        private IMessageRepository _repository;
        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = new MessageRepository(new MessageHandlerEntities());
            _controller = new MessagesController(_repository);
        }

        [Test]
        public void PostSetsLocationHeader()
        {
            
            _controller = new MessagesController(_repository)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/messages"),
                    Method = HttpMethod.Post
                }
            };

            var messageId = Guid.NewGuid();
            var message = new MessageContract {MessageId = messageId, ReceiverPhoneNumber = "0412345678"};

            // Act
            var response = _controller.Post(message);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<MessageContract>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Messages", createdResult.RouteName);
            Assert.AreEqual(messageId, createdResult.RouteValues["id"]);
        }

        [Test]
        public void Post_WhenTheContractIsIncomplete_ReturnsBadRequest()
        {
            _controller = new MessagesController(_repository)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/messages"),
                    Method = HttpMethod.Post
                }
            };

            var message = new MessageContract { ReceiverPhoneNumber = "0412345678", Sms = "123456" };

            // Act
            var response = _controller.Post(message);
            var createdResult = response as BadRequestErrorMessageResult;

            // Assert
            Assert.AreEqual("Message Id or Receiver phone number cannot be null", createdResult.Message);
        }

        [Test]
        public void Get_WhenAMessageIsFound_ReturnsOkResult()
        {
            // Action
            IHttpActionResult actionResult = _controller.GetMessage(Guid.Parse("34245B04-F370-4787-8748-0FE7A9D39D38"));
            var conNegResult = actionResult as OkNegotiatedContentResult<Message>;

            // Assert
            Assert.IsNotNull(conNegResult.Content);
        }
    }
}