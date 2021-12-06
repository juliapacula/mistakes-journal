namespace Mistakes.Journal.Api.Api.User.WebModels
{
    public class ConfigurationWebModel
    {
        public string LoginPath { get; set; }
        public string RegisterPath { get; set; }
        public string LogoutPath { get; set; }
        public string DeniedPath { get; set; }
    }
}