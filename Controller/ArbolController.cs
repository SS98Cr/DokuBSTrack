using DocuTrackBST.Model;
using DocuTrackBST.View;

namespace DocuTrackBST.Controller
{
    /// <summary>
    /// Controlador principal para orquestar casos de uso
    /// </summary>
    public class ArbolController
    {
        private readonly ArbolBinario _arbol;
        private readonly ConsolaView _view;

        public ArbolController()
        {
            _arbol = new ArbolBinario();
            _view = new ConsolaView();
        }

        public void Run()
        {
            // 1) Construcción inicial (≥14 inserciones efectivas, archivos son hojas)
            var inicial = new (string, bool)[] {
                ("M",true), ("D",true), ("T",true), ("B.txt",false), ("F",true), ("R",true), ("Y",true),
                ("E.txt",false), ("H",true), ("I.txt",false), ("A.txt",false), ("Q",true), ("W",true), ("Z.txt",false), ("C.txt",false), ("G.txt",false)
            };
            _view.Mensaje("Métricas de inserción:");
            int efectivas = 0;
            foreach (var (nombre, esCarpeta) in inicial)
            {
                var (insertado, comparaciones, _) = _arbol.Insertar(nombre, esCarpeta, msg => _view.Mensaje(msg));
                _view.Mensaje($"Insertar {nombre} — comparaciones: {comparaciones}");
                if (insertado) efectivas++;
            }
            _view.Mensaje($"Total inserciones efectivas: {efectivas}");
            _view.MostrarArbol(_arbol.Raiz);

            // 2) Búsquedas distribuidas
            var busquedasIzq = new[] { "B.txt", "F" };
            var busquedasDer = new[] { "T", "Y" };
            var busquedasNo = new[] { "NON1", "NON2" };
            foreach (var nombre in busquedasIzq)
            {
                var (encontrado, comp) = _arbol.Buscar(nombre);
                _view.Mensaje($"Búsqueda IZQUIERDA:");
                _view.MostrarComparaciones(nombre, comp, encontrado);
            }
            foreach (var nombre in busquedasDer)
            {
                var (encontrado, comp) = _arbol.Buscar(nombre);
                _view.Mensaje($"Búsqueda DERECHA:");
                _view.MostrarComparaciones(nombre, comp, encontrado);
            }
            foreach (var nombre in busquedasNo)
            {
                var (encontrado, comp) = _arbol.Buscar(nombre);
                _view.Mensaje($"Búsqueda INEXISTENTE:");
                _view.MostrarComparaciones(nombre, comp, encontrado);
            }

            // 3) Actualizaciones
            // Hoja: "E.txt" -> "E2.txt"
            if (_arbol.Actualizar("E.txt", "E2.txt", false, _view.Mensaje))
                _view.Mensaje("Actualizado: E.txt -> E2.txt");
            else
                _view.Mensaje("No se pudo actualizar: E.txt");
            _view.MostrarArbol(_arbol.Raiz);

            // Nodo con un hijo: "H" -> "H2025" (asegura que H tenga solo I.txt)
            if (_arbol.Actualizar("H", "H2025", true, _view.Mensaje))
                _view.Mensaje("Actualizado: H -> H2025");
            else
                _view.Mensaje("No se pudo actualizar: H");
            _view.MostrarArbol(_arbol.Raiz);

            // Raíz: "M" -> "N" (asegura que la raíz tenga dos hijos)
            var raizActual = _arbol.Raiz?.Nombre ?? "M";
            if (_arbol.Actualizar(raizActual, "N", true, _view.Mensaje))
                _view.Mensaje($"Actualizado: {raizActual} -> N");
            else
                _view.Mensaje($"No se pudo actualizar: {raizActual}");
            _view.MostrarArbol(_arbol.Raiz);

            // 4) Eliminaciones selectivas
            // Hoja: "A.txt"
            if (_arbol.Eliminar("A.txt", _view.Mensaje))
                _view.Mensaje("Eliminado: A.txt");
            else
                _view.Mensaje("No se pudo eliminar: A.txt");
            _view.MostrarArbol(_arbol.Raiz);

            // Un hijo: "Q" (o "H2025" si aplica)
            if (_arbol.Eliminar("Q", _view.Mensaje))
                _view.Mensaje("Eliminado: Q");
            else if (_arbol.Eliminar("H2025", _view.Mensaje))
                _view.Mensaje("Eliminado: H2025");
            else
                _view.Mensaje("No se pudo eliminar: Q ni H2025");
            _view.MostrarArbol(_arbol.Raiz);

            // Raíz con dos hijos: "N"
            if (_arbol.Eliminar("N", _view.Mensaje))
                _view.Mensaje("Eliminado: N");
            else
                _view.Mensaje("No se pudo eliminar: N");
            _view.MostrarArbol(_arbol.Raiz);

            // 5) Recorridos y altura final
            _view.MostrarRecorrido("Preorden", _arbol.Preorden());
            _view.MostrarRecorrido("Inorden", _arbol.Inorden());
            _view.MostrarRecorrido("Postorden", _arbol.Postorden());
            _view.MostrarRecorrido("PorNiveles", _arbol.PorNiveles());
            _view.MostrarAltura(_arbol.Altura());
        }
    }
}
