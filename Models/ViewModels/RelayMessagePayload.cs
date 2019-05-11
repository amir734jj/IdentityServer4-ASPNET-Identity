using System;

namespace Models.Models
{
    /// <summary>
    ///     The chat message being relayed to everyone else online
    /// </summary>
    public class RelayMessagePayload
    {
        public string From { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Time { get; set; }
    }
}