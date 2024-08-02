using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public class Picture : Media
    {
        public string ScreenImageURI { get; set; }


        public Picture(string title, DateTime date, string fullContentURI, string thumbnailURI, string screenImageURI, string extension):base(title, date, fullContentURI, thumbnailURI, extension)
        {
            ScreenImageURI = screenImageURI;
        }

        public override string ToString()
        {
            return Title + " " + Date.ToString();
        }
        
    }
}
