using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace Group
{
    internal class JsonNetFormatter : MediaTypeFormatter
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public JsonNetFormatter(JsonSerializerSettings jsonSerializerSettings)
        {
            this.jsonSerializerSettings = jsonSerializerSettings;
        }
    }
}