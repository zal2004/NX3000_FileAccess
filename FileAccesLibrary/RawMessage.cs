﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FileAccesLibrary
{
    /// <summary>
    /// Base class for the HTTP messages sendable to the camera
    /// </summary>
    public class RawMessage: Message
    {
        private IPAddress _ipAddr;
        private IPEndPoint _localEndPoint;
        /// <summary>
        /// The socket object responsible for the sending the messages
        /// </summary>
        private Socket _sender;

        protected string _message;
        protected string _response;

        public RawMessage(string ipAddr, int port)
        {
            _ipAddr = IPAddress.Parse(ipAddr);
            _localEndPoint = new IPEndPoint(_ipAddr, port);
            _sender = new Socket(_ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendRawMessage(string message)
        {
            try
            {
                _sender.Connect(_localEndPoint);
                byte[] messageSent = Encoding.ASCII.GetBytes(message);
                int byteSent = _sender.Send(messageSent);
                byte[] messageReceivedBuffer = new byte[2048];
                string messageReceived = "";
                int byteToRead = 0;
                while (true)
                {
                    int recievedCount = _sender.Receive(messageReceivedBuffer);
                    if(recievedCount == 0) { break; }
                    string decodedBuffer = Encoding.ASCII.GetString(messageReceivedBuffer, 0, recievedCount);
                    messageReceived = messageReceived + decodedBuffer;
                    byteToRead = recievedCount;
                }
                // translate the explicit character definitions
                _response = messageReceived.Replace("&lt;", "<");
                _response = _response.Replace("&gt;", ">");
                _response = messageReceived;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Send()
        {
            SendRawMessage(_message);
        }
        
    }
}
