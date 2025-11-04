using System;
using System.Collections.Generic;

namespace DocuTrackBST.Model
{
    /// <summary>
    /// Árbol Binario de Búsqueda con operaciones principales
    /// </summary>
    public class ArbolBinario
    {
    public Nodo? Raiz { get; private set; }

    /// <summary>
    /// Inserta un nodo en el BST respetando la propiedad y sin duplicados (case-insensitive)
    /// </summary>
        public (bool insertado, int comparaciones, string mensaje) Insertar(string nombre, bool esCarpeta, Action<string>? reportar = null)
        {
            int comparaciones = 0;
            if (string.IsNullOrWhiteSpace(nombre)) return (false, comparaciones, "Nombre vacío");
            nombre = nombre.Trim();
            if (Raiz == null)
            {
                Raiz = new Nodo(nombre, esCarpeta);
                return (true, 1, "Insertado como raíz");
            }
            return InsertarRec(Raiz, nombre, esCarpeta, ref comparaciones, reportar);
        }

        private (bool, int, string) InsertarRec(Nodo actual, string nombre, bool esCarpeta, ref int comparaciones, Action<string>? reportar)
        {
            comparaciones++;
            int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);
            if (cmp == 0)
                return (false, comparaciones, $"Duplicado rechazado: {nombre}");
            if (!actual.EsCarpeta)
            {
                string msg = $"No se puede agregar hijo a archivo: {actual.Nombre}";
                reportar?.Invoke(msg);
                return (false, comparaciones, msg);
            }
            if (cmp < 0)
            {
                if (actual.Izquierdo == null)
                {
                    actual.Izquierdo = new Nodo(nombre, esCarpeta);
                    return (true, comparaciones, $"Insertado a la izquierda de {actual.Nombre}");
                }
                return InsertarRec(actual.Izquierdo, nombre, esCarpeta, ref comparaciones, reportar);
            }
            else
            {
                if (actual.Derecho == null)
                {
                    actual.Derecho = new Nodo(nombre, esCarpeta);
                    return (true, comparaciones, $"Insertado a la derecha de {actual.Nombre}");
                }
                return InsertarRec(actual.Derecho, nombre, esCarpeta, ref comparaciones, reportar);
            }
        }

    /// <summary>
    /// Busca un nodo por nombre y retorna si existe y el número de comparaciones
    /// </summary>
    public (bool encontrado, int comparaciones) Buscar(string nombre)
        {
            int comparaciones = 0;
            Nodo? actual = Raiz;
            while (actual != null)
            {
                comparaciones++;
                int cmp = string.Compare(nombre, actual.Nombre, true);
                if (cmp == 0) return (true, comparaciones);
                actual = cmp < 0 ? actual.Izquierdo : actual.Derecho;
            }
            return (false, comparaciones);
        }

    /// <summary>
    /// Elimina un nodo por nombre, manejando los tres casos (hoja, un hijo, dos hijos)
    /// </summary>
        public bool Eliminar(string nombre, Action<string>? reportar = null)
        {
            bool eliminado = false;
            Raiz = EliminarRec(Raiz, nombre, ref eliminado, reportar);
            return eliminado;
        }

        private Nodo? EliminarRec(Nodo? nodo, string nombre, ref bool eliminado, Action<string>? reportar)
        {
            if (nodo == null) return null;
            int cmp = string.Compare(nombre, nodo.Nombre, StringComparison.OrdinalIgnoreCase);
            if (cmp < 0)
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, nombre, ref eliminado, reportar);
            else if (cmp > 0)
                nodo.Derecho = EliminarRec(nodo.Derecho, nombre, ref eliminado, reportar);
            else
            {
                eliminado = true;
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    reportar?.Invoke($"Caso hoja: {nodo.Nombre}");
                    return null;
                }
                if (nodo.Izquierdo == null)
                {
                    reportar?.Invoke($"Caso un hijo (reconexión): {nodo.Nombre}");
                    return nodo.Derecho;
                }
                if (nodo.Derecho == null)
                {
                    reportar?.Invoke($"Caso un hijo (reconexión): {nodo.Nombre}");
                    return nodo.Izquierdo;
                }
                Nodo sucesor = Minimo(nodo.Derecho);
                reportar?.Invoke($"Caso dos hijos (reemplazo por sucesor): {nodo.Nombre} -> {sucesor.Nombre}");
                nodo.Nombre = sucesor.Nombre;
                nodo.EsCarpeta = sucesor.EsCarpeta;
                nodo.Derecho = EliminarRec(nodo.Derecho, sucesor.Nombre, ref eliminado, reportar);
            }
            return nodo;
        }

    private Nodo Minimo(Nodo nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;
            return nodo;
        }

    /// <summary>
    /// Actualiza un nodo (Eliminar + Insertar)
    /// </summary>
        public bool Actualizar(string viejo, string nuevo, bool esCarpeta, Action<string>? reportar = null)
        {
            if (string.Compare(viejo, nuevo, StringComparison.OrdinalIgnoreCase) == 0) return false;
            if (!Eliminar(viejo, reportar)) return false;
            var resultado = Insertar(nuevo, esCarpeta, reportar);
            return resultado.insertado;
        }

    /// <summary>
    /// Retorna recorrido Preorden
    /// </summary>
    public List<string> Preorden()
        {
            var lista = new List<string>();
            PreordenRec(Raiz, lista);
            return lista;
        }
    private void PreordenRec(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            lista.Add(nodo.Nombre);
            PreordenRec(nodo.Izquierdo, lista);
            PreordenRec(nodo.Derecho, lista);
        }

    /// <summary>
    /// Retorna recorrido Inorden
    /// </summary>
    public List<string> Inorden()
        {
            var lista = new List<string>();
            InordenRec(Raiz, lista);
            return lista;
        }
    private void InordenRec(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            InordenRec(nodo.Izquierdo, lista);
            lista.Add(nodo.Nombre);
            InordenRec(nodo.Derecho, lista);
        }

    /// <summary>
    /// Retorna recorrido Postorden
    /// </summary>
    public List<string> Postorden()
        {
            var lista = new List<string>();
            PostordenRec(Raiz, lista);
            return lista;
        }
    private void PostordenRec(Nodo? nodo, List<string> lista)
        {
            if (nodo == null) return;
            PostordenRec(nodo.Izquierdo, lista);
            PostordenRec(nodo.Derecho, lista);
            lista.Add(nodo.Nombre);
        }

    /// <summary>
    /// Retorna recorrido por niveles (BFS)
    /// </summary>
    public List<string> PorNiveles()
        {
            var lista = new List<string>();
            if (Raiz == null) return lista;
            var queue = new Queue<Nodo>();
            queue.Enqueue(Raiz);
            while (queue.Count > 0)
            {
                var actual = queue.Dequeue();
                lista.Add(actual.Nombre);
                if (actual.Izquierdo != null) queue.Enqueue(actual.Izquierdo);
                if (actual.Derecho != null) queue.Enqueue(actual.Derecho);
            }
            return lista;
        }

    /// <summary>
    /// Retorna la altura del árbol
    /// </summary>
    public int Altura()
        {
            return AlturaRec(Raiz);
        }
    private int AlturaRec(Nodo? nodo)
        {
            if (nodo == null) return 0;
            return 1 + Math.Max(AlturaRec(nodo.Izquierdo), AlturaRec(nodo.Derecho));
        }
    }
}
