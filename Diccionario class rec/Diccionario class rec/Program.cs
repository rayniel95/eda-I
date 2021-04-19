using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_class_rec
{
    class Program
    {
        public class NodoAVL<T6>
        {
            public NodoAVL(T6 valor, int factorBalance, NodoAVL<T6> hijoIzq, NodoAVL<T6> hijoDer, NodoAVL<T6> padre = null)
            {
                HijoDer = hijoDer;
                HijoIzq = hijoIzq;
                Padre = padre;
                Valor = valor;
                FactorBalance = factorBalance;
                CantidadIzq = 0;
                CantidadDer = 0;
            }
            public NodoAVL<T6> HijoIzq { get; set; }
            public NodoAVL<T6> HijoDer { get; set; }
            public NodoAVL<T6> Padre { get; set; }
            public T6 Valor { get; set; }
            public int FactorBalance { get; set; }
            public int CantidadDer { get; set; }
            public int CantidadIzq { get; set; }
        }
        public class AVL<T5> where T5 : IComparable<T5>
        {
            NodoAVL<T5> raiz;
            public AVL()
            {
                this.Count = 0;
            }
            public bool Insertar(T5 valor)
            {
                
                if (this.raiz == null) { this.raiz = new NodoAVL<T5>(valor, 0, null, null); this.Count++; return true; }

                int valorRetorno = Insertar(this.raiz, valor);
                if (valorRetorno == 2) return false;
                return true;
            }
            public int Count { get; protected set; }
            public bool Miembro(T5 valor)
            {
                if (Miembro(valor, this.raiz) != null) return true;
                return false;   
            }
            NodoAVL<T5> Miembro(T5 valor, NodoAVL<T5> nodoActual)
            {
                if (nodoActual.Valor.Equals(valor)) return nodoActual;
                else if (nodoActual.Valor.CompareTo(valor) < 0 && nodoActual.HijoDer != null) return Miembro(valor, nodoActual.HijoDer);
                else if (nodoActual.Valor.CompareTo(valor) > 0 && nodoActual.HijoIzq != null) return Miembro(valor, nodoActual.HijoIzq);
                return null;
               
            }
            public int MayoresQ(T5 valor)
            {
                return MayoresQ(this.raiz, valor);
            }
            int MayoresQ(NodoAVL<T5> nodoActual, T5 valor)
            {
                if (nodoActual == null) return 0;
                if (nodoActual.Valor.Equals(valor)) return nodoActual.CantidadDer;
                else if (nodoActual.Valor.CompareTo(valor) > 0)
                    return 1 + nodoActual.CantidadDer + MayoresQ(nodoActual.HijoIzq, valor);
                else if (nodoActual.Valor.CompareTo(valor) < 0)
                    return MayoresQ(nodoActual.HijoDer, valor);
                return 0;
            }
            public int MenoresQ(T5 valor)
            {
                return MenoresQ(this.raiz, valor);
            }
            int MenoresQ(NodoAVL<T5> nodoActual, T5 valor)
            {
                if (nodoActual == null) return 0;
                if (nodoActual.Valor.Equals(valor)) return nodoActual.CantidadIzq;
                else if (nodoActual.Valor.CompareTo(valor) > 0)
                    return MenoresQ(nodoActual.HijoIzq, valor);
                else if (nodoActual.Valor.CompareTo(valor) < 0)
                    return 1 + nodoActual.CantidadIzq + MenoresQ(nodoActual.HijoDer, valor);
                return 0;
            }
            int Insertar(NodoAVL<T5> nodoActual, T5 valor)
            {
                if (nodoActual.Valor.CompareTo(valor).Equals(0)) return 2;
                else if (nodoActual.Valor.CompareTo(valor) > 0)
                {
                    if (nodoActual.HijoIzq != null)
                    {
                        int valorRetorno = Insertar(nodoActual.HijoIzq, valor);
                        if (valorRetorno == 2) return 2;
                        nodoActual.CantidadIzq++;
                        if (valorRetorno == 1) return 1;         
                        nodoActual.FactorBalance--;
                        // llamado a rotaciones
                        return InsertarRotaciones(nodoActual);
                    }
                    else
                    {
                        nodoActual.HijoIzq = new NodoAVL<T5>(valor, 0, null, null, nodoActual);
                        this.Count++;
                        nodoActual.CantidadIzq++;
                        nodoActual.FactorBalance--;
                        if (nodoActual.FactorBalance != 0) return 0;
                        return 1;
                    }
                }
                else if (nodoActual.Valor.CompareTo(valor) < 0)
                {
                    if (nodoActual.HijoDer != null)
                    {
                        int valorRetorno = Insertar(nodoActual.HijoDer, valor);
                        if (valorRetorno == 2) return 2;
                        nodoActual.CantidadDer++;
                        if (valorRetorno == 1) return 1;
                        nodoActual.FactorBalance++;
                        // lamado a rotaciones
                        return InsertarRotaciones(nodoActual);
                    }
                    else
                    {
                        nodoActual.HijoDer = new NodoAVL<T5>(valor, 0, null, null, nodoActual);
                        this.Count++;
                        nodoActual.CantidadDer++;
                        nodoActual.FactorBalance++;
                        if (nodoActual.FactorBalance != 0) return 0;
                        return 1;
                    }
                }
                return 0;
            }
            int InsertarRotaciones(NodoAVL<T5> nodoActual) // reduce las constantes
            {
                if (nodoActual.FactorBalance == 0) return 1;
                else if (nodoActual.FactorBalance == -1 || nodoActual.FactorBalance == 1) return 1;

                return RotacionesDesbalance(nodoActual);
            }
            int RotacionesDesbalance(NodoAVL<T5> nodoActual)
            {
                if (nodoActual.FactorBalance == 2 && nodoActual.HijoDer.FactorBalance == -1)
                {
                    RotaDer(nodoActual.HijoDer.HijoIzq);

                    nodoActual.HijoDer.HijoDer.FactorBalance = Altura(nodoActual.HijoDer.HijoDer.HijoDer) - Altura(nodoActual.HijoDer.HijoDer.HijoIzq);

                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoDer;
                    RotaIzq(nodoActual.HijoDer);

                    nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    nodoActual.Padre.FactorBalance = Altura(nodoActual.Padre.HijoDer) - Altura(nodoActual);

                    return 1;
                }
                else if (nodoActual.FactorBalance == -2 && nodoActual.HijoIzq.FactorBalance == 1)
                {
                    RotaIzq(nodoActual.HijoIzq.HijoDer);

                    nodoActual.HijoIzq.HijoIzq.FactorBalance = Altura(nodoActual.HijoIzq.HijoIzq.HijoDer) - Altura(nodoActual.HijoIzq.HijoIzq.HijoIzq);

                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoIzq;
                    RotaDer(nodoActual.HijoIzq);

                    nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    nodoActual.Padre.FactorBalance = Altura(nodoActual) - Altura(nodoActual.Padre.HijoIzq);

                    return 1;
                }
                else if (nodoActual.FactorBalance == 2 && nodoActual.HijoDer.FactorBalance == 1)
                {
                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoDer;
                    RotaIzq(nodoActual.HijoDer);

                    nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    nodoActual.Padre.FactorBalance = Altura(nodoActual.Padre.HijoDer) - Altura(nodoActual);

                    return 1;
                }
                else if (nodoActual.FactorBalance == -2 && nodoActual.HijoIzq.FactorBalance == -1)
                {
                    if (nodoActual == this.raiz) this.raiz = nodoActual.HijoIzq;
                    RotaDer(nodoActual.HijoIzq);

                    nodoActual.FactorBalance = Altura(nodoActual.HijoDer) - Altura(nodoActual.HijoIzq);
                    nodoActual.Padre.FactorBalance = Altura(nodoActual) - Altura(nodoActual.Padre.HijoIzq);

                    return 1;
                }
                return 0;
            }
            bool BuscaMenorMayor(NodoAVL<T5> nodoActual, out NodoAVL<T5> menorMayor)
            {
                menorMayor = nodoActual;
                if (nodoActual == null) return true;
                if (nodoActual.HijoIzq == null)
                {
                    if (nodoActual.Padre.HijoIzq == nodoActual)
                        nodoActual.Padre.HijoIzq = nodoActual.HijoDer;
                    else if (nodoActual.Padre.HijoDer == nodoActual)
                        nodoActual.Padre.HijoDer = nodoActual.HijoIzq;
                    if (nodoActual.HijoDer != null)
                        nodoActual.HijoDer.Padre = nodoActual.Padre;
                    return false;
                }
                if (BuscaMenorMayor(nodoActual.HijoIzq, out menorMayor)) return true;

                int anteriorFactorBalance = nodoActual.FactorBalance;
                nodoActual.FactorBalance++;

                return EliminarRotaciones(nodoActual, anteriorFactorBalance);
            }
            bool EliminarRotaciones(NodoAVL<T5> nodoActual, int anteriorFactorBalance)
            {
                if (anteriorFactorBalance == 0 && (nodoActual.FactorBalance == -1 || nodoActual.FactorBalance == 1))
                    return true;
                else if (nodoActual.FactorBalance == -2 && nodoActual.HijoIzq.FactorBalance == 0)
                {
                    if (this.raiz == nodoActual) this.raiz = nodoActual.HijoIzq;
                    RotaDer(nodoActual.HijoIzq);

                    nodoActual.FactorBalance++;
                    nodoActual.Padre.FactorBalance++;
                    return true;
                }
                else if (nodoActual.FactorBalance == 2 && nodoActual.HijoDer.FactorBalance == 0)
                {
                    if (this.raiz == nodoActual) this.raiz = nodoActual.HijoDer;
                    RotaIzq(nodoActual.HijoDer);

                    nodoActual.FactorBalance--;
                    nodoActual.Padre.FactorBalance--;
                    return true;
                }
                this.RotacionesDesbalance(nodoActual);
                return false;
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
                Console.Write("(" + nodoActual.Valor + ")" + " " + nodoActual.FactorBalance);
                Console.WriteLine();

                if (nodoActual.HijoIzq != null) this.ShowTree(nodoActual.HijoIzq, llamado + 1);
                if (nodoActual.HijoDer != null) this.ShowTree(nodoActual.HijoDer, llamado + 1);
            }
            public void Elimina(T5 valor)
            {
                Elimina(this.raiz, valor);
            }
            bool Elimina(NodoAVL<T5> nodoActual, T5 valor)
            {
                if (nodoActual.Valor.Equals(valor))
                {
                    if (nodoActual.HijoDer == null)
                    {
                        if (nodoActual.HijoIzq != null) nodoActual.HijoIzq.Padre = nodoActual.Padre;
                        if (nodoActual.Padre != null)
                        {
                            if (nodoActual.Padre.HijoIzq == nodoActual) nodoActual.Padre.HijoIzq = nodoActual.HijoDer;
                            else nodoActual.Padre.HijoDer = nodoActual.HijoDer;
                        }
                        if (nodoActual == this.raiz) this.raiz = nodoActual.HijoIzq;
                    }
                    else
                    {
                        NodoAVL<T5> menorMayor;
                        bool criterioParada = BuscaMenorMayor(nodoActual.HijoDer, out menorMayor);
                        nodoActual.Valor = menorMayor.Valor;
                        if (criterioParada) return true;
                        int anteriorFactorBalance = nodoActual.FactorBalance;
                        nodoActual.FactorBalance--;

                        return EliminarRotaciones(nodoActual, anteriorFactorBalance);
                    }
                }
                else if (nodoActual.HijoIzq != null && nodoActual.Valor.CompareTo(valor) > 0)
                {
                    if (Elimina(nodoActual.HijoIzq, valor)) return true;
                    int anteriorFactorBalance = nodoActual.FactorBalance;
                    nodoActual.FactorBalance++;
                    return EliminarRotaciones(nodoActual.HijoIzq, anteriorFactorBalance);
                }
                else if (nodoActual.HijoDer != null && nodoActual.Valor.CompareTo(valor) < 0)
                {
                    if (Elimina(nodoActual.HijoDer, valor)) return true;
                    int anteriorFactorBalance = nodoActual.FactorBalance;
                    nodoActual.FactorBalance--;
                    return EliminarRotaciones(nodoActual, anteriorFactorBalance);
                }
                return true;
            }
        }

        static void Main(string[] args)
        {
            AVL<int> myAvl = new AVL<int>();
            int casos = int.Parse(Console.ReadLine());
            string[,] matrix = new string[casos, 2];

            // IO
            for (int veces = 0; veces < casos; veces++)
            {
                string[] orden = Console.ReadLine().Split();
                if (orden[0].Equals("2"))
                {
                    matrix[veces, 0] = "2";
                    matrix[veces, 1] = " ";
                }
                else
                {
                    matrix[veces, 0] = orden[0];
                    matrix[veces, 1] = orden[1];
                }
            }

            for(int veces = 0; veces < casos; veces++)
            {
                if(matrix[veces, 0].Equals("1"))
                    Console.WriteLine(myAvl.Insertar(int.Parse(matrix[veces, 1])));
                else if(matrix[veces, 0].Equals("2"))
                    Console.WriteLine(myAvl.Count);
                else if(matrix[veces, 0].Equals("3"))
                    Console.WriteLine(myAvl.Miembro(int.Parse(matrix[veces, 1])));
                else if(matrix[veces, 0].Equals("4"))
                    Console.WriteLine(myAvl.MenoresQ(int.Parse(matrix[veces, 1])));
                else if(matrix[veces, 0].Equals("5"))
                    Console.WriteLine(myAvl.MayoresQ(int.Parse(matrix[veces, 1])));
            }


        }
    }
}
