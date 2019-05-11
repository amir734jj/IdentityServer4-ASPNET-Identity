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
        
        /// <summary>
        ///     Need to set TypeName to text as Answer text may be long
        /// </summary>
        [Column(TypeName = "text")]
        public string Text { get; set; }
        
        [JsonIgnore]
        public Question QuestionRef { get; set; }
    }
}