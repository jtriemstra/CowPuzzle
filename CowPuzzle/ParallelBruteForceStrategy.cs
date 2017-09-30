using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CowPuzzle
{
    public class ParallelBruteForceStrategy : BruteForceStrategy
    {
        public ParallelBruteForceStrategy(Tile[] bag) : base(bag)
        {
            
        }

        public void bruteForce()
        {
            Parallel.For(0, m_lstCombinations.Count, i =>
            {
                for (int j = 0; j < m_lstRotations.Count; j++)
                {
                    bruteForceTry(m_lstCombinations[i], m_lstRotations[j]);
                }

            });
        }
    }
}
