using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Mistakes.Journal.Api.Resources
{
    public class AccountLocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public AccountLocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(AccountResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("AccountResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return _localizer[key, parameter];
        }
    }
}