using System;
using System.ComponentModel.DataAnnotations;

namespace SmsService.Models
{
    public class MessageContract
    {
        [Required]
        public Guid? MessageId { get; set; }
        [Required]
        public string ReceiverPhoneNumber { get; set; }
        public int Id { get; set; }
        public string Sms { get; set; }
        public string Status { get; set; }
    }

}