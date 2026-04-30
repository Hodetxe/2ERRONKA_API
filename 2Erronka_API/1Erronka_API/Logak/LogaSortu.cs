using System;
using System.IO;
public class LogService
{
    public void LogaSortu(string mezua)
    {
        string directorioApp = AppDomain.CurrentDomain.BaseDirectory;
        string rutaArchivo = Path.Combine(directorioApp, "logTpv.txt");
 
        try
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
            {
                string hora = DateTime.Now.ToString("HH:mm:ss");
                sw.WriteLine($"[{hora}] {mezua}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore larria loga idaztean: {ex.Message}");
        }
    }
}
