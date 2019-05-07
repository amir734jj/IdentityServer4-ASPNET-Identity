using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models.Models
{
    public class Answer : IEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Column(TypeName = "text")]
        public string Text { get; set; }
        
        [JsonIgnore]
        public Question QuestionRef { get; set; }
    }
}