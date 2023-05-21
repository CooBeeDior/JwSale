using FreeSql.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LocalizerCore
{

    public class MsSqlStringLocalizer : IStringLocalizer
    {
        private readonly string _typeName;
        private readonly IServiceProvider _serviceProvider;
        private readonly IdleBus<IFreeSql> _idleBusFreeSql;


        public MsSqlStringLocalizer(string typeName, IServiceProvider serviceProvider)
        {
            _typeName = typeName;
            _serviceProvider = serviceProvider;
            _idleBusFreeSql = serviceProvider.GetService<IdleBus<IFreeSql>>();
        }

        private LocalizedString getLocallizedString(string name, params object[] arguments)
        {
            var freeSql = _idleBusFreeSql.Get(nameof(MsSqlStringLocalizerOptions));
            var localizedString = freeSql.Select<MsSqlStringLocalizedTable>().Where(o => o.TypeName == _typeName && o.Name == name && o.Culture == CultureInfo.CurrentCulture.Name).ToOne();
            if (localizedString != null)
            {
                return new LocalizedString(name, string.Format(localizedString.LocalizedStringTemplate, arguments));
            }

            return new LocalizedString(name, "");
        }
        public LocalizedString this[string name]
        {
            get
            {
                return getLocallizedString(name);

            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                return getLocallizedString(name, arguments);
            }
        }


        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var list = new List<LocalizedString>();
            var freeSql = _idleBusFreeSql.Get(nameof(MsSqlStringLocalizerOptions));
            var localizedStrings = freeSql.Select<MsSqlStringLocalizedTable>().Where(o => o.TypeName == _typeName && o.Culture == CultureInfo.CurrentCulture.Name).ToList();
            if (localizedStrings != null && localizedStrings.Count > 0)
            {
                foreach (var item in localizedStrings)
                {
                    var LocalizedString = new LocalizedString(item.Name,item.LocalizedStringTemplate);
                }
            }         

            return list;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;


            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            return new MsSqlStringLocalizer(_typeName, _serviceProvider);
        }
    }
}
