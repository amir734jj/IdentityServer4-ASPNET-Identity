using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;

namespace Models.Models
{
    public class Question : IEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public List<Tag> Tags { get; set; }
        
        public List<Answer> Answers { get; set; }

        public int Vote { get; set; }

        public DateTime Time { get; set; }
    }
}