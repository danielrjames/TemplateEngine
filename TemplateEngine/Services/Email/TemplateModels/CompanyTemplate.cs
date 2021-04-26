using Newtonsoft.Json;

namespace TemplateEngine.Services.Email.TemplateModels
{
    public class CompanyTemplate
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("company")]
        public string Company { get; set; }
    }
}
