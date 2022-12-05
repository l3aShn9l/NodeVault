using System;
using System.Reflection;
using System.Text.Json;
using System.Collections;

namespace Part1
{
    class Node<T>
    {
        string name = "";
        T obj;
        public string Name()
        {
            return this.name;
        }
        public T Object()
        {
            return this.obj;
        }
        public Node() { }
        public Node(string name, T obj)
        {
            this.name = name;
            this.obj = obj;
        }
    }
    partial class Vault : IEnumerable
    {
        private Dictionary<string, Node<object>> nodes = new Dictionary<string, Node<object>>();
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
        public Node<object> Find(string name)
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
        public void Add(Node<object> node)
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
        public void Save()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\NodesOut";
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                fileInfo.Delete();
            }
            foreach (Node<object> item in this)
            {
                StreamWriter writer = new StreamWriter(path + $"\\{item.Name()}.node", false);
                string json = JsonSerializer.Serialize(item.Object());
                writer.Write(json);
                writer.Close();
            }

        }
        //partial void GetNodes();
        public void Load()
        {
            //GetNodes();
            this.nodes.Clear();
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\NodesIn";
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                if (Path.GetExtension(fileName) == ".node")
                {
                    StreamReader reader = new StreamReader(fileName);
                    Node<object> node = new Node<object>(Path.GetFileNameWithoutExtension(fileName), JsonSerializer.Deserialize<object>(reader.ReadToEnd()));
                    this.Add(node);
                }
            }
        }
    }
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Vault vault = new Vault();
            vault.Load();
            Console.WriteLine(vault.Count());
            Console.WriteLine(vault.Find("FirstNode").Object());
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6 };
            Node<object> node = new Node<object>("hello",list);
            vault.Add(node);
            foreach (Node<object> item in vault)
            {
                Console.WriteLine(item.Name());
            }
            vault.Save();
        }
    }
}
