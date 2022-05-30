/*
 * Arquivo: Clear.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBLauncher.Utils
{
    public static class Clear
    {
        public static async Task Trash()
        {
            try
            {
                if (Directory.Exists(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles")))
                {
                    Logger.Log("[!] Deletando pasta temporária da atualização.");
                    Directory.Delete(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles"), true);
                    await Task.Delay(20);
                }
                if (Directory.Exists(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles")))
                {
                    Logger.Log("[!] Deletando pasta temporária de download.");
                    Directory.Delete(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles"), true);
                    await Task.Delay(20);
                }
                string[] _files = Directory.GetFiles(Application.StartupPath, "*.PendingOverwrite", SearchOption.AllDirectories);
                foreach (string _file in _files)
                {
                    File.Delete(_file);
                    Logger.Log("[!] Deletando arquivo temporário da atualização.");
                    await Task.Delay(30);
                }
            }
            catch { }
        }
    }
}
