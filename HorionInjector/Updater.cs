using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;

namespace HorionInjector
{
    partial class MainWindow
    {
        private void CheckForUpdate()
        {
            try
            {
                //var latest = new WebClient().DownloadString("https://horion.download/latest");
                var latest = new WebClient().DownloadString("https://download.com/latest");
                if (Version.Parse(latest) > GetVersion())
                {
                    if (MessageBox.Show("New update available! Do you want to update now?", null, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        Update();
                }
            }
            catch (Exception)
            {
                // Ignore, can't check for updates
            }
        }

        private void Update()
        {
            var path = Assembly.GetExecutingAssembly().Location;

            try
            {
                Directory.GetAccessControl(Path.GetDirectoryName(path));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Uh oh! The updater has no permission to access the injectors directory!");
                return;
            }

            File.Move(path, Path.ChangeExtension(path, "old"));
            //new WebClient().DownloadFile("https://horion.download/bin/HorionInjector.exe", path);
            new WebClient().DownloadFile("https://download.com/injector.exe", path);

            MessageBox.Show("Updater is done! The injector will now restart.");
            Process.Start(path);
            Application.Current.Shutdown();
        }
    }
}