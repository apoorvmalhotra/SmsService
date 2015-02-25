using System;

namespace SmsService.Models
{
    public class Verification
    {
        public Guid MessageId { get; set; }
        public string SmsToVerify { get; set; }
    }
}