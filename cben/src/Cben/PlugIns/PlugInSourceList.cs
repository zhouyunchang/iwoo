﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Cben.PlugIns
{
    public class PlugInSourceList : List<IPlugInSource>
    {
        public List<Type> GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToList();
        }
    }
}