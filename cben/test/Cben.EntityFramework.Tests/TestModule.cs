using Cben.Core;
using Cben.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.EntityFramework.Tests
{
    
    [DependsOn(
        typeof(CbenCoreDbModule))]
    public class TestModule : CbenModule
    {
    }
}
