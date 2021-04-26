namespace TemplateEngine.Services.Template
{
    public interface ITemplateService
    {
        string GetTemplateByAlias(string templateAlias);
        string TokenizeMessage(string template, object templateModel);
    }
}
