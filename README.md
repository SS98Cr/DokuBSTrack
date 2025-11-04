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

## Ejemplo de salida
- Inserciones con métricas
- Búsquedas distribuidas (izquierdo, derecho, inexistente)
- Actualizaciones y eliminaciones con impresión del árbol y mensajes de caso
- Recorridos y altura

## Autor
- Taller BST MVC C#
