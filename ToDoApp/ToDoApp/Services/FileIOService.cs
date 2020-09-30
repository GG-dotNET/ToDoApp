using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    class FileIOService
    {
        private readonly string _path;

        public FileIOService(string path)
        {
            _path = path;
        }
        public BindingList<TodoModel> LoadData()
        {
            var fileExist = File.Exists(_path);
            if (!fileExist)
            {
                File.CreateText(_path).Dispose();
                return new BindingList<TodoModel>();
            }

            using (var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveData(object todoDataList)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
        }
    }
}
