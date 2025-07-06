using System;
using System.Collections.Generic;
using System.Linq;


class Estudiante
{
    public string Nombre = string.Empty;
    //ARRAY UNIDIMENSIONAL 1: VECTOR DE 3 ELEMENTOS
    public double[] Notas = new double[3];
    public double Promedio;
    public string Condicion = string.Empty;
    public char Clasificacion;
}

class Program
{
    //ARRAY UNIDIMENSIONAL 2
    static char[] letrasClasificacion = new char[] { 'A', 'B', 'C', 'D' };
    //ARRAY UNIDIMENSIONAL 3
    static string[] mensajesSistema = new string[] { "BIENVENIDO", "ERROR", "GUARDADO", "ELIMINADO" };

    static void Main()
    {
        //LISTA GENÉRICA DE ESTUDIANTES
        List<Estudiante> estudiantes = new List<Estudiante>();
        int opcion;

        //ARRAY UNIDIMENSIONAL 3
        Console.WriteLine(mensajesSistema[0]);

        //BUCLE 1: DO - WHILE
        do
        {
            Console.WriteLine("----- MENÚ -----");
            Console.WriteLine("1. Agregar Estudiante");
            Console.WriteLine("2. Buscar Estudiante");
            Console.WriteLine("3. Ver Todos los Estudiantes");
            Console.WriteLine("4. Editar Nota de Estudiante");
            Console.WriteLine("5. Eliminar Estudiante");
            Console.WriteLine("6. Ver ranking de estudiantes");
            Console.WriteLine("7. Filtrar por condición (APROBADO/DESAPROBADO)");
            Console.WriteLine("8. Salir");
            Console.Write("Elija una opción: ");

            //VALIDACIÓN 1
            bool entradaValida = int.TryParse(Console.ReadLine(), out opcion);
            //CONDICIONAL SIMPLE 1: Validación de entrada
            if (!entradaValida)
            {
                //ARRAY UNIDIMENSIONAL 3
                Console.WriteLine(mensajesSistema[1] + ": Por favor ingrese un número del 1 al 8.");
                continue;
            }//FINALIZA VALIDACIÓN 1

            //CONDICIONAL MÚLTIPLE 1: SWITCH-CASE
            switch (opcion)
            {
                case 1:
                    // FUNCIÓN 1
                    AgregarEstudiante(estudiantes);
                    break;

                case 2:
                    // FUNCIÓN 2
                    BuscarEstudiante(estudiantes);
                    break;

                case 3:
                    // FUNCIÓN 3
                    VerTodosEstudiantes(estudiantes);
                    break;

                case 4:
                    // FUNCIÓN 4
                    EditarNotaEstudiante(estudiantes);
                    break;

                case 5:
                    // FUNCIÓN 5
                    EliminarEstudiante(estudiantes);
                    break;

                case 6:
                    // FUNCIÓN 6
                    VerRankingEstudiantes(estudiantes);
                    break;

                case 7:
                    // FUNCIÓN 7
                    FiltrarPorCondicion(estudiantes);
                    break;

                case 8:
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    break;
            }

        } while (opcion != 8);
    }

    // FUNCIÓN 1
    static void AgregarEstudiante(List<Estudiante> estudiantes)
    {
        Estudiante est = new Estudiante();
        bool nombreValido;

        //BUCLE 2: DO - WHILE
        do
        {
            Console.Write("Nombre del estudiante (solo letras): ");
            est.Nombre = Console.ReadLine() ?? string.Empty;

            //VALIDACIÓN 2
            nombreValido = !string.IsNullOrWhiteSpace(est.Nombre) &&
                           est.Nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));

            //CONDICIONAL SIMPLE 2
            if (!nombreValido) Console.WriteLine(mensajesSistema[1] + ": solo letras."); //ARRAY UNID 3

        } while (!nombreValido);

        //BUCLE 3: FOR
        for (int j = 0; j < 3; j++)
        {
            double nota;
            bool notaValida;
            //BUCLE 4: DO - WHILE
            do
            {
                Console.Write($"Nota {j + 1} (0-20): ");

                //VALIDACIÓN 3
                notaValida = double.TryParse(Console.ReadLine(), out nota) && nota >= 0 && nota <= 20;

                //CONDICIONAL SIMPLE 3
                if (!notaValida) Console.WriteLine(mensajesSistema[1] + ": Nota inválida."); //ARRAY UNIDIMENSIONAL 3

            } while (!notaValida);
            est.Notas[j] = nota;
        }

        //FUNCIÓN 8
        CalcularEstado(est);
        estudiantes.Add(est);
        Console.WriteLine(mensajesSistema[2] + ": Estudiante agregado correctamente."); // ARRAY UNIDIMENSIONAL 3
    }

    // FUNCIÓN 2
    static void BuscarEstudiante(List<Estudiante> estudiantes)
    {
        //VALIDACIÓN 4
        //CONDICIONAL DOBLE 1
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes registrados");
            return;
        }//FINALIZA VALIDACIÓN 4
        else
        {
            string busqueda;
            //BUCLE 5: DO - WHILE
            do
            {
                Console.Write("Ingrese el nombre a buscar (o 'salir' para volver al menú): ");
                busqueda = Console.ReadLine() ?? string.Empty;
                bool encontrado = false;

                //CONDICIONAL ANIDADA 1
                if (!busqueda.Equals("salir", StringComparison.OrdinalIgnoreCase))
                {
                    //BUCLE 6: FOREACH
                    foreach (var e in estudiantes)
                    {
                        //CONDICIONAL ANIDADA 2
                        if (e.Nombre.Equals(busqueda, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Nombre: {e.Nombre}, Promedio: {e.Promedio:F2}, " +
                                        $"Condición: {e.Condicion}, Clasificación: {e.Clasificacion}");
                            encontrado = true;
                            break;
                        }
                    }
                    //VALIDACIÓN 5
                    //CONDICIONAL ANIDADA 3
                    if (!encontrado)
                        Console.WriteLine("Estudiante no encontrado.");
                    //FINALIZA DE LA VALIDACIÓN 5
                }
            } while (!busqueda.Equals("salir", StringComparison.OrdinalIgnoreCase));
        }
    }

    // FUNCIÓN 3
    static void VerTodosEstudiantes(List<Estudiante> estudiantes)
    {
        //VALIDACIÓN 6
        //CONDICIONAL DOBLE 2
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }//FINALIZA DE LA VALIDACIÓN 6
        else
        {
            Console.WriteLine("--- REPORTE DE ESTUDIANTES ---");
            Console.WriteLine("Nombre\t\tPromedio\tCondición\tClasificación");
            Console.WriteLine("-------------------------------------------------------------");

            int aprobados = 0;
            int desaprobados = 0;

            //BUCLE 7: FOREACH
            foreach (var e in estudiantes)
            {
                Console.WriteLine($"{e.Nombre}\t\t{e.Promedio:F2}\t\t{e.Condicion}\t\t{e.Clasificacion}");

                //VALIDACIÓN 7
                //CONDICIONAL ANIDADA 4 (DOBLE)
                if (e.Condicion.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                    aprobados++;
                else
                    desaprobados++;
            }

            int total = estudiantes.Count;
            double porcentajeAprobados = (double)aprobados / total * 100;
            double porcentajeDesaprobados = (double)desaprobados / total * 100;
            double promedioGeneral = CalcularPromedioGeneral(estudiantes);

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("--- ESTADÍSTICAS DEL GRUPO ---");
            Console.WriteLine($"- Total de estudiantes: {total}");
            Console.WriteLine($"- Aprobados: {aprobados} ({porcentajeAprobados:F2}%)");
            Console.WriteLine($"- Desaprobados: {desaprobados} ({porcentajeDesaprobados:F2}%)");
            Console.WriteLine($"- Promedio general del grupo: {promedioGeneral:F2}");
        }
    }

    // FUNCIÓN 4
    static void EditarNotaEstudiante(List<Estudiante> estudiantes)
    {
        //VALIDACIÓN 8
        //CONDICIONAL DOBLE 3
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes para editar.");
            return;
        }//FINALIZA VALIDACIÓN 8
        else
        {
            Console.Write("Ingrese el nombre del estudiante a editar: ");
            string nombreEditar = Console.ReadLine() ?? string.Empty;
            bool editado = false;

            //BUCLE 8: FOREACH
            foreach (var e in estudiantes)
            {
                //CONDICIONAL ANIDADA 5
                if (e.Nombre.Equals(nombreEditar, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Ingrese las nuevas notas:");

                    //BUCLE 9: FOR
                    for (int j = 0; j < 3; j++)
                    {
                        double nuevaNota;
                        bool notaValida;
                        //BUCLE 10: DO - WHILE
                        do
                        {
                            Console.Write($"Nota {j + 1} (entre 0 y 20): ");
                            notaValida = double.TryParse(Console.ReadLine(), out nuevaNota);

                            //VALIDACIÓN 9
                            //CONDICIONAL ANIDADA 6
                            if (!notaValida || nuevaNota < 0 || nuevaNota > 20)
                            {
                                Console.WriteLine(mensajesSistema[1] + ": Ingrese una nota válida entre 0 y 20.");
                                notaValida = false;
                            }
                        } while (!notaValida);

                        e.Notas[j] = nuevaNota;
                    }

                    //FUNCION 8
                    CalcularEstado(e);
                    Console.WriteLine(mensajesSistema[2] + ": Estudiante actualizado correctamente."); //ARRAY UNID 3
                    editado = true;
                    break;
                }
            }
            //VALIDACIÓN 10
            //CONDICIONAL ANIDADA 7
            if (!editado)
                Console.WriteLine("Estudiante no encontrado.");
        }
    }

    // FUNCIÓN 5
    static void EliminarEstudiante(List<Estudiante> estudiantes)
    {
        //VALIDACIÓN 11
        //CONDICIONAL DOBLE 4
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes para eliminar");
            return;
        }//FINALIZA VALIDACIÓN 11
        else
        {
            Console.Write("Ingrese el nombre del estudiante a eliminar: ");
            string nombreEliminar = Console.ReadLine() ?? string.Empty;
            bool eliminado = false;

            //BUCLE 11: FOR
            for (int i = 0; i < estudiantes.Count; i++)
            {
                //CONDICIONAL ANIDADA 8
                if (estudiantes[i].Nombre.Equals(nombreEliminar, StringComparison.OrdinalIgnoreCase))
                {
                    estudiantes.RemoveAt(i);
                    Console.WriteLine(mensajesSistema[3] + ": Estudiante eliminado correctamente.");
                    eliminado = true;
                    break;
                }
            }
            //VALIDACIÓN 12
            //CONDICIONAL ANIDADA 9
            if (!eliminado)
                Console.WriteLine("Estudiante no encontrado");
        }
    }

    // FUNCIÓN 6
    static void VerRankingEstudiantes(List<Estudiante> estudiantes)
    {
        //VALIDACIÓN 13
        //CONDICIONAL DOBLE 5
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes para mostrar");
            return;
        }//FINALIZA VALIDACIÓN 13
        else
        {
            OrdenarPorPromedio(estudiantes);
            Console.WriteLine("--- Ranking de Estudiantes ---");
            Console.WriteLine("Posición\tNombre\t\tPromedio\tCondición\tClasificación");
            Console.WriteLine("---------------------------------------------------------------------");

            //BUCLE 12: FOR
            for (int i = 0; i < estudiantes.Count; i++)
            {
                var e = estudiantes[i];
                Console.WriteLine($"{i + 1}\t\t{e.Nombre}\t\t{e.Promedio:F2}\t\t{e.Condicion}\t\t{e.Clasificacion}");
            }
        }
    }

    // FUNCIÓN 7
    static void FiltrarPorCondicion(List<Estudiante> estudiantes)
    {
        //CONDICIONAL DOBLE 6
        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }
        else
        {
            Console.WriteLine("1. Ver solo aprobados");
            Console.WriteLine("2. Ver solo desaprobados");
            Console.Write("Seleccione una opción: ");
            string subopcion = Console.ReadLine() ?? string.Empty;

            //VALIDACIÓN 14
            //CONDICIONAL COMPUESTA MÚLTIPE 1 (ANIDADA)
            if (subopcion == "1")
            {
                MostrarEstudiantesPorCondicion(estudiantes, "APROBADO");
            }
            else if (subopcion == "2")
            {
                MostrarEstudiantesPorCondicion(estudiantes, "DESAPROBADO");
            }
            else
            {
                Console.WriteLine("Opción inválida.");
            }//FINALIZA VALIDACIÓN 14
        }
    }

    // FUNCIÓN 8
    static void CalcularEstado(Estudiante e)
    {
        //FUNCIÓN 12
        e.Promedio = CalcularPromedio(e.Notas);
        e.Condicion = e.Promedio >= 10.5 ? "APROBADO" : "DESAPROBADO";

        // CONDICIONAL MÚLTIPLE 2: SWITCH-CASE
        switch (e.Promedio)
        {
            case >= 18:
                e.Clasificacion = letrasClasificacion[0]; //ARRAY UNID 2
                break;
            case >= 14:
                e.Clasificacion = letrasClasificacion[1]; //ARRAY UNID 2
                break;
            case >= 10:
                e.Clasificacion = letrasClasificacion[2]; //ARRAY UNID 2
                break;
            default:
                e.Clasificacion = letrasClasificacion[3]; //ARRAY UNID 2
                break;
        }
    }

    // FUNCIÓN 9
    static double CalcularPromedioGeneral(List<Estudiante> lista)
    {
        //VALIDACIÓN 15
        //CONDICIONAL SIMPLE 4
        if (lista.Count == 0)
            return 0;

        double sumaPromedios = 0;

        //BUCLE 13: FOREACH
        foreach (var estudiante in lista)
        {
            sumaPromedios += estudiante.Promedio;
        }
        return sumaPromedios / lista.Count;
    }

    // FUNCIÓN 10
    static void OrdenarPorPromedio(List<Estudiante> lista)
    {
        int n = lista.Count;

        //ORDENAMIENTO BURBUJA
        //BUCLE EXTERNO
        for (int i = 0; i < n - 1; i++)
        {
            //BUCLE INTERNO
            for (int j = 0; j < n - i - 1; j++)
            {
                //CONDICIONAL SIMPLE 5
                if (lista[j].Promedio < lista[j + 1].Promedio)
                {
                    var temp = lista[j];
                    lista[j] = lista[j + 1];
                    lista[j + 1] = temp;
                }
            }
        }
    }

    // FUNCIÓN 11
    static void MostrarEstudiantesPorCondicion(List<Estudiante> lista, string condicion)
    {
        //LISTA DE FILTRACIÓN
        var filtrados = lista.FindAll(e => e.Condicion.Equals(condicion, StringComparison.OrdinalIgnoreCase));

        //VALIDACIÓN 16
        //CONDICIONAL DOBLE 7
        if (filtrados.Count == 0)
        {
            Console.WriteLine($"No hay estudiantes con condición: {condicion}");
        }
        else
        {
            Console.WriteLine($"--- Estudiantes {condicion}s ---");
            Console.WriteLine("Nombre\t\tPromedio\tClasificación");
            Console.WriteLine("----------------------------------------------");

            //BUCLE 12: FOREACH
            foreach (var e in filtrados)
            {
                Console.WriteLine($"{e.Nombre}\t\t{e.Promedio:F2}\t\t{e.Clasificacion}");
            }
        }
    }

    // FUNCIÓN 12
    static double CalcularPromedio(double[] notas)
    {
        double suma = 0;

        //BUCLE 13: FOREACH
        foreach (var nota in notas)
            suma += nota;
        return suma / notas.Length;
    }
}