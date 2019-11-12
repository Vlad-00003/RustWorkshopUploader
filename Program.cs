using System;
using System.Windows.Forms;
using Steamworks;

namespace RustWorkshopUploader
{
    static class Program
    {
        public static AppId RustAppId = 252490;
        public static AppId SdkAppId = 391750;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
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
                    result = MessageBox.Show("Steam is not running!", "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            } while (result == DialogResult.Retry);

            try
            {
                Application.Run(new frmMain());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Application error: {e}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
