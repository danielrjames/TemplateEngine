using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TemplateEngine.Services.Template
{
    public class TemplateService : ITemplateService
    {
        public TemplateService()
        {

        }

        public string GetTemplateByAlias(string templateAlias)
        {
            // query the db for the template by alias
            // efcore: var template = await _context.templates.FirstOrDefault(t => t.alias == templateAlias);

            // just for demo - using name, company (not present in default model) and url. Works with different space sizing on the brackets
            return "Welcome {{name}} from {{ company }}. Make sure you check out {{url }}.";
        }

        public string TokenizeMessage(string template, object templateModel)
        {
            var dict = GetModelDictionary(templateModel);

            return MatchTemplate(template, dict);
        }

        /// <summary>
        /// Loads in an anonymous template model and creates a dictionary from each property's json attribute name (token) and value
        /// </summary>
        /// <param name="templateModel"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetModelDictionary(object templateModel)
        {
            var dict = new Dictionary<string, string>();

            var props = templateModel.GetType().GetProperties();

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<JsonPropertyAttribute>();

                string json = attr.PropertyName;
                string value = prop.GetValue(templateModel, null).ToString();

                dict.Add(json, value);
            }

            return dict;
        }

        /// <summary>
        /// This method runs a token regex against our template and then looks for matches in our templateModel dictionary
        /// </summary>
        /// <param name="template"></param>
        /// <param name="tokenDictionary"></param>
        /// <returns></returns>
        private string MatchTemplate(string template, Dictionary<string, string> tokenDictionary)
        {
            // regex for the token brackets
            var regex = new Regex("{{(.+?)}}");

            var splits = regex.Split(template); // splitting the template on each tokenized string {{ }}
            var matches = regex.Matches(template); // finding all token matches

            var sb = new StringBuilder(); 
            
            foreach (string section in splits)
            {
                var trimmed = section.Trim();

                // if token exists in the dictionary, replace it with the value. If not, leave it be
                if (tokenDictionary.ContainsKey(trimmed))
                {
                    sb.Append(tokenDictionary[trimmed]);
                }
                else if (matches.Any(m => m.Value.Contains(section))) // if it's an unused token (not found in model), make it blank (or throw error, whatever you want to do)
                {
                    sb.Append(string.Empty);
                }
                else // leave it be
                {
                    sb.Append(section);
                }
            }
               
            return sb.ToString();
        }
    }
}
