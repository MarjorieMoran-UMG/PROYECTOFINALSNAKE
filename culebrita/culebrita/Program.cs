using culebrita.Colas.ColaArrayLst;
using culebrita.Colas.ColaCircular;
using culebrita.Colas.ColaLinealA;
using culebrita.Colas.ColaLstEnlazada;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace culebrita
{
    class Program
    {
        internal enum Direction
        {
            Abajo, Izquierda, Derecha, Arriba
        }


        private static void DibujaPantalla(Size size)
        {
            Console.Title = " JUEGO DE LA CULEBRITA :) ";
            Console.WindowHeight = size.Height + 2;
            Console.WindowWidth = size.Width + 2;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Yellow;
            for (int row = 0; row < size.Height; row++)
            {
                for (int col = 0; col < size.Width; col++)
                {
                    Console.SetCursorPosition(col + 1, row + 1);
                    Console.Write(" ");
                }
            }
        }



        private static void MuestraPunteo(int punteo)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(1, 0);
            Console.Write($"Punteo: {punteo.ToString("00000000")}");
        }




        private static Direction ObtieneDireccion(Direction direccionAcutal)
        {
            if (!Console.KeyAvailable) return direccionAcutal;

            var tecla = Console.ReadKey(true).Key;
            switch (tecla)
            {
                case ConsoleKey.DownArrow:
                    if (direccionAcutal != Direction.Arriba)
                        direccionAcutal = Direction.Abajo;
                    break;
                case ConsoleKey.LeftArrow:
                    if (direccionAcutal != Direction.Derecha)
                        direccionAcutal = Direction.Izquierda;
                    break;
                case ConsoleKey.RightArrow:
                    if (direccionAcutal != Direction.Izquierda)
                        direccionAcutal = Direction.Derecha;
                    break;
                case ConsoleKey.UpArrow:
                    if (direccionAcutal != Direction.Abajo)
                        direccionAcutal = Direction.Arriba;
                    break;
            }
            return direccionAcutal;
        }



        private static Point ObtieneSiguienteDireccion(Direction direction, Point currentPosition)
        {
            Point siguienteDireccion = new Point(currentPosition.X, currentPosition.Y);
            switch (direction)
            {
                case Direction.Arriba:
                    siguienteDireccion.Y--;
                    break;
                case Direction.Izquierda:
                    siguienteDireccion.X--;
                    break;
                case Direction.Abajo:
                    siguienteDireccion.Y++;
                    break;
                case Direction.Derecha:
                    siguienteDireccion.X++;
                    break;
            }
            return siguienteDireccion;
        }



        private static bool MoverLaCulebrita(Queue<Point> culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = culebra.Last();

            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.Any(x => x.Equals(posiciónObjetivo))) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.Enqueue(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            // Quitar cola
            if (culebra.Count > longitudCulebra)
            {
                var removePoint = culebra.Dequeue();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }

        private static Point MostrarComida(Size screenSize, Queue<Point> culebra)
        {
            var lugarComida = Point.Empty;
            var cabezaCulebra = culebra.Last();
            var rnd = new Random();
            do
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.All(p => p.X != x || p.Y != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        static void Menu()
        {
            int Opc;
            Console.WriteLine("Juego de la Culebrita");

            Console.WriteLine("0 Lista Enlazada");
            Console.WriteLine("1 Lineal");
            Console.WriteLine("2 Lista");
            Console.WriteLine("3 Circular");
            Opc = int.Parse(Console.ReadLine());

            switch (Opc)
            {
                case 0: op1(); break;
                case 1: op2(); break;
                case 2: op3(); break;
                case 3: op4(); break;
                case 4: return;
            }
        }
        static void op1()
        {
            Console.BackgroundColor = ConsoleColor.White;
            opc11();
        }
        static void op2()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            opc22();
        }
        static void op3()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            opc33();
        }
        static void op4()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            opc44();
        }

        static void Main()
        {
            Menu();
        }

        static void opc11()
        {
            var punteo = 0;
            var velocidad = 100; 
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            ColaEnlazada culebrita = new ColaEnlazada();
            var longitudCulebra = 3; 
            var posiciónActual = new Point(0, 9); 
            culebrita.Insert(posiciónActual);
            var dirección = Direction.Arriba; 
            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverCulebritaLisE(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra += 3; 
                    punteo += 5; 
                    MuestraPunteo(punteo);
                    velocidad -= 10;
                }
                if (posiciónComida == Point.Empty) 
                {
                    posiciónComida = ComidaLisE(tamañoPantalla, culebrita);
                }
            }
            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("The End");
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        private static bool MoverCulebritaLisE(ColaEnlazada culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.FinColitaEnlazada();

            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.Any(posiciónObjetivo)) return false;//

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.Insert(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            if (culebra.ElementosdeColitaEnlazada() > longitudCulebra)//
            {
                var removePoint = (Point)culebra.BorrarElemento();//
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }

        private static Point ComidaLisE(Size screenSize, ColaEnlazada culebra)
        {
            var lugarComida = Point.Empty;

            var cabezaCulebra = (Point)culebra.FinColitaEnlazada();
            var coor = cabezaCulebra.X;
            var rnd = new Random();
            do
            {
                var xi = rnd.Next(0, screenSize.Width - 1);
                var yi = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(x => coor != xi || coor != yi)
                    && Math.Abs(xi - cabezaCulebra.X) + Math.Abs(yi - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(xi, yi);
                    Console.Beep(659, 125);
                }
            } while (lugarComida == Point.Empty);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");
            return lugarComida;
        }

        static void opc22()
        {
            var punteo = 0;
            var velocidad = 100; 
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaLineal();
            var longitudCulebra = 3; 
            var posiciónActual = new Point(0, 9);
            culebrita.AggLineal(posiciónActual);
            var dirección = Direction.Arriba; 

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverCulebritaLineal(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra++;
                    punteo += 5; 
                    MuestraPunteo(punteo);
                    velocidad -= 5;
                }

                if (posiciónComida == Point.Empty) 
                {
                    posiciónComida = ComidaLineal(tamañoPantalla, culebrita);
                }
            }
            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("The End");
            Thread.Sleep(2000);
            Console.ReadKey();

        }
        private static bool MoverCulebritaLineal(ColaLineal culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.FinalColaLin();
            int pausa = 0;
            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;
            //if (culebra.Any(posiciónObjetivo)) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.AggLineal(posiciónObjetivo);
            int pausa1 = 0;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            if (culebra.Tamaniooo() > longitudCulebra)
            {
                var removePoint = (Point)culebra.BorrarColitaLineal();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }
        private static Point ComidaLineal(Size screenSize, ColaLineal culebra)
        {
            var lugarComida = Point.Empty;
            var cabezaCulebra = (Point)culebra.FinalColaLin();
            var rnd = new Random();
            var Px = cabezaCulebra.X;
            var Py = cabezaCulebra.Y;
            do
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(p => Px != x || Py != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);
                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");
            return lugarComida;
        }


        static void opc33()
        {
            var punteo = 0;
            var velocidad = 100; 
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaLst();
            var longitudCulebra = 3; 
            var posiciónActual = new Point(0, 9); 
            culebrita.Insert(posiciónActual);
            var dirección = Direction.Arriba; 

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverCulebritaLista(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra++;
                    punteo += 5; 
                    MuestraPunteo(punteo);
                    velocidad -= 10;

                }

                if (posiciónComida == Point.Empty) 
                {
                    posiciónComida = ComidaLista(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("The End");
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        private static Point ComidaLista(Size screenSize, ColaLst culebra)
        {
            var lugarComida = Point.Empty;
            var cabezaCulebra = (Point)culebra.FinColita();
            var rnd = new Random();

            var Px = cabezaCulebra.X;
            var Py = cabezaCulebra.Y;
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().Any(p => Px != x || Py != y)
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);
                }

            } while (lugarComida == Point.Empty) ;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");

            return lugarComida;
        }

        private static bool MoverCulebritaLista(ColaLst culebra, Point posiciónObjetivo,
           int longitudCulebra, Size screenSize)
        {

            var lastPoint = (Point)culebra.FinalColaLista();

            int pausa = 0;
            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;
            //if (culebra.Any(posiciónObjetivo)) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.Insert(posiciónObjetivo);
            int pausa1 = 0;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            if (culebra.ElementosLst() > longitudCulebra)
            {
                var removePoint = (Point)culebra.BorrarElemento();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }


        static void opc44()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            var punteo = 0;
            var velocidad = 100; 
            var posiciónComida = Point.Empty;
            var tamañoPantalla = new Size(60, 20);
            var culebrita = new ColaCirqlar();
            var longitudCulebra = 5; 
            var posiciónActual = new Point(0, 9); 
            culebrita.agg(posiciónActual);
            var dirección = Direction.Arriba; 

            DibujaPantalla(tamañoPantalla);
            MuestraPunteo(punteo);

            while (MoverCulebritaCircular(culebrita, posiciónActual, longitudCulebra, tamañoPantalla))
            {
                Thread.Sleep(velocidad);
                dirección = ObtieneDireccion(dirección);
                posiciónActual = ObtieneSiguienteDireccion(dirección, posiciónActual);

                if (posiciónActual.Equals(posiciónComida))
                {
                    posiciónComida = Point.Empty;
                    longitudCulebra += 3; 
                    punteo += 5; 
                    MuestraPunteo(punteo);
                    velocidad -= 5;
                }

                if (posiciónComida == Point.Empty) 
                {
                    posiciónComida = ComidaCircular(tamañoPantalla, culebrita);
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(tamañoPantalla.Width / 2 - 4, tamañoPantalla.Height / 2);
            Console.Beep(659, 125);
            Console.Write("The End");
            Console.Beep(650, 2);
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        private static bool MoverCulebritaCircular(ColaCirqlar culebra, Point posiciónObjetivo,
            int longitudCulebra, Size screenSize)
        {
            var lastPoint = (Point)culebra.FinColitaCirq();

            if (lastPoint.Equals(posiciónObjetivo)) return true;

            if (culebra.ToString().Any(x => x.Equals(posiciónObjetivo))) return false;

            if (posiciónObjetivo.X < 0 || posiciónObjetivo.X >= screenSize.Width
                    || posiciónObjetivo.Y < 0 || posiciónObjetivo.Y >= screenSize.Height)
            {
                return false;
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(lastPoint.X + 1, lastPoint.Y + 1);
            Console.WriteLine(" ");

            culebra.agg(posiciónObjetivo);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posiciónObjetivo.X + 1, posiciónObjetivo.Y + 1);
            Console.Write(" ");

            if (culebra.tamanio() > longitudCulebra)
            {
                var removePoint = (Point)culebra.BorrarElement();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(removePoint.X + 1, removePoint.Y + 1);
                Console.Write(" ");
            }
            return true;
        }

        private static Point ComidaCircular(Size screenSize, ColaCirqlar culebra)
        {

            var lugarComida = Point.Empty;
            var cabezaCulebra = (Point)culebra.FinColitaCirq();//ca
            var coor = cabezaCulebra.X;//
            var rnd = new Random();
            do
            {
                var x = rnd.Next(0, screenSize.Width - 1);
                var y = rnd.Next(0, screenSize.Height - 1);
                if (culebra.ToString().All(p => coor != x || coor != y)//
                    && Math.Abs(x - cabezaCulebra.X) + Math.Abs(y - cabezaCulebra.Y) > 8)
                {
                    lugarComida = new Point(x, y);
                    Console.Beep(659, 125);

                }

            } while (lugarComida == Point.Empty);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(lugarComida.X + 1, lugarComida.Y + 1);
            Console.Write(" ");
            return lugarComida;
        }
    }
}
