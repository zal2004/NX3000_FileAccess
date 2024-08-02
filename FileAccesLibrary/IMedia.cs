using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAccesLibrary
{
    public interface IMedia
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ThumbnailURI { get; set; }
        public string FullContentURI { get; set; }
        public string Extension { get; set; }

        public Task Download(string dest);
        public Task Download(string dest, Action completedEventHandler);

    }
}
