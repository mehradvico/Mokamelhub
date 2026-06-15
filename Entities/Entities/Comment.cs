using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Comment : Id_Field
    {

        public string Text { get; set; }
        public int? Rate { get; set; }
        public long StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Answer { get; set; }
        public long? UserId { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public User User { get; set; }
        public Code Status { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}
