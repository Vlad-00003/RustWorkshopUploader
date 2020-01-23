using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using RustWorkshopUploader.Localization;
using Steamworks;

namespace RustWorkshopUploader.Classes
{
    internal class CustomSkin
    {
        public string Description;
        public ulong ItemId;
        public string Title;

        [JsonIgnore] 
        public string FilePath;

        [JsonIgnore] 
        public string ItemIdString => ItemId.ToString();

        [JsonIgnore]
        public Manifest Manifest => Manifest.DefaultManifest.WithData(DateTime.UtcNow, SteamClient.SteamId);

        [JsonIgnore] 
        public string ManifestText => JsonConvert.SerializeObject(Manifest, Formatting.Indented);

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Clear()
        {
            Title = string.Empty;
            Description = string.Empty;
            ItemId = 0ul;
        }

        public void Save()
        {
            File.WriteAllText(FilePath, ToString());
        }
        
        public static CustomSkin FromFile(string path)
        {
            var text = File.ReadAllText(path);

            CustomSkin skin;
            try
            {
                skin = JsonConvert.DeserializeObject<CustomSkin>(text);
            }
            catch (Exception ex)
            {
                skin = new CustomSkin();
                MessageBox.Show(string.Format(strings.CustomSkin_DeserializationFailed, path, ex),
                    strings.Message_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            skin.FilePath = path;
            return skin;
        }
    }
}