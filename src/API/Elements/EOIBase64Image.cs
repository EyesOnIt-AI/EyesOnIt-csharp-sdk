using System;
using System.IO;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public abstract class EOIBase64Image
    {
        private string base64Image;

        [JsonPropertyName("image")]
        public string Base64Image
        {
            get
            {
                return base64Image;
            }
            set
            {
                base64Image = value;
                Image = DecodeBase64Image(value);
            }
        }

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }

        private static System.Drawing.Image DecodeBase64Image(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            byte[] imageBytes = Convert.FromBase64String(value);
            using (var ms = new MemoryStream(imageBytes))
            using (var tempImg = System.Drawing.Image.FromStream(ms))
            {
                return new System.Drawing.Bitmap(tempImg);
            }
        }
    }
}
