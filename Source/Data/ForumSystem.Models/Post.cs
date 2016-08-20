namespace ForumSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}