using System.Collections.Generic;

namespace Cben.PlugIns
{
    public class CbenPlugInManager : ICbenPlugInManager
    {
        public PlugInSourceList PlugInSources { get; }

        public CbenPlugInManager()
        {
            PlugInSources = new PlugInSourceList();
        }
    }
}