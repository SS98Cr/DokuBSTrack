# DocuTrackBST

Proyecto de consola en C# (.NET) que implementa un Árbol Binario de Búsqueda (BST) siguiendo arquitectura MVC estricta.

## Estructura
- **Model**: Lógica del BST y nodos. Los archivos son siempre hojas; las carpetas pueden tener hijos.
- **View**: Toda la salida por consola (mensajes, métricas, árbol ASCII, recorridos, altura).
- **Controller**: Orquesta los casos de uso, secuencia de inserciones, búsquedas, actualizaciones y eliminaciones.
- **Program.cs**: Solo instancia el controlador y ejecuta `Run()`.

## Reglas y funcionalidades
- Inserción de nodos (carpetas y archivos) respetando la invariante: los archivos nunca tienen hijos.
- Comparación de nombres case-insensitive (`StringComparison.OrdinalIgnoreCase`).
- Métricas de comparaciones en cada inserción y búsqueda.
- Actualizaciones y eliminaciones muestran el árbol y el caso ejecutado (hoja, un hijo, dos hijos con sucesor).
- Recorridos (preorden, inorden, postorden, por niveles) y altura final.
- Toda la salida va por ConsolaView; el Model no imprime nada.

## Ejecución
1. Compila el proyecto:
   ```powershell
   dotnet build .
   ```
2. Ejecuta el proyecto:
   ```powershell
   dotnet run .
   ```

## Proceso de construcción e impresión
Al ejecutar el proyecto, se realiza automáticamente:

1. **Construcción del árbol inicial**
   - Se insertan 14 nodos efectivos (carpetas y archivos) siguiendo la invariante: los archivos son siempre hojas.
   - Cada inserción muestra el número de comparaciones realizadas.
   - Se imprime el árbol ASCII tras la construcción.

2. **Búsquedas distribuidas**
   - Se realizan 6 búsquedas: 2 en el subárbol izquierdo, 2 en el derecho, 2 inexistentes.
   - Se imprime para cada búsqueda si fue hallada y el número de comparaciones.

3. **Actualizaciones**
   - Se actualizan nodos (hoja, nodo con un hijo, raíz) usando Eliminar+Insertar.
   - Tras cada actualización se imprime el árbol.

4. **Eliminaciones**
   - Se eliminan nodos (hoja, nodo con un hijo, raíz con dos hijos).
   - Se imprime el árbol y el mensaje del caso ejecutado (hoja, un hijo, dos hijos con sucesor).

5. **Recorridos y altura**
   - Se muestran los recorridos (preorden, inorden, postorden, por niveles) y la altura final del árbol.

## Ejemplo de salida
- Inserciones con métricas
- Búsquedas distribuidas (izquierdo, derecho, inexistente)
- Actualizaciones y eliminaciones con impresión del árbol y mensajes de caso
- Recorridos y altura

## Autor
- Taller BST MVC C#
