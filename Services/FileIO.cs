using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace TodoApp.Services
{
    class FileIO
    {
        public FileIO(string path)
        {
            this.path = path;
        }

        private readonly string path;

        public BindingList<T> LoadData<T>()
        {
            bool fileExists = File.Exists(path);
            if (!fileExists)
            {
                File.CreateText(path).Dispose();
                return new BindingList<T>();
            }

            using (var streamReader = File.OpenText(path))
            {
                string text = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<T>>(text);
            }
        }

        public void SaveData(object list)
        {
            using (var streamWriter = File.CreateText(path))
            {
                string output = JsonConvert.SerializeObject(list);
                streamWriter.Write(output);
            }
        }
    }
}
