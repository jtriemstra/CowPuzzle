using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    public class Tile
    {
        public int T;
        public int R;
        public int B;
        public int L;
        private int rotations = 0;
        public int id;
        private static int currentId = 0;

        public Tile(int t, int r, int b, int l)
        {
            T = t;
            R = r;
            B = b;
            L = l;
            this.id = currentId++;
        }

        public bool rotate()
        {
            int temp = T;
            T = L;
            L = B;
            B = R;
            R = temp;

            rotations++;
            return true;
        }

        public void rotate(int numberOfRotations)
        {
            for (int i = 1; i <= numberOfRotations; i++)
            {
                rotate();
            }
        }

        public void resetRotations()
        {
            rotations = 0;
        }

        public Tile getRotatedCopy(int numberOfRotations)
        {
            if (numberOfRotations < 0 || numberOfRotations > 3)
            {
                throw new Exception("invalid rotation number: " + numberOfRotations);
            }

            Tile objReturn = new Tile(T, R, B, L);
            for (int i = 1; i <= numberOfRotations; i++)
            {
                objReturn.rotate();
            }

            return objReturn;
        }
    }
}
