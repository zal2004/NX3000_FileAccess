using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public class Picture
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ThumbnailURI { get; set; }
        public string ScreenImageURI { get; set; }
        public string FullImageURI { get; set; }

        public Picture(string title, DateTime date, string fullImageURI, string thumbnailURI, string screenImageURI)
        {
            Title = title;
            Date = date;
            FullImageURI = fullImageURI;
            ThumbnailURI = thumbnailURI;
            ScreenImageURI = screenImageURI;
        }

        public override string ToString()
        {
            return Title + " " + Date.ToString();
        }

        public async Task Download(string dest)
        {
            // Download the image from the URI
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(FullImageURI);
                response.EnsureSuccessStatusCode();
                string destPath;
                destPath = Path.Combine(dest, Title + ".jpg");

                using(var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
