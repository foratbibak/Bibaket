using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Bibaket.Application.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" +
                pc.GetMonth(date).ToString("00") + "/" +
                pc.GetDayOfMonth(date).ToString("00");
        }

        public static string ToShamsiWithTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" +
                pc.GetMonth(date).ToString("00") + "/" +
                pc.GetDayOfMonth(date).ToString("00") + " - " +
                pc.GetHour(date).ToString("00") + ":" +
                pc.GetMinute(date).ToString("00");
        }

        //public static DateTime ToMiladi(string shamsi)
        //{
        //    string[] arr = shamsi.Split('/');
        //    PersianCalendar pc = new PersianCalendar();
        //    return pc.ToDateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), 0, 0, 0, 0);
        //}
        public static DateTime ToMiladi(string shamsi)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(shamsi);
            return persianDateTime.ToDateTime();
        }
    }
}
