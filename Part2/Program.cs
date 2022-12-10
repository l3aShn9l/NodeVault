using System;
using System.Reflection;
using System.Text.Json;
using System.Collections;

namespace Part2
{
    public class Node<T>
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
    public partial class Vault : IEnumerable
    {
        public string dirpath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

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
