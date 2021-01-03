using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using img_processing_shared_dll;
using img_processing_shared_dll.Filter;

namespace img_processing_slave
{
    class ImagePeerService : PeerBaseContainer
    {

        public ImagePeerService(): base()
        {
           
        }

        public bool IsConnected
        {
            get
            {
                return Client.Connected;
            }
        }

        public bool TryToConnect(IPEndPoint endPoint)
        {
            try
            {
                Client.Connect(endPoint.Address, endPoint.Port);
            }
            catch
            {
                Console.WriteLine("Connection rejected");
                return false;
            }
            byte[] temp = new byte[16];
            Client.GetStream().Read(temp, 0, 16);
            Guid = new Guid(temp);
            return true;
        }

        public ImageProcessingMessage WaitForMessage()
        {
            Client.GetStream().Read(Buffer, 0, 4);
            int length = BitConverter.ToInt32(Buffer, 0);
            Client.GetStream().Read(Buffer, 0, length);

            return new ImageProcessingMessage(Buffer, length);
        }
    }
}
