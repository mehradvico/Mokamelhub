using Application.Common.DayToDate.Dto;
using Application.Common.DayToDate.Iface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DayToDate
{
    public class DayToDateService : IDayToDateService
    {
        public DayToDateVDto GetNextDateByDayNumber(DayToDateDto dto)
        {
            var today = DateTime.Today;
            var currentDayNumber = GetDayNumber(today.DayOfWeek);

            int daysToAdd = (int)dto.DayNumber - currentDayNumber;
            if (daysToAdd < 0)
                daysToAdd += 7;

            if (daysToAdd == 0)
                daysToAdd = 7;

            var resultDate = today.AddDays(daysToAdd);
            var dayName = GetDayName(dto.DayNumber);

            return new DayToDateVDto
            {
                Day = dayName,
                Date = resultDate.Date
            };
        }

        private int GetDayNumber(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Saturday => 1,
                DayOfWeek.Sunday => 2,
                DayOfWeek.Monday => 3,
                DayOfWeek.Tuesday => 4,
                DayOfWeek.Wednesday => 5,
                DayOfWeek.Thursday => 6,
                DayOfWeek.Friday => 7,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private string GetDayName(long dayNumber)
        {
            return dayNumber switch
            {
                1 => "شنبه",
                2 => "یک‌شنبه",
                3 => "دوشنبه",
                4 => "سه‌شنبه",
                5 => "چهارشنبه",
                6 => "پنج‌شنبه",
                7 => "جمعه",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
