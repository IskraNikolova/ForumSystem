namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        public Post()
        {
            this.Answers = new List<Answer>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Tag Tag { get; set; }

        public IList<Answer> Answers { get; set; }

        [Index]
        public ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}