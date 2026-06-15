using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Setting.SettingSrv.Dto
{
    public class CargoPriceDto
    {
        public double DefaultDomesticPrice { get; set; }
        public double DefaultForeignPrice { get; set; }
        public double NotAccompanyPrice { get; set; }
        public double ReturnPrice { get; set; }
    }
}