using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models.Models
{
    public class Question : IEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [MaxLength(500)]
        public string Text { get; set; }

        public List<Tag> Tags { get; set; }
        
        public List<Answer> Answers { get; set; }

        public int Vote { get; set; }

        public DateTime Time { get; set; }
        
        [JsonIgnore]
        public User UserRef { get; set; }
    }
}