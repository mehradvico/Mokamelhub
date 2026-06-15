using Entities.Entities.CommonField;
using Entities.Entities.Security;
using System;

namespace Entities.Entities
{
    public class CommentLike : Id_Field
    {
        public bool IsLike { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }

    }
}
