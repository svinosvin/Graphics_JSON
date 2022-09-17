using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Task_Graph.Models;

namespace Task_Graph.Base
{
    public static class FileGetter
    {
        public static void SetInfoDays(params string[] files)
        {

            foreach (var item in files)
            {
                using (StreamReader r = new StreamReader(item))
                {
                    try
                    {
                        string json = r.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<List<Day>>(json);
                        if (result != null)
                        {
                            Day.AddList(result);
                            foreach (Day day in result)
                            {
                                AddDayToUser(day);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"В одном из файлов была ошибка! {item}");
                    }
                    
                }
            }
            Models.UserModel.All().ToList().ForEach(x => x.FindValuesProps());
                       
        }

        public static void SetRangeFiles()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Json files (*.json)|*.json"; 
            if (openFileDialog.ShowDialog() == true)
            {
                UserModel.ClearAll();
                SetInfoDays(openFileDialog.FileNames);
            }
        }

        private static void AddDayToUser(Day day)
        {
            UserModel.CreateOrAddDay(day);

        }
    }
}
