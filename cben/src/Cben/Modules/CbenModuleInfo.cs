using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Cben.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// </summary>
    public class CbenModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public CbenModule Instance { get; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<CbenModuleInfo> Dependencies { get; }

        /// <summary>
        /// Creates a new CbenModuleInfo object.
        /// </summary>
        public CbenModuleInfo([NotNull] Type type, [NotNull] CbenModule instance)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;

            Dependencies = new List<CbenModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}