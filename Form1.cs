using Steamworks;
using Steamworks.Ugc;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RustWorkshopUploader
{
    public partial class frmMain : Form
    {
        private CustomSkin _editing;
        private CustomSkin Editing
        {
            get => _editing ?? (_editing = new CustomSkin());
            set => _editing = value;
        }
        private string _folderPath;
        public frmMain()
        {
            InitializeComponent();
            pictureBox1.Image = getImageFromURL("https://i.imgur.com/jokpDbK.png");
            pictureBox2.Image = getImageFromURL("https://i.imgur.com/obe6jvH.png");


            txtWorkshopId.Maximum = ulong.MaxValue;
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/CustomSkins/");
        }

        void client_DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
            });
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }


        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            Bitmap image;
            txtWorkshopId.Value = 0;
            txtWorkshopName.Text = "";
            txtFolder.Text = "";
            txtWorkshopDesc.Text = "";
            txtItemType.Text = "";
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.PNG)|*.PNG";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);

                    if (image.Width != 512 && image.Height != 512)
                    {
                        MessageBox.Show("Размер изображения должен быть 512x512", "Image size error!", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                        return;
                    }
                    pictureBox2.Image = image;
                    pictureBox2.Invalidate();
                    txtFolder.Text = open_dialog.FileName;
                    FileInfo file = new FileInfo(open_dialog.FileName);

                    if (file != null)
                    {
                        string folder = $"{Directory.GetCurrentDirectory()}/CustomSkins/Skin_{Path.GetFileNameWithoutExtension(file.Name)}/";
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        if (File.Exists(folder + "icon.png"))
                            File.Delete(folder + "icon.png");
                        File.Copy(open_dialog.FileName, folder + "icon.png");
                        if (File.Exists(folder + "icon_background.png"))
                            File.Delete(folder + "icon_background.png");
                        File.Copy(open_dialog.FileName, folder + "icon_background.png");
                        _folderPath = folder;
                        var dataPath = GetDataPath(_folderPath);
                        Editing.FilePath = dataPath;
                        if (File.Exists(dataPath))
                            Editing = CustomSkin.FromFile(dataPath);
                    }
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            txtWorkshopId.Value = Editing.ItemId;
            txtWorkshopName.Text = Editing.Title;
            txtWorkshopDesc.Text = Editing.Description;
            txtItemType.Text = Editing.ItemType;
            txtFolder.Text = _folderPath;
        }

        private void UpdateEditing()
        {
            ProgressBar.Value = 10;
            Editing.ItemId = decimal.ToUInt64(txtWorkshopId.Value);
            Editing.Title = txtWorkshopName.Text;
            ProgressBar.Value = 15;
            Editing.Description = txtWorkshopDesc.Text;
            Editing.ItemType = txtItemType.Text;
            _folderPath = txtFolder.Text;
            ProgressBar.Value = 20;
        }

        private string GetDataPath(string path)
        {
            var folderName = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar);
            var projectName = Path.GetFileName(folderName);
            return Path.GetFullPath(Path.Combine(path, $"..\\{projectName}.data"));
        }

        private void txtWorkshopId_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtWorkshopId.Text == "0") return;
            if (ModifierKeys == Keys.Control)
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + txtWorkshopId.Text);
        }

        private void txtWorkshopId_MouseEnter(object sender, EventArgs e)
        {
            tooltipWorkshopID.Show("Hold CONTROL and click to open workshop url", txtWorkshopId);
        }

        private void btnRecreate_Click(object sender, EventArgs e)
        {
            Editing.Clear();
            txtWorkshopId.Value = 0ul;
            Editing.Save();
        }
        private void btnDo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                MessageBox.Show("Вы не выбрали изображение!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtWorkshopName.Text))
            {
                MessageBox.Show("Вы должны указать имя для скина!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtWorkshopDesc.Text))
            {
                MessageBox.Show("Вы должны указать описание для скина!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtItemType.Text))
            {
                MessageBox.Show("Вы не указали тип предмета", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(ManifestPath))
                File.WriteAllText(ManifestPath, Editing.ManifestText);
            ProgressBar.Value = 0;
            UpdateEditing();
            PublishToSteam();
        }

        private string ManifestPath => _folderPath + Path.DirectorySeparatorChar + "manifest.txt";

        private async Task<DialogResult> ShowMessage(string message)
        {
            return await Task.Run(() => MessageBox.Show(message));
        }

        private async void PublishToSteam()
        {
            ProgressBar.Value = 30;

            Editor editor = default(Editor);
            editor = Editing.ItemId == 0UL ? Editor.NewMicrotransactionFile : new Editor(Editing.ItemId);

            editor = editor.ForAppId(Program.RustAppId).WithContent(_folderPath).WithPreviewFile(_folderPath + Path.DirectorySeparatorChar + "icon_background.png")
                .WithTitle(Editing.Title).WithTag("Version3").WithTag(Editing.ItemType).WithTag("Skin")
                .WithPublicVisibility().WithDescription(Editing.Description);
            ProgressBar.Value = 40;
            await ShowMessage("Publishing To Steam");
            PublishResult publishResult = await editor.SubmitAsync();
            ProgressBar.Value = 50;
            ProgressBar.ForeColor = Color.Red;

            if (!publishResult.Success)
            {
                ProgressBar.Value = 0;
                await ShowMessage("Error: " + publishResult.Result);
            }
            else
            {
                ProgressBar.Value = 75;
                await ShowMessage("Published File: " + publishResult.FileId);
            }
            Item? item = await SteamUGC.QueryFileAsync(publishResult.FileId);
            if (item == null)
            {
                ProgressBar.Value = 0;

                await ShowMessage("Error Retrieving item information!");
            }
            else
            {
                ProgressBar.Value = 100;
                await ShowMessage("Success!");
                Editing.Title = item.Value.Title;
                Editing.Description = item.Value.Description;
                Editing.ItemId = item.Value.Id;
                Editing.Save();
                UpdateTexts();
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + Editing.ItemIdString);
                btnDo.Text = "Загружено!";
            }
        }

        private void txtWorkshopId_ValueChanged(object sender, EventArgs e)
        {
            if (txtWorkshopId.Value != 0)
            {
                txtWorkshopId.ForeColor = Color.Blue;
                txtWorkshopId.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline);
            }
            else
            {
                txtWorkshopId.ForeColor = Color.Black;
                txtWorkshopId.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            }

            Editing.ItemId = decimal.ToUInt64(txtWorkshopId.Value);
        }

        private void txtWorkshopDesc_TextChanged(object sender, EventArgs e)
        {
            Editing.Description = txtWorkshopDesc.Text;
        }

        private void txtItemType_TextChanged(object sender, EventArgs e)
        {
            Editing.ItemType = txtItemType.Text;
        }

        private void txtWorkshopName_TextChanged(object sender, EventArgs e)
        {
            Editing.Title = txtWorkshopName.Text;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void ProgressBar_Click(object sender, EventArgs e)
        {

        }

        public Bitmap getImageFromURL(string sURL)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(sURL);
            Request.Method = "GET";
            Request.UseDefaultCredentials = true;

            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            Bitmap bmp = new Bitmap(Response.GetResponseStream());
            Response.Close();
            return bmp;
        }
    }
}
