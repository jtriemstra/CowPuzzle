using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildCombinations
{
    class Program
    {
        static int m_intCombinations = 0;
        static int m_intRotations = 0;

        static void Main(string[] args)
        {
            addCharacter("");
            addRotation("");

            Console.WriteLine(m_intRotations);
            Console.WriteLine(m_intCombinations);
            Console.Read();
        }

        static void addCharacter(String s)
        {
            if (s.Length == 9)
            {
                //Console.WriteLine(s);
                m_intCombinations++;
                return;
            }

            for (int i=0; i<9; i++)
            {
                if (!s.Contains(i.ToString()))
                {
                    addCharacter(s + i);
                }
            }
        }

        static void addRotation(String s)
        {
            if (s.Length == 9)
            {
                m_intRotations++;
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                addRotation(s + i);
            }
        }
    }
}
