namespace FileAccesLibrary
{
    public class ConnectionMessage : RawMessage
    {
        public ConnectionMessage(string ipAddr, int port, string mac) : base(ipAddr, port)
        {
            _message = "HEAD /mode/control HTTP/1.0\r\n" +
                "HOST: http://192.168.107.1:7788\r\n" +
                "User-Agent: SEC_MODE_{0}\r\n" +
                "Access-Method: manual\r\n" +
                "NTS: alive\r\n" +
                "Content-Length: 0\r\n" +
                "HOST-Mac: MY_MAC\r\n" +
                "HOST-Address: 192.168.107.11\r\n" +
                "HOST-port: 7788\r\n" +
                "HOST-PNumber: none\r\n\r\n";
            _message = String.Format(_message, mac);
        }

    }
}
