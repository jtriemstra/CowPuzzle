using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class Arrangement
    {
        /*
         * Structure the puzzle array indexes like
         * 0 1 2
         * 3 4 5
         * 6 7 8
         */

        Tile[] puzzle = new Tile[9];
        int current = 0;

        public bool isComplete()
        {
            return current >= puzzle.Length;
        }

        public void pushCurrent()
        {
            current++;
        }

        public Tile discardCurrent()
        {
            Tile temp = puzzle[current];
            puzzle[current] = null;
            return temp;
        }

        public Tile popCurrent()
        {
            Tile temp = puzzle[current];
            puzzle[current] = null;
            current--;
            return temp;
        }

        public void addTile(Tile objNew)
        {
            if (isIdInArrangement(objNew.id)) throw new Exception("attempt to duplicate a tile");

            puzzle[current] = objNew;
            objNew.resetRotations();
        }

        public bool rotateCurrent()
        {
            return puzzle[current].rotate();
        }

        private bool isIdInArrangement(int id)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (puzzle[i].id == id) return true;
            }

            return false;
        }

        public int getLowestUnusedId(List<int> ineligibleIds)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!isIdInArrangement(i) && !ineligibleIds.Contains(i)) return i;
            }
            return -1;
        }

        public bool evaluateCurrent()
        {
            if (current == 0) return true;
            if (currentHasLeft() && puzzle[current].L != -1 * puzzle[current - 1].R) return false;
            if (currentHasTop() && puzzle[current].T != -1 * puzzle[current - 3].B) return false;
            if (currentHasRight() && puzzle[current].R != -1 * puzzle[current + 1].L) return false;
            if (currentHasBottom() && puzzle[current].B != -1 * puzzle[current + 3].T) return false;
            return true;
        }

        private bool currentHasLeft()
        {
            return (!(current == 0 || current == 3 || current == 6) );
        }

        private bool currentHasTop()
        {
            return (current > 2 );
        }

        private bool currentHasRight()
        {
            return (!(current == 2 || current == 5 || current == 8) );
        }

        private bool currentHasBottom()
        {
            return (current < 6 );
        }

        public static Arrangement build(PossibleCombination objCombination, PossibleRotation objRotation, Tile[] bag)
        {
            Arrangement objReturn = new Arrangement();
            for (int i = 0; i < 9; i++)
            {
                int tile = objCombination.get(i);
                int rotations = objRotation.get(i);
                objReturn.puzzle[i] = bag[tile].getRotatedCopy(rotations);
            }
            return objReturn;
        }

        public bool evaluate(Tile[] input)
        {
            puzzle = input;

            for (int i = 0; i < 9; i++)
            {
                current = i;
                if (!evaluateCurrent()) return false;
            }

            return true;
        }

        public bool evaluate()
        {
            for (int i = 0; i < 9; i++)
            {
                current = i;
                if (!evaluateCurrent()) return false;
            }

            return true;
        }
    }
}
