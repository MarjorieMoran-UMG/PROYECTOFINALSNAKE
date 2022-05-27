using culebrita.Colas.Objeto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace culebrita.Colas.ColaLstEnlazada
{
    class ColaEnlazada : ColaArrayLst.ColaLst
    {
        public void FinColita(Object elemento)
        {
            Insert(elemento);
        }

        public void PrincipioColita(Object elemento)
        {
            Nodo nnuevo;
            nnuevo = new Nodo(elemento);
            if (VacioColita())
            {
                end = nnuevo;
            }
            nnuevo.next = first;
            first = nnuevo;
        }

        public Object BorrarInicioC()
        {
            return BorrarElemento();
        }


        public Object BorrarFinC()
        {
            Object aux;
            if (!VacioColita())
            {
                if (first == end)
                {
                    aux = BorrarElemento();
                }
                else
                {
                    Nodo nuevo = first;
                    while (nuevo.next != end)
                    {
                        nuevo = nuevo.next;
                    }

                    nuevo.next = nuevo;
                    aux = end.elemento;
                    end = nuevo;
                }
            }
            else
            {
                throw new Exception("La colita esá vacía :)");
            }
            return aux;
        }

        public Object PrincipioColitaEnlazada()
        {
            return EnColita();
        }

        public Object FinColitaEnlazada()
        {
            if (VacioColita())
            {
                throw new Exception("La colita esá vacía :)");
            }
            return (end.elemento);
        }

        public bool VacioColitaEnlazada()
        {
            return VacioColita();
        }

        public void BorrarColitaEnlazada()
        {
            BorrarColita();
        }

        public int ElementosdeColitaEnlazada()
        {
            int n;
            Nodo a = first;

            if (VacioColitaEnlazada())
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