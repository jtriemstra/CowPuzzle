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

        

        void greedy()
        {
            List<int> discardedIds = new List<int>();

            while(!currentState.isComplete())
            {
                int nextId = currentState.getLowestUnusedId(discardedIds);

                if (nextId > -1)
                {
                    currentState.addTile(bag[nextId]);
                    bool thisTileFits = false;

                    if (!currentState.evaluateCurrent())
                    {
                        while (currentState.rotateCurrent() && !thisTileFits)
                        {
                            if (currentState.evaluateCurrent())
                            {
                                thisTileFits = true;
                            }
                        }

                        if (!thisTileFits)
                        {
                            Tile discardedTile = currentState.discardCurrent();
                            discardedIds.Add(discardedTile.id);
                        }
                    }
                    else
                    {
                        thisTileFits = true;
                    }

                    if (thisTileFits)
                    {
                        currentState.pushCurrent();
                        discardedIds.Clear();
                    }
                }
                else
                {
                    //this is going to generate an infinite loop
                    discardedIds.Clear();
                    Tile poppedTile = currentState.popCurrent();
                    discardedIds.Add(poppedTile.id);
                }
            }
        }


        void greedyNext(int i)
        {
            
        }

        void solve()
        {
            
        }

        void addTile()
        {

        }

        
    }
}
/*
 * new
- complete? no
- 0
-- complete? no
-- 1
--- complete? no
....
------- 6
-------- complete? no
-------- 7
--------- complete? no
--------- 8
---------- complete? yes
---------- return 8
--------- end loop return
--------

0 1 2 3 4 5 6 7 8
0 1 2 3 4 5 6 

NOSE_LEFT 4
NOSE_RIGHT 5
SMALL_TAIL 3
SMALL_HEAD 5
LAY_BOTTOM 6
LAY_TOP 3
BIG_TAIL 6
BIG_HEAD 4

1
2
3
2

4
3
3
4
 
 36 edges
 4*5 3*5 6*3 6*4 = 77
 * 
 * */
/*
1 2 3 4 5 6 7 8 9
1 3 2 4 5 6 7 8 9
1 3 4 2 5 6 7 8 9


1a 2a 3a

1a 2a 3b

1a 2a 3c

1a 2a 3d

1a 2a 4a


place 1 (place tile, evaluate)

place 2 (if success, place tile, evaluate)

rotate 2 (if fail, rotate)

rotate 2 (if fail, rotate)

place 3 in slot 3 (if success, place tile, evaluate)

rotate 3 (if fail, rotate)

place 4 in slot 4 (if success, place tile, evaluate)

place 5 in slot 5 (if success, place tile, evaluate)

rotate 5 (if fail, rotate)

rotate 5 (if fail, rotate)

rotate 5 (if fail, rotate)

discard 5 (if 3 fails, discard and try next card

place/rotate3/discard 6 (place tile, evaluate)
place/rotate3/discard 7 (place tile, evaluate)
place/rotate3/discard 8 (place tile, evaluate)
place/rotate3/discard 9 (place tile, evaluate)

bag empt

reload bag from discard w/ 5-9 (if bag empty, reload)

rotate 4 (if ____ rotate)

rotate 4 (if fail, rotate)

repeat the 5-9 section


discard 4

reload bag from discard w/ 4-9

place 5 (this looks like a loop, not recursion)

rotate 5

place/rotate3/discard 4
place/rotate3/discard 6
place/rotate3/discard 7
place/rotate3/discard 8
place/rotate3/discard 9

bag empty

reload bag from discard

discard 5

...

discard 9



*/