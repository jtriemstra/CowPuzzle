using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class PossibleCombination
    {
        public const int NOT_SET = -1;

        private int[] tilesInOrder;
        private int[] tilesByPosition;
        private int currentIndex;

        public PossibleCombination()
        {
            tilesByPosition = new int[9];
            tilesInOrder = new int[9];
            currentIndex = 0;

            for (int i = 0; i < 9; i++)
            {
                tilesInOrder[i] = NOT_SET;
                tilesByPosition[i] = NOT_SET;
            }
        }

        public bool add(int i)
        {
            if (tilesByPosition[i] == NOT_SET)
            {
                tilesInOrder[currentIndex] = i;
                tilesByPosition[i] = 1;
                currentIndex++;

                return true;
            }

            return false;
        }

        public bool isComplete()
        {
            return currentIndex == 9;
        }

        public PossibleCombination copy()
        {
            PossibleCombination objReturn = new PossibleCombination();
            for (int i = 0; i < 9; i++)
            {
                objReturn.tilesByPosition[i] = this.tilesByPosition[i];
                objReturn.tilesInOrder[i] = this.tilesInOrder[i];
                objReturn.currentIndex = this.currentIndex;
            }

            return objReturn;
        }

        public int get(int position)
        {
            return tilesInOrder[position];
        }

        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                sb.Append(tilesInOrder[i]);
                sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
