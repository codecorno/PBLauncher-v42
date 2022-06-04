/*
 * Arquivo: MainForm.cs
 * Criado em: 23-11-2021
 * Última modificação: 04-06-2022
 */
using Core;
using Ionic.Zip;
using PBLauncher.Properties;
using PBLauncher.Utils;
using PBLauncher.Utils.Enum;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBLauncher
{
    public partial class MainForm : Form
    {
        private bool Updat, Verif;

        public Point NewPoint;
        private int AVersion;
        private readonly WebClient GameUpdate = new WebClient();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


            ClientVersionn.Text = "ClientVersion: " + Connect._version;
            LauncherConfig.Visible = false;
            WEB_Announce.Refresh();
            Logger.Log("[<<] Verificação do sistema concluída.");
            LTitulo.Text = Instancia._strings.LOADING;
            LTotalFixo.Text = Instancia._strings.TOTAL;
            LAquivoFixo.Text = Instancia._strings.FILE;
            WEB_Announce.Navigate(Connect._webURL);
            WEB_Announce.Refresh(); 
            Buttons_Enable(false, false, false);
            Buttons_Visible(true, true, false);
            CheckUpdate(1);
            CheckAnnounce();
        }

       

        private async void CheckAnnounce()
        {
            if (Connect._state == HostStatus.ONLINEALERT && !string.IsNullOrEmpty(Connect._message))
            {
                await Task.Delay(40);
                MessageBox.Show(Connect._message.Replace("\\n", Environment.NewLine), Connect.GameName);
            }
        }

        #region Buttons
        private void ConfigPBox_Click(object sender, EventArgs e)
        {
            GameVersionn.Text = "GameVersion: " + Connect._gamev;
            ClientVersionn.Text = "ClientVersion: " + Connect._version;
            ClientFilesCountSERVER.Text = "ClientFilesCount[S_Side}: " + Connect._fcountCompare;
            ClientFilesCountCLIENT.Text = "ClientFilesCount[C_Side]: " + Directory.GetFiles(Application.StartupPath, "*.*", SearchOption.AllDirectories).Count().ToString();
            LauncherVersionn.Text = "LauncherVersion: " + Application.ProductVersion;                      

            LauncherConfig.Visible = true;
            StartPBox.Enabled = false;
            CheckPBox.Enabled = false;
            WEB_Announce.Visible = false;
            StartPBox.Image = Resources.started;
            CheckPBox.Image = Resources._checked;

        }
        #endregion

        #region Start Game
        private async void StartPBox_Click(object sender, EventArgs e)
        {
            if (Connect._useXCLoader && !File.Exists(string.Concat(Application.StartupPath, "\\xigncode3_loader.exe")))
            {
                Logger.Log("[!!] Não foi possível localizar xigncode3_loader.exe");
                MessageBox.Show("Não foi possível localizar xigncode3_loader.exe", Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else if (File.Exists(string.Concat(Application.StartupPath, "\\PointBlank.exe")))
            {
                StartPBox.Image = Resources.start_click;
                CheckPBox.Image = Resources.check_click;
                StartPBox.Enabled = false;
                LTitulo.Update();
                await Task.Delay(10);
                try
                {
                    if (Connect._useXCLoader)
                    {
                        Process.Start(string.Concat(Application.StartupPath, "\\xigncode3_loader.exe"));
                        await Task.Delay(60);
                    }
                    Process.Start(string.Concat(Application.StartupPath, "\\PointBlank.exe"), Connect._keyHost ? Connect._launcherKey.ToString() : Param.key_dll);
                    Hide();
                }
                catch (Exception arg)
                {
                    Logger.Log("[!] Jogo não iniciado! [" + arg.Message + "]");
                    MessageBox.Show("Jogo não iniciado! \n[" + arg.Message + "]", Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                Logger.Log("[!] O Jogo foi iniciado com sucesso!");
                await Task.Delay(10);
                PBDetect.Start();
                Hide();
            }
            else
            {
                MessageBox.Show(Instancia._strings.GAME_NOT_FOUND, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Log("[!!] Não foi possível localizar PointBlank.exe");
                Close();
            }
        }
        #endregion

        #region Check Client
        private async void CheckPBox_Click(object sender, EventArgs e)
        {
            Verif = true;
            CheckPBox.Image = Resources._checked;
            StartPBox.Image = Resources.started;
            LTitulo.Text = Instancia._strings.CHECKING_FILES;
            LTitulo.Refresh();
            Logger.Log("[>] Verificação dos arquivos do jogo iniciada.");
            Buttons_Enable(false, false, false);
            Buttons_Visible(true, true, false);
            LArquivo.Visible = true;

            for (int i = 0; i < 100; i++)
            {
                LArquivo.Text = "     verificando arquivos!";
                Bar1SetProgress(i, 100);
                await Task.Delay(20);
            }

            Verif = false;
            LDownload.Visible = false;
            Logger.Log("[<] Verificação dos arquivos do jogo finalizada.");
            await Clear.Trash();
            CheckPBox.Image = Resources.check;
            StartPBox.Image = Resources.start;
            Buttons_Enable(true, true, false);
            Buttons_Visible(true, true, false);
            Arquivo_Bar.Width = Arquivo_BarFixo.Width;
            Total_Bar.Width = Total_BarFixo.Width;
            LTitulo.Text = Instancia._strings.GAME_IS_UPDATE;
            LArquivo.Visible = false;
        }
        #endregion

        #region Update
        private async void CheckUpdate(short start)
        {
            long LVersion;
            if (start == 1)
                LVersion = Connect._version;
            else
                LVersion = await Connect.GetVersionUP();
           await  Clear.Trash();
            if (LVersion == -1)
            {
                Logger.Log("[>!] Não tem atualização disponível. [RECEIVE_COUNT_NULL]");
                MessageBox.Show(Instancia._strings.GET_UPDATE_ERROR, "[RECEIVE_NULL] ~ " + Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AVersion = LerVersion();
            Arquivo_Bar.Width = Arquivo_BarFixo.Width;
            Total_Bar.Width = Total_BarFixo.Width;
            LArquivo.Visible = false;
            UpdatePBox.Image = Resources.update;
            Updat = false;
            if (LVersion > AVersion)
            {
                Logger.Log("Aguardando atualização da cliente.");
                Logger.Log("Versão: " + AVersion + "/" + LVersion);
                Arquivo_Bar.Width = 0;
                Total_Bar.Width = 0;
                LTitulo.Text = Instancia._strings.UPDATE_REC;
                Buttons_Enable(false, false, true);
                Buttons_Visible(false, true, true);
                CheckPBox.Image = Resources._checked;
            }
            else if (LVersion == AVersion)
            {
                Logger.Log("A cliente está atualizada.");
                Logger.Log("[!] " + ClientVersionn.Text);
                Logger.Log("[!] ClientFilesCount: " + Directory.GetFiles(Application.StartupPath, "*.*", SearchOption.AllDirectories).Count().ToString());
                Logger.Log("[!] ClientFilesCount[SERVER]: " + Connect._fcountCompare);
                Logger.Log("Versão: " + AVersion + "/" + LVersion);
                LArquivo.Visible = false;
                LTitulo.Text = Instancia._strings.GAME_IS_UPDATE;
                Buttons_Enable(true, true, false);
                Buttons_Visible(true, true, false);
                CheckPBox.Image = Resources.check;
                StartPBox.Image = Resources.start;
            }
            else
            {
                Logger.Log("Versão da Cliente maior que a versão do servidor.");
                Logger.Log("Versão: " + AVersion + "/" + LVersion);
                Logger.Log("Resetando atualizações...");
                await Task.Delay(50);
                MessageBox.Show(Instancia._strings.LARGER_VERSION, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                File.Delete(string.Concat(Application.StartupPath, "\\config.zpt"));
                await Task.Delay(50);
                EscreverVersion(0);
                CheckUpdate(0);
            }
        }

        private void UpdatePBox_Click(object sender, EventArgs e)
        {
            Buttons_Enable(false, false, false);
            Updat = true;
            UpdatePBox.Image = Resources.update_click;
            GameUpdate.DownloadFileCompleted += GameUpdate_DownloadFileCompleted;
            GameUpdate.DownloadProgressChanged += GameUpdate_DownloadProgressChanged;
            LTitulo.Text = Instancia._strings.GET_UPDATE_INFO;
            StartUpdate();
        }

        private void GameUpdate_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Bar1SetProgress(e.BytesReceived, e.TotalBytesToReceive); 
            LDownload.Text = string.Format("{0}/{1} MB", (e.BytesReceived / 1024.0 / 1024.0).ToString("0.00"), (e.TotalBytesToReceive / 1024.0 / 1024.0).ToString("0.00"));
        }

        private async void GameUpdate_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                LDownload.Visible = false;
                LTitulo.Text = Instancia._strings.EXTRACT_FILE_UPDATE;
                LTitulo.Update();
                Total_Bar.Width = 0;
                string startupPath = Application.StartupPath;
                object[] actualVersion = new object[] { Application.StartupPath, "\\_DownloadPatchFiles\\patch_", AVersion + 1, ".zip" };
                await Unzip(startupPath, string.Concat(actualVersion));
                Total_Bar.Width = Total_BarFixo.Width;
                StartUpdate();
            }
        }

        private void StartUpdate()
        {
            AVersion = LerVersion();
            int LVersion = Connect._version;
            if (LVersion != AVersion)
            {
                int newV = AVersion + 1;
                Arquivo_Bar.Width = 0;
                LArquivo.Text = string.Concat("Atualização_", newV, ".zip");
                LArquivo.Visible = true;
                Logger.Log("Iniciando atualização da cliente.");
                Logger.Log("Baixando " + LArquivo.Text);
                Bar2SetProgress(newV, LVersion);
                Directory.CreateDirectory(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles"));
                try
                {
                    Uri uri = new Uri(Connect._upURL + "patch_" + newV + ".zip");
                    object[] startupPath = new object[] { Application.StartupPath, "\\_DownloadPatchFiles\\patch_", newV, ".zip" };
                    LDownload.Visible = true;
                    GameUpdate.DownloadFileAsync(uri, string.Concat(startupPath));
                }
                catch (Exception arg)
                {
                    Logger.Log("O arquivo [" + newV + "] não foi encontrado no servidor. \n[" + arg.Message + "]");
                    MessageBox.Show(string.Format(Instancia._strings.DOWNLOAD_FILE_ERROR, newV), Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            else
                CheckUpdate(0);
        }

        private int LerVersion()
        {
            IniFile fileini = new IniFile(Application.StartupPath + "\\config.zpt");
            return fileini.IniReadInt("UPDATE", "version");
        }

        private void EscreverVersion(int version)
        {
            IniFile fileini = new IniFile(Application.StartupPath + "\\config.zpt");
            fileini.IniWriteInt("UPDATE", "version", version);
        }

        #endregion

        #region Image buttons
        private void ExitPBox_MouseMove(object sender, MouseEventArgs e)
        {
            ExitPBox.Image = Resources.exit_mouse;
        }

        private void ExitPBox_MouseLeave(object sender, EventArgs e)
        {
            ExitPBox.Image = Resources.exit;
        }

        private void ExitPBox_MouseDown(object sender, MouseEventArgs e)
        {
            ExitPBox.Image = Resources.exit_click;
        }

        private void MinPBox_MouseMove(object sender, MouseEventArgs e)
        {
            MinPBox.Image = Resources.minim_mouse;
        }

        private void MinPBox_MouseLeave(object sender, EventArgs e)
        {
            MinPBox.Image = Resources.minim;
        }

        private void MinPBox_MouseDown(object sender, MouseEventArgs e)
        {
            MinPBox.Image = Resources.minim_click;
        }

        private void ConfigPBox_MouseMove(object sender, MouseEventArgs e)
        {
            ConfigPBox.Image = Resources.config_mouse;
        }

        private void ConfigPBox_MouseLeave(object sender, EventArgs e)
        {
            ConfigPBox.Image = Resources.config;
        }

        private void UpdatePBox_MouseDown(object sender, MouseEventArgs e)
        {
            UpdatePBox.Image = Resources.update_click;
        }

        private void UpdatePBox_MouseLeave(object sender, EventArgs e)
        {
            if (Updat)
                UpdatePBox.Image = Resources.update_click;
            else
                UpdatePBox.Image = Resources.update;
        }

        private void UpdatePBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Updat)
                UpdatePBox.Image = Resources.update_click;
            else
                UpdatePBox.Image = Resources.update_mouse;
        }

        private void StartPBox_MouseDown(object sender, MouseEventArgs e)
        {
            StartPBox.Image = Resources.start_click;
        }

        private void StartPBox_MouseLeave(object sender, EventArgs e)
        {
            StartPBox.Image = Resources.start;
        }

        private void StartPBox_MouseMove(object sender, MouseEventArgs e)
        {
            StartPBox.Image = Resources.start_mouse;
        }

        private void CheckPBox_MouseDown(object sender, MouseEventArgs e)
        {
            CheckPBox.Image = Resources.check_click;
        }

        private void CheckPBox_MouseLeave(object sender, EventArgs e)
        {
            if (Verif)
                CheckPBox.Image = Resources.check_click;
            else
                CheckPBox.Image = Resources.check;
        }

        private void CheckPBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Verif)
                CheckPBox.Image = Resources.check_click;
            else
                CheckPBox.Image = Resources.check_mouse;
        }
        #endregion

        #region Movimentação e saida do Form
        
        private void ExitPBox_Click(object sender, EventArgs e)
        {       
            if (MessageBox.Show(Instancia._strings.EXIT_ASK, Connect.GameName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Logger.Log("[><] PBLauncher foi finalizado.");
                System.Environment.Exit(0);
            }
            else
            {
               //Launcher continua aberto
            }

        }

        private void MinPBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Launcher_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                int left = Left;
                Point mousePosition = MousePosition;
                NewPoint.X = left - mousePosition.X;
                int top = Top;
                Point point = MousePosition;
                NewPoint.Y = top - point.Y;
            }
        }

        private void Launcher_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                int x = NewPoint.X;
                Point mousePosition = MousePosition;
                Left = x + mousePosition.X;
                int y = NewPoint.Y;
                mousePosition = MousePosition;
                Top = y + mousePosition.Y;
            }
        }

        private void Launcher_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Log("[><] PBLauncher foi finalizado.");
        }
       // => Logger.Log("[><] PBLauncher foi finalizado.");


        #endregion

        #region Códigos base
        public async Task Unzip(string TargetDir, string ZipToUnpack)
        {
            try
            {
                int x = 0;
                ZipFile zipFile = ZipFile.Read(ZipToUnpack);
                try
                {
                    zipFile.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(Unzip_ExtractProgressChanged);
                    LArquivo.Visible = true;
                    foreach (ZipEntry zipEntry in zipFile)
                    {
                        Arquivo_Bar.Width = 0;
                        Bar2SetProgress(x++, zipFile.Count);

                        //Pausa necessaria para atualizar os elementos visuais.
                        await Task.Delay(1);

                        string fileName = zipEntry.FileName;
                        if (fileName.Contains("/"))
                        {
                            int num2 = fileName.LastIndexOf("/");
                            fileName = fileName.Substring(num2 + 1);
                        }
                        if (!zipEntry.IsDirectory)
                        {
                            LArquivo.Text = fileName;
                            zipEntry.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
                finally
                {
                    Arquivo_Bar.Width = Arquivo_BarFixo.Width;
                    if (zipFile != null)
                        zipFile.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Unzip_ExtractProgressChanged(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer != 0)
            {
                Bar1SetProgress(e.BytesTransferred, e.TotalBytesToTransfer);
                Arquivo_Bar.Update();
            }

        }

        public void Bar1SetProgress(long received, long maximum) => Arquivo_Bar.Width = (int)(received * Arquivo_BarFixo.Width / maximum);

        public void Bar2SetProgress(int received, int maximum) => Total_Bar.Width = received * Total_BarFixo.Width / maximum;
        
        private void Buttons_Enable(bool Start, bool Check, bool Update)
        {
            StartPBox.Enabled = Start;
            CheckPBox.Enabled = Check;
            UpdatePBox.Enabled = Update;
        }

      
        private void WEB_Announce_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void BAN()
        {
            if (File.Exists(string.Concat(Application.StartupPath, "\\Gui\\ban.ban")))
            {
                Logger.Log("[XX] Usuário banido permanentemente do launcher.");
                MessageBox.Show(Instancia._strings.AUTH_ACC_BANNED, Connect.GameName);
                Close();
            }
            else
            {

            }
        }

        private void PBDetect_Tick(object sender, EventArgs e)
        {
            
           try
            {
                Process[] PB = Process.GetProcessesByName("PointBlank");
                if (PB.Length == 0)
                {
                    //Jogo fechado
                    Logger.Log("[><] O Jogo foi finalizado");
                    Close();

                }
                else
                {
                    //Jogo aberto
                    try
                    {
                        Process[] CE = Process.GetProcessesByName("cheatengine-x86_64-SSE4-AVX2");
                        if (CE.Length == 0)
                        {
                            //CE fechado      

                        }
                        else
                        {
                            //CE aberto
                            WebClient banWEBCLIENT = new WebClient();
                            banWEBCLIENT.DownloadFile(Connect._banPermURL, (Application.StartupPath + "\\Gui\\ban.ban"));

                            //            Logger.Log("[><] O Jogo foi finalizado");
                            var PBK = Process.GetProcessesByName("PointBlank");
                            foreach (var pb in PBK)
                                pb.Kill();
                        }

                    }

                    catch (Exception arg)
                    {
                        // Logger.Log("[!] Erro [" + arg.Message + "]");
                    }

                }
            }
            catch (Exception arg)
            {
               // Logger.Log("[!] Exception [" + arg.Message + "]");
            }


        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LauncherConfig.Visible = false;
            StartPBox.Enabled = true;
            CheckPBox.Enabled = true;
            WEB_Announce.Visible = true;
            StartPBox.Image = Resources.start;
            CheckPBox.Image = Resources.check;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            OkConfig.Image = Resources.Ok_1;
        }

        private void OkConfig_MouseMove(object sender, MouseEventArgs e)
        {
            OkConfig.Image = Resources.Ok_2;
        }

        private void OkConfig_MouseDown(object sender, MouseEventArgs e)
        {
            OkConfig.Image = Resources.Ok_3;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Updat)
            {
                try
                {
                    Process.Start("PBConfig.exe");
                }
                catch
                {
                    MessageBox.Show(Instancia._strings.CONFIG_ERROR, Connect.GameName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Buttons_Visible(bool Start, bool Check, bool Update)
        {
            StartPBox.Visible = Start;
            CheckPBox.Visible = Check;
            UpdatePBox.Visible = Update;
        }
        #endregion
    }
}
