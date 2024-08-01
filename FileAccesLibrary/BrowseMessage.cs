using System.Xml.Linq;

namespace FileAccesLibrary
{
    public class BrowseMessage : RawMessage
    {
        string head = "POST /smp_4_ HTTP/1.0\r\n" +
            "Content-Type: text/xml; charset=\"utf-8\"\r\n" +
            "HOST: 192.168.107.1\r\n" +
            "Content-Length: {0}\r\n" +
            "SOAPACTION: \"urn:schemas-upnp-org:service:ContentDirectory:1#Browse\"\r\n" +
            "Connection: close\r\n\r\n";
        string payload = @"<?xml version=""1.0"" encoding=""utf-8""?>
<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
<s:Body>
<u:Browse xmlns:u=""urn:schemas-upnp-org:service:ContentDirectory:1"">
<ObjectID>8</ObjectID>
<BrowseFlag>BrowseDirectChildren</BrowseFlag>
<Filter>*</Filter>
<StartingIndex>{0}</StartingIndex>
<RequestedCount>{1}</RequestedCount>
<SortCriteria>
</SortCriteria>
</u:Browse>
</s:Body>
</s:Envelope>";
        int from, count;


        public BrowseMessage(string ipAddr, int port, int from, int count) : base(ipAddr, port) {
            this.from = from;
            this.count = count;

            
            payload = String.Format(payload, from, count);
            head = String.Format(head, payload.Length);

            _message = head + payload;

        }

        private XElement getResponse() {
            string payload = _response.Substring(_response.IndexOf("<?xml"));
            payload = payload.Replace("&lt;", "<");
            payload = payload.Replace("&gt;", ">");
            return XElement.Parse(payload);
        }

        public List<IMedia> GetPictures() {
            List<IMedia> result = new List<IMedia>();
            XElement response = getResponse();
            var items = response.Descendants().Where(x => x.Name.LocalName == "item");

            foreach (var item in items)
            {
                var title = item.Descendants().Where(x => x.Name.LocalName == "title").First().Value;
                var date = item.Descendants().Where(x => x.Name.LocalName == "date").First().Value;

                var links = item.Descendants().Where(x => x.Name.LocalName == "res");
                List<string> linksToPicture = new List<string>();
                var protocolInfo = links.First().Attribute("protocolInfo").Value;
                string type = protocolInfo.Substring(protocolInfo.IndexOf("http-get:*:") + "http-get:*:".Length, protocolInfo.IndexOf("/") - protocolInfo.IndexOf("http-get:*:") - "http-get:*:".Length);
                string extension = protocolInfo.Substring(protocolInfo.IndexOf("/") + 1, protocolInfo.IndexOf(":DLNA.ORG") - protocolInfo.IndexOf("/") - 1);

                foreach (var link in links)
                {
                    linksToPicture.Add(link.Value);
                }

                if(type == "image") { result.Add(new Picture(title, DateTime.Parse(date), linksToPicture[0], linksToPicture[1], linksToPicture[2], extension)); }
                else if(type == "video") { result.Add(new Video(title, DateTime.Parse(date), linksToPicture[0], linksToPicture[1], extension)); }
                else { throw new NotSupportedException(); }
                

            }
            return result;
        }
    }
}
