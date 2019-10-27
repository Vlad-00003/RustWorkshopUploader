using System;
using System.Windows.Forms;
using Steamworks;

namespace RustWorkshopUploader
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            DialogResult result = DialogResult.Retry;
            while (result == DialogResult.Retry)
            {
                try
                {
                    SteamClient.Init(252490);
                    result = DialogResult.Cancel;
                    Application.Run(new frmMain());
                    return;
                }
                catch (Exception ex)
                {
                    result = MessageBox.Show("Steam is not running!", "ERROR", MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                    //Process.Start("steam://");
                }
            }
        }
    }
}
