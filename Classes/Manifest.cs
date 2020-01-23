using System;
using System.Collections.Generic;

namespace RustWorkshopUploader.Classes
{
    class Manifest
    {
        public int Version { get; set; }

        public string ItemType { get; set; }

        public ulong AuthorId { get; set; }

        public DateTime PublishDate { get; set; }

        public Group[] Groups { get; set; }

        public Manifest WithData(DateTime publishDate, ulong authorId)
        {
            this.PublishDate = publishDate;
            this.AuthorId = authorId;
            return this;
        }

        public static Manifest DefaultManifest => new Manifest
        {
            Version = 3,
            ItemType = "CustomItem",
            Groups = new Group[]
            {
                new Group
                {
                    Floats =
                    {
                        ["_Cutoff"] = 0.0f,
                        ["_BumpScale"] = 1.0f,
                        ["_Glossiness"] = 0.0f,
                        ["_OcclusionStrength"] = 1.0f,
                        ["_MicrofiberFuzzIntensity"] = 1.0f,
                        ["_MicrofiberFuzzScatter"] = 1.0f,
                        ["_MicrofiberFuzzOcclusion"] = 1.0f
                    },
                    Colors =
                    {
                        ["_Color"] = new ColorEntry(1f, 1f, 1f),
                        ["_SpecColor"] = new ColorEntry(0f, 0f, 0f),
                        ["_EmissionColor"] = new ColorEntry(0f, 0f, 0f),
                        ["_MicrofiberFuzzColor"] = new ColorEntry(1f, 1f, 1f)
                    }
                }
            }
        };

        public class Group
        {
            public Dictionary<string, string> Textures { get; set; } = new Dictionary<string, string>();

            public Dictionary<string, float> Floats { get; set; } = new Dictionary<string, float>();

            public Dictionary<string, ColorEntry> Colors { get; set; } = new Dictionary<string, ColorEntry>();
        }

        public class ColorEntry
        {
            public ColorEntry(float r, float g, float b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public float r { get; set; }

            public float g { get; set; }

            public float b { get; set; }
        }
    }
}
