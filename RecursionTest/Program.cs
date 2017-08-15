using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<int> l = new List<int>();
            l.Add(1);
            l.Add(1);
            l.Add(1);
            l.Add(1);
            l.Add(1);

            recurse(l);

            String s = "asdfg";
            recurse(s);*/

            Stack<int> s = new Stack<int>();
            s.Push(1);
            s.Push(1);
            s.Push(1);
            s.Push(1);
            s.Push(1);
            recurse(s);
            Console.Read();

        }

        static void recurse(List<int> l)
        {
            Console.WriteLine(l.Count);
            if (l.Count > 0)
            {
                l.RemoveAt(0);
                recurse(l);
            }
            Console.WriteLine(l.Count);
        }

        static void recurse(Stack<int> l)
        {
            Console.WriteLine(l.Count);
            if (l.Count > 0)
            {
                int i = l.Pop();
                recurse(l);
                l.Push(i);
            }
            Console.WriteLine(l.Count);
        }

        static void recurse(String s)
        {
            Console.WriteLine(s);
            if (s.Length > 1)
            {
                recurse(s.Substring(1));
            }
            Console.WriteLine(s);
        }
    }


}
