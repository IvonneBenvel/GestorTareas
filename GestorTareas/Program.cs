using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Tarea> listaTareas = new List<Tarea>();
    static int contadorId = 1;

    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.WriteLine("\n--- GESTOR DE TAREAS ---");
            Console.WriteLine("1. Crear tarea");
            Console.WriteLine("2. Buscar por tipo");
            Console.WriteLine("3. Eliminar tarea");
            Console.WriteLine("4. Exportar tareas");
            Console.WriteLine("5. Importar tareas");
            Console.WriteLine("0. Salir");

            Console.Write("Opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1: CrearTarea(); break;
                case 2: BuscarPorTipo(); break;
                case 3: EliminarTarea(); break;
                case 4: Exportar(); break;
                case 5: Importar(); break;
            }

        } while (opcion != 0);
    }

    static void CrearTarea()
    {
        Tarea t = new Tarea();

        t.Id = contadorId++;

        Console.Write("Nombre: ");
        t.Nombre = Console.ReadLine();

        Console.Write("Descripción: ");
        t.Descripcion = Console.ReadLine();

        Console.Write("Tipo (persona, trabajo, ocio): ");
        t.Tipo = (TipoTarea)Enum.Parse(typeof(TipoTarea), Console.ReadLine());

        Console.Write("Prioridad (true/false): ");
        t.Prioridad = bool.Parse(Console.ReadLine());

        listaTareas.Add(t);
    }

    static void BuscarPorTipo()
    {
        Console.Write("Tipo: ");
        TipoTarea tipo = (TipoTarea)Enum.Parse(typeof(TipoTarea), Console.ReadLine());

        foreach (Tarea t in listaTareas)
        {
            if (t.Tipo == tipo)
                Console.WriteLine(t);
        }
    }

    static void EliminarTarea()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());

        Tarea t = listaTareas.Find(x => x.Id == id);

        if (t != null)
        {
            listaTareas.Remove(t);
            Console.WriteLine("Eliminada.");
        }
        else
        {
            Console.WriteLine("No encontrada.");
        }
    }

    static void Exportar()
    {
        using (StreamWriter sw = new StreamWriter("tareas.txt"))
        {
            foreach (Tarea t in listaTareas)
            {
                sw.WriteLine(t.ToFile());
            }
        }

        Console.WriteLine("Exportado.");
    }

    static void Importar()
    {
        if (!File.Exists("tareas.txt"))
        {
            Console.WriteLine("No existe el fichero.");
            return;
        }

        string[] lineas = File.ReadAllLines("tareas.txt");
        listaTareas.Clear();

        foreach (string linea in lineas)
        {
            string[] datos = linea.Split(';');

            Tarea t = new Tarea
            {
                Id = int.Parse(datos[0]),
                Nombre = datos[1],
                Descripcion = datos[2],
                Tipo = (TipoTarea)Enum.Parse(typeof(TipoTarea), datos[3]),
                Prioridad = bool.Parse(datos[4])
            };

            listaTareas.Add(t);
        }

        Console.WriteLine("Importado.");
    }
}