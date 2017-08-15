using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    class DuplicateLimitingParallelStrategy
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

        public DuplicateLimitingParallelStrategy(Tile[] bag)
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
                    if ((currentIndex == 2 || currentIndex == 6 || currentIndex == 8) && i < current[0]) 
                    {

                    }
                    else
                    {
                        current[currentIndex] = i;
                        addCharacter(current, currentIndex + 1);
                    }
                }
                else {
                    
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
                Array.Copy(previous, current, 9);
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
            
            Parallel.For(0, m_lstCombinations.Count, i =>
            {
                Arrangement a = new Arrangement();
                innerLoop(m_lstCombinations[i], a);
                //if (i % 10000 == 0) Console.WriteLine(i);
            });
        }

        void innerLoop(int[] objCombination, Arrangement a)
        {
            Tile[] objTest = new Tile[9];
            for (int j = 0; j < m_lstRotations.Count; j++)
            {
                bruteForceTry(objCombination, m_lstRotations[j], objTest, a);
            }
        }
        
        

        void bruteForceTry(int[] objCombination, int[] objRotation, Tile[] objTest, Arrangement a)
        {
            
            objTest[0] = bagRotations[objCombination[0], objRotation[0]];
            objTest[1] = bagRotations[objCombination[1], objRotation[1]];
            objTest[2] = bagRotations[objCombination[2], objRotation[2]];
            objTest[3] = bagRotations[objCombination[3], objRotation[3]];
            objTest[4] = bagRotations[objCombination[4], objRotation[4]];
            objTest[5] = bagRotations[objCombination[5], objRotation[5]];
            objTest[6] = bagRotations[objCombination[6], objRotation[6]];
            objTest[7] = bagRotations[objCombination[7], objRotation[7]];
            objTest[8] = bagRotations[objCombination[8], objRotation[8]];
            

            if (a.evaluate(objTest))
            {
                Console.WriteLine(objCombination.ToString() + " " + objRotation.ToString());
            }
        }

        
    }
}
