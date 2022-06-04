/*
 * Arquivo: Connect.cs
 * Criado em: 23-11-2021
 * Última modificação: 04-06-2022
 */
using Core;
using PBLauncher.Utils;
using PBLauncher.Utils.Enum;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PBLauncher
{
    public static class Connect
    {
        /// <summary>
        /// URL of the configuration file hosted on the site
        /// URL do arquivo configuração hospedado no site
        /// Recomendado: Arquivo de configuração estar com o encode em UTF-8
        /// </summary>
        private static readonly string HostURL = "http://localhost/launcher/settings.conf";
        public static string GameName = "Point Blank Launcher - Private Server";



        #region Modelo e processamento
        public static HostStatus _state = HostStatus.OFFLINE;
        public static string _launcherVer, _message, _webURL, _upURL, _userList, _fileURL, _gamev, _fcountCompare, _banPermURL;
        public static int _version;
        public static bool  _keyHost, _useXCLoader;
        public static long _launcherKey;

        /// <summary>
        /// Leitura e processamento dos dados
        /// </summary>
        /// <returns></returns>
        public static async Task<HostStatus> GetHostInfo()
        {
            await Task.Delay(5);
            try
            {
                using (WebClient wc = new WebClient() { Encoding = Encoding.UTF8 })
                using (var sr = new StringReader(wc.DownloadString(HostURL)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string id = line.Split('=')[0];
                            switch (id)
                            {
                                case "launcherversion":
                                    _launcherVer = Get.Conf(line, id);
                                    break;
                                case "status":
                                    _state = (HostStatus)int.Parse(Get.Conf(line, id));
                                    break;
                                case "message":
                                    _message = Get.Conf(line, id);
                                    break;
                                case "webpage":
                                    _webURL = Get.Conf(line, id);
                                    break;
                                case "files":
                                    _fileURL = Get.Conf(line, id);
                                    break;
                                case "userlist":
                                    _userList = Get.Conf(line, id);
                                    break;
                                case "updateurl":
                                    _upURL = Get.Conf(line, id);
                                    break;
                                case "clientversion":
                                    _version = int.Parse(Get.Conf(line, id));
                                    break;
                                case "keyhost":
                                    _keyHost = bool.Parse(Get.Conf(line, id));
                                    break;
                                case "key":
                                    _launcherKey = long.Parse(Get.Conf(line, id));
                                    break;
                                case "use_xingload":
                                    _useXCLoader = bool.Parse(Get.Conf(line, id));
                                    break;
                                case "GameVersionn":
                                    _gamev = Get.Conf(line, id);
                                    break;
                                case "FilesCountCompare":
                                    _fcountCompare = Get.Conf(line, id);
                                    break;
                                case "BanPerm":
                                    _banPermURL = Get.Conf(line, id);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Salva o erro no log para facil verificação.
                Logger.Log($"GetHostInfo -> {ex.Message}");
                _state = HostStatus.UNK;
            }
            return _state;
        }

        public static async Task<long> GetVersionUP()
        {
            await Task.Delay(5);
            try
            {
                using (WebClient wc = new WebClient() { Encoding = Encoding.UTF8 })
                using (var sr = new StringReader(wc.DownloadString(HostURL)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("="))
                        {
                            string id = line.Split('=')[0];
                            switch (id)
                            {
                                case "clientversion":
                                    _version = int.Parse(Get.Conf(line, id));
                                    return _version;
                                
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //Salva o erro no log para facil verificação.
                Logger.Log($"GetVersionUP -> {ex.Message}");
                _version = -1;
            }
            return _version;
        }
        #endregion

    }
}
