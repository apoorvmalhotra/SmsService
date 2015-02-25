using System;
using NSubstitute;
using NUnit.Framework;
using Sms.Data;
using SmsService.Models;

namespace SmsService.UnitTests.Models
{
    [TestFixture]
    public class MessageMapperTests
    {
        private IMessageRepository _repository;
        private MessageMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IMessageRepository>();
            _mapper = new MessageMapper(_repository);
        }

        [Test]
        public void Parse_TransformsMessageContractToMessage()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            const string receiverPhoneNumber = "0412345678";
            const string sms = "123456";
            var message = new MessageContract { MessageId = messageId, ReceiverPhoneNumber = receiverPhoneNumber, Sms = sms };
            
            // Act
            var parsedMessage = _mapper.Parse(message);

            // Assert
            Assert.AreEqual(messageId, parsedMessage.MessageId);
            Assert.AreEqual(receiverPhoneNumber, parsedMessage.ReceiverPhone);
            Assert.AreEqual(sms, parsedMessage.Sms);
        }

        [Test]
        public void Map_TransformsEntityObjectToMessageContract()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            const string receiverPhoneNumber = "0412345678";
            const string sms = "123456";
            var message = new Message { Id = 1, MessageId = messageId, ReceiverPhone = receiverPhoneNumber, Sms = sms };

            // Act
            var parsedMessage = _mapper.Map(message);

            // Assert
            Assert.AreEqual(typeof(MessageContract), parsedMessage.GetType());
            Assert.AreEqual(messageId, parsedMessage.MessageId);
            Assert.AreEqual(receiverPhoneNumber, parsedMessage.ReceiverPhoneNumber);
            Assert.AreEqual(sms, parsedMessage.Sms);
        }

        [Test]
        public void Insert_InsertsEntityToDatabase()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            const string receiverPhoneNumber = "0412345678";
            const string sms = "123456";
            var message = new MessageContract { MessageId = messageId, ReceiverPhoneNumber = receiverPhoneNumber, Sms = sms };

            // Act
            _mapper.Insert(message);

            _repository.Received().Insert(Arg.Any<Message>());
        }
        
    }
}