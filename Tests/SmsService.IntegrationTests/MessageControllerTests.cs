using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using Sms.Data;
using SmsService.Controllers;
using SmsService.Models;

namespace SmsService.IntegrationTests
{
    [TestFixture]
    public class MessageControllerTests
    {
        private IMessageRepository _repository;
        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = new MessageRepository(new MessageHandlerEntities(), new MessageMapper());
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
        public void Post_CreatesNewEntryInDatabase()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var message = new MessageRequest { MessageId = messageId, ReceiverPhoneNumber = "0412345678" };

            // Act
            var response = _controller.Post(message);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<MessageResponse>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content.Sms);
            Assert.AreEqual(messageId, createdResult.RouteValues["id"]);
        }
    }
}
