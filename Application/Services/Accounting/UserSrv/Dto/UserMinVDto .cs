using Application.Common.Dto.Field;
using System;

namespace Application.Services.Dto
{
    public class UserMinVDto : Id_FieldDto
    {

        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string BonusCode { get; set; }
        public bool IsFemale { get; set; }
        public DateTime BirthDate { get; set; }
        public long PictureId { get; set; }

    }
}
