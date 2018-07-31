using Newtonsoft.Json;
using System;
using System.IO;

namespace ListDataMigrator.Common
{
    public static class JsonUtility
    {
        public static T FromFile<T>(string path)
        {
            var model = default(T);
            try
            {
                using (var r = new StreamReader($@"{path}"))
                {
                    var json = r.ReadToEnd();
                    model = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to get json model from file for the path: {path}. Please check the path to the file is correct.");
            }

            return model;
        }
    }
}
