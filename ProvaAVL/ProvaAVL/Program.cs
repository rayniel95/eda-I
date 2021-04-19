using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAVL
{
    class Program
    {
        public class NodoAVL<T6>
        {
            public NodoAVL(T6 valor, int altura, NodoAVL<T6> hijoIzq, NodoAVL<T6> hijoDer, NodoAVL<T6> padre = null)
            {
                HijoDer = hijoDer;
                HijoIzq = hijoIzq;
                Padre = padre;
                Valor = valor;
                Altura = altura;
            }
            public int Altura { get; set; }
            public NodoAVL<T6> HijoIzq { get; set; }
            public NodoAVL<T6> HijoDer { get; set; }
            public NodoAVL<T6> Padre { get; set; }
            public T6 Valor { get; set; }
            public int FactorBalance
            {
                get
                {
                    int alturaIzq = -1;
                    int alturaDer = -1;
                    if (this.HijoIzq != null) alturaIzq = this.HijoIzq.Altura;
                    if (this.HijoDer != null) alturaDer = this.HijoDer.Altura;

                    return alturaDer - alturaIzq;
                }
            }
        }
        public class AVL<T5> where T5 : IComparable<T5>
        {
            NodoAVL<T5> raiz;
            public AVL()
            {
                this.Count = 0;
            }
            public void Insertar(T5 valor)
            {
                this.Count++;
                if (this.raiz == null) { this.raiz = new NodoAVL<T5>(valor, 0, null, null); return; }

                Insertar(this.raiz, valor);
            }
            public int Count { get; protected set; }
            bool Insertar(NodoAVL<T5> nodoActual, T5 valor)
            {
                if (nodoActual.Valor.CompareTo(valor).Equals(0)) return true;
                else if (nodoActual.Valor.CompareTo(valor) > 0)
                {
                    if (nodoActual.HijoIzq != null)
                    {
                        if (Insertar(nodoActual.HijoIzq, valor)) return true;
                        nodoActual.Altura++;
                        // nodoActual.FactorBalance--;
                        // llamado a rotaciones
                        return InsertarRotaciones(nodoActual);
                    }
                    else
                    {
                        nodoActual.HijoIzq = new NodoAVL<T5>(valor, 0, null, null, nodoActual);
                        // nodoActual.FactorBalance--;
                        if (nodoActual.HijoIzq != null && nodoActual.HijoDer != null) return true;
                        nodoActual.Altura++;
                        return false;
                    }
                }
                else if (nodoActual.Valor.CompareTo(valor) < 0)
                {
                    if (nodoActual.HijoDer != null)
                    {
                        if (Insertar(nodoActual.HijoDer, valor)) return true;
                        nodoActual.Altura++;
                        // nodoActual.FactorBalance++;
                        // lamado a rotaciones
                        return InsertarRotaciones(nodoActual);
                    }
                    else
                    {
                        nodoActual.HijoDer = new NodoAVL<T5>(valor, 0, null, null, nodoActual);
                        // nodoActual.FactorBalance++;
                        if (nodoActual.HijoIzq != null && nodoActual.HijoDer != null) return true;
                        nodoActual.Altura++;
                        return false;
                    }
                }
                return false;
            }
            bool InsertarRotaciones(NodoAVL<T5> nodoActual)
            {
                if (nodoActual.FactorBalance == 0) return true;
                else if (nodoActual.FactorBalance == -1 || nodoActual.FactorBalance == 1) return false;

                else if (nodoActual.FactorBalance > 0 && nodoActual.HijoDer.FactorBalance < 0)
                {
                    nodoActual.HijoDer.HijoIzq.Altura++;
                    nodoActual.HijoDer.Altura--;

                    RotaDer(nodoActual.HijoDer.HijoIzq);
                    
                    // nodoActual.HijoDer.FactorBalance = Altura(nodoActual.HijoDer.HijoDer) - Altura(nodoActual.HijoDer.HijoIzq);
                    // nodoActual.HijoDer.HijoDer.FactorBalance = Altura(nodoActual.HijoDer.HijoDer.HijoDer) - Altura(nodoActual.HijoDer.HijoDer.HijoIzq);

                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoDer;
                    // nodoActual.HijoDer.Altura++;
                    nodoActual.Altura -= 2;

                    RotaIzq(nodoActual.HijoDer);

                    // nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    // nodoActual.Padre.FactorBalance = Altura(nodoActual.Padre.HijoDer) - Altura(nodoActual);

                    return true;
                }
                else if (nodoActual.FactorBalance < 0 && nodoActual.HijoIzq.FactorBalance > 0)
                {
                    nodoActual.HijoIzq.HijoDer.Altura++;
                    nodoActual.HijoIzq.Altura--;

                    RotaIzq(nodoActual.HijoIzq.HijoDer);

                    // nodoActual.HijoIzq.FactorBalance = Altura(nodoActual.HijoIzq.HijoDer) - Altura(nodoActual.HijoIzq.HijoIzq);
                    // nodoActual.HijoIzq.HijoIzq.FactorBalance = Altura(nodoActual.HijoIzq.HijoIzq.HijoDer) - Altura(nodoActual.HijoIzq.HijoIzq.HijoIzq);

                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoIzq;
                    // nodoActual.HijoIzq.Altura++;
                    nodoActual.Altura -= 2;

                    RotaDer(nodoActual.HijoIzq);

                    // nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    // nodoActual.Padre.FactorBalance = Altura(nodoActual) - Altura(nodoActual.Padre.HijoIzq);

                    return true;
                }
                else if (nodoActual.FactorBalance > 0 && nodoActual.HijoDer.FactorBalance > 0)
                {
                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoDer;
                    // nodoActual.HijoDer.Altura++;
                    nodoActual.Altura -= 2;

                    RotaIzq(nodoActual.HijoDer);

                    // nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    // nodoActual.Padre.FactorBalance = Altura(nodoActual.Padre.HijoDer) - Altura(nodoActual);

                    return true;
                }
                else if (nodoActual.FactorBalance < 0 && nodoActual.HijoIzq.FactorBalance < 0)
                {
                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoIzq;
                    // nodoActual.HijoIzq.Altura++;
                    nodoActual.Altura -= 2;

                    RotaDer(nodoActual.HijoIzq);

                    // nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    // nodoActual.Padre.FactorBalance = Altura(nodoActual) - Altura(nodoActual.Padre.HijoIzq);

                    return true;
                }
                return false;
            }
            NodoAVL<T5> BuscaMenorMayor(NodoAVL<T5> nodoActual)
            {
                if (nodoActual.HijoIzq == null) return nodoActual;
                return BuscaMenorMayor(nodoActual.HijoIzq);
            }
            void RotaIzq(NodoAVL<T5> nodoActual)
            {
                NodoAVL<T5> temp = nodoActual.Padre;
                nodoActual.Padre = temp.Padre;
                if (nodoActual.Padre != null)
                {
                    if (nodoActual.Padre.HijoDer == temp)
                        nodoActual.Padre.HijoDer = nodoActual;
                    else
                        nodoActual.Padre.HijoIzq = nodoActual;

                }
                temp.Padre = nodoActual;
                temp.HijoDer = nodoActual.HijoIzq;
                nodoActual.HijoIzq = temp;
                if (temp.HijoDer != null)
                    temp.HijoDer.Padre = temp;
            }
            void RotaDer(NodoAVL<T5> nodoActual)
            {
                NodoAVL<T5> temp = nodoActual.Padre;
                nodoActual.Padre = temp.Padre;
                if (nodoActual.Padre != null)
                {
                    if (nodoActual.Padre.HijoIzq == temp)
                        nodoActual.Padre.HijoIzq = nodoActual;
                    else
                        nodoActual.Padre.HijoDer = nodoActual;
                }
                temp.Padre = nodoActual;
                temp.HijoIzq = nodoActual.HijoDer;
                nodoActual.HijoDer = temp;
                if (temp.HijoIzq != null)
                    temp.HijoIzq.Padre = temp;
            }
            int Altura(NodoAVL<T5> nodoActual)
            {
                if (nodoActual == null) return -1;
                if (nodoActual.HijoIzq == null && nodoActual.HijoDer == null)
                    return 0;

                int altura = 0;

                if (nodoActual.FactorBalance > 0)
                    altura = Altura(nodoActual.HijoDer);
                else
                    altura = Altura(nodoActual.HijoIzq);
                return altura + 1;
            }
            public void ShowTree()
            {
                this.ShowTree(this.raiz, 0);
            }
            void ShowTree(NodoAVL<T5> nodoActual, int llamado)
            {
                for (int veces = 0; veces < llamado; veces++)
                    Console.Write("-");
                Console.Write("(" + nodoActual.Valor + ")" + " factor de balance: " + nodoActual.FactorBalance + " altura: " + nodoActual.Altura);
                Console.WriteLine();

                if (nodoActual.HijoIzq != null) this.ShowTree(nodoActual.HijoIzq, llamado + 1);
                if (nodoActual.HijoDer != null) this.ShowTree(nodoActual.HijoDer, llamado + 1);
            }

        }
        static void Main(string[] args)
        {
            #region Prueba1
            AVL<int> myAvl = new AVL<int>();

            myAvl.Insertar(4);
            myAvl.Insertar(5);
            myAvl.Insertar(2);
            //myAvl.ShowTree();
            myAvl.Insertar(2);
            myAvl.Insertar(7);
            //myAvl.ShowTree();

            myAvl.Insertar(8);
            //myAvl.ShowTree();

            myAvl.Insertar(3);

            myAvl.Insertar(1);
            myAvl.Insertar(0);
            myAvl.Insertar(-1);
            myAvl.Insertar(15);
            myAvl.Insertar(10);
            myAvl.Insertar(6);
            myAvl.Insertar(-20);
            myAvl.ShowTree();

            myAvl.Insertar(-10);
            myAvl.Insertar(20);
            myAvl.Insertar(17);
            myAvl.Insertar(30);



            //myAvl.ShowTree();
            Console.WriteLine();

            #endregion
        }
    }
}
