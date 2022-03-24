// Decompiled with JetBrains decompiler
// Type: StormFN_Launcher.MainWindow
// Assembly: StormFN Launcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEC8F8E9-5CC5-4557-8117-6B0C48140081
// Assembly location: C:\Users\remec\OneDrive\Рабочий стол\all here\Fortnite Stuff\Fortnite Private Servers\Storm servers\Storm\StormFN Launcher.exe

using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using StormFN_Launcher.Epic;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StormFN_Launcher
{
    public partial class MainWindow : Window, IComponentConnector
    {
        public static bool disableConsole = false;
        public static bool nokick = false;
        private string token;
        private string exchange;
        private string username;
        public static string tempPath = Path.GetTempPath();
        private readonly WebClient webClient = new WebClient();
        public static string consoledllpath = MainWindow.foldername + "FortConsole.dll";
        public static string plataniumdllpath = MainWindow.foldername + "Platanium.dll";
        internal Button LoginButton;
        internal Label Logged_in_as;
        internal Label DisplayName;
        internal TextBox FN_Path;
        internal Button select_fn_path_button;
        internal CheckBox nokickenabled;
        private bool _contentLoaded;

        public static string foldername => Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\";

        private void Give_AntiCheat_Love()
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                string str = "system*";
                powerShell.AddScript("Set-Service 'BEService' -StartupType Disabled" + str);
                powerShell.AddScript("Set-Service 'EasyAntiCheat' -StartupType Disabled" + str);
                Config_file.Default.AC_bypass = true;
                Config_file.Default.Save();
                foreach (PSObject psObject in powerShell.Invoke())
                {
                    if (psObject != null)
                        this.msg("An Error occured \n" + psObject.Properties["Status"].Value.ToString() + " - ");
                }
            }
        }

        private void Save_settings(object sender, EventArgs e)
        {
            Config_file.Default.Path = this.FN_Path.Text;
            Config_file.Default.Save();
            Application.Current.Shutdown();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.DeleteCms();
            this.FN_Path.Text = !(this.FN_Path.Text == "Fortnite Path") && !(this.FN_Path.Text == "") && !string.IsNullOrEmpty(this.FN_Path.Text) ? Config_file.Default.Path : MainWindow.GetEpicInstallLocations().FirstOrDefault<MainWindow.Installation>((Func<MainWindow.Installation, bool>)(i => i.AppName == "Fortnite"))?.InstallLocation;
            if (!Config_file.Default.AC_bypass)
                this.Give_AntiCheat_Love();
            if (!Config_file.Default.Show)
                return;
            Config_file.Default.Show = false;
            Config_file.Default.Save();
        }

        public void ShowLi(bool yesorno)
        {
            if (!yesorno)
            {
                this.Logged_in_as.Visibility = Visibility.Hidden;
                this.DisplayName.Visibility = Visibility.Hidden;
                this.LoginButton.Content = (object)"Login";
            }
            else
            {
                this.Logged_in_as.Visibility = Visibility.Visible;
                this.DisplayName.Visibility = Visibility.Visible;
                this.LoginButton.Content = (object)"Launch";
            }
        }

        public void msg(string text)
        {
            int num = (int)MessageBox.Show(text.ToString(), "Storm Launcher");
        }

        private void Login_click(object sender, RoutedEventArgs e)
        {
            if (this.LoginButton.Content.ToString() == "Login")
            {
                Config_file.Default.Path = this.FN_Path.Text;
                Config_file.Default.Save();
                string devicecode = Auth.GetDevicecode(Auth.GetDevicecodetoken());
                string[] strArray = devicecode.Split(new char[1]
                {
          ','
                }, 2);
                if (devicecode.Contains("error"))
                    return;
                this.username = strArray[1];
                this.DisplayName.Content = (object)(strArray[1] ?? "");
                this.ShowLi(true);
                this.token = strArray[0];
                this.LoginButton.Content = (object)"Launch";
            }
            else
            {
                if (!(this.LoginButton.Content.ToString() == "Launch"))
                    return;
                Config_file.Default.Path = this.FN_Path.Text;
                Config_file.Default.Save();
                this.exchange = Auth.GetExchange(this.token);
                string str = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
                string path1 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
                string path2 = Path.Combine(Config_file.Default.Path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");
                if (!System.IO.File.Exists(str))
                {
                    this.msg("\"" + str + "\" wasn't found, stop deleting game files!");
                    this.ShowLi(false);
                }
                else
                {
                    if (!System.IO.File.Exists(path1))
                    {
                        this.msg("\"" + path1 + "\" wasn't found, stop deleting game files!");
                        this.ShowLi(false);
                    }
                    if (!System.IO.File.Exists(MainWindow.plataniumdllpath) || !System.IO.File.Exists(MainWindow.consoledllpath))
                    {
                        this.msg("DLL's not found");
                        this.ShowLi(false);
                    }
                    else if (!System.IO.File.Exists(path2))
                    {
                        this.msg("\"" + path2 + "\" wasn't found, stop deleting game files!");
                        this.ShowLi(false);
                    }
                    else
                    {
                        Config_file.Default.Path = this.FN_Path.Text;
                        Config_file.Default.Save();
                        this.exchange = Auth.GetExchange(this.token);
                        string arguments = "-AUTH_LOGIN=unused -AUTH_PASSWORD=" + this.exchange + " -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=8c4aa8a9b77acdcbd918874b -skippatchcheck";
                        Process process1 = new Process()
                        {
                            StartInfo = new ProcessStartInfo(str, arguments)
                            {
                                UseShellExecute = false,
                                RedirectStandardOutput = false,
                                CreateNoWindow = true
                            }
                        };
                        Process process2 = new Process();
                        process2.StartInfo.FileName = path2;
                        process2.Start();
                        foreach (ProcessThread thread in (ReadOnlyCollectionBase)process2.Threads)
                            StormFN_Launcher.Utilities.Win32.SuspendThread(StormFN_Launcher.Utilities.Win32.OpenThread(2, false, thread.Id));
                        Process process3 = new Process();
                        process3.StartInfo.FileName = path1;
                        process3.StartInfo.Arguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=8c4aa8a9b77acdcbd918874b -skippatchcheck";
                        process3.Start();
                        foreach (ProcessThread thread in (ReadOnlyCollectionBase)process3.Threads)
                            StormFN_Launcher.Utilities.Win32.SuspendThread(StormFN_Launcher.Utilities.Win32.OpenThread(2, false, thread.Id));
                        process1.Start();
                        Thread.Sleep(2000);
                        this.Hide();
                        Thread.Sleep(6000);
                        try
                        {
                            System.IO.File.Delete(MainWindow.tempPath + "/Injector.exe");
                        }
                        catch
                        {
                        }
                        try
                        {
                            this.webClient.DownloadFile("https://cdn.discordapp.com/attachments/698620628881899530/812703176381562880/Injector.exe", MainWindow.tempPath + "/Injector.exe");
                        }
                        catch
                        {
                            int num = (int)MessageBox.Show("Please make sure that you are connected to the internet.");
                            this.ShowLi(false);
                            return;
                        }
                        process1.WaitForInputIdle();
                        Thread.Sleep(3000);
                        new Process()
                        {
                            StartInfo = {
                Arguments = string.Format("\"{0}\" \"{1}\"", (object) process1.Id, (object) MainWindow.plataniumdllpath),
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = (MainWindow.tempPath + "/Injector.exe")
              }
                        }.Start();
                        if (MainWindow.nokick)
                        {
                            Thread.Sleep(16000);
                            new Process()
                            {
                                StartInfo = {
                  Arguments = string.Format("\"{0}\" \"{1}\"", (object) process1.Id, (object) MainWindow.consoledllpath),
                  CreateNoWindow = true,
                  UseShellExecute = false,
                  FileName = (MainWindow.tempPath + "/Injector.exe")
                }
                            }.Start();
                        }
                        process1.WaitForExit();
                        try
                        {
                            process2.Close();
                            process3.Close();
                        }
                        catch
                        {
                        }
                        this.Show();
                        this.ShowLi(false);
                        this.LoginButton.Content = (object)"Login";
                    }
                }
            }
        }

        private void Info_Click(object sender, EventArgs e) => Process.Start("https://discord.gg/stormfn");

        private void SignUp_click(object sender, RoutedEventArgs e) => Process.Start("https://stormfn.herokuapp.com/");

        public static List<MainWindow.Installation> GetEpicInstallLocations()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");
            return !Directory.Exists(Path.GetDirectoryName(path)) || !System.IO.File.Exists(path) ? (List<MainWindow.Installation>)null : ((MainWindow.EpicInstallLocations)JsonConvert.DeserializeObject<MainWindow.EpicInstallLocations>(System.IO.File.ReadAllText(path))).InstallationList;
        }

        private void Select_fn_path_button_Click(object sender, EventArgs e)
        {
            string text = this.FN_Path.Text;
            CommonOpenFileDialog commonOpenFileDialog1 = new CommonOpenFileDialog();
            commonOpenFileDialog1.set_IsFolderPicker(true);
            CommonOpenFileDialog commonOpenFileDialog2 = commonOpenFileDialog1;
            if (((CommonFileDialog)commonOpenFileDialog2).ShowDialog() == 1)
            {
                this.FN_Path.Text = ((CommonFileDialog)commonOpenFileDialog2).get_FileName();
                Config_file.Default.Path = this.FN_Path.Text;
                Config_file.Default.Save();
            }
            else
            {
                this.FN_Path.Text = text;
                Config_file.Default.Path = this.FN_Path.Text;
                Config_file.Default.Save();
            }
        }

        private void DeleteCms()
        {
            try
            {
                string path1 = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\PersistentDownloadDir";
                string path2 = Environment.GetEnvironmentVariable("LocalAppData") + "\\FortniteGame\\Saved\\webcache";
                Directory.Delete(path1, true);
                Directory.Delete(path2, true);
            }
            catch
            {
            }
        }

        private void Console_Checked(object sender, RoutedEventArgs e) => MainWindow.disableConsole = false;

        private void Console_UnChecked(object sender, RoutedEventArgs e) => MainWindow.disableConsole = true;

        private void nokickenabled_Checked(object sender, RoutedEventArgs e) => MainWindow.nokick = true;

        private void nokickenabled_Unchecked(object sender, RoutedEventArgs e) => MainWindow.nokick = false;

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/StormFN Launcher;component/mainwindow.xaml", UriKind.Relative));
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.MainWindow_Load);
                    ((Window)target).Closed += new EventHandler(this.Save_settings);
                    break;
                case 2:
                    this.LoginButton = (Button)target;
                    this.LoginButton.Click += new RoutedEventHandler(this.Login_click);
                    break;
                case 3:
                    this.Logged_in_as = (Label)target;
                    break;
                case 4:
                    this.DisplayName = (Label)target;
                    break;
                case 5:
                    this.FN_Path = (TextBox)target;
                    break;
                case 6:
                    this.select_fn_path_button = (Button)target;
                    this.select_fn_path_button.Click += new RoutedEventHandler(this.Select_fn_path_button_Click);
                    break;
                case 7:
                    this.nokickenabled = (CheckBox)target;
                    this.nokickenabled.Checked += new RoutedEventHandler(this.nokickenabled_Checked);
                    this.nokickenabled.Unchecked += new RoutedEventHandler(this.nokickenabled_Unchecked);
                    break;
                default:
                    this._contentLoaded = true;
                    break;
            }
        }

        public class EpicInstallLocations
        {
            [JsonProperty("InstallationList")]
            public List<MainWindow.Installation> InstallationList { get; set; }
        }

        public class Installation
        {
            [JsonProperty("InstallLocation")]
            public string InstallLocation { get; set; }

            [JsonProperty("AppName")]
            public string AppName { get; set; }
        }
private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No settings right now, its under developing.");
        
        }
    }
}
