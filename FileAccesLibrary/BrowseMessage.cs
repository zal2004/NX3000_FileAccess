﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
