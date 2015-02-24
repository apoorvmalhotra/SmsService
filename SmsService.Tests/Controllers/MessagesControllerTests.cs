using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Emails.Data;
using EmailService.Controllers;
using EmailService.Models;
using NSubstitute;
using NUnit.Framework;

namespace EmailService.Tests.Controllers
{
    public class MessagesControllerTests
    {
        private IMessageRepository _repository;
        private MessagesController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IMessageRepository>();
            _controller = new MessagesController(_repository);
        }

        [Test]
        public void PostSetsLocationHeader_MockVersion()
        {
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
            const string locationUrl = "http://location/";

            
            var mockUrlHelper = Substitute.For<UrlHelper>(_controller.Request);
//            mockUrlHelper.Link(string.Empty, new object()).ReturnsForAnyArgs(locationUrl);
            _controller.Url = mockUrlHelper;

             MessageModel message = new MessageModel {MessageId = Guid.NewGuid()};
            var msg = Substitute.For<Message>();
            _repository.Insert(msg).ReturnsForAnyArgs()

            // Act
           
            var response = _controller.Post(message);

            // Assert
            Assert.AreEqual(locationUrl, response.Headers.Location.AbsoluteUri);

        }
    }
}