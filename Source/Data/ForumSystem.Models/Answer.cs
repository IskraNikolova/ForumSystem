namespace ForumSystem.Models
{
    using System;
    using Data.Common.Models;

    public class Answer : AuditInfo, IDeletableEntity
    {
        private int postId;

        public Answer()
        {
            this.PostId = this.postId;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId
        {
            get { return this.Post.Id; }
            set { this.postId = value; }
        }

        public Post Post { get; set; }

        public ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
