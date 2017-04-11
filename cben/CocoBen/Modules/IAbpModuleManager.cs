using System;
using System.Collections.Generic;

namespace Cben.Modules
{
    public interface ICbenModuleManager
    {
        CbenModuleInfo StartupModule { get; }

        IReadOnlyList<CbenModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}