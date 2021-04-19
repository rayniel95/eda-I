using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prova1
{
    class Program
    {
        static void Main(string[] args)
        {
        
        }
        public class Vertex
        {
            public Vertex(int numero)
            {
                this.Numero = numero;
                this.Visitado = false;
                this.EsAP = false;
                this.Adyacentes = new LinkedList<Vertex>();
                this.Hijos = new LinkedList<Vertex>();
                this.Padre = null;
            }
            public int Numero{get; set;}
            public int Low{get;set;}
            public int DiscoveryTime{get; set;}
            public LinkedList<Vertex> Adyacentes{get; set;}
            public bool EsAP{get; set;}
            public bool Visitado { get; set; }
            public LinkedList<Vertex> Hijos { get; set; }
            public Vertex Padre { get; set; }
            public int IndiceHeap { get; set; }
            public int VerticeCosto { get; set; }
        }
        public class MinHeap
        {
            List<Vertex> lista;
            public MinHeap(int capacity = 100000)
            {
                this.lista = new List<Vertex>(capacity);
            }
            public MinHeap(List<Vertex> lista)
            {
                this.lista = lista;
            }
            public Vertex Peek()
            {
                return this.lista[0];
            }
            public static void HeapifyDown(List<Vertex> vertices, int indice)
            {
                if (indice >= vertices.Count / 2) return;

                Vertex hijoIzquierdo = vertices[2 * indice + 1];
                Vertex hijoDerecho = hijoIzquierdo;

                if (2 * indice + 2 < vertices.Count)
                    hijoDerecho = vertices[2 * indice + 2];

                if(vertices[indice].VerticeCosto > hijoIzquierdo.VerticeCosto)
                {
                    if(hijoIzquierdo.VerticeCosto > hijoDerecho.VerticeCosto)
                    {
                        vertices[2 * indice + 2] = vertices[indice];
                        vertices[2 * indice + 2].IndiceHeap = 2 * indice + 2;

                        vertices[indice] = hijoDerecho;
                        hijoDerecho.IndiceHeap = indice;

                        HeapifyDown(vertices, 2 * indice + 2);
                    }
                    else
                    {
                        vertices[2 * indice + 1] = vertices[indice];
                        vertices[2 * indice + 1].IndiceHeap = 2 * indice + 1;

                        vertices[indice] = hijoIzquierdo;
                        hijoIzquierdo.IndiceHeap = indice;

                        HeapifyDown(vertices, indice);
                    }
                }
                else if(vertices[indice].VerticeCosto > hijoDerecho.VerticeCosto)
                {
                    vertices[2 * indice + 2] = vertices[indice];
                    vertices[2 * indice + 2].IndiceHeap = 2 * indice + 2;

                    vertices[indice] = hijoDerecho;
                    hijoDerecho.IndiceHeap = indice;

                    HeapifyDown(vertices, 2 * indice + 2);
                }
            }
            public static void HeapifyUp(List<Vertex> vertices, int indice)
            {
                if (indice == 0) return;

                int indicePadre;

                if (indice % 2 == 0) // eres hijo derecho
                {
                    indicePadre = indice / 2 - 1;
                }
                else indicePadre = indice / 2;

                Vertex padre = vertices[indicePadre];

                if(vertices[indice].VerticeCosto < vertices[indicePadre].VerticeCosto)
                {
                    vertices[indicePadre] = vertices[indice];
                    vertices[indicePadre].IndiceHeap = indicePadre;

                    vertices[indice] = padre;
                    vertices[indice].IndiceHeap = indice;

                    HeapifyUp(vertices, indicePadre);
                }


            }
        }
        public class Grafo
        {
            public Grafo(int numeroVertices)
            {
                this.Vertices = new Vertex[numeroVertices];
                this.NumeroVertices = numeroVertices;
                for(int indice = 0; indice < this.Vertices.Length; indice++)
                {
                    this.Vertices[indice] = new Vertex(indice);
                }
            }
            public Vertex[] Vertices { get; set; }
            public int NumeroVertices { get; set; }
        }
        public static void DFSAP(Grafo grafo)
        {
            int time = 0;
            LinkedList<Vertex> raices = new LinkedList<Vertex>();

            foreach(var vertice in grafo.Vertices)
            {
                if(!vertice.Visitado)
                {
                    raices.AddLast(vertice);
                    DFSAP(vertice, ref time);
                }
            }

            foreach(var raiz in raices)
            {
                if (raiz.Hijos.Count < 2)
                    raiz.EsAP = false;
                else raiz.EsAP = true;
            }
        }
        public static void DFSAP(Vertex verticeActual, ref int time)
        {
            verticeActual.Visitado = true;
            time++;
            verticeActual.DiscoveryTime = time;
            verticeActual.Low = verticeActual.DiscoveryTime;

            foreach(var vertice in verticeActual.Adyacentes)
            {
                if (!vertice.Visitado)
                {
                    vertice.Padre = verticeActual;
                    verticeActual.Hijos.AddLast(vertice);

                    DFSAP(vertice, ref time);

                    verticeActual.Low = Math.Min(vertice.Low, verticeActual.Low);

                    if (vertice.Low >= verticeActual.DiscoveryTime)
                        verticeActual.EsAP = true;

                }
                else if (vertice.Visitado && vertice != verticeActual.Padre)
                    verticeActual.Low = Math.Min(verticeActual.Low, vertice.DiscoveryTime);
            }
            if(verticeActual.Padre != null && verticeActual.Low == verticeActual.DiscoveryTime)
            {
                // eres puente tu padre y tu
            }
        }
    }
}
