using System;
using System.IO;
using LectorTagMP3;

Console.WriteLine("Ingrese la ruta del archivo MP3:");
string ruta = Console.ReadLine();
FileInfo info = new FileInfo(ruta);

if (!File.Exists(ruta) || info.Length < 128)
{
    Console.WriteLine("El archivo no existe o es muy pequeño para tener un tag ID3v1");
}
else
{
    Id3v1Tag tag = Id3v1Tag.LeerDesdeArchivo(ruta);
    if (tag == null)
    {
        Console.WriteLine("El archivo no contiene un tag ID3v1 válido.");
    }
    else
    {
        Console.WriteLine("\n--- Información del archivo MP3 ---");
        Console.WriteLine($"Título : {tag.Titulo}");
        Console.WriteLine($"Artista: {tag.Artista}");
        Console.WriteLine($"Álbum  : {tag.Album}");
        Console.WriteLine($"Año    : {tag.Año}");
    }
}