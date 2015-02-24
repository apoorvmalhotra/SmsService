using System;
using System.ComponentModel.DataAnnotations;

namespace EmailService.Models
{
    public class MessageModel
    {
        [Required]
        public Guid MessageId { get; set; }
        public string Url { get; set; }
        [Required]
        public string ReceiverPhoneNumber { get; set; }
        public int Id { get; set; }
        public string Sms { get; set; }
        public string Status { get; set; }
    }
}