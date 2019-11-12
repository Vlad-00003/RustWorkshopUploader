using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Rust.Workshop;
using Steamworks;

namespace RustWorkshopUploader
{
    class CustomSkin
    {
        public string Title;
        public string Description;
        public ulong ItemId;
        public string ItemType;

        public CustomSkin()
        {
            ItemType = "TShirt";
        }

        [JsonIgnore]
        public string ItemIdString => ItemId.ToString(); 

        [JsonIgnore]
        public string FilePath;

        [JsonIgnore]
        public Skin.Manifest Manifest =>
            new Skin.Manifest
            {
                ItemType = ItemType,
                Version = 3,
                Groups = new Skin.Manifest.Group[1],
                PublishDate = DateTime.UtcNow,
                AuthorId = SteamClient.SteamId
            };

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
            File.WriteAllText(FilePath,ToString());
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
                MessageBox.Show($"Failed to Deserialize file {path} as CustomSkin!\n{ex}", "ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
           
            skin.FilePath = path;
            return skin;
        }
    }
}
