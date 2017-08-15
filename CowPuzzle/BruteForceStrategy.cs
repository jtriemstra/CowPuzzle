using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class BruteForceStrategy
    {
        List<PossibleCombination> m_lstCombinations = new List<PossibleCombination>();
        List<PossibleRotation> m_lstRotations = new List<PossibleRotation>();
        Tile[] bag;

        public BruteForceStrategy(Tile[] bag)
        {
            this.bag = bag;
        }

        public void bruteForce()
        {
            addCharacter(new PossibleCombination());
            addRotation(new PossibleRotation());
            Console.WriteLine("created combos and rotations");

            for (int i = 0; i < m_lstCombinations.Count; i++)
            {
                for (int j = 0; j < m_lstRotations.Count; j++)
                {
                    bruteForceTry(m_lstCombinations[i], m_lstRotations[j]);
                }

                if (i % 10000 == 0) Console.WriteLine(i);
            }
        }

        void bruteForceTry(PossibleCombination objCombination, PossibleRotation objRotation)
        {
            Arrangement a = Arrangement.build(objCombination, objRotation, bag);
            if (a.evaluate())
            {
                Console.WriteLine(objCombination.ToString() + " " + objRotation.ToString());
            }
        }

        void addCharacter(PossibleCombination previous)
        {
            //Console.WriteLine(previous.ToString());
            PossibleCombination current;

            if (previous.isComplete())
            {
                //Console.WriteLine(previous.ToString());
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

        void addRotation(PossibleRotation previous)
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
