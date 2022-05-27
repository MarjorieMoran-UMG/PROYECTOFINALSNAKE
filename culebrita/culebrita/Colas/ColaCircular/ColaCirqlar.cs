using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace culebrita.Colas.ColaCircular
{
    class ColaCirqlar
    {
        public int endcirq;
        private static int maximo = 90000;
        protected int start;
        int tamanios;

        protected Object[] listaCola;

        private int SigPos(int n)
        {
            return (n + 1) % maximo;
        }

        public ColaCirqlar()
        {
            start = 0;
            endcirq = maximo - 1;
            listaCola = new Object[maximo];
        }

        public bool VacioColitaCirq()
        {
            return start == SigPos(endcirq);
        }

        public bool LlenaColitaCirq()
        {
            return endcirq == SigPos(SigPos(endcirq));
        }

        public void agg(Object elemento)
        {
            if (!LlenaColitaCirq())
            {
                endcirq = SigPos(endcirq);
                listaCola[endcirq] = elemento;
                tamanios++;
            }
            else
            {
                throw new Exception("Overflow");
            }
        }

        public Object BorrarElement()
        {
            if (!VacioColitaCirq())
            {
                Object tm = listaCola[start];
                start = SigPos(start);
                tamanios--;
                return tm;
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
        }

        public void BorrarColita()
        {
            start = 0;
            endcirq = maximo - 1;
            listaCola = new Object[maximo];
        }


        public Object AdelanteColita()
        {
            if (!VacioColitaCirq())
            {
                return listaCola[start];
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
        }

        public Object FinColitaCirq()
        {

            if (!VacioColitaCirq())
            {
                return listaCola[endcirq];
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }

        }

        public int tamanio()
        {
            return tamanios;
        }

        public bool Any(Point dato)
        {
            int i = 0, cont = 0;
            bool flag;
            while (i <= endcirq)
            {
                Point a = (Point)listaCola[i];
                flag = ((a.X == dato.X) && (a.Y == dato.Y));
                int z = (flag == true) ? cont++ : cont + 0;
            }
            return (cont != 0) ? true : false;
        }
    }
}
