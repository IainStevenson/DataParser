using System.IO;
using Newtonsoft.Json;

namespace DataParser
{
    public class DataSourceHandler
    {
        public DataSourceHandler()
        {
        }

        public DataSource Load(string dataSourceFile)
        {
            var response = new DataSource();
            if (File.Exists(dataSourceFile))
            {
                var data = File.ReadAllText(dataSourceFile);
                response = JsonConvert.DeserializeObject<DataSource>(data);
            }
            return response;
        }
        public void  Save(DataSource dataSource, string dataSourceFile)
        {
            File.WriteAllText(dataSourceFile, JsonConvert.SerializeObject(dataSource));
        }
    }
}