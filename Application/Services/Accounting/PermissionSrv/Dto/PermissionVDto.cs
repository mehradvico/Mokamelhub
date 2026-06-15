using Application.Common.Dto.Field;
using System.Collections.Generic;

namespace Application.Services.Accounting.PermissionSrv.Dto
{
    public class PermissionVDto : Id_FieldDto
    {

        public string Name { get; set; }
        public string Label { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsMenu { get; set; }
        public int Priority { get; set; }
        public long? ParentId { get; set; }
        public List<PermissionVDto> Children { get; set; }

    }
}
