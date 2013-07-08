﻿namespace Microsoft.VisualStudio.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Validation;

    [DebuggerDisplay("{Type.Name}")]
    public class ComposablePartDefinition
    {
        public ComposablePartDefinition(Type partType, IReadOnlyCollection<ExportDefinition> exportsOnType, IReadOnlyDictionary<MemberInfo, ExportDefinition> exportsOnMembers, IReadOnlyDictionary<MemberInfo, ImportDefinition> imports, string sharingBoundary, MethodInfo onImportsSatisfied)
        {
            Requires.NotNull(partType, "partType");
            Requires.NotNull(exportsOnType, "exportsOnType");
            Requires.NotNull(exportsOnMembers, "exportsOnMembers");
            Requires.NotNull(imports, "imports");

            this.Type = partType;
            this.ExportDefinitionsOnType = exportsOnType;
            this.ExportDefinitionsOnMembers = exportsOnMembers;
            this.ImportDefinitions = imports;
            this.SharingBoundary = sharingBoundary;
            this.OnImportsSatisfied = onImportsSatisfied;
        }

        public Type Type { get; private set; }

        public string Id
        {
            get { return this.Type.Name; }
        }

        public string SharingBoundary { get; private set; }

        public bool IsShared
        {
            get { return this.SharingBoundary != null; }
        }

        public MethodInfo OnImportsSatisfied { get; private set; }

        public IReadOnlyCollection<ExportDefinition> ExportDefinitionsOnType { get; private set; }

        public IReadOnlyDictionary<MemberInfo, ExportDefinition> ExportDefinitionsOnMembers { get; private set; }

        public IEnumerable<KeyValuePair<MemberInfo, ExportDefinition>> ExportDefinitions
        {
            get
            {
                foreach (var export in this.ExportDefinitionsOnType)
                {
                    yield return new KeyValuePair<MemberInfo, ExportDefinition>(null, export);
                }

                foreach (var export in this.ExportDefinitionsOnMembers)
                {
                    yield return export;
                }
            }
        }

        public IReadOnlyDictionary<MemberInfo, ImportDefinition> ImportDefinitions { get; private set; }
    }
}