﻿using RustWorkshopUploader.Localization;
using Steamworks;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using RustWorkshopUploader.Properties;

namespace RustWorkshopUploader
{
    internal static class Program
    {
        public static AppId RustAppId = 252490;
        public static AppId SdkAppId = 391750;
        private static frmMain Form;

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.CurrentCulture = Settings.Default.Culture;
            CultureInfo.DefaultThreadCurrentCulture = Settings.Default.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = Settings.Default.Culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Settings.Default.Culture.Name);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Settings.Default.Culture.Name);

            Form = new frmMain();
            DialogResult result;
            do
            {
                try
                {
                    SteamClient.Init(SdkAppId);
                    result = DialogResult.OK;
                }
                catch (Exception)
                {
                    result = MessageBox.Show(strings.SteamNotFound, strings.Message_Error, MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                        return;
                }
            } while (result == DialogResult.Retry);

            try
            {
                Application.Run(Form);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(strings.AppError, e), strings.Message_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}