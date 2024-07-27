using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public class Picture
    {
        private string _title;
        private DateTime _date;
        private string _thumbnailURI;
        private string _screenImageURI;
        private string _fullImageURI;

        public Picture(string title, DateTime date, string fullImageURI, string thumbnailURI, string screenImageURI)
        {
            _title = title;
            _date = date;
            _fullImageURI = fullImageURI;
            _thumbnailURI = thumbnailURI;
            _screenImageURI = screenImageURI;
        }

        public override string ToString()
        {
            return _title + " " + _date.ToString();
        }

        public async Task Download(string dest)
        {
            // Download the image from the URI
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(_fullImageURI);
                response.EnsureSuccessStatusCode();
                string destPath;
                if(dest == "")
                {
                    destPath = "/" + _title;
                }
                else
                {
                    destPath = dest + "/" + _title;
                }

                destPath = destPath + ".jpg";
                using(var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
