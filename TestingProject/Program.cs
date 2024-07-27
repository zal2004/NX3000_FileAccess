namespace TestingProject
{
    using FileAccesLibrary;
    using System;
    class Program
    {

        // Main Method
        static async Task Main(string[] args)
        {
            var myMAC = "02:00:00:00:00:00";

            ConnectionMessage connectionMessage = new ConnectionMessage("192.168.107.1", 7788, myMAC);
            connectionMessage.Send();

            BrowseMessage browseMessage = new BrowseMessage("192.168.107.1", 7676, 0, 100);
            browseMessage.Send();

            foreach (var pic in browseMessage.GetPictures())
            {
                Console.WriteLine(pic);
            }

            var pic1 = browseMessage.GetPictures()[0];
            await pic1.Download(@"C:\Users\zzaal\Pictures");

        }
    }
}
