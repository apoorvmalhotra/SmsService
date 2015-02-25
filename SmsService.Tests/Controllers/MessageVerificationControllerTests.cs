using System;
using System.Net;
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
    public class MessageVerificationControllerTests
    {
        private IMessageRepository _repository;
        private MessageVerificationController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IMessageRepository>();
            _controller = new MessageVerificationController(_repository)
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
        public void Post_WhenMessageIsNotFound_ReturnsBadRequest()
        {
            // Arrange
            var verification = new Verification { MessageId = Guid.NewGuid(), SmsToVerify = "123"};
            _repository.VerifySms(Arg.Any<Guid>(), Arg.Any<string>()).ReturnsForAnyArgs(SmsStatus.NotFound);

            // Act
            IHttpActionResult response = _controller.Post(verification);
            var result = response as BadRequestErrorMessageResult;

            // Assert
            Assert.AreEqual("Invalid message id", result.Message);
        }

        [Test]
        public void Post_ForAnInvalidSms_ReturnsNotFound()
        {
            // Arrange
            var verification = new Verification { MessageId = Guid.NewGuid(), SmsToVerify = "123" };
            _repository.VerifySms(Arg.Any<Guid>(), Arg.Any<string>()).ReturnsForAnyArgs(SmsStatus.InvalidSmsCode);

            // Act
            IHttpActionResult response = _controller.Post(verification);
            var result = response as ResponseMessageResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.Response.StatusCode);
        }

        [Test]
        public void Post_OnSuccessfulVerification_SetsLocationHeader()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var verification = new Verification { MessageId = messageId, SmsToVerify = "123" };
            _repository.VerifySms(Arg.Any<Guid>(), Arg.Any<string>()).ReturnsForAnyArgs(SmsStatus.Success);
            _repository.GetMessageByMessageId(Arg.Any<Guid>()).ReturnsForAnyArgs(new MessageResponse { MessageId = messageId});

            // Act
            var response = _controller.Post(verification);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<MessageResponse>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Messages", createdResult.RouteName);
            Assert.AreEqual(messageId, createdResult.RouteValues["id"]);
        }
    }
}