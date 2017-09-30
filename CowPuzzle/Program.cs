using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            Puzzle p = new Puzzle();
            p.init();
            /*DuplicateLimitingParallelStrategy b = new DuplicateLimitingParallelStrategy(p.bag);
            b.bruteForce();*/

            EdgeFirstStrategy e = new EdgeFirstStrategy(p.bag);
            e.test();

            Console.WriteLine("done");
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();
        }
    }

    class Puzzle
    {
        const int NOSE_LEFT = 1;
        const int SMALL_HEAD = 2;
        const int BIG_HEAD = 3;
        const int LAY_TOP = 4;
        const int LAY_BOTTOM = LAY_TOP * -1;
        const int BIG_TAIL = BIG_HEAD * -1;
        const int SMALL_TAIL = SMALL_HEAD * -1;
        const int NOSE_RIGHT = NOSE_LEFT * -1;

        int[] rotations = new int[] { 0, 1, 2, 3 };
        public Tile[] bag = new Tile[9];
        Arrangement currentState = new Arrangement();
        
        public void init()
        {
            bag[2] = new Tile(NOSE_LEFT, SMALL_TAIL, LAY_BOTTOM, BIG_TAIL);
            bag[0] = new Tile(LAY_BOTTOM, LAY_BOTTOM, BIG_HEAD, SMALL_HEAD);
            bag[1] = new Tile(SMALL_HEAD, BIG_HEAD, LAY_BOTTOM, SMALL_HEAD);
            bag[3] = new Tile(BIG_TAIL, NOSE_LEFT, LAY_BOTTOM, NOSE_RIGHT);
            bag[4] = new Tile(LAY_TOP, BIG_TAIL, NOSE_RIGHT, BIG_TAIL);
            bag[5] = new Tile(LAY_BOTTOM, SMALL_TAIL, NOSE_LEFT, BIG_TAIL);
            bag[6] = new Tile(NOSE_RIGHT, BIG_HEAD, LAY_TOP, SMALL_HEAD);
            bag[7] = new Tile(BIG_HEAD, SMALL_TAIL, NOSE_LEFT, NOSE_RIGHT);
            bag[8] = new Tile(LAY_TOP, BIG_TAIL, NOSE_RIGHT, SMALL_HEAD);

            /*bag[0] = new Tile(NOSE_LEFT, NOSE_LEFT, NOSE_LEFT, NOSE_LEFT);
            bag[1] = new Tile(NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT);
            bag[2] = new Tile(NOSE_LEFT, NOSE_LEFT, NOSE_LEFT, NOSE_LEFT);
            bag[3] = new Tile(NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT);
            bag[4] = new Tile(NOSE_LEFT, NOSE_LEFT, NOSE_LEFT, NOSE_LEFT);
            bag[5] = new Tile(NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT);
            bag[6] = new Tile(NOSE_LEFT, NOSE_LEFT, NOSE_LEFT, NOSE_LEFT);
            bag[7] = new Tile(NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT, NOSE_RIGHT);
            bag[8] = new Tile(NOSE_LEFT, NOSE_LEFT, NOSE_LEFT, NOSE_LEFT);*/
        }        
    }
}
