using RustWorkshopUploader.Localization;
using Steamworks;
using System;
using System.Windows.Forms;

namespace RustWorkshopUploader
{
    internal static class Program
    {
        public static AppId RustAppId = 252490;
        public static AppId SdkAppId = 391750;

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                Application.Run(new frmMain());
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(strings.AppError, e), strings.Message_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}