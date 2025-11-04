namespace DocuTrackBST.Model
{
    /// <summary>
    /// Nodo del Árbol Binario de Búsqueda
    /// </summary>
    public class Nodo
    {
        public string Nombre { get; set; }
        public bool EsCarpeta { get; set; }
    public Nodo? Izquierdo { get; set; }
    public Nodo? Derecho { get; set; }

        public Nodo(string nombre, bool esCarpeta)
        {
            Nombre = nombre;
            EsCarpeta = esCarpeta;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
