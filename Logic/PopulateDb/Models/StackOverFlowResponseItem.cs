using System.Collections.Generic;

namespace Logic.PopulateDb.Models
{
    public class StackOverFlowResponseItem
    {
        public List<string> Tags { get; set; }
        
        public string Body { get; set; }
        
        public int QuestionId { get; set; }
        
        public string Title { get; set; }
    }
}