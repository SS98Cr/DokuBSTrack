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
            ConstruirArbolInicial();
            RealizarBusquedas();
            RealizarActualizaciones();
            RealizarEliminaciones();
            MostrarRecorridosYAltura();
        }
        private void ConstruirArbolInicial()
        {
            var datosIniciales = ObtenerDatosIniciales();
            _view.Mensaje("Métricas de inserción:");

            int insercionesExitosas = 0;
            foreach (var (nombre, esCarpeta) in datosIniciales)
            {
                var (insertado, comparaciones, _) = _arbol.Insertar(nombre, esCarpeta, msg => _view.Mensaje(msg));
                if (insertado) insercionesExitosas++;
                _view.Mensaje($"Insertar '{nombre}': {(insertado ? "Éxito" : "Fallo")}, Comparaciones: {comparaciones}");
            }
            _view.Mensaje($"Total inserciones exitosas: {insercionesExitosas}\n");
            _view.MostrarArbol(_arbol.Raiz);
        }

        private (string nombre, bool esCarpeta)[] ObtenerDatosIniciales()
        {
            return new (string, bool)[] {
                ("M", true),
                ("D", true),
                ("T", true),
                ("B.txt", false),
                ("F", true),
                ("R", true),
                ("Y", true),
                ("E.txt", false),
                ("H", true),
                ("I.txt", false),
                ("A.txt", false),
                ("Q", true),
                ("W", true),
                ("Z.txt", false),
                ("C.txt", false),
                ("G.txt", false)
            };
        }

        private void RealizarBusquedas()
        {
            RealizarBusquedasPorTipo("IZQUIERDA", new string[] { "B.txt", "F" });
            RealizarBusquedasPorTipo("DERECHA", new string[] { "Y", "T" });
            RealizarBusquedasPorTipo("INEXISTENTE", new[] { "NON1", "NON2" });
        }

        private void RealizarBusquedasPorTipo(string tipo, string[] nombres)
        {

            foreach (var nombre in nombres)
            {
                var (encontrado, comparaciones) = _arbol.Buscar(nombre);
                _view.Mensaje($"\nBúsquedas {tipo}:");
                _view.MostrarComparaciones(nombre, comparaciones, encontrado);
            }
        }

        private void RealizarActualizaciones()
        {
            ActualizarNodo("E.txt", "E2.txt", false, "E.txt -> E2.txt");
            ActualizarNodo("H", "H2025", true, "H -> H2025");

            var raizActual = _arbol.Raiz?.Nombre ?? "M";
            ActualizarNodo(raizActual, "N", true, $"{raizActual} -> N");
        }

        private void ActualizarNodo(string nombreViejo, string nombreNuevo, bool esCarpeta, string descripcion)
        {
            if (_arbol.Actualizar(nombreViejo, nombreNuevo, esCarpeta, _view.Mensaje))
            {
                _view.Mensaje($"\nActualización exitosa: {descripcion}");
            }
            else
            {
                _view.Mensaje($"\nActualización fallida: {nombreViejo}");
            }
            _view.MostrarArbol(_arbol.Raiz);
        }

        private void RealizarEliminaciones()
        {
            EliminarNodo("A.txt");
            EliminarNodoConAlternativas("Q", "H2025");
            EliminarNodo("N");
        }

        private void EliminarNodo(string nombre)
        {
            if (_arbol.Eliminar(nombre, _view.Mensaje))
            {
                _view.Mensaje($"\nEliminación exitosa: {nombre}");
            }
            else
            {
                _view.Mensaje($"\nEliminación fallida: {nombre}");
            }
            _view.MostrarArbol(_arbol.Raiz);
        }

        private void EliminarNodoConAlternativas(string nombrePrimario, string nombreAlternativo)
        {
            if (_arbol.Eliminar(nombrePrimario, _view.Mensaje))
                _view.Mensaje($"Eliminado: {nombrePrimario}");
            else if (_arbol.Eliminar(nombreAlternativo, _view.Mensaje))
                _view.Mensaje($"Eliminado: {nombreAlternativo}");
            else
                _view.Mensaje($"No se pudo eliminar: {nombrePrimario} ni {nombreAlternativo}");

            _view.MostrarArbol(_arbol.Raiz);
        }
        
     private void MostrarRecorridosYAltura()
        {
            _view.MostrarRecorrido("Preorden", _arbol.Preorden());
            _view.MostrarRecorrido("Inorden", _arbol.Inorden());
            _view.MostrarRecorrido("Postorden", _arbol.Postorden());
            _view.MostrarRecorrido("PorNiveles", _arbol.PorNiveles());
            _view.MostrarAltura(_arbol.Altura());
        }
    }
}
