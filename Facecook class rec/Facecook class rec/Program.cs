using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facecook_class_rec
{
    class Program
    {
        public class NodoDisjointSet<T6>
        {
            LinkedList<NodoDisjointSet<T6>> hijos;
            public NodoDisjointSet(T6 valor, NodoDisjointSet<T6> padre = null)
            {
                Valor = valor;
                Padre = padre;
                this.hijos = new LinkedList<NodoDisjointSet<T6>>();
                this.CantidadHijos = 0;
                SumaEdadesHijos = 0;
            }
            public int CantidadHijos { get; set; }
            public NodoDisjointSet<T6> Padre { get; set; }
            public T6 Valor { get; set; }
            public LinkedList<NodoDisjointSet<T6>> Hijos { get { return this.hijos; } }
            public int SumaEdadesHijos { get; set; }
        }
        public class DisjointSet
        {
            List<NodoDisjointSet<int>> lista;
            public DisjointSet(int capacidad, int valor)
            {
                this.lista = new List<NodoDisjointSet<int>>(capacidad);
                this.CantidadArboles = capacidad;

                for (int veces = 0; veces < capacidad; veces++)
                    this.lista.Add(new NodoDisjointSet<int>(valor));
            }
            public DisjointSet(string[] array)
            {
                this.lista = new List<NodoDisjointSet<int>>(array.Length);

                for (int veces = 0; veces < array.Length; veces++)
                    this.lista.Add(new NodoDisjointSet<int>(int.Parse(array[veces])));
            }
            public DisjointSet()
            {
                this.lista = new List<NodoDisjointSet<int>>();
                this.CantidadArboles = 0;
            }
            /// <summary>
            /// Permite que si la capacidad es mayor que el count de la lista el agragar sea O(1)
            /// </summary>
            /// <param name="capacidad"></param>
            public DisjointSet(int capacidad)
            {
                this.lista = new List<NodoDisjointSet<int>>(capacidad);
            }
            public void Agragar(int valor)
            {
                this.lista.Add(new NodoDisjointSet<int>(valor));
            }
            public int CantidadArboles { get; set; }
            public NodoDisjointSet<int> SetOf(int indice)
            {
                NodoDisjointSet<int> representante = this.lista[indice];

                while (representante.Padre != null)
                    representante = representante.Padre;
                return representante;
            }
            public void Merge(int primerIndice, int segundoIndice)
            {
                NodoDisjointSet<int> primerRepresentante = SetOf(primerIndice);
                NodoDisjointSet<int> segundoRepresentante = SetOf(segundoIndice);

                if (primerRepresentante != segundoRepresentante && primerRepresentante.CantidadHijos > segundoRepresentante.CantidadHijos)
                {
                    primerRepresentante.Hijos.AddLast(segundoRepresentante);
                    segundoRepresentante.Padre = primerRepresentante;
                    primerRepresentante.CantidadHijos += segundoRepresentante.CantidadHijos + 1;
                    primerRepresentante.SumaEdadesHijos += segundoRepresentante.SumaEdadesHijos + segundoRepresentante.Valor;
                }
                else if (primerRepresentante != segundoRepresentante)
                {
                    segundoRepresentante.Hijos.AddLast(primerRepresentante);
                    primerRepresentante.Padre = segundoRepresentante;
                    segundoRepresentante.CantidadHijos += primerRepresentante.CantidadHijos + 1;
                    segundoRepresentante.SumaEdadesHijos += primerRepresentante.SumaEdadesHijos + primerRepresentante.Valor;
                }
            }
            public NodoDisjointSet<int> this[int index]
            {
                get { return this.lista[index]; }
            }
            public double Promedio(int indice)
            {
                NodoDisjointSet<int> representante = SetOf(indice);
                double promedio = ((double)representante.SumaEdadesHijos + (double)representante.Valor) / ((double)representante.CantidadHijos + 1);
                return Math.Round(promedio, 2);
            }

        }
        static void Main(string[] args)
        {

            string[] primeraLinea = Console.ReadLine().Split();
            string[] edades = Console.ReadLine().Split();
            int acciones = int.Parse(primeraLinea[1]);
            DisjointSet myDisjointSet = new DisjointSet(edades);
            LinkedList<double> respuestaAcciones = new LinkedList<double>();

            for(int veces = 0; veces < acciones; veces++)
            {
                string[] accion = Console.ReadLine().Split();
                if (accion[0].Equals("2"))
                    respuestaAcciones.AddLast(myDisjointSet.Promedio(int.Parse(accion[1]) - 1));
                else if (accion[0].Equals("1"))
                    myDisjointSet.Merge(int.Parse(accion[1]) - 1, int.Parse(accion[2]) - 1);
            }

            foreach(var el in respuestaAcciones)
                Console.WriteLine("{0:f2}", el, 2);
          
            
            
        }
    }
}
