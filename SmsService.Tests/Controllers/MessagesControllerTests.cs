using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using Sms.Data;
using SmsService.Controllers;
using SmsService.Models;

namespace SmsService.UnitTests.Controllers
{
    public class MessagesControllerTests
    {
        private IMessageRepository _repository;
        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IMessageRepository>();
            _controller = new MessagesController(_repository)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/api/messages"),
                    Method = HttpMethod.Post
                }
            };
        }

        [Test]
        public void PostSetsLocationHeader()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var message = new MessageContract {MessageId = messageId, ReceiverPhoneNumber = "0412345678"};
            _repository.Insert(Arg.Any<Message>()).ReturnsForAnyArgs(new Message { MessageId = messageId, ReceiverPhone = "0412345678" });

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
            // Arrange
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
            // Arrange
            var messageId = Guid.NewGuid();
            _repository.GetMessageByMessageId(messageId).Returns(new Message { MessageId = messageId, ReceiverPhone = "0412345678" });

            // Action
            IHttpActionResult actionResult = _controller.GetMessage(messageId);
            var conNegResult = actionResult as OkNegotiatedContentResult<Message>;

            // Assert
            Assert.IsNotNull(conNegResult.Content);
        }
    }
}