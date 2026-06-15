using Newtonsoft.Json;
using System.Globalization;
using System.Resources;

namespace Resource
{
    public class ConvertToJson
    {
        public string Convert()
        {
            ResourceManager MyResourceClass =
    new ResourceManager(typeof(Resource.Notification));

            ResourceSet resourceSet =
                MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            //var obj = new Dictionary<string,string>();
            //foreach (DictionaryEntry entry in resourceSet)
            //{
            //    obj.Add(entry.Key.ToString(), entry.Value.ToString());
            //}
            return JsonConvert.SerializeObject(resourceSet, Newtonsoft.Json.Formatting.Indented);
        }

    }
}
