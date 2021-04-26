namespace TemplateEngine.Domain.Email
{
    public class TemplatedMessage : MessageBase
    {
        public string Template { get; set; }
        public object TemplateModel { get; set; }
    }
}
