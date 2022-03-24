using System;
using System.Diagnostics;
using GGDev_Launcher.Epic;
using System.Threading;
using System.Collections;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Management.Automation;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.Reflection;
using GGDev_Launcher.Utilities;
using GGDev_Launcher.DiscordRpcDemo;
using System.Runtime.InteropServices;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using System.Security.Cryptography;
using IronPython.Hosting;
using System.Text;
using System.Web;
using System.Windows.Input;
using Windows.UI;
using System.Timers;
using System.Threading.Tasks;

namespace GGDev_Launcher
{
    public partial class MainWindow : Window
    {

        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        public static string foldername
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path) + "\\";
            }
        }

        public static bool disableConsole = false;
        private string token;
        private string exchange;
        private string username;
        public static string tempPath = Path.GetTempPath();
        private readonly WebClient webClient = new WebClient();

        private void Give_AntiCheat_Love()
        {
            using (PowerShell PowerShellInst = PowerShell.Create())
            {
                string criteria = "system*";
                PowerShellInst.AddScript("Set-Service 'BEService' -StartupType Disabled" + criteria);
                PowerShellInst.AddScript("Set-Service 'EasyAntiCheat' -StartupType Disabled" + criteria);
                Config_file.Default.AC_bypass = true;
                Config_file.Default.Save();
                Collection<PSObject> PSOutput = PowerShellInst.Invoke();
                foreach (PSObject obj in PSOutput)
                {
                    if (obj != null)
                    {
                        msg("An Error occured \n" +
                            obj.Properties["Status"].Value.ToString() + " - ");
                    }
                }
            }
        }


        public static string plataniumdllpath = foldername + "Platanium.dll";
        public static string nothingdllpath = foldername + "Nothing.dll";
        public static string GGDevdllpath = tempPath + "GGDev.dll";
        public static string NoKickdllpath = tempPath + "Nokick.dll";
        public static string ConsoleUnlockerdllpath = tempPath + "FortConsole.dll";

        private void Save_settings(object sender, EventArgs e)
        {
            Config_file.Default.Path = FN_Path.Text;
            Config_file.Default.Save();
            Application.Current.Shutdown();
            Config_file.Default.Exchange = exchange;
            Config_file.Default.Save();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("954420013769588737", ref this.handlers, true, null);
            this.handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("954420013769588737", ref this.handlers, true, null);
            this.presence.details = "Join GGDev!";
            this.presence.largeImageKey = "ggdev";
            this.presence.smallImageKey = "Join GGDev";
            this.presence.largeImageText = "Join GGDev";
            this.presence.state = "https://dsc.gg/ggdev";
            DiscordRpc.UpdatePresence(ref this.presence);



            DeleteCms();
            if (FN_Path.Text == "Fortnite Path" || FN_Path.Text == "" || String.IsNullOrEmpty(FN_Path.Text))
            {
                FN_Path.Text = GetEpicInstallLocations().FirstOrDefault(i => i.AppName == "Fortnite")?.InstallLocation;
            }
            else
            {
                FN_Path.Text = Config_file.Default.Path;
            }
            if (Config_file.Default.AC_bypass == false)
            {
                Give_AntiCheat_Love();
            }
            if (Config_file.Default.Show == true)
            {
                Config_file.Default.Show = false;
                Config_file.Default.Save();
            }
        }

        public void ShowLi(bool yesorno)
        {
            if (!yesorno)
            {
                Logged_in_as.Visibility = Visibility.Hidden;
                DisplayName.Visibility = Visibility.Hidden;
                LoginButton.Content = "Login";
            }
            else
            {
                Logged_in_as.Visibility = Visibility.Visible;
                DisplayName.Visibility = Visibility.Visible;
                LoginButton.Content = "Launch";
            }
        }

        public void msg(string text)
        {
            MessageBox.Show(text.ToString(), "GGDev Launcher");
        }

        private void Login_click(object sender, RoutedEventArgs e)
        {
            if (LoginButton.Content.ToString() == "Login")
            {
                Hide();
                ShowLi(false);



                //Get Token
                string devicecode = Auth.GetDevicecode(Auth.GetDevicecodetoken());
                string[] strArray = devicecode.Split(new char[1]
                {
                ','
                }, 2);
                if (devicecode.Contains("error"))
                    return;
                username = strArray[1];
                DisplayName.Content = (string)(object)(strArray[1] ?? "");
                ShowLi(true);
                Show();
                token = strArray[0];
                LoginButton.Content = "Launch";
                Config_file.Default.Path = FN_Path.Text;
                Config_file.Default.Save();
            }
            else if (LoginButton.Content.ToString() == "Launch")
            {
                Config_file.Default.Path = FN_Path.Text;
                Config_file.Default.Save();

                //Get Exchange
                exchange = Auth.GetExchange(token);

                //Paths
                string FN = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
                string EAC = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
                string FNL = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

                //Path check FN
                if (!File.Exists(FN))
                {
                    msg("\"" + FN + "\" wasn't found, stop deleting game files!");
                    ShowLi(false);
                }
                else
                {
                    //Path check Dll's

                    {
                        //Path check EAC
                        if (!File.Exists(EAC))
                        {
                            msg("\"" + EAC + "\" wasn't found, stop deleting game files!");
                            ShowLi(false);
                        }

                        //Path check FNLauncher
                        else if (!File.Exists(FNL))
                        {
                            msg("\"" + FNL + "\" wasn't found, stop deleting game files!");
                            ShowLi(false);
                        }
                        //Start the game
                        else
                        {

                            Config_file.Default.Path = FN_Path.Text;
                            Config_file.Default.Save();

                            string arguments = "-obfuscationid=AqIHXG2ib9xcbISl_E2ui9ctgMCdzw -AUTH_LOGIN=unused -AUTH_PASSWORD=" + exchange + " -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -noeac -fltoken=919348d6add4c4c7c7507e61";
                            //define fortnite
                            Process Fortnite = new Process
                            {
                                StartInfo = new ProcessStartInfo(FN, arguments)
                                {
                                    UseShellExecute = false,
                                    RedirectStandardOutput = false,
                                    CreateNoWindow = true
                                }
                            };
                            Process FNLP = new Process();
                            FNLP.StartInfo.FileName = FNL;
                            FNLP.Start();
                            foreach (ProcessThread thread in FNLP.Threads)
                                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                            Process EACP = new Process();
                            EACP.StartInfo.FileName = EAC;
                            EACP.StartInfo.Arguments = "-epiclocale=en -noeac -fromfl=be -fltoken=6a3cc577a8858d6da3aa5313 -frombe";
                            EACP.Start();
                            foreach (ProcessThread thread in (ReadOnlyCollectionBase)EACP.Threads)
                                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                            Fortnite.Start();
                            Thread.Sleep(2000);

                            //hide this window
                            Hide();
                            //Download Injector
                            Thread.Sleep(6000);
                            try
                            {
                                File.Delete(tempPath + "/Injector.exe");
                            }
                            catch
                            {
                            }
                            try
                            {
                                webClient.DownloadFile("https://cdn.discordapp.com/attachments/870681151302426637/870681198765162546/Injector.exe", tempPath + "/Injector.exe");
                                webClient.DownloadFile("https://cdn.discordapp.com/attachments/870681151302426637/897451989544370216/NoKick.dll", tempPath + "NoKick.dll");
                                if (isDevAccount.IsChecked == true)
                                {
                                    webClient.DownloadFile("https://cdn.discordapp.com/attachments/906439505597300769/954716406753132614/GGDev.dll", tempPath + "GGDev.dll");
                                }
                                else
                                {
                                    webClient.DownloadFile("https://cdn.discordapp.com/attachments/906439505597300769/954715859782344794/GGDev.dll", tempPath + "GGDev.dll");
                                }
                                
                                webClient.DownloadFile("https://cdn.discordapp.com/attachments/870681151302426637/897451770521997402/FortConsole.dll", tempPath + "FortConsole.dll");
                            }
                            catch
                            {
                                MessageBox.Show("Please make sure that you are connected to the internet.");
                                ShowLi(true);
                                Show();
                                return;
                            }
                            new Process()
                            {
                                StartInfo = {
                                Arguments = string.Format("\"{0}\" \"{1}\"", (object) Fortnite.Id, (object) GGDevdllpath),
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                FileName = (tempPath + "/Injector.exe")
                                }
                            }.Start();
                            //Inject 1st dll
                            Fortnite.WaitForInputIdle();

                            
                            MessageBox.Show("Press OK After You Passed First Loading Screeen", "GGDev Launcher");
                            new Process()
                            {
                                StartInfo = {
                                Arguments = string.Format("\"{0}\" \"{1}\"", (object) Fortnite.Id, (object) NoKickdllpath),
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                FileName = (tempPath + "/Injector.exe")
                                }
                            }.Start();
                            new Process()
                            {
                                StartInfo = {
                                Arguments = string.Format("\"{0}\" \"{1}\"", (object) Fortnite.Id, (object) ConsoleUnlockerdllpath),
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                FileName = (tempPath + "/Injector.exe")
                                }
                            }.Start();




                            //Check if Fortnite is closed
                            Fortnite.WaitForExit();
                            try
                            {
                                FNLP.Close();
                                EACP.Close();
                            }
                            catch { }

                            //Show form
                            Show();
                            ShowLi(true);
                            LoginButton.Content = "Login";
                            Logged_in_as.Visibility = Visibility.Hidden;
                            DisplayName.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
        }


        public static List<Installation> GetEpicInstallLocations()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");

            if (!Directory.Exists(Path.GetDirectoryName(path)) || !File.Exists(path))
                return null;

            return JsonConvert.DeserializeObject<EpicInstallLocations>(File.ReadAllText(path)).InstallationList;
        }

        public class EpicInstallLocations
        {
            [JsonProperty("InstallationList")]
            public List<Installation> InstallationList { get; set; }
        }

        public class Installation
        {
            [JsonProperty("InstallLocation")]
            public string InstallLocation { get; set; }

            [JsonProperty("AppName")]
            public string AppName { get; set; }
        }
        private void Select_fn_path_button_Click(object sender, EventArgs e)
        {
            string old_path = FN_Path.Text;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FN_Path.Text = dialog.FileName;
                Config_file.Default.Path = FN_Path.Text;
                Config_file.Default.Save();
            }
            else
            {
                FN_Path.Text = old_path;
                Config_file.Default.Path = FN_Path.Text;
                Config_file.Default.Save();
            }
        }

       
        //delete cms cached shit
        void DeleteCms()
        {
            try
            {
                string cms = Environment.GetEnvironmentVariable("LocalAppData") + @"\FortniteGame\Saved\PersistentDownloadDir";
                string cache = Environment.GetEnvironmentVariable("LocalAppData") + @"\FortniteGame\Saved\webcache";
                Directory.Delete(cms, true);
                Directory.Delete(cache, true);
            }
            catch { }
        }

        private void Console_Checked(object sender, RoutedEventArgs e)
        {
            disableConsole = false;
        }

        private void Console_UnChecked(object sender, RoutedEventArgs e)
        {
            disableConsole = true;
        }
        private void Info_Click(object sender, EventArgs e)

        {
            Process.Start("https://dsc.gg/ggdev");
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Settings is not working we gonna delete thm.", "RyryFN Launcher", MessageBoxButton.OK);
        }

        private void Info_Click2(object sender, EventArgs e)
        {
            Process.Start("https://dsc.gg/ggdev");
        }

        private void FortniteKill_click(object sender, EventArgs e)
        {
            Process.Start("taskkill", "/F /IM FortniteLauncher.exe && echo e && taskkill /F /IM FortniteClient-Win64-Shipping_EAC.exe && taskkill /F /IM FortniteClient-Win64-Shipping_BE.exe && taskkill /F /IM FortniteClient-Win64-Shipping.exe && pause");
            MessageBox.Show("Killed Fortnite Process", "GGDev Launcher");
        }

        private bool Initalized = false;

        private void Settings_click(object sender, EventArgs e)
        {
        }

        private void ThreadSLeep_click(object sender, EventArgs e)
        {
            Process.Start(("Thread.Sleep(80000)"));
            Process.Start("                            Fortnite.WaitForInputIdle();");
        }

    }
}