using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportePeso_class_rec
{
    class Program
    {
        public class NodoHeap<T2>
        {
            public NodoHeap(T2 valor, int prioridad, NodoHeap<T2> hijoIzq, NodoHeap<T2> hijoDer, NodoHeap<T2> padre = null)
            {
                this.Valor = valor;
                this.HijoIzq = hijoIzq;
                this.HijoDer = hijoDer;
                this.Padre = padre;
                this.Prioridad = prioridad;
            }
            public int Prioridad { get; set; }
            public T2 Valor { get; set; }
            public NodoHeap<T2> HijoIzq { get; set; }
            public NodoHeap<T2> HijoDer { get; set; }
            public NodoHeap<T2> Padre { get; set; }
        }
        public class HeapMin<T1>
        {
            Stack<NodoHeap<T1>> pila;
            List<NodoHeap<T1>> lista;
            public HeapMin()
            {
                this.lista = new List<NodoHeap<T1>>();
                this.pila = new Stack<NodoHeap<T1>>();
            }
            public static HeapMin<T1> BuiltHeapMin(T1[] arrayValor, int[] arrayPrioridad)
            {
                HeapMin<T1> temp = new HeapMin<T1>();

                for (int indice = 0; indice < arrayValor.Length; indice++)
                    temp.Inserta(arrayValor[indice], arrayPrioridad[indice]);

                return temp;
            }
            public T1[] BuiltArray()
            {
                T1[] temp = new T1[this.lista.Count];

                for (int indice = 0; indice < temp.Length; indice++)
                    temp[indice] = this.lista[indice].Valor;

                return temp;
            }
            void HeapifyDownMin(NodoHeap<T1> nodoActual)
            {
                if (nodoActual.HijoIzq != null && nodoActual.HijoDer != null)
                {
                    NodoHeap<T1> menor = null;

                    if (nodoActual.HijoIzq.Prioridad <= nodoActual.HijoDer.Prioridad)
                    {
                        menor = nodoActual.HijoIzq;
                    }
                    else if (nodoActual.HijoIzq.Prioridad > nodoActual.HijoDer.Prioridad)
                    {
                        menor = nodoActual.HijoDer;
                    }
                    if (menor != null && nodoActual.Prioridad > menor.Prioridad)
                    {
                        T1 tempVal = nodoActual.Valor;
                        int tempPrior = nodoActual.Prioridad;

                        nodoActual.Valor = menor.Valor;
                        nodoActual.Prioridad = menor.Prioridad;

                        menor.Prioridad = tempPrior;
                        menor.Valor = tempVal;

                        this.pila.Push(nodoActual);

                        this.HeapifyDownMin(menor);
                    }
                }
                else if (nodoActual.HijoIzq != null && nodoActual.Prioridad > nodoActual.HijoIzq.Prioridad)
                {
                    T1 tempVal = nodoActual.Valor;
                    int tempPrior = nodoActual.Prioridad;

                    nodoActual.Valor = nodoActual.HijoIzq.Valor;
                    nodoActual.Prioridad = nodoActual.HijoIzq.Prioridad;

                    nodoActual.HijoIzq.Prioridad = tempPrior;
                    nodoActual.HijoIzq.Valor = tempVal;

                    this.pila.Push(nodoActual);

                    this.HeapifyDownMin(nodoActual.HijoIzq);
                }
                else if (nodoActual.HijoDer != null && nodoActual.Prioridad > nodoActual.HijoDer.Prioridad)
                {
                    T1 tempVal = nodoActual.Valor;
                    int tempPrior = nodoActual.Prioridad;

                    nodoActual.Valor = nodoActual.HijoDer.Valor;
                    nodoActual.Prioridad = nodoActual.HijoDer.Prioridad;

                    nodoActual.HijoDer.Prioridad = tempPrior;
                    nodoActual.HijoDer.Valor = tempVal;

                    this.pila.Push(nodoActual);

                    this.HeapifyDownMin(nodoActual.HijoDer);
                }
                this.pila.Push(nodoActual);
            }
            void HeapifyUpMin(NodoHeap<T1> nodoActual)
            {
                if (nodoActual.Padre != null && nodoActual.Padre.Prioridad > nodoActual.Prioridad)
                {
                    T1 tempValor = nodoActual.Valor;
                    int tempPrioridad = nodoActual.Prioridad;

                    nodoActual.Valor = nodoActual.Padre.Valor;
                    nodoActual.Prioridad = nodoActual.Padre.Prioridad;

                    nodoActual.Padre.Valor = tempValor;
                    nodoActual.Padre.Prioridad = tempPrioridad;

                    this.pila.Push(nodoActual);

                    this.HeapifyUpMin(nodoActual.Padre);
                }
                this.pila.Push(nodoActual);
            }
            public int Size { get { return this.lista.Count; } }
            public NodoHeap<T1> SuprimeMin()
            {
                NodoHeap<T1> temp = new NodoHeap<T1>(this.lista[0].Valor, this.lista[0].Prioridad, null, null);

                this.lista[0].Valor = this.lista[this.lista.Count - 1].Valor;
                this.lista[0].Prioridad = this.lista[this.lista.Count - 1].Prioridad;

                if ((this.lista.Count - 1) % 2 != 0)
                    this.lista[this.lista.Count - 1].Padre.HijoIzq = null;
                else
                    this.lista[this.lista.Count - 1].Padre.HijoDer = null;

                this.lista.RemoveAt(this.lista.Count - 1);

                this.HeapifyDownMin(this.lista[0]);

                return temp;
            }
            public void ShowTree()
            {
                this.ShowTree(this.lista[0], 0);
            }
            void ShowTree(NodoHeap<T1> nodoActual, int llamado)
            {
                for (int veces = 0; veces < llamado; veces++)
                    Console.Write("-");
                Console.Write(nodoActual.Valor + " " + nodoActual.Prioridad);
                Console.WriteLine();

                if (nodoActual.HijoIzq != null) this.ShowTree(nodoActual.HijoIzq, llamado + 1);
                if (nodoActual.HijoDer != null) this.ShowTree(nodoActual.HijoDer, llamado + 1);
            }
            public void ShowList()
            {
                foreach (var element in this.lista)
                    Console.WriteLine("valor: " + element.Valor + "\tprioridad: " + element.Prioridad);
            }
            public void Inserta(T1 valor, int prioridad)
            {
                NodoHeap<T1> temp = new NodoHeap<T1>(valor, prioridad, null, null);

                if (this.lista.Count == 0) { this.lista.Add(temp); return; }

                temp.Padre = this.lista[(this.lista.Count - 1) / 2];

                if (this.lista.Count % 2 != 0)
                    temp.Padre.HijoIzq = temp;
                else
                    temp.Padre.HijoDer = temp;
                this.lista.Add(temp);

                // this.HeapifyUpMin(temp);
            }
            public NodoHeap<T1> MuestraMin()
            {
                return this.lista[0];
            }
            
            //public T1 PonYRetorna(int indice, T1 valor)
            //{
            //    this.lista[indice].Valor = valor;

            //    // para ver si es posible hacer heapifyup
                
            //}

        }
        static void Main(string[] args)
        {
            SortedDictionary<int, int> cosa = new SortedDictionary<int, int>();

            cosa.Add(1, 3);
            cosa.Add(2, 0);
            cosa.Add(0, 5);
            foreach(var el in cosa.Values)
                Console.WriteLine(el);
        }
    }
}
