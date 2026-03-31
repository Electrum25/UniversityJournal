using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Helpers
{
    public static class ScheduleFormatter
    {
        public static string GetPairTime(int pairNumber)
        {
            return pairNumber switch
            {
                1 => "08:30 - 10:05",
                2 => "10:15 - 11:50",
                3 => "12:10 - 13:45",
                4 => "14:00 - 15:35",
                _ => "Время не определено"
            };
        }
    }
}
