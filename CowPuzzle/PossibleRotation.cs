using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class PossibleRotation
    {
        public const int NOT_SET = -1;

        private int[] tilesInOrder;
        
        private int currentIndex;

        public PossibleRotation()
        {
            
            tilesInOrder = new int[9];
            currentIndex = 0;

            for (int i = 0; i < 9; i++)
            {
                tilesInOrder[i] = NOT_SET;
            
            }
        }

        public bool add(int i)
        {
                tilesInOrder[currentIndex] = i;
            
                currentIndex++;

                return true;
            

            
        }

        public bool isComplete()
        {
            return currentIndex == 9;
        }

        public PossibleRotation copy()
        {
            PossibleRotation objReturn = new PossibleRotation();
            for (int i = 0; i < 9; i++)
            {
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
