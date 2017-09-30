using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    class DuplicateLimitingStrategy : NoObjectBruteForceStrategy
    {
        public DuplicateLimitingStrategy(Tile[] bag) : base(bag)
        {
            
        }

        override protected void addCharacter(int[] previous, int currentIndex)
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

        override protected void addRotation(int[] previous, int currentIndex)
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
    }
}
