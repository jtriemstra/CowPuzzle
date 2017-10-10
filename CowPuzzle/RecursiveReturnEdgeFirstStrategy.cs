using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace CowPuzzle
{
    //
    public class RecursiveReturnEdgeFirstStrategy : ISolvePuzzle
    {
        //TODO: can probably clean up
        int[,] m_intEdges = new int[9, 4];
        Tile[] m_objBag;

        public RecursiveReturnEdgeFirstStrategy(Tile[] bag)
        {
            for (int i = 0; i < 9; i++)
            {
                m_intEdges[i, 0] = bag[i].T;
                m_intEdges[i, 1] = bag[i].R;
                m_intEdges[i, 2] = bag[i].B;
                m_intEdges[i, 3] = bag[i].L;
            }

            m_objBag = bag;
        }

        private int getEdge(int tileIndex, int edgeIndex)
        {
            if (edgeIndex == 0) return m_objBag[tileIndex].T;
            if (edgeIndex == 1) return m_objBag[tileIndex].R;
            if (edgeIndex == 2) return m_objBag[tileIndex].B;
            return m_objBag[tileIndex].L;
        }

        private void print(ImmutableStack<Tile> used)
        {
            Tile[] state = used.ToArray();
            for (int i = state.Length-1; i >= 0 ; i--)
            {
                Console.Write(state[i].id);
                Console.Write(" [");
                Console.Write(state[i].T);
                Console.Write(state[i].R);
                Console.Write(state[i].B);
                Console.Write(state[i].L);
                Console.Write("] ");
            }
            Console.WriteLine();
        }

        private String stackToString(ImmutableStack<Tile> used)
        {
            StringBuilder sb = new StringBuilder();
            Tile[] state = used.ToArray();
            for (int i = state.Length - 1; i >= 0; i--)
            {
                sb.Append(state[i].id);
                sb.Append(" [");
                sb.Append(state[i].T);
                sb.Append(state[i].R);
                sb.Append(state[i].B);
                sb.Append(state[i].L);
                sb.Append("] ");
            }

            return sb.ToString();
        }

        public void solve()
        {
            ImmutableStack<Tile> used = ImmutableStack.Create<Tile>();
            String strReturn = "";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j > 0) m_objBag[i].rotate(j);

                    ImmutableStack<Tile> newStack = used.Push(m_objBag[i]);
                    strReturn = recurse(newStack);
                    //used.Pop();
                }
            }

            Console.WriteLine(strReturn);
        }

        public String recurse(ImmutableStack<Tile> used)
        {
            if (used.Count() == 9)
            {
                return stackToString(used);
            }
            if (used.Count() == 3 || used.Count() == 6) return recurseBottom(used);
            else if (used.Count() == 0 || used.Count() == 1 || used.Count() == 2) return recurseRight(used);
            else return recurseRightAndBottom(used);
        }

        public String recurseRight(ImmutableStack<Tile> used)
        {
            int nextEdgeValue = used.Peek().R * -1;
            
            for (int i = 0; i < 9; i++)
            {
                if (!used.Contains(m_objBag[i]))
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (getEdge(i, j) == nextEdgeValue)
                        {
                            Tile t = m_objBag[i];
                            t.rotate(Math.Abs(3-j));
                            ImmutableStack<Tile> newStack = used.Push(t);
                            //returning here may find fewer solutions
                            String s = recurse(newStack);
                            if (s != "") return s;
                            //used.Pop();
                        }
                    }
                }
            }

            return "";

        }

        public String recurseBottom(ImmutableStack<Tile> used)
        {
            Tile[] state = used.ToArray();
            int nextEdgeValue = state[2].B * -1;

            for (int i = 0; i < 9; i++)
            {
                if (!used.Contains(m_objBag[i]))
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (getEdge(i, j) == nextEdgeValue)
                        {
                            Tile t = m_objBag[i];
                            t.rotate(j);
                            ImmutableStack<Tile> newStack = used.Push(t);
                            String s = recurse(newStack);
                            if (s != "") return s;
                            //used.Pop();
                        }
                    }
                }
            }

            return "";
        }

        public String recurseRightAndBottom(ImmutableStack<Tile> used)
        {
            int nextEdgeValueR = used.Peek().R * -1;
            Tile[] state = used.ToArray();
            int nextEdgeValueB = state[2].B * -1;

            for (int i = 0; i < 9; i++)
            {
                if (!used.Contains(m_objBag[i]))
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (getEdge(i, j) == nextEdgeValueR && getEdge(i, (j+1) % 4) == nextEdgeValueB)
                        {
                            Tile t = m_objBag[i];
                            t.rotate(Math.Abs(3 - j));
                            ImmutableStack<Tile> newStack = used.Push(t);
                            String s = recurse(newStack);
                            if (s != "") return s;
                            //used.Pop();
                        }
                    }
                }
            }

            return "";
        }
    }
}
