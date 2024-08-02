using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public class Video : Media
    {
        public Video(string title, DateTime date, string fullContentURI, string thumbnailURI, string extension) : base(title, date, fullContentURI, thumbnailURI, extension)
        {
        }

        public override string ToString()
        {
            return Title + " " + Date.ToString();
        }
    }
}
