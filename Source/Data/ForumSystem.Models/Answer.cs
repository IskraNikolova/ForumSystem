﻿namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.AccessControl;
    using Data.Common.Models;

    public class Answer : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int RatingUp { get; set; }

        public int RatingDown { get; set; }

        public int RatingPoint { get; set; }

        public string RatingUpUsers { get; set; }

        public string RatingDownUsers { get; set; }

        public ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
