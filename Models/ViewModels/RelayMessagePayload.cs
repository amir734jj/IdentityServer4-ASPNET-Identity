using System;

namespace Models.Models
{
    public class RelayMessagePayload
    {
        public string From { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Time { get; set; }
    }
}