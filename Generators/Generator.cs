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
            this.nodes.Clear();
            Console.WriteLine(" + "\"Load go\"" + @");
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + " + "\"\\NodesIn\"" + @";
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                if(Path.GetExtension(fileName) == " + "\".node\"" + @")
                {
                    StreamReader reader = new StreamReader(fileName);
                    Node node = new Node(Path.GetFileNameWithoutExtension(fileName),reader.ReadToEnd());
                    this.Add(node);
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
