using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                _editing = null;
            }
            else
            {
                var dataPath = GetDataPath(_folderPath);
                _editing = new CustomSkin { FilePath = dataPath };

                if (File.Exists(dataPath))
                    _editing = CustomSkin.FromFile(dataPath);
            }

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            if (_editing == null)
            {
                UpdateWorkshopIdText(0ul);
                txtWorkshopName.Text = string.Empty;
                txtWorkshopDesc.Text = string.Empty;
                txtItemType.Text = string.Empty;
                txtFolder.Text = string.Empty;
                return;
            }
            UpdateWorkshopIdText(_editing.ItemId);
            txtWorkshopName.Text = _editing.Title;
            txtWorkshopDesc.Text = _editing.Description;
            txtItemType.Text = _editing.ItemType;
            txtFolder.Text = _folderPath;
        }

        private string GetDataPath(string path)
        {
            var folderName = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar);
            var projectName = Path.GetFileName(folderName);
            return Path.GetFullPath(Path.Combine(path, $"..\\{projectName}.data"));
        }

        private void UpdateWorkshopIdText(ulong id)
        {
            if (id != 0)
            {
                txtWorkshopId.ForeColor = Color.Blue;
                txtWorkshopId.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline);
            }
            else
            {
                txtWorkshopId.ForeColor = Color.Black;
                txtWorkshopId.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            }

            //txtWorkshopId.Text =  id.ToString();
            txtWorkshopId.Value = id;
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
            _editing.Clear();
            UpdateWorkshopIdText(0ul);
            _editing.Save();
        }
        private void btnDo_Click(object sender, EventArgs e)
        {
            if (_editing == null)
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
                File.WriteAllText(ManifestPath, _editing.ManifestText);
            Task.Run(() => PublishToSteam(_folderPath, _editing));
        }

        private string ManifestPath => _folderPath + Path.DirectorySeparatorChar + "manifest.txt";

        private async Task PublishToSteam(string folder, CustomSkin skin)
        {
            Editor editor = default(Editor);
            editor = skin.ItemId == 0UL ? Editor.NewMicrotransactionFile : new Editor(skin.ItemId);

            editor = editor.WithContent(folder).WithPreviewFile(folder + Path.DirectorySeparatorChar+"icon_background.png")
                .WithTitle(skin.Title).WithTag("Version3").WithTag(skin.ItemType).WithTag("Skin")
                .WithPublicVisibility().WithDescription(skin.Description);

            MessageBox.Show("Publishing To Steam");
            PublishResult publishResult = await editor.SubmitAsync();
            if (!publishResult.Success)
            {
                MessageBox.Show("Error: " + publishResult.Result);
            }
            else
            {
                MessageBox.Show("Published File: " + publishResult.FileId);
            }
            Item? item = await SteamUGC.QueryFileAsync(publishResult.FileId);
            if (item == null)
            {
                MessageBox.Show("Error Retrieving item information!");
            }
            else
            {
                _editing.ItemId = item.Value.Id;
                _editing.Title = item.Value.Title;
                UpdateWorkshopIdText(_editing.ItemId);
                txtWorkshopName.Text = _editing.Title;
                Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + txtWorkshopId.Text);
                _editing.Save();
            }
        }

        private void txtWorkshopId_ValueChanged(object sender, EventArgs e)
        {
            _editing.ItemId = decimal.ToUInt64(txtWorkshopId.Value);
        }

        private void txtWorkshopDesc_TextChanged(object sender, EventArgs e)
        {
            _editing.Description = txtWorkshopDesc.Text;
        }

        private void txtItemType_TextChanged(object sender, EventArgs e)
        {
            _editing.ItemType = txtItemType.Text;
        }

        private void txtWorkshopName_TextChanged(object sender, EventArgs e)
        {
            _editing.Title = txtWorkshopName.Text;
        }
    }
}
