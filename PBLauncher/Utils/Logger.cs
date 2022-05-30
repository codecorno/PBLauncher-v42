/*
 * Arquivo: Logger.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBLauncher.Utils
{
    public static class Logger
    {
        public static void Log(string texto)
        {
            string str = string.Concat(Application.StartupPath, "\\PBLauncher.log");
            DateTime now = DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(str, true);
            texto = texto == "" ? "" : "[" + now.ToString("yyyy/MM/dd HH:mm:ss") + "] " + texto;
            streamWriter.WriteLine(texto);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
