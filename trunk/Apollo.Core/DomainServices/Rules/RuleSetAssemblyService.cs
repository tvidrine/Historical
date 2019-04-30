// ------------------------------------------------------------------------------------------------------------------------
//  Copyright (c) Zoom Audits, LLC.
// 
//  Created By: Tim Vidrine
//  Created On: 11/05/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.Domain.Rules;
using Apollo.Core.Contracts.DomainServices.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Apollo.Core.DomainServices.Rules
{
    public class RuleSetAssemblyService : IRuleSetAssemblyService
    {
        private readonly ILogManager _logManager;

        public RuleSetAssemblyService(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public byte[] CreateAssembly(IRuleSet ruleSet)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(ruleSet.Code);

            var assemblyName = ruleSet.Name;
            var references = GetReferences();

            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new [] { syntaxTree},
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

     

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms.ToArray();
                }

                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    _logManager.LogError(new Exception(diagnostic.GetMessage()), "RuleSetAssemblyService.CreateAssembly" );
                }

                return null;
            }
        }

        private IEnumerable<MetadataReference> GetReferences()
        {
            var assemblies = new[]
            {
                typeof(IRuleSet).Assembly,
                typeof(object).Assembly
            };

            var references = assemblies
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            //The location of the .NET assemblies
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")));
            references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")));
            references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")));
            references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll")));
            references.Add(MetadataReference.CreateFromFile(Assembly.Load("netstandard, Version=2.0.0.0").Location));
            return references;
        }
    }


}