using Application.Common.Dto.Field;
using Application.Common.Dto.LocationPoint;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Setting.BaseDetailSrv.Dto
{
    public class BaseDetailDto : FullName_FieldDto
    {
        public string AddressValue { get; set; }
        public PointDto Location { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PostalCode { get; set; }
        public string ClickUserGuid { get; set; }
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }

    }
}
