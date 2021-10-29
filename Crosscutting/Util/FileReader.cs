using Newtonsoft.Json;

namespace Crosscutting.Util
{
    public static class FileReader
    {
        public static T GetJsonFileContent<T>(string file)
        {
            var fileTextAsJson = JsonConvert.DeserializeObject<T>(file);

            return fileTextAsJson;
        }
    }
}
