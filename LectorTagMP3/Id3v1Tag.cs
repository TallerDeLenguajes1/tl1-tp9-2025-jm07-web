using System;
using System.IO;
using System.Text;

namespace LectorTagMP3
{
    public class Id3v1Tag
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string Año { get; set; }
        
        public static Id3v1Tag LeerDesdeArchivo(string rutaArchivo)
        {
            const int tamañoTag = 128;
            using (var fs = new System.IO.FileStream(rutaArchivo, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                if (fs.Length < tamañoTag)
                {
                    return null;
                }
                
                fs.Seek(-tamañoTag, System.IO.SeekOrigin.End);
                byte[] buffer = new byte[tamañoTag];
                fs.Read(buffer, 0, tamañoTag);
                
                string cabecera = Encoding.GetEncoding("latin1").GetString(buffer, 0, 3);
                if (cabecera != "TAG")
                {
                    return null;
                }
                
                var tag = new Id3v1Tag
                {
                    Titulo = Encoding.GetEncoding("latin1").GetString(buffer, 3, 30).TrimEnd('\0', ' '),
                    Artista = Encoding.GetEncoding("latin1").GetString(buffer, 33, 30).TrimEnd('\0', ' '),
                    Album = Encoding.GetEncoding("latin1").GetString(buffer, 63, 30).TrimEnd('\0', ' '),
                    Año = Encoding.GetEncoding("latin1").GetString(buffer, 93, 4).TrimEnd('\0', ' ')
                };
                    
                return tag;
            }
        }
    }
}
