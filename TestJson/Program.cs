using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static TestJson.Program;

namespace TestJson
{
    public class Program
    {

      
      

        static void Main(string[] args)
        {

            Day.All();
            List<UserModel> um =  UserModel.All();
 
            string answer = JsonConvert.SerializeObject(um[1]);
            Console.WriteLine(answer);

        }

        public class UserModel
        {
            [JsonProperty]
            public string Name { get; set; }

            [JsonProperty]
            public List<Day> UserDays { get; set; } = new List<Day>();

           
            public int MyProperty { get; set; } = 1;
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


        public class Day
        {
            public int Rank { get ; set; }
            public string User { get; set; }
            public string Status { get; set; }
            public int Steps { get; set; }
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
}
