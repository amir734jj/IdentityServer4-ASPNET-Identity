using System;

namespace Models.Models
{
    public class MessagePayload
    {
        public string From { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Time { get; set; }
    }
}