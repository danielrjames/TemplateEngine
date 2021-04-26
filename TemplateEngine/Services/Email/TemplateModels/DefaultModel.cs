using Newtonsoft.Json;

namespace TemplateEngine.Services.Email.TemplateModels
{
    public class DefaultModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
