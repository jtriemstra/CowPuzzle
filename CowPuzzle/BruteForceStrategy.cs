using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class BruteForceStrategy : BruteForceStrategyBase
    {
        public BruteForceStrategy(Tile[] bag)
        {
            this.bag = bag; 
            addCharacter(new PossibleCombination());
            addRotation(new PossibleRotation());
            Console.WriteLine("created combos and rotations");
        }

        override public void solve()
        {
            for (int i = 0; i < m_lstCombinations.Count; i++)
            {
                for (int j = 0; j < m_lstRotations.Count; j++)
                {
                    bruteForceTry(m_lstCombinations[i], m_lstRotations[j]);
                }

                if (i % 10000 == 0) Console.WriteLine(i);
            }
        }

        protected void bruteForceTry(PossibleCombination objCombination, PossibleRotation objRotation)
        {
            Arrangement a = Arrangement.build(objCombination, objRotation, bag);
            if (a.evaluate())
            {
                Console.WriteLine(objCombination.ToString() + " " + objRotation.ToString());
            }
        }        
    }
}
