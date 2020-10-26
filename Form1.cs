using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using RustWorkshopUploader.Classes;
using RustWorkshopUploader.Localization;
using RustWorkshopUploader.Properties;
using Steamworks;
using Steamworks.Ugc;

namespace RustWorkshopUploader
{
    public partial class FrmMain : Form
    {
        private CustomSkin _editing;
        private string _folderPath;

        public FrmMain()
        {
            InitializeComponent();

            txtWorkshopId.Maximum = ulong.MaxValue;
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "CustomSkins");
        }

        private string ManifestPath => _folderPath + Path.DirectorySeparatorChar + "manifest.txt";

        private void SetStatus(bool editing)
        {
            txtWorkshopId.Enabled = editing;
            txtWorkshopDesc.Enabled = editing;
            txtWorkshopName.Enabled = editing;
            btnDo.Enabled = editing;
            button1.Enabled = editing;
        }

        private static Image FromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return img;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog {Filter = @"Image Files(*.PNG)|*.PNG"};

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            Image image;
            try
            {
                image = FromFile(openDialog.FileName);
            }
            catch
            {
                MessageBox.Show(strings.Select_UnableToOpen, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                SetStatus(false);
                return;
            }

            if (image.Width != 512 && image.Height != 512)
            {
                MessageBox.Show(strings.Select_WrongResolution, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                SetStatus(false);
                return;
            }

            pictureBox2.Image = image;
            pictureBox2.Invalidate();

            _folderPath = string.Format("{1}{0}CustomSkins{0}Skin_{2}", Path.DirectorySeparatorChar,
                Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(openDialog.FileName));

            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
            if (File.Exists(_folderPath + Path.DirectorySeparatorChar + "icon.png"))
                File.Delete(_folderPath + Path.DirectorySeparatorChar + "icon.png");
            File.Copy(openDialog.FileName, _folderPath + Path.DirectorySeparatorChar + "icon.png");

            var dataPath = GetDataPath(_folderPath);
            _editing = File.Exists(dataPath) ? CustomSkin.FromFile(dataPath) : new CustomSkin();
            _editing.FilePath = dataPath;
            SetStatus(true);
            UpdateTexts();
        }


        private string GetDataPath(string path)
        {
            var folderName = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar);
            var projectName = Path.GetFileName(folderName);
            return Path.GetFullPath(Path.Combine(path, $"..\\{projectName}.data"));
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                MessageBox.Show(strings.Do_NoImage, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtWorkshopName.Text))
            {
                MessageBox.Show(strings.Do_NoName, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtWorkshopDesc.Text))
            {
                MessageBox.Show(strings.Do_NoDescription, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(ManifestPath))
                File.WriteAllText(ManifestPath, _editing.ManifestText);

            PublishToSteam();
        }

        private async void PublishToSteam()
        {
            ProgressBar.Value = 0;
            SetStatus(false);

            var editor = _editing.ItemId == 0UL ? Editor.NewMicrotransactionFile : new Editor(_editing.ItemId);

            ProgressBar.Value = 20;

            editor = editor.ForAppId(Program.RustAppId).WithContent(_folderPath)
                .WithPreviewFile(_folderPath + Path.DirectorySeparatorChar + "icon.png")
                .WithTitle(_editing.Title).WithTag("Version3").WithTag("Skin")
                .WithPublicVisibility().WithDescription(_editing.Description);

            ProgressBar.Value = 40;
            var publishResult = await editor.SubmitAsync();
            ProgressBar.Value = 60;

            if (!publishResult.Success)
            {
                ProgressBar.Value = 0;
                if(publishResult.NeedsWorkshopAgreement)
                {
                    if (MessageBox.Show(string.Format(strings.ErrorText, strings.NeedsWorkshopAgreement),
                            strings.Message_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        
                        Process.Start($"https://steamcommunity.com/workshop/workshoplegalagreement/?l={(IsEnglish ?"english":"russian") }");
                    }
                }
                else
                {
                    MessageBox.Show(string.Format(strings.ErrorText, publishResult.Result), strings.Message_Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SetStatus(true);
                return;
            }

            ProgressBar.Value = 85;

            var item = await SteamUGC.QueryFileAsync(publishResult.FileId);
            if (item == null)
            {
                ProgressBar.Value = 0;
                MessageBox.Show(strings.Publish_RetreiveFailed, strings.Message_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                SetStatus(true);
                return;
            }

            ProgressBar.Value = 100;
            _editing.Title = item.Value.Title;
            _editing.Description = item.Value.Description;
            _editing.ItemId = item.Value.Id;
            _editing.Save();
            UpdateTexts();
            var result = MessageBox.Show(strings.Publish_OpenBrowser, strings.Message_Success, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + _editing.ItemIdString);
            SetStatus(true);
        }

        private void txtWorkshopId_DoubleClick(object sender, EventArgs e)
        {
            if (txtWorkshopId.Value == 0) return;
            //steam://url/CommunityFilePage/<PublishedFileId_t>
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + txtWorkshopId.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtWorkshopId.Value = 0ul;
            btnDo.Text = strings.BtnDo_Upload;
        }

        private void btnDo_EnabledChanged(object sender, EventArgs e)
        {
            if (btnDo.Enabled && _editing != null)
            {
                btnDo.Text = _editing.ItemId > 0 ? strings.BtnDo_Update : strings.BtnDo_Upload;
                return;
            }

            btnDo.Text = strings.BtnDo_Uploading;
        }

        private new void Closed(object sender, FormClosedEventArgs e)
        {
#if !DEBUG
            if (MessageBox.Show(strings.AdvMessage, strings.AdvMessage_Title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start("https://rustplugin.ru");
            }
#endif
        }

        private bool IsEnglish => Settings.Default.Culture.Name == "en";
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Culture =
                sender == englishToolStripMenuItem ? new CultureInfo("en") : new CultureInfo("ru");
            Settings.Default.Save();
            Application.Restart();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Field updates

        private bool _updating;

        private void UpdateTexts()
        {
            _updating = true;
            txtWorkshopId.Value = _editing.ItemId;
            txtWorkshopName.Text = _editing.Title;
            txtWorkshopDesc.Text = _editing.Description;
            txtFolder.Text = _folderPath;

            if (_editing.ItemId > 0)
                btnDo.Text = strings.BtnDo_Update;
            else
                btnDo.Text = strings.BtnDo_Upload;
            _updating = false;
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

            if (!_updating)
                _editing.ItemId = decimal.ToUInt64(txtWorkshopId.Value);
        }

        private void txtWorkshopDesc_TextChanged(object sender, EventArgs e)
        {
            if (!_updating)
                _editing.Description = txtWorkshopDesc.Text;
        }

        private void txtWorkshopName_TextChanged(object sender, EventArgs e)
        {
            if (!_updating)
                _editing.Title = txtWorkshopName.Text;
        }

        #endregion
    }
}