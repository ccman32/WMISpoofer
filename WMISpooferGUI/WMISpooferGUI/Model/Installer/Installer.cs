using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMISpooferGUI.Model
{
    class Installer
    {
        private const int MAX_PATH = 255;

        private static string localAppDataPath = getShortPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        private static string installationFilePath = Path.Combine(localAppDataPath, "WMIS");
        private static string installationFileName32 = Path.Combine(installationFilePath, "WMIS32.dll");
        private static string installationFileName64 = Path.Combine(installationFilePath, "WMIS64.dll");
        private static string installationIniFileName = Path.Combine(installationFilePath, "WMIS.ini");

        private static string getShortPath(string path)
        {
            StringBuilder shortPath = new StringBuilder(MAX_PATH);
            NativeMethods.GetShortPathName(path, shortPath, MAX_PATH);

            return shortPath.ToString();
        }

        private static RegistryKey getAppInitDllsKey32()
        {
            RegistryKey baseKey32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            return baseKey32.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", true);
        }

        private static RegistryKey getAppInitDllsKey64()
        {
            RegistryKey baseKey64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            return baseKey64.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", true);
        }

        private static List<string> splitAppInitDllValue(RegistryKey appInitDllsKey)
        {
            return ((string)appInitDllsKey.GetValue("AppInit_DLLs")).Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static void setAppInitDllsValue(RegistryKey appInitDllsKey, List<string> entries)
        {
            appInitDllsKey.SetValue("AppInit_DLLs", string.Join(",", entries), RegistryValueKind.String);
        }

        private static List<string> getAppInitDllsEntries32()
        {
            return splitAppInitDllValue(getAppInitDllsKey32());
        }

        private static List<string> getAppInitDllsEntries64()
        {
            return splitAppInitDllValue(getAppInitDllsKey64());
        }

        private static bool checkInstallationFilesExist()
        {
            return File.Exists(installationFileName32) && File.Exists(installationFileName64);
        }

        private static bool checkAppInitDllsKeysExist()
        {
            return getAppInitDllsEntries32().Contains(installationFileName32) && getAppInitDllsEntries64().Contains(installationFileName64);
        }

        private static void setAppInitDllsEntries32(List<string> entries)
        {
            setAppInitDllsValue(getAppInitDllsKey32(), entries);
        }

        private static void setAppInitDllsEntries64(List<string> entries)
        {
            setAppInitDllsValue(getAppInitDllsKey64(), entries);
        }

        private static bool checkLoadAppInitDllsValue(RegistryKey loadAppInitDllsKey)
        {
           return (int)loadAppInitDllsKey.GetValue("LoadAppInit_DLLs") == 1;
        }

        private static void setLoadAppInitDllsValue(RegistryKey loadAppInitDllsKey)
        {
            loadAppInitDllsKey.SetValue("LoadAppInit_DLLs", 1, RegistryValueKind.DWord);
        }

        public static bool IsInstalled()
        {
            bool installationFileExists = checkInstallationFilesExist();
            bool appInitDllEntryFound = checkAppInitDllsKeysExist();
            bool loadAppInitDllsEnabled32 = checkLoadAppInitDllsValue(getAppInitDllsKey32());
            bool loadAppInitDllsEnabled64 = checkLoadAppInitDllsValue(getAppInitDllsKey64());

            if (installationFileExists && appInitDllEntryFound && loadAppInitDllsEnabled32 && loadAppInitDllsEnabled64)
            {
                return true;
            }

            return false;
        }

        public static void Install()
        {
            Directory.CreateDirectory(installationFilePath);
            File.WriteAllBytes(installationFileName32, WMISpooferGUI.Properties.Resources.WMISpoofer_32);
            File.WriteAllBytes(installationFileName64, WMISpooferGUI.Properties.Resources.WMISpoofer_64);

            List<string> appInitDllsEntries32 = getAppInitDllsEntries32();
            appInitDllsEntries32.Add(installationFileName32);
            setAppInitDllsEntries32(appInitDllsEntries32);

            List<string> appInitDllsEntries64 = getAppInitDllsEntries64();
            appInitDllsEntries64.Add(installationFileName64);
            setAppInitDllsEntries64(appInitDllsEntries64);

            setLoadAppInitDllsValue(getAppInitDllsKey32());
            setLoadAppInitDllsValue(getAppInitDllsKey64());

            MessageBox.Show("Successfully installed WMI Spoofer!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Uninstall()
        {
            bool appInitDllEntryFound = checkAppInitDllsKeysExist();
            bool installationFileExists = checkInstallationFilesExist();
            bool systemRestartRequired = false;

            if (appInitDllEntryFound)
            {
                List<string> appInitDllsEntries32 = getAppInitDllsEntries32();
                appInitDllsEntries32.Remove(installationFileName32);
                setAppInitDllsEntries32(appInitDllsEntries32);

                List<string> appInitDllsEntries64 = getAppInitDllsEntries64();
                appInitDllsEntries64.Remove(installationFileName64);
                setAppInitDllsEntries64(appInitDllsEntries64);
            }

            if (installationFileExists)
            {
                try
                {
                    Directory.Delete(installationFilePath, true);
                }
                catch (UnauthorizedAccessException)
                {
                    systemRestartRequired = true;

                    if (!NativeMethods.MoveFileEx(installationIniFileName, null, MoveFileFlags.DelayUntilReboot)
                        || !NativeMethods.MoveFileEx(installationFileName32, null, MoveFileFlags.DelayUntilReboot)
                        || !NativeMethods.MoveFileEx(installationFileName64, null, MoveFileFlags.DelayUntilReboot)
                        || !NativeMethods.MoveFileEx(installationFilePath, null, MoveFileFlags.DelayUntilReboot))
                    {
                        MessageBox.Show("Failed to schedule the installation folder for deletion!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            MessageBox.Show("Successfully uninstalled WMI Spoofer!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (systemRestartRequired
                && MessageBox.Show("In order to complete the uninstall process you must restart your computer.\nDo you want to restart your computer now?",
                "WMI Spoofer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RestartComputer();
            }
        }

        public static string GetInstallationIniFileName()
        {
            return installationIniFileName;
        }

        public static void RestartComputer()
        {
            Process.Start("shutdown", "/r /t 00");
        }
    }
}
