using Newtonsoft.Json;

namespace TemplateEngine.Services.Email.TemplateModels
{
    public class DefaultTemplate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
