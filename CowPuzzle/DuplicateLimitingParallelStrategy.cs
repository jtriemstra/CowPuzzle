using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    class DuplicateLimitingParallelStrategy : DuplicateLimitingStrategy
    {
        public DuplicateLimitingParallelStrategy(Tile[] bag) : base(bag)
        {
            
        }

        override public void bruteForce()
        {
            Parallel.For(0, m_lstCombinations.Count, i =>
            {
                Arrangement a = new Arrangement();
                innerLoop(m_lstCombinations[i], a);
            });
        }

        protected void innerLoop(int[] objCombination, Arrangement a)
        {
            Tile[] objTest = new Tile[9];
            for (int j = 0; j < m_lstRotations.Count; j++)
            {
                bruteForceTry(objCombination, m_lstRotations[j], objTest, a);
            }
        }
        
        override protected void bruteForceTry(int[] objCombination, int[] objRotation, Tile[] objTest, Arrangement a)
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
