using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaStrike.Gameplay.DateTimeSystem
{
    public static class DateTimeUtil
    {
        public static System.DateTime DateTimeFromIntArray(int[] dateTime)
        {
            // year, month, day, hour, minute, second, millisecond
            System.DateTime returnDateTime = new System.DateTime(dateTime[0], dateTime[1], dateTime[2], dateTime[3],
                dateTime[4], dateTime[5], dateTime[6], DateTimeKind.Utc);
            return returnDateTime;
        }

        public static int[] DateTimeToIntArray(System.DateTime dateTime)
        {
            int[] returnDateTime = { dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
            dateTime.Minute, dateTime.Second, dateTime.Millisecond };
            return returnDateTime;
        }

    }
}