using System.ComponentModel;

namespace Cben.Localization
{
    public class CbenDisplayNameAttribute : DisplayNameAttribute
    {
        public override string DisplayName => LocalizationHelper.GetString(SourceName, Key);

        public string SourceName { get; set; }
        public string Key { get; set; }

        public CbenDisplayNameAttribute(string sourceName, string key)
        {
            SourceName = sourceName;
            Key = key;
        }
    }
}
