using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Ookii.Dialogs.WinForms;
using Steamworks;
using Steamworks.Ugc;

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
            txtWorkshopId.Maximum = ulong.MaxValue;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var folderDialog = new VistaFolderBrowserDialog
            {
                Description = "Select skin folder",
                RootFolder = Environment.SpecialFolder.Favorites,
                ShowNewFolderButton = true,
                UseDescriptionForTitle = true
            };

            var res = folderDialog.ShowDialog();
            if (res != DialogResult.OK)
                return;

            _folderPath = folderDialog.SelectedPath;
            if (!File.Exists(_folderPath + Path.DirectorySeparatorChar + "icon_background.png"))
            {
                MessageBox.Show(
                    "icon_background.png not found! This file would be used as the preview for the workshop", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Editing = null;
            }
            else
            {
                var dataPath = GetDataPath(_folderPath);
                Editing.FilePath = dataPath;

                if (File.Exists(dataPath))
                    Editing = CustomSkin.FromFile(dataPath);
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
            Editing.ItemId = decimal.ToUInt64(txtWorkshopId.Value);
            Editing.Title = txtWorkshopName.Text;
            Editing.Description = txtWorkshopDesc.Text;
            Editing.ItemType = txtItemType.Text;
            _folderPath = txtFolder.Text;
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
            {
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + txtWorkshopId.Text);
            }
        }

        private void txtWorkshopId_MouseEnter(object sender, EventArgs e)
        {
            tooltipWorkshopID.Show("Hold CONTROL and click to open workshop url",txtWorkshopId);
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
                MessageBox.Show("You must select folder first!", "No item selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtWorkshopName.Text))
            {
                MessageBox.Show("You should specify workshop entry name!", "No name specified", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtWorkshopDesc.Text))
            {
                MessageBox.Show("You should specify workshop entry description!", "No description specified", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtItemType.Text))
            {
                MessageBox.Show("You should specify workshop item type!", "No item type specified", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if(!File.Exists(ManifestPath))
                File.WriteAllText(ManifestPath, Editing.ManifestText);
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
            Editor editor = default(Editor);
            editor = Editing.ItemId == 0UL ? Editor.NewMicrotransactionFile : new Editor(Editing.ItemId);

            editor = editor.WithContent(_folderPath).WithPreviewFile(_folderPath + Path.DirectorySeparatorChar+"icon_background.png")
                .WithTitle(Editing.Title).WithTag("Version3").WithTag(Editing.ItemType).WithTag("Skin")
                .WithPublicVisibility().WithDescription(Editing.Description);

            await ShowMessage("Publishing To Steam");
            PublishResult publishResult = await editor.SubmitAsync();
            if (!publishResult.Success)
            {
                await ShowMessage("Error: " + publishResult.Result);
            }
            else
            {
                await ShowMessage("Published File: " + publishResult.FileId);
            }
            Item? item = await SteamUGC.QueryFileAsync(publishResult.FileId);
            if (item == null)
            {
                await ShowMessage("Error Retrieving item information!");
            }
            else
            {
                await ShowMessage("Success!");
                Editing.Title = item.Value.Title;
                Editing.Description = item.Value.Description;
                Editing.ItemId = item.Value.Id;
                Editing.Save();
                UpdateTexts();
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + Editing.ItemIdString);
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
    }
}
