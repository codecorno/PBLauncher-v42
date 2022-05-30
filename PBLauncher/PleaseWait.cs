/*
 * Arquivo: PleaseWait.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
using Core;
using Ionic.Zip;
using PBLauncher.Utils;
using PBLauncher.Utils.Enum;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBLauncher
{
    public partial class PleaseWait : Form
    {
        public PleaseWait()
        {
            InitializeComponent();
        }

        private async void PleaseWait_Load(object sender, EventArgs e)
        {
           
            lb_loading.Text = Instancia._strings.LOADING;
            Logger.Log("");
            Logger.Log("[<>] PBLauncher [" + Connect.GameName + "] iniciado.");
            Logger.Log("[!] Launcher Versão:" + Application.ProductVersion);
            if (!CheckLF())
                Close();
            else
            {
                await Task.Delay(5);
                lb_loading.Text = Instancia._strings.CONNECTING_TO_SERVER;
                Status(await Connect.GetHostInfo());
            }

        }

        /// <summary>
        /// Verifica se já existe um processo do launcher ou do jogo aberto
        /// </summary>
        /// <returns></returns>
        private bool CheckLF()
        {
            lb_loading.Text = Instancia._strings.CHECK_IMPORTANT_FILES;
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                Logger.Log("[!] Já existe uma janela do Launcher aberta.");
                MessageBox.Show(Instancia._strings.LAUNCHER_RUNNING, Connect.GameName);
                return false;
            }
            else if (Process.GetProcessesByName("PointBlank").Length > 0)
            {
                Logger.Log("[!] Já existe uma janela do jogo aberta.");
                MessageBox.Show(Instancia._strings.GAME_RUNNING, Connect.GameName);
                return false;
            }
            else if (!File.Exists(string.Concat(Application.StartupPath, "\\UserFileList.dat")))
            {
                StreamWriter streamWriter = new StreamWriter(string.Concat(Application.StartupPath, "\\UserFileList.dat"));
                streamWriter.Close();
            }
            lb_loading.Text = Instancia._strings.CONNECTING_TO_SERVER;
            Logger.Log("[>>] Conectando-se ao servidor...");
            return true;
        }

        private async void Status(HostStatus p)
        {
            if (p != HostStatus.UNK && await CheckLauncherUpdate() || p == HostStatus.UNK)
            {

                lb_loading.Text = Instancia._strings.CHECK_SERVER_STATUS;
                Logger.Log("[>>] Verificando status do servidor...");
                switch (p)
                {
                    case HostStatus.MAINTENANCE:
                        Logger.Log("[<<] O jogo está em manutenção no momento");
                        MessageBox.Show(Instancia._strings.SERVER_MAINTENANCE, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Close();
                        break;
                    case HostStatus.MAINTENANCEALERT:
                        if (string.IsNullOrEmpty(Connect._message))
                            goto case HostStatus.MAINTENANCE;
                        Logger.Log("[<<] O jogo está em manutenção no momento");
                        MessageBox.Show(Connect._message, "MAINTENANCE - " + Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                        Close();
                        break;
                    case HostStatus.ONLINE:
                    case HostStatus.ONLINEALERT:
                        Userlist_check();
                        break;
                    case HostStatus.OFFLINE:
                        Logger.Log("[<<] Conexão com o servidor interrompida");
                        MessageBox.Show(Instancia._strings.CONNECTION_LOST, "MAINTENANCE - " + Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Close();
                        break;
                    default:
                        Logger.Log("[<<] Não foi possível conectar-se ao servidor");
                        MessageBox.Show(Instancia._strings.SERVER_CONNECTION_UNKNOWN, "MAINTENANCE - " + Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Close();
                        break;

                }
            }
        }


        /// <summary>
        /// Verifica se a versão do PBLauncher em execução é a mesma da vps
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckLauncherUpdate()
        {
            lb_loading.Text = Instancia._strings.CHECK_LAUNCHER_UPDATE;

            //Pausa pra atualizar o label
            await Task.Delay(1);

            if (Connect._launcherVer.Equals(Application.ProductVersion))

                //Versão atual está atualizada
                return true;
             
            //Vamos atualizar
            else 
            {
                using (WebClient WCL = new WebClient())
                {
                    WCL.DownloadFileCompleted += new AsyncCompletedEventHandler(WCL_DownloadCompleted);
                    WCL.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WCL_DownloadProgressChanged);
                    try
                    {
                        Logger.Log("[!] Baixando atualização do Launcher.");
                        Directory.CreateDirectory(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles"));
                        Uri uri = new Uri(Connect._fileURL + "//PBLauncher.zip");
                        await WCL.DownloadFileTaskAsync(uri, string.Concat(Application.StartupPath, "\\_LauncherPatchFiles\\PBLauncher.zip"));
                    }
                    catch (Exception ex)
                    {
                        lb_loading.Text = Instancia._strings.LAUNCHER_UPDATE_ERROR;
                        Logger.Log("[!] Ocorreu um erro ao baixar a Atualização do Launcher! -> " + ex.Message);
                        MessageBox.Show(Instancia._strings.LAUNCHER_UPDATE_ERROR, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                return false;
            }
        }

        public async void Userlist_check()
        {
            try
            {
                string LocalMD5checksum = Get.FileMD5(string.Concat(Application.StartupPath, "\\UserFileList.dat"));
                if (Connect._userList.Equals(LocalMD5checksum))
                {
                    lb_loading.Text = Instancia._strings.DELET_TRASH;
                    await Clear.Trash();
                    OpenMain();
                }
                else
                {
                    lb_loading.Text = Instancia._strings.DOWNLOAD_FILE;
                    await Task.Delay(100);
                    using (WebClient WCU = new WebClient())
                    {
                        Logger.Log("[!] Baixando novo UserFileList.");
                        Directory.CreateDirectory(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles"));
                        Uri uri = new Uri(Connect._fileURL + "//UserFileList.zip");
                        WCU.DownloadFile(uri, string.Concat(Application.StartupPath, "\\_LauncherPatchFiles\\UserFileList.zip"));
                        lb_loading.Text = Instancia._strings.EXTRACT_FILE;
                        Unzip(Application.StartupPath, string.Concat(Application.StartupPath, "\\_LauncherPatchFiles\\UserFileList.zip"));
                        await Task.Delay(100);
                        lb_loading.Text = Instancia._strings.DELET_TRASH;
                        await Clear.Trash(); 
                        OpenMain();
                    }
                }
            }
            catch
            {
                Logger.Log("[!] Erro ao baixar novo UserFileList.");
                MessageBox.Show(Instancia._strings.DOWNLOAD_USERLIST_ERROR, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void Unzip(string TargetDir, string ZipToUnpack)
        {
            try
            {
                ZipFile zipFile = ZipFile.Read(ZipToUnpack);
                try
                {
                    foreach (ZipEntry zipEntry in zipFile)
                    {
                        zipEntry.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
                finally
                {
                    if (zipFile != null)
                    {
                        zipFile.Dispose();
                    }
                }
            }
            catch 
            {
               
            }
        }

        private void OpenMain()
        {
            Instancia._main = new MainForm()
            {
                BackgroundImage = Core.Properties.Resources.bg
            };
            Instancia._main.Closed += (s, args) => Close();
            Hide();
            Instancia._main.Show();
        }

        private void WCL_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lb_loading.Text = string.Format(Instancia._strings.DOWNLOAD_LAUNCHER_UPDATE + "{0}/{1} MB", (e.BytesReceived / 1024.0 / 1024.0).ToString("0.00"), (e.TotalBytesToReceive / 1024.0 / 1024.0).ToString("0.00"));
        }

        private async void WCL_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lb_loading.Text = Instancia._strings.EXTRACT_FILE_UPDATE;
                await Task.Delay(2); //Pausa
                string startupPath = Application.StartupPath;
                object[] newLaunch = new object[] { Application.StartupPath, "\\_LauncherPatchFiles\\PBLauncher.zip" };
                Unzip(startupPath, string.Concat(newLaunch));
                Application.Restart();
            }

        }
    }
}
