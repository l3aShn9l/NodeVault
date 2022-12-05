using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Generators
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace Generated
{
    internal partial class Vault
    {
        partial void GetNodes() 
        {
            Console.WriteLine(" + "\"Load go\"" + @");
                }
            }
        }
    }
}");
            context.AddSource("Program.g.cs", SourceText.From(sourceBuilder.ToString()));
        }



        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
