using System;
using System.Reflection;
using System.Collections;

namespace Part1
{
    public class Node
    {
        string name = "";
        string text = "";
        public string Name()
        {
            return this.name;
        }
        public string Text()
        {
            return this.text;
        }
        public Node() { }
        public Node(string name, string text)
        {
            this.name = name;
            this.text = text;
        }
    }
    public partial class Vault : IEnumerable
    {
        public string dirpath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        /* По условию нам необходимо добиться максимального быстродействия, про ограничения по памяти не указано.
        По требуемому функционалу наиболее подходящим выглядит Dictionary
        Поиск по имени (ключу) за O(1)
        Добавление элемента за O(1), O(N) только в случае, если требуется увеличить Capacity
        Поддерживает итерирование
        */

        //Поддержка итерируемости
        public IEnumerator GetEnumerator()
        {
            foreach (var node in this.nodes)
            {
                yield return node.Value;
            }
        }

        //Поиск (способ №1)
        public Node Find(string name)
        {
            try
            {
                return nodes[name];
            }
            catch
            {
                throw new Exception();
            }
        }
        //Поиск (способ №2)
        /*public Node this[string name]
        {
            get
            {
                try
                {
                    return nodes[name];
                }
                catch
                {
                    throw new Exception();
                }
            }
        }*/
        public void Add(Node node)
        {
            try
            {
                nodes.Add(node.Name(), node);
            }
            catch
            {
                throw new Exception();
            }
        }
        public int Count()
        {
            return this.nodes.Count();
        }
        public Vault() { }
        public partial void Save();
        public partial void Load();
    }
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Vault vault = new Vault();
            vault.Load();
            Console.WriteLine(vault.Count());
            foreach(Node item in vault)
            {
                Console.WriteLine(item.Name());
            }
            vault.Save();
            //Console.WriteLine(vault["FirstNode"].Text());
            Console.WriteLine(vault.Find("FirstNode").Text());
        }
    }
}