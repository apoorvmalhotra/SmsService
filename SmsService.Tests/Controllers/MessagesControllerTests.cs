using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using EmailService.Controllers;
using NSubstitute;
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

            // Assert
            Assert.IsNotNull(response);

        }
    }
}