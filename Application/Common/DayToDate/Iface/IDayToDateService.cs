using Application.Common.DayToDate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DayToDate.Iface
{
    public interface IDayToDateService
    {
        DayToDateVDto GetNextDateByDayNumber(DayToDateDto dto);
    }
}
