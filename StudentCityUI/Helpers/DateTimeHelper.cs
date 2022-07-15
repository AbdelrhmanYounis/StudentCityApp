using System;

namespace StudentCityUI.Helpers
{
    public static class DateTimeHelper
    {
        public static bool Compare(this DateTime first, DateTime second)
        {
            return first.Day == second.Day && first.Month == second.Month && first.Year == second.Year;
        }
    }
}