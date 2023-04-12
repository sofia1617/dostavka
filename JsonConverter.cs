using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace dostavka
{
    internal class JsonConverter
    {
        public static T DeserializeObject<T>()
        {
            OpenFileDialog dialog= new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else 
            {
                return default(T);
            }
        }
    }
}
