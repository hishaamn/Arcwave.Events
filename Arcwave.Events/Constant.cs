using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcwave.Events
{
    public static class Constant
    {
        public static class TemplateId
        {
            public static readonly ID ApplicationShortcutTemplateId = new ID("{72450C9C-98C4-4117-88B7-573110C7E0C0}");
        }

        public static class FieldId
        {
            public static readonly ID ApplicationFieldId = new ID("{8A1AC797-0304-4C50-AAAC-4D9DBBFF435F}");
        }

        public static class Pattern
        {
            public static readonly string UrlPattern = @"url=""([^""]*)""";

            public static readonly string IdPattern = @"id=""{([^}]*)}""";
        }
    }
}
