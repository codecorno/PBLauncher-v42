/*
 * Arquivo: Program.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
using PBLauncher.Localize;
using PBLauncher.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace PBLauncher
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Uncomment the desired language
            //Remova o comentário da linguagem desejada

            Instancia._strings = new LanguagePTBR();
            //Instancia._strings = new LanguageEN();


            if (!File.Exists(string.Concat(Application.StartupPath, "\\launcher_core.dll")))
            {
                Logger.Log("[ERROR] Launcher_core.dll not found.");
                MessageBox.Show(Instancia._strings.CORE_NOTFOUND, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
                Application.Run(new PleaseWait());
        }
    }
}
