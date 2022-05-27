using culebrita.Colas.Objeto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Colas.ColaArrayLst
{
    class ColaLst
    {
        public Nodo first;
        public Nodo end;
        int tamanio;

        public ColaLst()
        {
            first = end = null;
        }

        public bool VacioColita()
        {
            return (first == null);
        }

        public void Insert(Object elemento)
        {
            Nodo nuevo;
            nuevo = new Nodo(elemento);

            if (VacioColita())
            {
                first = nuevo;
            }
            else
            {
                end.next = nuevo;
            }
            end = nuevo;
            tamanio++;
        }

        public Object BorrarElemento()
        {
            Object aux;
            if (!VacioColita())
            {
                aux = first.elemento;
                first = first.next;
                tamanio--;
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
            return aux;
        }

        public void BorrarColita()
        {
            for (; first != null;)
            {
                first = first.next;
            }
        }

        public Object EnColita()
        {
            if (VacioColita())
            {
                throw new Exception("La colita esá vacía :)");
            }
            return (first.elemento);
        }

        public Object FinColita()
        {
            if (VacioColita())
            {
                throw new Exception("La colita esá vacía :)");
            }
            return (end.elemento);
        }

        public Object FinalColaLista()
        {
            if (!VacioColita())
            {
                return (end.elemento);
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
        }

        public int Tamanioo()
        {
            return tamanio;
        }

        public int ElementosLst()
        {
            int n;
            Nodo a = first;

            if (VacioColita())
            {
                n = 0;
            }
            else
            {
                n = 1;
                while (a != end)
                {
                    n++;
                    a = a.next;
                }
            }
            return n;
        }

        public bool Any(Point dato)
        {
            int i = 0, cont = 0;

            Nodo aux = first;
            bool flag;
            while (aux != null)
            {
                Point a = (Point)aux.elemento;
                flag = ((a.X == dato.X) && (a.Y == dato.Y));
                int z = (flag == true) ? cont++ : cont + 0;
                i++;
                aux = aux.next;
            }
            return (cont != 0) ? true : false;
        }

    }
}
