using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace PruebaHeap
{
    class Program
    {
    
        static void Main(string[] args)
        {
            int[] valores = { 5, 6, 8, 7, 8, 4, 4, 1 };
            int[] prioridad = { 5, 6, 8, 7, 8, 4, 4, 1 };

            HeapMin<int> myHeap = HeapMin<int>.BuiltHeapMin(valores, prioridad);
            myHeap.ShowTree();
            myHeap.ShowList();
            Console.WriteLine();

            Console.WriteLine(myHeap.SuprimeMin().Valor);

            myHeap.ShowList();
            myHeap.ShowTree();
        }
    }
}
