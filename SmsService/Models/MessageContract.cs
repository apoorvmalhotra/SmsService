using System;

namespace SmsService.Models
{
    public class MessageContract
    {
        public Guid? MessageId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string Sms { get; set; }
        public string Status { get; set; }
    }

}