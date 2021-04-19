using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_DisjointSet
{
    class Program
    {
        public class NodoDisjointSet<T6>
        {
            LinkedList<NodoDisjointSet<T6>> hijos;
            public NodoDisjointSet(T6 valor, NodoDisjointSet<T6> padre=null)
            {
                Valor = valor;
                Padre = padre;
                this.hijos = new LinkedList<NodoDisjointSet<T6>>();
                this.CantidadHijos = 0; 
            }
            public int CantidadHijos { get; set; }
            public NodoDisjointSet<T6> Padre { get; set; }
            public T6 Valor { get; set; }
            public LinkedList<NodoDisjointSet<T6>> Hijos { get { return this.hijos; } }
        }
        public class DisjointSet<T7>
        {
            List<NodoDisjointSet<T7>> lista;
            public DisjointSet(int capacidad, T7 valor)
            {
                this.lista = new List<NodoDisjointSet<T7>>(capacidad);
                this.CantidadArboles = capacidad;
                
                for(int veces = 0; veces < capacidad; veces++)  
                    this.lista.Add(new NodoDisjointSet<T7>(valor));
            }
            public DisjointSet()
            {
                this.lista = new List<NodoDisjointSet<T7>>();
                this.CantidadArboles = 0;
            }
            public void Agragar(T7 valor)
            {
                this.lista.Add(new NodoDisjointSet<T7>(valor));
                this.CantidadArboles++;
            }
            public int CantidadArboles { get; set; }
            public NodoDisjointSet<T7> SetOf(int indice)
            {
                NodoDisjointSet<T7> representante = this.lista[indice];

                while (representante.Padre != null)
                    representante = representante.Padre;
                return representante;
            }
            public void Merge(int primerIndice, int segundoIndice)
            {
                NodoDisjointSet<T7> primerRepresentante = SetOf(primerIndice);
                NodoDisjointSet<T7> segundoRepresentante = SetOf(segundoIndice);

                if(primerRepresentante != segundoRepresentante && primerRepresentante.CantidadHijos > segundoRepresentante.CantidadHijos)
                {
                    primerRepresentante.Hijos.AddLast(segundoRepresentante);
                    segundoRepresentante.Padre = primerRepresentante;
                    primerRepresentante.CantidadHijos += segundoRepresentante.CantidadHijos + 1;
                }
                else if(primerRepresentante != segundoRepresentante)
                {
                    segundoRepresentante.Hijos.AddLast(primerRepresentante);
                    primerRepresentante.Padre = segundoRepresentante;
                    segundoRepresentante.CantidadHijos += primerRepresentante.CantidadHijos + 1;
                }
                CantidadArboles--;
            }
            public NodoDisjointSet<T7> this[int index]
            {
                get { return this.lista[index]; }
            }

        }
        static void Main(string[] args)
        {
  
        }
    }
}
