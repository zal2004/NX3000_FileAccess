using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public class Video : IMedia
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ThumbnailURI { get; set; }
        public string FullContentURI { get; set; }
        public string Extension { get; set; }

        public Video(string title, DateTime date, string fullContentURI, string thumbnailURI, string extension)
        {
            Title = title;
            Date = date;
            FullContentURI = fullContentURI;
            ThumbnailURI = thumbnailURI;
            Extension = extension;
        }

        public async Task Download(string dest)
        {
            // Download the image from the URI
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(FullContentURI);
                response.EnsureSuccessStatusCode();
                string destPath;
                destPath = Path.Combine(dest, Title + "." + Extension);

                using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
