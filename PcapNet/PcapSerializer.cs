using System.IO;
using BinarySerialization;

namespace PcapNet
{
    public class PcapSerializer
    {
        public BinarySerializer Serializer { get; } = new BinarySerializer();

        public void Serialize(Stream stream, PcapNet.Pcap pcap)
        {
            Serializer.Serialize(stream, pcap);
        }

        public PcapNet.Pcap Deserialize(Stream stream)
        {
            return Serializer.Deserialize<PcapNet.Pcap>(stream);
        }
    }
}