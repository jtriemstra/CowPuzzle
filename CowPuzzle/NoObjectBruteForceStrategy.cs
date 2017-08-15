using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    class NoObjectBruteForceStrategy
    {
        const int TOP_OFFSET = 0;
        const int RIGHT_OFFSET = 1;
        const int BOTTOM_OFFSET = 2;
        const int LEFT_OFFSET = 3;
        const int ARRAY_LENGTH = 4 * 9;

        List<int[]> m_lstCombinations = new List<int[]>();
        List<int[]> m_lstRotations = new List<int[]>();
        Tile[] bag;
        Tile[,] bagRotations = new Tile[9,4];

        public NoObjectBruteForceStrategy(Tile[] bag)
        {
            this.bag = bag;
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    bagRotations[j, i] = bag[j].getRotatedCopy(i);
                }
            }
        }

        void addCharacter(int[] previous, int currentIndex)
        {
            if (currentIndex == 9)
            {
                m_lstCombinations.Add(previous);
                return;
            }


            for (int i = 0; i < 9; i++)
            {
                int[] current = new int[9];

                Array.Copy(previous, current, 9);
                if (!current.Contains(i))
                {
                    current[currentIndex] = i;
                    addCharacter(current, currentIndex + 1);
                }
                else
                {
                    //Console.WriteLine("add fail " + previous.ToString());
                }
            }

        }

        void addRotation(int[] previous, int currentIndex)
        {
            if (currentIndex == 9)
            {
                m_lstRotations.Add(previous);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int[] current = new int[9];
                Array.Copy(previous, current, currentIndex);
                current[currentIndex] = i;
                addRotation(current, currentIndex + 1);                
            }
        }

        public void bruteForce()
        {
            int[] start = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            addCharacter(start, 0);
            start = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            addRotation(start, 0);
            Console.WriteLine("created combos and rotations");

            for (int i = 0; i < m_lstCombinations.Count; i++)
            {
                for (int j = 0; j < m_lstRotations.Count; j++)
                {
                    bruteForceTry(m_lstCombinations[i], m_lstRotations[j]);
                }

                //if (i % 10000 == 0) Console.WriteLine(i);
            }
        }

        Tile[] m_objTest = new Tile[9];
        Arrangement a = new Arrangement();

        void bruteForceTry(int[] objCombination, int[] objRotation)
        {
            for (int i = 0; i < 9; i++)
            {
                m_objTest[i] = bagRotations[objCombination[i], objRotation[i]];
            }

            if (a.evaluate(m_objTest))
            {
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(objCombination[i] + "(" + objRotation[i] + ") ");
                }
                Console.WriteLine(objCombination.ToString() + " " + objRotation.ToString());
            }
        }
    }
}
