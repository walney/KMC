﻿using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace KeppyMIDIConverter
{
    static class Program
    {
        private const int timerAccuracy = 10;

        public static bool DebugLang = false;
        public static bool DebugMode = false;
        public static string Who = "Keppy's";
        public static string Title = "MIDI Converter";

        public static string OGGEnc = String.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "kmcogg.exe");
        public static string MP3Enc = String.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "kmcmp3.exe");
        public static bool DeleteEncoder = false;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        /// 
        [STAThread]
        static void Main(String[] args)
        {
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootUp.CheckUp(args);

            Application.Run(new MainWindow(Environment.GetCommandLineArgs()));
        }
    }
}