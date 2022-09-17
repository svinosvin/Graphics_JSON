using System;
using System.Collections.Generic;
using System.IO;
using Task_Graph.Base;

namespace Task_Graph.Models
{
    [Serializable]

    public class Day
    {
        #region properties
        public int Rank { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int Steps { get; set; }

        #endregion


        #region static_region
        private static List<Day> allDays { get; set; } = new List<Day>();
        public static List<Day> All() // get all Days and push them to usersDays
        {

            if (Day.allDays != null)
            {
                return allDays;

            }
            FullDays();

            return Day.allDays ?? new List<Day>();
        }
        public static void FullDays()
        {
            Day.ClearAll();
            UserModel.ClearAll();
            string[] files = Directory.GetFiles(@"Days", "*.json");
            FileGetter.SetInfoDays(files);
        }   
        public static void ClearAll()
        {
            Day.allDays.Clear();

        }
        public static void AddList(List<Day> days)
        {
            allDays.AddRange(days);
        }
        #endregion
    }
}
