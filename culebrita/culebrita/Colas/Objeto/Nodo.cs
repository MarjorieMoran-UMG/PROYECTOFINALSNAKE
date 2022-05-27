using System;
using System.Collections.Generic;
using System.Text;

namespace culebrita.Colas.Objeto
{
    class Nodo
    {
        public Object elemento;
        public Nodo next;
        public Nodo(Object dato)
        {
            elemento = dato;
            next = null;
        }
    }
}
