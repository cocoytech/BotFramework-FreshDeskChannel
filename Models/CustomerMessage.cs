﻿namespace BotFramework.FreshDeskChannel.Models
{
    public class CustomerMessage
    {
        public string RequesterName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
