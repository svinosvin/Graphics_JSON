using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_Graph.Models
{
    [Serializable]
    public class UserModel
    {
        #region properties
        public string? Name { get; set; }
        public int? AvgSteps { get; set; }
        public int? MaxSteps { get; set; } 
        public int? MinSteps { get; set; }
        public List<Day> UserDays { get; set; } = new List<Day>();


        [JsonIgnore]
        public string Difference => MaxSteps > AvgSteps * 0.2 + AvgSteps || MinSteps < AvgSteps - AvgSteps * 0.2 ? "Да" : "Нет";

        #endregion



        #region methods
        public List<Day> GetDays()
        {
            return UserDays;
        }
        public void AddDay(Day day)
        {
            UserDays.Add(day);
        }
        public void FindValuesProps()
        {
            MaxSteps = (int)UserDays.Max(x => Convert.ToInt32(x.Steps));
            MinSteps = (int)UserDays.Min(x => Convert.ToInt32(x.Steps));
            AvgSteps = (int)UserDays.Average(x => Convert.ToInt32(x.Steps));
        }

        #endregion


        #region static_region
        private static ICollection<UserModel> AllUser { get; set; } = new List<UserModel>();
        public static void ClearAll()
        {
            AllUser.Clear();
        }
        public static IEnumerable<UserModel> All()
        {
            return AllUser;
        }
        public static void CreateOrAddDay(Day day)
        {
            UserModel userModel = UserModel.GetUser(day.User);
            if (userModel == null)
            {
                userModel = new UserModel
                {
                    Name = day.User

                };
                AllUser.Add(userModel);
            }

            userModel.AddDay(day);

        }
        private static UserModel? GetUser(string name)
        {
            List<UserModel> users = UserModel.All().ToList();
            return users.FirstOrDefault(x => x.Name == name);

        }

        #endregion
    }

}
