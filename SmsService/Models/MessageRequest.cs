using System;

namespace SmsService.Models
{
    public class MessageRequest
    {
        public Guid? MessageId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        
    }

    public class MessageResponse : MessageRequest
    {
        public string Sms { get; set; }
        public string Status { get; set; }
    }

}