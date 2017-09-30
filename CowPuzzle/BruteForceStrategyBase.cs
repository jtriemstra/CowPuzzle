using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class BruteForceStrategyBase
    {
        protected List<PossibleCombination> m_lstCombinations = new List<PossibleCombination>();
        protected List<PossibleRotation> m_lstRotations = new List<PossibleRotation>();
        protected Tile[] bag;

        protected void addCharacter(PossibleCombination previous)
        {
            PossibleCombination current;

            if (previous.isComplete())
            {
                m_lstCombinations.Add(previous);
                return;
            }

            for (int i = 0; i < 9; i++)
            {
                current = previous.copy();
                if (current.add(i))
                {
                    addCharacter(current);
                }
                else
                {
                    //Console.WriteLine("add fail " + previous.ToString());
                }
            }

        }

        protected void addRotation(PossibleRotation previous)
        {
            PossibleRotation current;

            if (previous.isComplete())
            {
                m_lstRotations.Add(previous);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                current = previous.copy();
                if (current.add(i))
                {
                    addRotation(current);
                }
            }
        }
    }
}
