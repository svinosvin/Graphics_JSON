using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Graphics.Models
{
    public class Day
    {
        public int Rank { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string Steps { get; set; }
        private static List<Day> allDays { get; set; } = null;
        public static List<Day> All() // get all Days and push them to usersDays
        {

            if (Day.allDays != null)
            {
                return allDays;

            }
            allDays = new List<Day>();
            for (int i = 1; i <= 30; i++)
            {
                using (StreamReader r = new StreamReader($"E:\\Code\\Test_task\\Task_Graphics\\TestJson\\Days\\day{i}.json "))
                {
                    string json = r.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<List<Day>>(json);
                    Day.AddList(result);
                    foreach (Day day in result)
                    {
                        AddDayToUser(day);
                    }

                }
            }
            return allDays;
        }
        private static void AddDayToUser(Day day)
        {
            UserModel.CreateOrAddDay(day);

        }
        public static void AddList(List<Day> days)
        {

            allDays.AddRange(days);
        }

    }
}
