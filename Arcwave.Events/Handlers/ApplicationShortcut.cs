using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.StringExtensions;
using System;
using System.Text.RegularExpressions;

namespace Arcwave.Events.Handlers
{
    public class ApplicationShortcut
    {
        private Database CoreDatabase
        {
            get
            {
                return Factory.GetDatabase("core");
            }
        }

        public void OnItemSaving(object sender, EventArgs args)
        {
            var eventArgs = args as SitecoreEventArgs;

            var updatedItem = eventArgs.Parameters[0] as Item;

            if (this.ShouldSkipItem(updatedItem))
            {
                return;
            }

            var itemChanges = eventArgs.Parameters[1] as ItemChanges;

            if (itemChanges.FieldChanges.Contains(Constant.FieldId.ApplicationFieldId)) //Application Field ID
            {
                var applicationLinkNewValue = itemChanges.FieldChanges[Constant.FieldId.ApplicationFieldId].Value;

                if (!applicationLinkNewValue.IsNullOrEmpty())
                {
                    Match match = Regex.Match(applicationLinkNewValue, Constant.Pattern.IdPattern);
                    
                    if (match.Success)
                    {
                        var idValue = match.Groups[1].Value;

                        var applicationItem = this.CoreDatabase.GetItem(new ID(idValue));

                        if(applicationItem != null)
                        {
                            var path = applicationItem.Paths.ContentPath;

                            applicationLinkNewValue = Regex.Replace(applicationLinkNewValue, Constant.Pattern.IdPattern, $"url=\"{path}\"");

                            itemChanges.SetFieldValue(updatedItem.Fields[Constant.FieldId.ApplicationFieldId], applicationLinkNewValue);
                        }
                    }                    
                }
            }
        }

        private bool ShouldSkipItem(Item item)
        {
            return item == null || item.Database.Name.ToLower() != "core" || item.TemplateID != Constant.TemplateId.ApplicationShortcutTemplateId;
        }
    }
}
