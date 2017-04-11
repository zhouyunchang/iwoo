using System.Collections.Generic;

namespace Cben.UI.Inputs
{
    public interface ILocalizableComboboxItemSource
    {
        ICollection<ILocalizableComboboxItem> Items { get; }
    }
}