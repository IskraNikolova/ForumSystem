namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common.Models;

    public class ContentHolder : AuditInfo, IDeletableEntity
    {
        private ICollection<Tag> tags;

        public ContentHolder()
        {
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public virtual ICollection<Tag> SubmissionTypes { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
