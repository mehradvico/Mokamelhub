using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;
using System;

namespace Application.Services.Dto
{
    public class UserVDto : Id_FieldDto
    {

        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string BonusCode { get; set; }
        public long? PictureId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsFemale { get; set; }
        public bool Locked { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }

        public PictureVDto Picture { get; set; }
    }
}
