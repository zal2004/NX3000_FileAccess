namespace TestingProject
{
    using FileAccesLibrary;
    using System;
    class Program
    {

        // Main Method
        static void Main(string[] args)
        {
            var myMAC = "02:00:00:00:00:00";

            ConnectionMessage connectionMessage = new ConnectionMessage("192.168.107.1", 7788, myMAC);
            connectionMessage.Send();

            BrowseMessage browseMessage = new BrowseMessage("192.168.107.1", 7676, 0, 100);
            browseMessage.Send();

            foreach (var uri in browseMessage.getThumbnailURIs())
            {
                Console.WriteLine(uri);
            }
        }
    }
}
