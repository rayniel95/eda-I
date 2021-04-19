using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
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
        List<NodoHeap<T1>> lista;
        public HeapMin()
        {
            this.lista = new List<NodoHeap<T1>>();
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

                this.HeapifyDownMin(nodoActual.HijoDer);
            }

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

                this.HeapifyUpMin(nodoActual.Padre);
            }
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

            this.HeapifyUpMin(temp);
        }
        public NodoHeap<T1> MuestraMin()
        {
            return this.lista[0];
        }
    }
    public class HeapMax<T3>
    {
        List<NodoHeap<T3>> lista;
        public HeapMax()
        {
            this.lista = new List<NodoHeap<T3>>();
        }
        public static HeapMax<T3> BuiltHeapMin(T3[] arrayValor, int[] arrayPrioridad)
        {
            HeapMax<T3> temp = new HeapMax<T3>();

            for (int indice = 0; indice < arrayValor.Length; indice++)
                temp.Inserta(arrayValor[indice], arrayPrioridad[indice]);

            return temp;
        }
        public void Inserta(T3 valor, int prioridad)
        {
            NodoHeap<T3> temp = new NodoHeap<T3>(valor, prioridad, null, null);

            if (this.lista.Count == 0) { this.lista.Add(temp); return; }

            temp.Padre = this.lista[(this.lista.Count - 1) / 2];

            if (this.lista.Count % 2 != 0)
                temp.Padre.HijoIzq = temp;
            else
                temp.Padre.HijoDer = temp;
            this.lista.Add(temp);

            this.HeapifyUpMax(temp);
        }
        void HeapifyUpMax(NodoHeap<T3> nodoActual)
        {
            if (nodoActual.Padre != null && nodoActual.Prioridad > nodoActual.Padre.Prioridad)
            {
                T3 tempValor = nodoActual.Valor;
                int tempPrioridad = nodoActual.Prioridad;

                nodoActual.Valor = nodoActual.Padre.Valor;
                nodoActual.Prioridad = nodoActual.Padre.Prioridad;

                nodoActual.Padre.Valor = tempValor;
                nodoActual.Padre.Prioridad = tempPrioridad;

                this.HeapifyUpMax(nodoActual.Padre);
            }
        }
        public void ShowTree()
        {
            this.ShowTree(this.lista[0], 0);
        }
        void ShowTree(NodoHeap<T3> nodoActual, int llamado)
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
        public int Size { get { return this.lista.Count; } }
        void HeapifyDownMax(NodoHeap<T3> nodoActual)
        {
            if (nodoActual.HijoIzq != null && nodoActual.HijoDer != null)
            {
                NodoHeap<T3> mayor = null;

                if (nodoActual.HijoIzq.Prioridad >= nodoActual.HijoDer.Prioridad)
                {
                    mayor = nodoActual.HijoIzq;
                }
                else if (nodoActual.HijoIzq.Prioridad < nodoActual.HijoDer.Prioridad)
                {
                    mayor = nodoActual.HijoDer;
                }
                if (mayor != null && nodoActual.Prioridad < mayor.Prioridad)
                {
                    T3 tempVal = nodoActual.Valor;
                    int tempPrior = nodoActual.Prioridad;

                    nodoActual.Valor = mayor.Valor;
                    nodoActual.Prioridad = mayor.Prioridad;

                    mayor.Prioridad = tempPrior;
                    mayor.Valor = tempVal;

                    this.HeapifyDownMax(mayor);
                }
            }
            else if (nodoActual.HijoIzq != null && nodoActual.Prioridad < nodoActual.HijoIzq.Prioridad)
            {
                T3 tempVal = nodoActual.Valor;
                int tempPrior = nodoActual.Prioridad;

                nodoActual.Valor = nodoActual.HijoIzq.Valor;
                nodoActual.Prioridad = nodoActual.HijoIzq.Prioridad;

                nodoActual.HijoIzq.Prioridad = tempPrior;
                nodoActual.HijoIzq.Valor = tempVal;

                this.HeapifyDownMax(nodoActual.HijoIzq);
            }
            else if (nodoActual.HijoDer != null && nodoActual.Prioridad < nodoActual.HijoDer.Prioridad)
            {
                T3 tempVal = nodoActual.Valor;
                int tempPrior = nodoActual.Prioridad;

                nodoActual.Valor = nodoActual.HijoDer.Valor;
                nodoActual.Prioridad = nodoActual.HijoDer.Prioridad;

                nodoActual.HijoDer.Prioridad = tempPrior;
                nodoActual.HijoDer.Valor = tempVal;

                this.HeapifyDownMax(nodoActual.HijoDer);
            }
        }
        public T3[] BuiltArray()
        {
            T3[] temp = new T3[this.lista.Count];

            for (int indice = 0; indice < temp.Length; indice++)
                temp[indice] = this.lista[indice].Valor;

            return temp;
        }
        public NodoHeap<T3> SuprimeMin()
        {
            NodoHeap<T3> temp = new NodoHeap<T3>(this.lista[0].Valor, this.lista[0].Prioridad, null, null);

            this.lista[0].Valor = this.lista[this.lista.Count - 1].Valor;
            this.lista[0].Prioridad = this.lista[this.lista.Count - 1].Prioridad;

            if ((this.lista.Count - 1) % 2 != 0)
                this.lista[this.lista.Count - 1].Padre.HijoIzq = null;
            else
                this.lista[this.lista.Count - 1].Padre.HijoDer = null;

            this.lista.RemoveAt(this.lista.Count - 1);

            this.HeapifyDownMax(this.lista[0]);

            return temp;
        }
    }
}
