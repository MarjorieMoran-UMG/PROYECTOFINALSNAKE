using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Colas.ColaLinealA
{
    class ColaLineal
    {
        public int end;
        private static int maximo = 90000;
        protected int start;
        int tamanio;
        protected Object[] LstColita;


        public ColaLineal()
        {
            start = 0;
            end = -1;
            LstColita = new Object[maximo];
        }


        public void AggLineal(Object elemento)
        {
            if (!ColitaLinealLlena())
            {
                LstColita[++end] = elemento;
                tamanio++;

            }
            else
            {
                throw new Exception("Overflow");
            }
        }

        public bool ColitaLinealVacia()
        {
            return start > end;
        }

        public bool ColitaLinealLlena()
        {
            if (end == maximo - 1)
            {
                return true;
            }
            return false;
        }

        public Object BorrarColitaLineal()
        {
            if (!ColitaLinealVacia())
            {
                tamanio--;
                return LstColita[start++];     
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
        }

        public void ColitaClean()
        {
            start = 0;
            end = -1;
        }

        public Object InicioColaLin()
        {
            if (!ColitaLinealVacia())
            {
                return LstColita[start++];
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
        }

        public Object FinalColaLin()
        {

            if (!ColitaLinealVacia())
            {
                return LstColita[end];
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }

        }

        public int Tamaniooo()
        {
            return tamanio;
        }

        public bool Any(Point dato)
        {
            int i = 0, cont = 0;
            bool flag;
            while (i <= end)
            {
                Point a = (Point)LstColita[i];
                flag = ((a.X == dato.X) && (a.Y == dato.Y));
                int z = (flag == true) ? cont++ : cont + 0;
                i++;
            }
            return (cont != 0) ? true : false;
        }

    }
}

