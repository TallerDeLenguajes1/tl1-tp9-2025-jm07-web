using System;
using System.IO;
using System.Globalization;

string path = "";

while (true)
{
    Console.Write("Ingrese el path del directorio que desea analizar: ");
    path = Console.ReadLine();

    if (Directory.Exists(path))
    {
        break;
    }

    Console.WriteLine("El directorio ingresado no existe. Intente nuevamente.");
}

Console.WriteLine("\nCarpetas encontradas:");
string[] carpetas = Directory.GetDirectories(path);
foreach (string carpeta in carpetas)
{
    Console.WriteLine("- " + Path.GetFileName(carpeta));
}

Console.WriteLine("\nArchivos encontrados:");
string[] archivos = Directory.GetFiles(path);

foreach (string archivo in archivos)
{
    FileInfo info = new FileInfo(archivo);
    double tamanioKb = Math.Round(info.Length / 1024.0, 2);
    string nombreArchivo = info.Name;

    Console.WriteLine($"- {nombreArchivo} ({tamanioKb} KB)");
}

string rutaCsv = Path.Combine(path, "reporte_archivos.csv");
using (StreamWriter writer = new StreamWriter(rutaCsv))
{
    writer.WriteLine("Nombre del Archivo,Tamaño (KB),Fecha de Última Modificación");

    foreach (string archivo in archivos)
    {
        FileInfo info = new FileInfo(archivo);
        double tamanioKB = Math.Round(info.Length / 1024.0, 2);
        string fecha = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

        writer.WriteLine($"{info.Name},{tamanioKB},{fecha}");
    }
}

Console.WriteLine($"\nInforme generado: reporte_archivos.csv en {path}");
