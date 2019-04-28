using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models.Models
{
    public class Tag : IEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Text { get; set; }
        
        [JsonIgnore]
        public Question QuestionRef { get; set; }
    }
}