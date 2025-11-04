using System;
using System.Collections.Generic;
using DocuTrackBST.Model;
namespace DocuTrackBST.View
{
    /// <summary>
    /// Vista para impresión en consola y mensajes
    /// </summary>
    public class ConsolaView
    {
        public void Mensaje(string texto)
        {
            Console.WriteLine(texto);
        }

        public void MostrarComparaciones(string nombre, int comparaciones, bool encontrado)
        {
            Console.WriteLine($"Buscar '{nombre}': {(encontrado ? "Encontrado" : "No encontrado")}, Comparaciones: {comparaciones}");
        }

        public void MostrarRecorrido(string tipo, List<string> recorrido)
        {
            Console.WriteLine($"Recorrido {tipo}: {string.Join(", ", recorrido)}");
        }

        public void MostrarAltura(int altura)
        {
            Console.WriteLine($"Altura del árbol: {altura}");
        }

    public void MostrarArbol(Nodo? raiz)
        {
            Console.WriteLine("Árbol BST:");
            MostrarArbolRec(raiz, "", true);
        }

    private void MostrarArbolRec(Nodo? nodo, string prefijo, bool esUltimo)
        {
            if (nodo == null) return;
            Console.WriteLine($"{prefijo}{(esUltimo ? "└──" : "├──")} {nodo.Nombre} {(nodo.EsCarpeta ? "[Carpeta]" : "[Archivo]")}");
            var nuevoPrefijo = prefijo + (esUltimo ? "    " : "│   ");
            if (nodo.Izquierdo != null || nodo.Derecho != null)
            {
                if (nodo.Izquierdo != null)
                    MostrarArbolRec(nodo.Izquierdo, nuevoPrefijo, nodo.Derecho == null);
                if (nodo.Derecho != null)
                    MostrarArbolRec(nodo.Derecho, nuevoPrefijo, true);
            }
        }
    }
}
