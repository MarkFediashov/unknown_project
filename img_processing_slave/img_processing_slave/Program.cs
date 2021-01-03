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
    class Program
    {
        static readonly IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30687);


        static void Main(string[] args)
        {
            ImagePeerService peerService = new ImagePeerService();
            while (!peerService.IsConnected)
            {
                peerService.TryToConnect(EndPoint);
                Console.WriteLine($"Appointed GUID: {peerService.Guid}");
            }

            while (true)
            {
                ImageProcessingMessage message = peerService.WaitForMessage();
                ZeroNeighborsSharedFilter filter = ZeroNeighborsSharedFilter.GetFilterByID(message.FilterID);
                for (int i = 0; i < message.ImageArray.Length; i++)
                {
                    message.ImageArray[i] = filter.Filter(message.ImageArray[i]);
                }
                peerService.SendMessage(message);
            }
        }
    }
}
