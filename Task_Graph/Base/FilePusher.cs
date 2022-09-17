using LINQtoCSV;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Task_Graph.Models;

namespace Task_Graph.Base
{
    public enum Formats
    {
        JSON,
        XML,
        CSV

    }
    public static class FilePusher
    {
        public static async Task PushIntoJsonFile(UserModel userModel)
        {
            string json = JsonConvert.SerializeObject(userModel);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON (.json)|.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                using (StreamWriter stream = new StreamWriter(fs))
                {
                    await stream.WriteAsync(json);
                }
            }
        }
        public static void PushIntoXMLFile(UserModel userModel)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(UserModel));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML (.xml)|.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                using (StreamWriter stream = new StreamWriter(fs))
                {
                    formatter.Serialize(stream, userModel);
                }
            }   
        }
        public static void PushIntoCsvFile(UserModel userModel)
        {

            
            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                SeparatorChar = ','

            };
            var csvContext = new CsvContext();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV (.csv)|.csv";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    csvContext.Write<Day>(userModel.GetDays(), saveFileDialog.FileName, csvFileDescription);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:{ex.Message}");
            }

        }
    }
}
