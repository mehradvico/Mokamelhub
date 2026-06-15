using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using System;

namespace Application.Services.CommonSrv.CommentSrv.Dto
{
    public class CommentVDto : Name_FieldDto
    {
        public string Text { get; set; }
        public int? Rate { get; set; }
        public long StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? UpOrDownThumb { get; set; }
        public string Answer { get; set; }
        public long? UserId { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public UserMinVDto User { get; set; }
        public CodeDto Status { get; set; }
    }
}
