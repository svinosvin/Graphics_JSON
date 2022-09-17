using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Graphics.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public List<Day> UserDays { get; set; } = new List<Day>();
        private static List<UserModel> AllUser { get; set; } = new List<UserModel>();


        public List<Day> GetDays()
        {
            return UserDays;
        }
        public void AddDay(Day day)
        {
            UserDays.Add(day);
        }
        public static List<UserModel> All()
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
        private static UserModel GetUser(string name)
        {
            List<UserModel> users = UserModel.All();
            return users.FirstOrDefault(x => x.Name == name);

        }


    }

}
